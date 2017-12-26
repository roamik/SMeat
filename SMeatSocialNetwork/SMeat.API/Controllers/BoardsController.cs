using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        [HttpGet]
        [Route("boards")]
        public async Task<IActionResult> Boards()
        {
            throw new NotImplementedException();
            return Ok();
        }
    }
}
