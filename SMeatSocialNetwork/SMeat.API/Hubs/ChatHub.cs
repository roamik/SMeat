using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SMeat.DAL;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
 
namespace SMeatSocialNetwork.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private IUnitOfWork _unitOfWork;
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


        public async override Task OnConnectedAsync()
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

        public async override Task OnDisconnectedAsync(Exception exception)
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