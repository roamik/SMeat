using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Abstract
{
    public interface IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
        IRepliesRepository RepliesRepository { get; }
        IBoardsRepository BoardsRepository { get; }
        IChatsRepository ChatsRepository { get; }
        ILocationsRepository LocationsRepository { get; }
        IWorkplacesRepository WorkplacesRepository { get; }
        IMessagesRepository MessagesRepository { get; }
        IContactsRepository ContactsRepository { get; }
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }
        //object RepliesRepository { get; }

        Task<int> Save();
    }
}
