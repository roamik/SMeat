using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Models;

namespace SMeat.DAL.Concrete
{
    public class LocationsRepository : GenericRepository<Location>, ILocationsRepository
    {
        public LocationsRepository(IApplicationContext context) : base(context) { }
    }
}
