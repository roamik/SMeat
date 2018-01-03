using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Models;

namespace SMeat.DAL.Concrete
{
    public class BoardsRepository : GenericRepository<Board>, IBoardsRepository
    {
        public BoardsRepository(IApplicationContext context) : base(context) { }
    }
}
