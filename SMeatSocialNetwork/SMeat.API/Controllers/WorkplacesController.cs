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
using System.Linq.Expressions;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class WorkplacesController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public WorkplacesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        [Route("paged")]
        public async Task<IActionResult> GetWorkplaces([FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy)
        {
            Expression<Func<Workplace, bool>> filter = null;
            if (searchBy != null)
            {
                filter = (w => w.Location.City.Contains(searchBy));
            }

            var workplaces = await _unitOfWork.WorkplacesRepository.GetPagedAsync(filter: filter, count: count, page: page, includes: w => w.Location);
            var workplacesCount = await _unitOfWork.WorkplacesRepository.CountAsync(filter: filter);

            //return Ok(new { Items = workplaces, TotalCount = workplacesCount, CurrentPage = page });

            return Ok(workplaces);
        }
    }
}