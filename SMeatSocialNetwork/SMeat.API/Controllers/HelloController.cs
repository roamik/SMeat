using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using SMeat.MODELS.Models;

namespace SMeat.API.Controllers
{
    [Route("api/hello")]
    public class HelloController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            //var user = new User { Name = "user", LastName = "new", Birthdate = DateTimeOffset.UtcNow };

            //var userGroupChat = new UserGroupChat {User = user };

            //var groupChat = new GroupChat { Name = "test", UserGroupChats = new List<UserGroupChat>() { userGroupChat } };

            //groupChat.UserGroupChats.First().User.Name;

            return Ok("Hello Angular4 and ASP.NET Core");
        }
    }
}