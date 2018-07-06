﻿using Microsoft.AspNetCore.Authorization;
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
using SMeat.MODELS.Enums;
using SMeat.MODELS.BindingModels;
using System.IdentityModel.Tokens.Jwt;

namespace SMeat.API.Controllers
{
    [Route("api/[controller]")]
    public class ChatsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisConnectionFactory _cache;
        private readonly IMapper _mapper;
        public ChatsController ( IUnitOfWork unitOfWork, IRedisConnectionFactory cache, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
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
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);

            var filters = new List<Expression<Func<Chat, bool>>>();
            filters.Add(b => b.UserChats.Any(c => c.UserId == currentUserId) || b.User == currentUser);
            if ( searchBy != null ) {
                filters.Add(b => b.Text.Contains(searchBy));
            }

            var chats = await _unitOfWork.ChatsRepository.GetPagedFullAsync(filters, count, page, c => c.User);
            //var chatsCount = await _unitOfWork.ChatsRepository.CountAsync(filter: filter);
            foreach ( var chat in chats ) {
                chat.Messages = await _unitOfWork.MessagesRepository.GetPagedAsync(c=>c.ChatId == chat.Id, 25, includes: c => c.User);
                foreach ( var userChat in chat.UserChats ) {
                    var st = Enum.TryParse<UserStatusType>(await _cache.Database.StringGetAsync($"uc{userChat.UserId}{userChat.ChatId}"), out var status) ? status : 0;
                    userChat.Status = st;
                }
            }
            
            return Ok(_mapper.Map<IEnumerable<ChatDTO>>(chats).OrderByDescending(r => r.DateTime).ToList());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateChat([FromBody] ChatCreateBindingModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(ModelState);
            }

            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var currentUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == currentUserId);

            // If chat with these users exists - return it
            // WARNING! SHITCODE!! User cand be the chat starter and chat participand, and those are considere two different chats =(
            if (model.UserIds.Length == 1) // Only works on 1v1 users
            {
                // Try if user was the chat starter
                var chatUser = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == model.UserIds[0]);

                // If user is the starter
                var exChatFilters = new List<Expression<Func<Chat, bool>>>();
                exChatFilters.Add(c => c.UserId == currentUserId);
                exChatFilters.Add(c => c.UserChats.Count == 1);
                exChatFilters.Add(c => c.UserChats.First().UserId == chatUser.Id);

                var exChat = await _unitOfWork.ChatsRepository.FirstOrDefaultAsync(exChatFilters);
                if (exChat != null)
                    return Ok(exChat);

                else // If user is not the starter
                {
                    exChatFilters.Clear();
                    exChatFilters.Add(c => c.UserId == chatUser.Id);
                    exChatFilters.Add(c => c.UserChats.Count == 1);
                    exChatFilters.Add(c => c.UserChats.First().UserId == currentUserId);

                    var exChat2 = await _unitOfWork.ChatsRepository.FirstOrDefaultAsync(exChatFilters);
                    if (exChat2 != null)
                        return Ok(exChat2);
                }
            }

            if (model.Text == "" || model.Text == null)
            {
                model.Text = currentUser.Name + ' ' + currentUser.LastName + ", ";
                for (int i = 0; i < model.UserIds.Length; i++)
                {
                    var user = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == model.UserIds[i]);
                    model.Text += user.Name + ' ' + user.LastName;
                    if (i != model.UserIds.Length - 1)
                        model.Text += ", ";
                }
            }

            var chat = new Chat();
            chat.Text = model.Text;

            chat.User = currentUser;

            if (model.UserIds.Length != 0)
            {
                for (int i = 0; i < model.UserIds.Length; i++)
                {
                    var userToChat = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(u => u.Id == model.UserIds[i]);
                    if (userToChat != null)
                        chat.UserChats.Add(new UserChat { User = userToChat, ChatId = chat.Id });
                }
            }

            await _unitOfWork.ChatsRepository.AddAsync(chat);
            await _unitOfWork.Save();

            return Ok(chat);
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
