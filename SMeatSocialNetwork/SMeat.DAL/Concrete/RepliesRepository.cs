using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.DAL.Concrete
{
    public class RepliesRepository : GenericRepository<Reply>, IRepliesRepository
    {
        public RepliesRepository(ApplicationContext context) : base(context) { }
    }
}
