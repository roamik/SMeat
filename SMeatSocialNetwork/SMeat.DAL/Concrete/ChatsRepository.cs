using SMeat.DAL.Abstract;
using SMeat.MODELS;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Concrete {
    public class ChatsRepository : GenericRepository<Chat>, IChatsRepository {
        public ChatsRepository ( IApplicationContext context ) : base(context) { }

        public Task<Chat> GetByIdFullAsync ( string id, params Expression<Func<Chat, object>>[] includes ) {
            var query = Data;
            query = query.Include( c => c.Messages.OrderByDescending( m=>m.DateTime).Take(20)).ThenInclude( c => c.User);
            query = query.Include( c => c.UserChats ).ThenInclude( c => c.User );
            if ( includes != null ) {
                query = includes.Aggregate( query, ( current, include ) => current.Include( include ) );
            }
            return query.FirstOrDefaultAsync( c=>c.Id == id );
        }

        public async Task<List<Chat>> GetPagedFullAsync ( List<Expression<Func<Chat, bool>>> filters = null, /*Func<T, object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<Chat, object>>[] includes ) {
            return await GetAsync(filters, includes)
                //.Include(c => c.Messages).ThenInclude(c => c.User)
                .Include(c => c.UserChats).ThenInclude(c => c.User)
                .Skip(page * count)
                .Take(count)
                .ToListAsync();
        }
    }
}