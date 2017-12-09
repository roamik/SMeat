using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.DAL
{
    class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(IApplicationContext context) : base(context) { }
    }
}
