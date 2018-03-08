using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Concrete
{
    public class WorkplacesRepository : GenericRepository<Workplace>, IWorkplacesRepository
    {
        public WorkplacesRepository(ApplicationContext context) : base(context) { }
    }
}
