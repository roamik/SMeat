using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SMeat.DAL;
using SMeat.DAL.Abstract;

namespace SMeat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task SendAsync(string message)
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
               var userObject = new { UserName = user.UserName, Name = user.Name, LastName = user.LastName, Id = user.Id };
                await Clients.All.InvokeAsync("OnSend", Context.ConnectionId, userObject, message);
            }            
        }


        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userObject = new { UserName = user.UserName, Name = user.Name, LastName = user.LastName, Id = user.Id };
                await Clients.All.InvokeAsync("OnConnected", Context.ConnectionId, userObject);
            }
            await base.OnConnectedAsync();
        }

        public async Task OnReconnectedAsync () {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if ( user != null ) {
                var userObject = new { UserName = user.UserName, Name = user.Name, LastName = user.LastName, Id = user.Id };
                await Clients.All.InvokeAsync("OnReconnected", Context.ConnectionId, userObject);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value; // Get user id from token Sid claim
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userObject = new { UserName = user.UserName, Name = user.Name, LastName = user.LastName, Id = user.Id };
                await Clients.All.InvokeAsync("OnDisconnected", Context.ConnectionId, userObject, exception.Message);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}