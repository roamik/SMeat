using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMeat.DAL;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SMeat.DAL.Abstract;
using SMeat.MODELS.Entities;
using AutoMapper;
using SMeat.MODELS.BindingModels;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BoardsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBoardByIdAsync(string id)
        {
            var board = await _unitOfWork.BoardsRepository.FirstOrDefaultAsync(b => b.Id == id);
            if (board == null)
            {
                return BadRequest("User not found!");
            }

            return Ok(new { Id = board.Id, Name = board.Name, Text = board.Text, Likes = board.Likes, Dislikes = board.Dislikes });
        }

        [HttpGet]
        [Authorize]
        [Route("paged")]
        public async Task<IActionResult> GetBoards([FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy)
        {
            Expression<Func<Board, bool>> filter = null;
            if (searchBy != null)
            {
                filter = (b => b.Name.Contains(searchBy));
            }

            var boards = await _unitOfWork.BoardsRepository.GetPagedAsync(filter: filter, count: count, page: page);
            var boardsCount = await _unitOfWork.BoardsRepository.CountAsync(filter: filter);

            return Ok(boards);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBoard([FromBody] BoardCreateBindingModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }
            
            var board = _mapper.Map<Board>(model);

            await _unitOfWork.BoardsRepository.AddAsync(board);
            await _unitOfWork.Save();
            return Ok(board);
        }
    }
}
