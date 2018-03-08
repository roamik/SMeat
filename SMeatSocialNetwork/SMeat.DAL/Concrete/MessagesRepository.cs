using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Concrete {
    public class MessagesRepository : GenericRepository<Message>, IMessagesRepository {
        public MessagesRepository (ApplicationContext context ) : base(context) { }

        public Task<Message> GetByIdFullAsync ( string id, params Expression<Func<Message, object>>[] includes ) {
            var query = Data;
            query = query.Include( c => c.User );
            if ( includes != null ) {
                query = includes.Aggregate(query, ( current, include ) => current.Include(include));
            }
            return query.FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public async Task<List<Message>> GetPagedAsync ( List<Expression<Func<Message, bool>>> filters = null, int count = 10, int page = 0, Expression<Func<Message, object>> orderBy = null, bool isReversed = true, params Expression<Func<Message, object>>[] includes ) {
            if(orderBy == null) { orderBy = m => m.DateTime; }  
            var items = await GetAsync(filters:filters, includes: includes)
                .Include(c => c.User)
                .OrderByDescending(orderBy)
                .Skip(count * page)
                .Take(count)
                .ToListAsync();
            if ( isReversed ) {
                items.Reverse();
            }
            return items;
        }

        public Task<List<Message>> GetPagedAsync ( Expression<Func<Message, bool>> filter = null, int count = 10, int page = 0, Expression<Func<Message, object>> orderBy = null, bool isReversed = true, params Expression<Func<Message, object>>[] includes ) {
            return GetPagedAsync( new List<Expression<Func<Message, bool>>> { filter }, count, page, orderBy, isReversed, includes );
        }

        
    }
}