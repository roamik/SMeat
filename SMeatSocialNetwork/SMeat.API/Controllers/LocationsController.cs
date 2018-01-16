using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using SMeat.DAL;
using System.Linq.Expressions;
using SMeat.DAL.Abstract;
using AutoMapper;
using SMeat.MODELS.Entities;
using SMeat.MODELS.BindingModels;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LocationsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddLocation([FromBody] LocationCreateBindingModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }

            //var location = new Location { City = model.City, Country = model.Country};
            var location = _mapper.Map<Location>(model);// the same behaviour as commented above

            await _unitOfWork.LocationsRepository.AddAsync(location);
            await _unitOfWork.Save();
            return Ok(location);
        }
    }
}