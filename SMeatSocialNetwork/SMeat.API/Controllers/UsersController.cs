using System;
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

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        [Route("me")]
        public async Task<IActionResult> GetMyInfoAsync()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)
                ?.Value; // Get user id from token Sid claim
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
                Id = user.Id
            });
        }

        [HttpGet]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim

            //var userContacts = _unitOfWork.UsersRepository.GetAsync()
            var user = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == id, //1st filterBy
                u => u.Location, u => u.Workplace.Location); //include foreign entities
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            if (user.Id != currentUserId)
            {

            }

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
                CurrentUserId = currentUserId
            });
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

        [HttpPost]
        [Authorize]
        [Route("add/{id:guid}")]
        public async Task<IActionResult> AddConnection(string id)
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var friendUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == id);


            if (friendUser == null || currentUser == null)
            {
                return BadRequest("");
            }

            currentUser.ContactsAddedByMe.Add(new Friends { Friend = friendUser, Status = ContactStatus.Send });

            await _unitOfWork.Save();

            return Ok();
        }

        [Authorize]
        [Route("confirm/{id:guid}")]
        public async Task<IActionResult> ConfirmConnection(string id)
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            var friendUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == id);


            if (friendUser == null || currentUser == null)
            {
                return BadRequest("");
            }

            friendUser.ContactsIAddedTo.Add(new Friends { Friend = currentUser, Status = ContactStatus.Confirmed });

            await _unitOfWork.Save();

            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("paged")]
        public async Task<IActionResult> GetFriendRequests([FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy)
        {
            Expression<Func<Friends, bool>> filter = null;

                       
            var requests = await _unitOfWork.ContactsRepository.GetPagedRequestsAsync(count: count, page: page);

            var requestsCount = await _unitOfWork.ContactsRepository.CountAsync(filter: filter);
            
            return Ok(requests);
        }
    }
}