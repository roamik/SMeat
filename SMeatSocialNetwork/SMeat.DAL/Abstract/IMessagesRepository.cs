using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SMeat.MODELS.Entities;

namespace SMeat.DAL.Abstract {
    public interface IMessagesRepository : IGenericRepository<Message> {
        Task<Message> GetByIdFullAsync ( string id, params Expression<Func<Message, object>>[] includes );

        Task<List<Message>> GetPagedAsync ( List<Expression<Func<Message, bool>>> filters = null, int count = 10,
                                            int page = 0, Expression<Func<Message, object>> orderBy = null, bool isReversed = true,
                                            params Expression<Func<Message, object>>[] includes );

        Task<List<Message>> GetPagedAsync ( Expression<Func<Message, bool>> filter = null, int count = 10,
                                            int page = 0, Expression<Func<Message, object>> orderBy = null, bool isReversed = true,
                                            params Expression<Func<Message, object>>[] includes );
    }
}