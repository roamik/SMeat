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
        //[Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetReplyByBoard(string id)
        {
            var board = await _unitOfWork.BoardsRepository.FirstOrDefaultAsync(b => b.Id == id);
            if (board == null)
            {
                return BadRequest("Board not found!");
            }

            //var replies = await _unitOfWork.RepliesRepository.GetPagedAsync(rep => rep.BoardId == board.Id);
            var replies = await _unitOfWork.RepliesRepository.GetPagedAsync(filter: rep => rep.BoardId == board.Id, count: 500, page: 0, rep => rep.ReplyTo);

            return Ok(replies.OrderBy(r => r.DateTime).ToList());
        }

        [HttpGet]
        //[Authorize]
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

        [HttpGet]
        //[Authorize]
        [Route("to/{id:guid}")]
        public async Task<IActionResult> GetReplyiedAt(string id)
        {
            var reply = await _unitOfWork.RepliesRepository.FirstOrDefaultAsync(r => r.Id == id);
            if (reply == null)
            {
                return BadRequest("Reply not found!");
            }
            
            var replies = await _unitOfWork.RepliesRepository.GetPagedAsync(rep => rep.ReplyTo.Any(r => r.ReplyToId == reply.Id));
            var idStrings = replies.Select(r => r.Id);

            return Ok(idStrings);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReply([FromBody] ReplyCreateBindingModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }
            if (model.Text == "" || model.Text == null)
            {
                return BadRequest("Empty name or text");
            }

            var reply = new Reply();
            reply.Text = model.Text;
            reply.BoardId = model.BoardId;
            reply.DateTime = DateTimeOffset.Now;

            for(int i = 0; i < model.ReplyId.Length; i++)
            {
                Reply replyTo = await _unitOfWork.RepliesRepository.FirstOrDefaultAsync(r => r.Id == model.ReplyId[i]);

                if (replyTo != null)
                {
                    try
                    {
                        reply.ReplyTo.Add(new ReplyReply { ReplyTo = replyTo } );
                    }
                    catch (Exception e)
                    {
                        Exception thisEx = e;
                    }
                }
            }

            await _unitOfWork.RepliesRepository.AddAsync(reply);
            await _unitOfWork.Save();
            return Ok(reply);
        }
    }
}
