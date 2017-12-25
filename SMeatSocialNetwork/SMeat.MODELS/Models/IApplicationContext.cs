using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMeat.MODELS.Models;

namespace SMeat.MODELS
{
    public interface IApplicationContext : IContext, IIdentityContext
    {
        DbSet<Reply> Replies { get; set; }
        DbSet<Board> Boards { get; set; }
        DbSet<Chat> Chats { get; set; }
        DbSet<Contacts> Contacts { get; set; }
        DbSet<GroupChat> GroupChats { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<UserChat> UserChats { get; set; }
        DbSet<UserGroupChat> UserGroupChats { get; set; }
        DbSet<Workplace> WorkPlaces { get; set; }      

    }
}