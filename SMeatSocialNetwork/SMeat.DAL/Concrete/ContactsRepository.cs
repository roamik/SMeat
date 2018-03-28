using SMeat.DAL.Abstract;
using SMeat.MODELS;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using SMeat.MODELS.Entities;
using SMeat.MODELS.Enums;

namespace SMeat.DAL.Concrete
{
    public class ContactsRepository : GenericRepository<Friends>, IContactsRepository
    {
        public ContactsRepository(ApplicationContext context) : base(context) { }

      public async Task<List<Friends>> GetPagedRequestsAsync(List<Expression<Func<Friends, bool>>> filters = null, /*Func<T, object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<Friends, object>>[] includes)
        {
            return await GetAsync(filters, includes)
                .Include(c => c.User).Include(f => f.Friend).Where(s => s.Status == ContactStatus.Send)
                .Skip(page * count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Friends>> GetPagedContactsAsync(List<Expression<Func<Friends, bool>>> filters = null, /*Func<T, object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<Friends, object>>[] includes)
        {
            return await GetAsync(filters, includes)
                .Include(f => f.Friend).Where(s => s.Status == ContactStatus.Confirmed)
                .Skip(page * count)
                .Take(count)
                .ToListAsync();
        }
    }
}