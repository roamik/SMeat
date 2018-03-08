using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Concrete
{
    public class LocationsRepository : GenericRepository<Location>, ILocationsRepository
    {
        public LocationsRepository(ApplicationContext context) : base(context) { }
    }
}
