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
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;

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
            var board = await _unitOfWork.BoardsRepository.FirstOrDefaultAsync(b => b.Id == id, b => b.Likes, b => b.Dislikes);
            if (board == null)
            {
                return BadRequest("Board not found!");
            }
            
            return Ok(board);
        }

        [HttpGet]
        [Authorize]
        [Route("like/{id:guid}")]
        public async Task<IActionResult> Like(string id)
        {
            var board = await _unitOfWork.BoardsRepository.FirstOrDefaultAsync(b => b.Id == id, b => b.Likes, b => b.Dislikes );
            if (board == null)
            {
                return BadRequest("Board not found!");
            }

            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);

            var existingLike = board.Likes.FirstOrDefault( b => b.LikeFromId == currentUserId);
            if (existingLike != null) {
                board.Likes.Remove(existingLike);
            }
            else {
                board.Likes.Add(new BoardLike { LikeFrom = currentUser });
                var existingDislike = board.Dislikes.FirstOrDefault(b => b.DislikeFromId == currentUserId);
                if (existingDislike != null) board.Dislikes.Remove(existingDislike);
            }

            await _unitOfWork.Save();
            return Ok(board);
        }

        [HttpGet]
        [Authorize]
        [Route("dislike/{id:guid}")]
        public async Task<IActionResult> Dislike(string id)
        {
            var board = await _unitOfWork.BoardsRepository.FirstOrDefaultAsync(b => b.Id == id, b => b.Likes, b => b.Dislikes);
            if (board == null)
            {
                return BadRequest("Board not found!");
            }

            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);

            var existingDislike = board.Dislikes.FirstOrDefault(b => b.DislikeFromId == currentUserId);
            if (existingDislike != null) {
                board.Dislikes.Remove(existingDislike);
            }
            else {
                board.Dislikes.Add(new BoardDislike { DislikeFrom = currentUser });
                var existingLike = board.Likes.FirstOrDefault(b => b.LikeFromId == currentUserId);
                if (existingLike != null) board.Likes.Remove(existingLike);
            }

            await _unitOfWork.Save();
            return Ok(board);
        }

        [HttpGet]
        [Authorize]
        [Route("my/{id:guid}")]
        public async Task<IActionResult> GetMyBoardsAsync(string id)
        {
            var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == id);
            if (currentUser == null)
            {
                return BadRequest("User not found.");
            }

            //var boards = await _unitOfWork.BoardsRepository.GetPagedAsync(b => b.MadeBy == currentUser);
            var boards = await _unitOfWork.BoardsRepository.GetPagedAsync(filter: b => b.MadeBy == currentUser, count: 100, page: 0, b => b.Likes, b => b.Dislikes);
            if (boards == null)
            {
                return BadRequest("No boards found.");
            }

            return Ok(boards);
        }

        [HttpGet]
        //[Authorize]
        [Route("paged")]
        public async Task<IActionResult> GetBoards([FromQuery] int page = 0, [FromQuery] int count = 100, [FromQuery] string searchBy = "")
        {
            Expression<Func<Board, bool>> filter = null;
            if (searchBy != null)
            {
                filter = (b => b.Name.Contains(searchBy));
            }

            var boards = await _unitOfWork.BoardsRepository.GetPagedAsync(filter: filter, count: count, page: page, b => b.Likes, b => b.Dislikes);
            var boardsCount = await _unitOfWork.BoardsRepository.CountAsync(filter: filter);

            boards = boards.OrderByDescending(b => b.Likes.Count - b.Dislikes.Count).ToList();

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
            if (model.Name == "" || model.Text == "" || model.Name == null || model.Text == null)
            {
                return BadRequest("Empty name or text");
            }

            var board = _mapper.Map<Board>(model);

            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);
            board.MadeBy = currentUser;

            await _unitOfWork.BoardsRepository.AddAsync(board);
            await _unitOfWork.Save();
            return Ok(board);
        }
    }
}
