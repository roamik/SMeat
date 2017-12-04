using SMeat.DAL.Abstract;
using SMeat.MODELS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SMeat.MODELS.Models;

namespace SMeat.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationContext _context;

        private UserManager<User> _userManager;

        private SignInManager<User> _signInManager;

        public UnitOfWork(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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

        private ILocationsRepository locationsRepository;

        public ILocationsRepository LocationsRepository
        {
            get
            {
                if (locationsRepository == null)
                {
                    locationsRepository = new LocationsRepository(_context);
                }
                return locationsRepository;
            }
        }

        private IWorkplacesRepository workplacesRepository;

        public IWorkplacesRepository WorkplacesRepository
        {
            get
            {
                if (workplacesRepository == null)
                {
                    workplacesRepository = new WorkplacesRepository(_context);
                }
                return workplacesRepository;
            }
        }

        public UserManager<User> UserManager
        {
            get
            {
                return _userManager;
            }
        }
        public SignInManager<User> SignInManager
        {
            get
            {
                return _signInManager;
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
