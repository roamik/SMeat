using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SMeat.DAL;
using System.IdentityModel.Tokens.Jwt;
using SMeat.DAL.Abstract;
using SMeat.MODELS.BindingModels;
using SMeat.MODELS.Entities;
using SMeat.MODELS.Enums;
using System.Linq.Expressions;
using SMeat.MODELS.DTO;
using AutoMapper;

namespace SMeat.API.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    [Route("me")]
    public async Task<IActionResult> GetMyInfoAsync()
    {
      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
      var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
      if (user == null)
      {
        return Forbid("User not found!");
      }

      return Ok(new
      {
        Name = user.Name,
        LastName = user.LastName,
        About = user.About,
        LocationId = user.LocationId,
        WorkplaceId = user.WorkplaceId,
        Gender = user.GenderType,
        CustomGender = user.CustomGenderType,
        Relationship = user.RelationshipType,
        Id = user.Id,
        PictureUrl = user.PictureUrl
      });
    }

    [HttpGet]
    [Authorize]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUserByIdAsync(string id)
    {
      bool isFriend;
      bool inRequest;
      var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
                                                                                                         //var userContacts = _unitOfWork.UsersRepository.GetAsync()
      var user = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == id, //1st filterBy
          u => u.Location, u => u.Workplace.Location, u => u.ContactsIAddedTo); //include foreign entities
      if (user == null)
      {
        return BadRequest("User not found!");
      }

      var friend = user.ContactsIAddedTo.FirstOrDefault(c => c.FriendId == id);
      isFriend = friend != null && friend.Status == ContactStatus.Confirmed ? isFriend = true : isFriend = false;
      inRequest = friend != null && friend.Status == ContactStatus.Send ? inRequest = true : inRequest = false;

      return Ok(new
      {
        Name = user.Name,
        LastName = user.LastName,
        About = user.About,
        Location = user.Location,
        Workplace = user.Workplace,
        Gender = user.GenderType,
        CustomGender = user.CustomGenderType,
        Relationship = user.RelationshipType,
        Id = user.Id,
        IsFriend = isFriend,
        InRequest = inRequest,
        PictureUrl = user.PictureUrl
      });
    }

    [HttpGet]
    [Authorize]
    [Route("find")]
    public async Task<IActionResult> GetUsers([FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy)
    {
      var filters = new List<Expression<Func<User, bool>>> { c => c.Name.Contains(searchBy) };

      var users = await _unitOfWork.UsersRepository.GetPagedAsync(filters, count, page);
      var usersCount = await _unitOfWork.UsersRepository.CountAsync(filters);

      var pageReturnModel = new PageReturnModel<UserDTO>
      {
        Items = _mapper.Map<IEnumerable<UserDTO>>(users),
        TotalCount = usersCount,
        CurrentPage = page
      };
      return Ok(pageReturnModel);
    }

    [HttpPost]
    [Authorize]
    [Route("updateStatus")]
    public async Task<IActionResult> UpdateUserStatus([FromBody] User model)
    {
            var userToUpdate = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == model.Id);

            if(userToUpdate == null)
            {
                return BadRequest("User not found");
            }

            userToUpdate.Status = model.Status;

            _unitOfWork.UsersRepository.Update(userToUpdate);
            await _unitOfWork.Save();

            return Ok(userToUpdate);
        }

    [HttpPut]
    [Authorize]
    [Route("me")]
    public async Task<IActionResult> Update([FromBody] UpdateSettingsBindingModel model)
    {
      if (!ModelState.IsValid || model == null)
      {
        return BadRequest(ModelState);
      }

      var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)
          ?.Value;
      var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

      if (user == null)
      {
        return BadRequest();
      }

      user.Name = model.Name;
      user.LastName = model.LastName;
      user.About = model.About;
      user.LocationId = model.LocationId;
      user.GenderType = model.Gender;
      user.RelationshipType = model.Relationship;
      user.WorkplaceId = model.WorkplaceId;
      user.CustomGenderType = model.CustomGender;

      await _unitOfWork.UserManager.UpdateAsync(user);
      await _unitOfWork.Save();
      return new NoContentResult();
    }
  }
}