using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Models;

namespace SMeat.DAL.Concrete
{
    public class WorkplacesRepository : GenericRepository<Workplace>, IWorkplacesRepository
    {
        public WorkplacesRepository(IApplicationContext context) : base(context) { }
    }
}
