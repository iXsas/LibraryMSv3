using LibraryMSv3.Data;
using LibraryMSv3.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryMSv3.Repositories.Interfaces;

namespace LibraryMSv3.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public User GetUser(string username)
        {
            return _context.Users.SingleOrDefault(x => x.UserName == username);
        }
        public async Task CreateUser(User user)
        {
            if (_context.Users != null)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<User>?> Get()
        {
            if (_context.Users != null)
            {
                return await _context.Users.ToListAsync();
            }
            return null;
        }
        public async Task<User> Get(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User?> Get(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public void SaveUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public async Task DeleteUser(User user)
        {
            if (_context.Users != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
