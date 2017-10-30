using System;
using System.Collections.Generic;
using System.Text;
using SMeat.MODELS.Models;

namespace SMeat.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> UsersRepository { get; }
        void Save();
    }
}
