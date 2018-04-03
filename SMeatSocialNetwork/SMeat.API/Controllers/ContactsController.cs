using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMeat.DAL.Abstract;
using SMeat.MODELS.BindingModels;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;
using SMeat.MODELS.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SMeat.API.Controllers
{
  [Route("api/[controller]")]
  public class ContactsController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ContactsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    [HttpPost]
    [Authorize]
    [Route("add/{id:guid}")]
    public async Task<IActionResult> AddConnection(string id)
    {
      var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
      var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId, c => c.ContactsIAddedTo);
      var friendUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == id);

      if (friendUser == null || currentUser == null)
      {
        return BadRequest("");
      }

      var connection = currentUser.ContactsIAddedTo.FirstOrDefault(c => c.FriendId == currentUserId);
      if (connection != null)
      {
        connection.Status = ContactStatus.Confirmed;
      }

      currentUser.ContactsAddedByMe.Add(new Friends { Friend = friendUser, Status = connection != null ? ContactStatus.Confirmed : ContactStatus.Send });

      await _unitOfWork.Save();

      return Ok();
    }

    [HttpPost]
    [Authorize]
    [Route("remove/{id:guid}")]
    public async Task<IActionResult> RemoveConnection(string id)
    {
      bool isFriend;
      var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
      var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId, c => c.ContactsIAddedTo, c => c.ContactsAddedByMe);
      var friendUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == id, c => c.ContactsAddedByMe);
      var connection = currentUser.ContactsAddedByMe.FirstOrDefault(c => c.FriendId == id);
      var friendConnection = currentUser.ContactsIAddedTo.FirstOrDefault(c => c.UserId == id);
      isFriend = connection != null ? isFriend = true : isFriend = false;

      if (currentUser == null)
      {
        return BadRequest("");
      }

      if (isFriend == false)
      {
        return BadRequest("");
      }

      if (connection != null)
      {
        currentUser.ContactsAddedByMe.Remove(connection);
        //friendConnection.Status = ContactStatus.Send;
        friendConnection.Status = ContactStatus.Send;
      }

      await _unitOfWork.Save();

      return Ok();
    }

    [HttpGet]
    [Authorize]
    [Route("requests")]
    public async Task<IActionResult> GetFriendRequests([FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy, [FromQuery] ContactStatus status)
    {
      var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;

      var filters = new List<Expression<Func<Friends, bool>>> { c => c.FriendId == currentUserId, c => c.Status == ContactStatus.Send };

      var requests = await _unitOfWork.ContactsRepository.GetPagedRequestsAsync(filters: filters, count: count, page: page);

      var requestsCount = await _unitOfWork.ContactsRepository.CountAsync(filters: filters);

      var pageReturnModel = new PageReturnModel<RequestDTO>
      {
        Items = _mapper.Map<IEnumerable<RequestDTO>>(requests),
        TotalCount = requestsCount,
        CurrentPage = page
      };

      return Ok(pageReturnModel);
    }

    [HttpGet]
    [Authorize]
    [Route("contacts")]
    public async Task<IActionResult> GetUserContacts([FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy)
    {
      var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;

      var filters = new List<Expression<Func<Friends, bool>>> { c => c.UserId == currentUserId, c => c.Status == ContactStatus.Confirmed };

      var contactsList = await _unitOfWork.ContactsRepository.GetPagedContactsAsync(filters: filters, count: count, page: page);

      var contactsCount = await _unitOfWork.ContactsRepository.CountAsync(filters: filters);

      return Ok(contactsList);
    }
  }
}
