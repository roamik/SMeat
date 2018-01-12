using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMeat.DAL;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using SMeat.DAL.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class ChatsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ChatsController ( IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetChat ( string id) {
           // var chatUsersOnline = ByteToJson<string[]>(await _distributedCache.GetAsync("chats"));
            var chat = await _unitOfWork.ChatsRepository.GetByIdAsync(id, c => c.User, c => c.UserChats);
            return Ok(chat);
        }

        [HttpGet]
        [Authorize]
        [Route("paged")]
        public async Task<IActionResult> GetChats( [FromQuery] int page, [FromQuery] int count, [FromQuery] string searchBy)
        {
            var filters = new List<Expression<Func<Chat, bool>>>();
            if ( searchBy != null ) {
                filters.Add(b => b.Text.Contains(searchBy));
            }

            var chats = await _unitOfWork.ChatsRepository.GetPagedFullAsync(filters, count: count, page: page, includes: c => c.User);
            //var chatsCount = await _unitOfWork.ChatsRepository.CountAsync(filter: filter);
            foreach ( var chat in chats ) {
                chat.Messages = await _unitOfWork.MessagesRepository.GetPagedAsync(c=>c.ChatId == chat.Id, 25, includes: c => c.User );
            }
            return Ok(_mapper.Map<IEnumerable<ChatDTO>>(chats));
        }

        public static Dictionary<string, object> ByteToJson ( byte[] json ) {
            var jsonStr = Encoding.UTF8.GetString(json);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
        }

        public static T ByteToJson<T> ( byte[] json ) {
            var jsonStr = Encoding.UTF8.GetString(json);
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        
    }
}
