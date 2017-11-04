using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMeat.DAL;
using SMeat.MODELS.Models;
using SMeat.MODELS.Models.BindingModels;

namespace SMeatSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST api/values
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginBindingModel model)
        {
            var user = new User { UserName = model.Login, Email = model.Login, LastName = "", Name = "" };
            var result = await _unitOfWork.UserManager.CreateAsync(user, model.Password);
            return Ok(user);
        }

        // PUT api/values/5
        [HttpPost]
        [Route("register")]
        public void Register([FromBody]RegisterBindingModel model)
        {

        }
    }
}
