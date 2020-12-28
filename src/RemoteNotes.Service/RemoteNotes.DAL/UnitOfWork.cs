using System;
using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Repositories;

namespace RemoteNotes.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private IAccountRepository _accountRepository;
        private INoteRepository _noteRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_context);

        public INoteRepository NoteRepository => _noteRepository ??= new NoteRepository(_context);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        
        public void Commit() => _context.SaveChanges();
    }
}