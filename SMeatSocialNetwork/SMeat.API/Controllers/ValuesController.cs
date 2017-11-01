using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMeat.DAL;
using SMeat.MODELS.Models;

namespace SMeatSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _unitOfWork.UsersRepository.AddAsync(new User { Name = "user", LastName = "new", Birthdate = DateTimeOffset.UtcNow });
            await _unitOfWork.Save();
            return Ok(user);/*new string[] { "value1", "value2", "value3", "value4" }*/
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
