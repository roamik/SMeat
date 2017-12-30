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
    public class LocationsController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public LocationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        [Route("paged")]
        public async Task<IActionResult> GetLocations([FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy)
        {
            Expression<Func<Location, bool>> filter = null;
            if (searchBy != null)
            {
               filter = (l => l.City.Contains(searchBy));
            }

            var locations = await _unitOfWork.BoardsRepository.GetPagedAsync(filter:filter, count: count, page: page);
            var locationsCount = await _unitOfWork.BoardsRepository.CountAsync(filter:filter);

            //return Ok(new { Items = locations, TotalCount = locationsCount, CurrentPage = page });

            return Ok(locations);
        }
    }
}