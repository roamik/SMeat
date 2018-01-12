using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Abstract {
    public interface IChatsRepository : IGenericRepository<Chat> {
        Task<Chat> GetByIdFullAsync ( string id, params Expression<Func<Chat, object>>[] includes );
        Task<List<Chat>> GetPagedFullAsync ( List<Expression<Func<Chat, bool>>> filters = null, /*Func<T, object> orderBy = null,*/ int count = 10, int page = 0, params Expression<Func<Chat, object>>[] includes );
    }
}