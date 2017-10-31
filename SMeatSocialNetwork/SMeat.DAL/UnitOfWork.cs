using SMeat.DAL.Abstract;
using SMeat.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMeat.DAL
{
    class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationContext context = new ApplicationContext();
        private IUsersRepository usersRepository;

        public IUsersRepository UsersRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new UsersRepository(context);
                }
                return usersRepository;
            }
        }

        public async Task<int> Save()
        {
            await context.SaveChangesAsync();
            return 1;
        }

        // IDisposable
        bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
