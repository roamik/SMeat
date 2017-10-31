using System;
using System.Collections.Generic;
using System.Text;
using SMeat.MODELS.Models;
using System.Threading.Tasks;
using SMeat.DAL.Abstract;

namespace SMeat.DAL
{
    public interface IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
        Task<int> Save();
    }
}
