using SMeat.DAL.Abstract;
using SMeat.MODELS;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Concrete
{
    public class ContactsRepository : GenericRepository<Contacts>, IContactsRepository
    {
        public ContactsRepository(IApplicationContext context) : base(context) { }

      public async Task<List<Contacts>> GetPagedFullAsync(List<Expression<Func<Contacts, bool>>> filters = null, /*Func<T, object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<Contacts, object>>[] includes)
        {
            return await GetAsync(filters, includes)
                .Include(c => c.FirstUser).ThenInclude(c => c.Id)
                .Skip(page * count)
                .Take(count)
                .ToListAsync();
        }
    }
}