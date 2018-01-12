using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Concrete
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(IApplicationContext context) : base(context) { }
    }
}
