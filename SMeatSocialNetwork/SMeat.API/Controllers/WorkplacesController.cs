using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using SMeat.DAL.Abstract;
using SMeat.MODELS.Entities;
using SMeat.MODELS.BindingModels;
using AutoMapper;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class WorkplacesController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WorkplacesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddWorkplace([FromBody] WorkplaceCreateBindingModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }

            // var workplace = new Workplace { CompanyName = model.CompanyName, Position = model.Position, LocationId = model.LocationId};
            var workplace = _mapper.Map<Workplace>(model);// the same behaviour as commented above

            await _unitOfWork.WorkplacesRepository.AddAsync(workplace);
            await _unitOfWork.Save();
            return Ok(workplace);
        }
    }
}