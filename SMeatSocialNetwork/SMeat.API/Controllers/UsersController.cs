using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using SMeat.MODELS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using SMeat.DAL;
using SMeat.MODELS.Models.BindingModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return Ok(new { Name = user.Name, LastName = user.LastName, About = user.About, LocationId = user.LocationId, WorkplaceId = user.WorkplaceId, Gender = user.GenderType , Relationship = user.RelationshipType, Id = user.Id });
        }

        [HttpGet]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var user = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == id, //1st filterBy
                u => u.Location, u=>u.Workplace.Location ); //include foreign entities
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            return Ok(new { Name = user.Name, LastName = user.LastName, About = user.About, Location = user.Location, Workplace = user.Workplace, Gender = user.GenderType, Relationship = user.RelationshipType, Id = user.Id });
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

            var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
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

            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.Save();
            return new NoContentResult();
        }
    }
}