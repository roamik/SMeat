using SMeat.DAL.Abstract;
using SMeat.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.DAL
{
    class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationContext context) : base(context) { }
    }
}
