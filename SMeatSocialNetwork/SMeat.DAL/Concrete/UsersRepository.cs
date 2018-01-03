using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Models;

namespace SMeat.DAL.Concrete
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(IApplicationContext context) : base(context) { }
    }
}
