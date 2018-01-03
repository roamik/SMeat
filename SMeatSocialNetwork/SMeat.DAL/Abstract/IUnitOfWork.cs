using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SMeat.MODELS.Models;

namespace SMeat.DAL.Abstract
{
    public interface IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
        IBoardsRepository BoardsRepository { get; }
        ILocationsRepository LocationsRepository { get; }
        IWorkplacesRepository WorkplacesRepository { get; }
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }
        Task<int> Save();
    }
}
