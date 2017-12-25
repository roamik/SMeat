using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.DAL.Concrete
{
    class BoardsRepository : GenericRepository<Board>, IBoardsRepository
    {
        public BoardsRepository(IApplicationContext context) : base(context) { }
    }
}
