using System;
using SMeat.MODELS;
using SMeat.MODELS.Models;
using System.Collections.Generic;
using System.Text;
using SMeat.DAL.Abstract;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SMeat.DAL
{
    class LocationsRepository : GenericRepository<Location>, ILocationsRepository
    {
        public LocationsRepository(IApplicationContext context) : base(context) { }
    }
}
