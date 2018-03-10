using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMeat.DAL.Abstract;
using SMeat.MODELS.BindingModels;
using SMeat.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class RepliesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RepliesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetReplyByBoard(string id)
        {
            var board = await _unitOfWork.BoardsRepository.FirstOrDefaultAsync(b => b.Id == id);
            if (board == null)
            {
                return BadRequest("Board not found!");
            }

            var replies = await _unitOfWork.RepliesRepository.GetPagedAsync(rep => rep.BoardId == board.Id);
            replies.Sort((x, y) => DateTimeOffset.Compare(y.DateTime, x.DateTime));
            replies.Reverse();
            return Ok(replies);
        }

        [HttpGet]
        [Authorize]
        [Route("count/{id:guid}")]
        public async Task<IActionResult> GetReplyCountByBoard(string id)
        {
            var board = await _unitOfWork.BoardsRepository.FirstOrDefaultAsync(b => b.Id == id);
            if (board == null)
            {
                return BadRequest("Board not found!");
            }

            var replies = await _unitOfWork.RepliesRepository.GetPagedAsync(rep => rep.BoardId == board.Id);

            return Ok(replies.Count);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReply([FromBody] ReplyCreateBindingModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }

            var reply = new Reply();
            reply.Text = model.Text;
            reply.BoardId = model.BoardId;
            reply.DateTime = new DateTimeOffset();

            await _unitOfWork.RepliesRepository.AddAsync(reply);
            await _unitOfWork.Save();
            return Ok(reply);
        }
    }
}
