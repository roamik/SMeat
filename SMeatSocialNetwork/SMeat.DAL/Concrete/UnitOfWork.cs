using SMeat.DAL.Abstract;
using SMeat.MODELS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMeat.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        private IUsersRepository usersRepository;

        public IUsersRepository UsersRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new UsersRepository(_context);
                }
                return usersRepository;
            }
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        // IDisposable
        bool disposed = false;

       

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
