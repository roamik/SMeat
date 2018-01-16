using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SMeat.DAL;
using SMeat.DAL.Abstract;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;
using StackExchange.Redis;

namespace SMeat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisConnectionFactory _cache;
        private readonly IMapper _mapper;
        public ChatHub(IUnitOfWork unitOfWork, IRedisConnectionFactory cache, IMapper mapper ) {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }
        
        public async Task SendAsync(MessageDTO messageDTO)
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user != null) {
                var message = _mapper.Map<Message>( messageDTO );
                var tempId = messageDTO.TempId;
                message.UserId = user.Id;
                message.DateTime = DateTimeOffset.UtcNow;
                messageDTO = _mapper.Map<MessageDTO>( message );
                messageDTO.TempId = tempId;
                await _unitOfWork.MessagesRepository.AddAsync(message);
                await _unitOfWork.Save();
                await Clients.Group(message.ChatId).InvokeAsync("OnSend", Context.ConnectionId, _mapper.Map<UserDTO>(user), messageDTO);
            }            
        }

        public async Task ConnectToChatAsync ( string chatId ) {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if ( user != null ) {
                await Groups.AddAsync( Context.ConnectionId, chatId );
                await Clients.Group(chatId).InvokeAsync("OnConnectToChat", Context.ConnectionId, _mapper.Map<UserDTO>(user));
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                await Clients.Client(Context.ConnectionId).InvokeAsync("OnConnected", Context.ConnectionId, _mapper.Map<UserDTO>(user));
                await _cache.Database.StringSetAsync( user.Id, Context.ConnectionId );
                await _cache.Database.ListRightPushAsync("chats", user.Id);
            }
            await base.OnConnectedAsync();
        }

        //public override async Task OnReconectAsync() {
        //    var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
        //    var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
        //    if ( user != null ) {
        //        await Clients.All.InvokeAsync("OnReconnected", Context.ConnectionId, _mapper.Map<UserDTO>(user));
        //    }
        //}

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                await Clients.All.InvokeAsync("OnDisconnected", Context.ConnectionId, _mapper.Map<UserDTO>(user), exception.Message);
                await _cache.Database.SetRemoveAsync(user.Id, Context.ConnectionId);
                await _cache.Database.ListRemoveAsync("chats", user.Id);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}