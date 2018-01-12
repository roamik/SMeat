using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Abstract
{
    public interface IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
        IBoardsRepository BoardsRepository { get; }
        IChatsRepository ChatsRepository { get; }
        ILocationsRepository LocationsRepository { get; }
        IWorkplacesRepository WorkplacesRepository { get; }
        IMessagesRepository MessagesRepository { get; }
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }
        Task<int> Save();
    }
}
