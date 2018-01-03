using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using SMeat.MODELS.Models;
using Microsoft.AspNetCore.Authorization;
using SMeat.DAL;
using System.Linq.Expressions;
using SMeat.DAL.Abstract;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
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
               filter = l => l.City.Contains(searchBy);
            }

            var locations = await _unitOfWork.LocationsRepository.GetPagedAsync(filter:filter, count: count, page: page);
            var locationsCount = await _unitOfWork.LocationsRepository.CountAsync(filter:filter);

            //return Ok(new { Items = locations, TotalCount = locationsCount, CurrentPage = page });

            return Ok(locations);
        }
    }
}