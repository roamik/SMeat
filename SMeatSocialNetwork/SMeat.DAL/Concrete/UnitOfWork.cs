using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SMeat.DAL.Abstract;
using SMeat.MODELS;
using SMeat.MODELS.Models;

namespace SMeat.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IApplicationContext _context;

        public UnitOfWork(IApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        private IUsersRepository _usersRepository;

        public IUsersRepository UsersRepository
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new UsersRepository(_context);
                }
                return _usersRepository;
            }
        }

        private IBoardsRepository _boardsRepository;

        public IBoardsRepository BoardsRepository
        {
            get
            {
                if (_boardsRepository == null)
                {
                    _boardsRepository = new BoardsRepository(_context);
                }
                return _boardsRepository;
            }
        }

        private ILocationsRepository _locationsRepository;

        public ILocationsRepository LocationsRepository
        {
            get
            {
                if (_locationsRepository == null)
                {
                    _locationsRepository = new LocationsRepository(_context);
                }
                return _locationsRepository;
            }
        }

        private IWorkplacesRepository _workplacesRepository;

        public IWorkplacesRepository WorkplacesRepository
        {
            get
            {
                if (_workplacesRepository == null)
                {
                    _workplacesRepository = new WorkplacesRepository(_context);
                }
                return _workplacesRepository;
            }
        }

        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        // IDisposable
        readonly bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
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
