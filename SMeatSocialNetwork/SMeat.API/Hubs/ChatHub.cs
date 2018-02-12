using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
using SMeat.MODELS.Enums;
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
            var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
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
            var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if ( user != null ) {
                await Groups.AddAsync( Context.ConnectionId, chatId );
                //await _cache.Database.ListRightPushAsync("UserChats"+ user.Id, chatId);
                //await _cache.Database.ListRightPushAsync("UsersStatus" + user.Id, (int)user.Status);
                await _cache.Database.StringSetAsync(Context.ConnectionId, chatId);
                await _cache.Database.StringSetAsync($"uc{userId}{chatId}", (int) UserStatusType.Online);
                await Clients.Group(chatId).InvokeAsync("OnConnectToChat", Context.ConnectionId, _mapper.Map<UserDTO>(user));
                await Clients.Group(chatId).InvokeAsync("OnUserStatusChange", Context.ConnectionId, userId, UserStatusType.Online);
            }
        }


        public async Task UserStatusChangeAsync ( UserStatusType status, string chatId) {
            //todo:add user satus to each chat
            var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            //var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            //if ( user != null ) {
                //await _cache.Database.ListRightPushAsync("UserChats" + userId, chatId);
                // await _cache.Database.ListRightPushAsync("UsersStatus" + userId, (int)status);
                await _cache.Database.StringSetAsync($"uc{userId}{chatId}", (int)status);
                await Clients.Group(chatId).InvokeAsync("OnUserStatusChange", Context.ConnectionId, userId, status);
           // }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                await Clients.Client(Context.ConnectionId).InvokeAsync("OnConnected", Context.ConnectionId, _mapper.Map<UserDTO>(user));

                await _cache.Database.ListRightPushAsync( user.Id, Context.ConnectionId );
                //await _cache.Database.ListRightPushAsync("chats", user.Id);
            }
            await base.OnConnectedAsync();
        }

        //public override async Task OnReconect () {
        //    var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
        //    var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
        //    if ( user != null ) {
        //        await Clients.All.InvokeAsync("OnReconnected", Context.ConnectionId, _mapper.Map<UserDTO>(user));
        //    }
        //}

        public override async Task OnDisconnectedAsync( Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            //var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            //if (user != null)
            //{
            //await Clients.All.InvokeAsync("OnDisconnected", Context.ConnectionId, _mapper.Map<UserDTO>(user), exception?.Message);
            var chatId = await _cache.Database.StringGetAsync(Context.ConnectionId);
            if ( chatId.HasValue ) {
                await _cache.Database.KeyDeleteAsync($"uc{userId}{chatId}");
                await _cache.Database.KeyDeleteAsync(Context.ConnectionId);
                await Clients.Group(chatId).InvokeAsync("OnUserStatusChange", Context.ConnectionId, userId, UserStatusType.Offline);
            }
            await _cache.Database.ListRemoveAsync(userId, Context.ConnectionId, 1);
            await _cache.Database.ListRemoveAsync("chats", userId);
            //  }
        }
    }
}