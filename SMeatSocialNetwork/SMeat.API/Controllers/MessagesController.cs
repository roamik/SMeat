using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using SMeat.DAL.Abstract;
using System.Collections.Generic;
using AutoMapper;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessagesController ( IUnitOfWork unitOfWork, IMapper mapper ) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("paged")]
        public async Task<IActionResult> GetMessages ( [FromQuery] int page, [FromQuery] int count, [FromQuery] DateTimeOffset? from, [FromQuery] string searchBy, [FromQuery] string chatId ) {
            var filters = new List<Expression<Func<Message, bool>>>();
            if ( searchBy != null ) {
                filters.Add(m => m.Text.Contains(searchBy));
            }
            if ( from.HasValue ) {
                filters.Add( m => m.DateTime <= from );
            }
            if ( !string.IsNullOrEmpty( chatId ) ) {
                filters.Add( m => m.ChatId == chatId );
            }
            var messages =  await _unitOfWork.MessagesRepository.GetPagedAsync(filters: filters, count: count, page: page, includes: c => c.User);
            //var messagesCount = await _unitOfWork.MessagesRepository.CountAsync(filters: filters);

            //return Ok(new { Items = locations, TotalCount = locationsCount, CurrentPage = page });

            return Ok(_mapper.Map<IEnumerable<MessageDTO>>(messages));
        }
    }
}