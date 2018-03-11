using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Abstract
{
    public interface IContactsRepository : IGenericRepository<Friends>
    {
       Task<List<Friends>> GetPagedRequestsAsync(List<Expression<Func<Friends, bool>>> filters = null, int count = 10, int page = 0, params Expression<Func<Friends, object>>[] includes);
    }
}