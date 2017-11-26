using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using SMeat.MODELS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace SMeat.API.Controllers
{
    [Route("api/hello")]
    public class HelloController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
           

            //var user = new User { Name = "User", LastName = "Test", Birthdate = DateTimeOffset.UtcNow };

            //var userGroupChat = new UserGroupChat {User = user };

            //var groupChat = new GroupChat { Name = "test", UserGroupChats = new List<UserGroupChat>() { userGroupChat } };

            //groupChat.UserGroupChats.First().User.Name;

            //return Ok("Hello Angular4 and ASP.NET Core");
            var dict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList()
               .ForEach(item => dict.Add(item.Type, item.Value));

            return Ok(dict);
        }
    }
}