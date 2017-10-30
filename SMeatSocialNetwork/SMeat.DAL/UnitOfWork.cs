using SMeat.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.DAL
{
    class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationContext context = new ApplicationContext();
        private GenericRepository<User> usersRepository;

        public IGenericRepository<User> UsersRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new GenericRepository<User>(context);
                }
                return usersRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
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
