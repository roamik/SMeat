using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class BoardController : Controller
    {
        [HttpGet]
        [Route("GetBoards")]
        public async Task<IActionResult> GetBoards()
        {
            throw new NotImplementedException();
            return Ok();
        }
    }
}
