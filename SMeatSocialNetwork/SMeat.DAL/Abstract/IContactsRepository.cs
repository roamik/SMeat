using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Abstract
{
    public interface IContactsRepository : IGenericRepository<Contacts>
    {
       Task<List<Contacts>> GetPagedFullAsync(List<Expression<Func<Contacts, bool>>> filters = null, int count = 10, int page = 0, params Expression<Func<Contacts, object>>[] includes);
    }
}