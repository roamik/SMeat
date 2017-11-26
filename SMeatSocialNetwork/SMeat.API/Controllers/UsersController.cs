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
using System.Security.Claims;

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
            var userName = User?.Identity?.Name ?? ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name).Value;
            var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
            if(user == null)
            {
                return Forbid("User not found!");
            }
            return Ok(new { UserName = user.UserName, Id = user.Id });
        }

        [HttpGet]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            return Ok(new { UserName = user.UserName, UserLastName = user.LastName, Id = user.Id });
        }
    }
}