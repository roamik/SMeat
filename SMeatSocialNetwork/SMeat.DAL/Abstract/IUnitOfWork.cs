using System;
using System.Collections.Generic;
using System.Text;
using SMeat.MODELS.Models;
using System.Threading.Tasks;
using SMeat.DAL.Abstract;
using Microsoft.AspNetCore.Identity;

namespace SMeat.DAL
{
    public interface IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
        UserManager<User> UserManager { get; }
        SignInManager<User> SignInManager { get; }
        Task<int> Save();
    }
}
