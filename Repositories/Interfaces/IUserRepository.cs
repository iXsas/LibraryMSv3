using LibraryMSv3.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMSv3.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User GetUser(string userName);
        void SaveUser(User user);
        public Task<IEnumerable<User>?> Get();
        public Task<User> Get(Guid id);
        public Task<User?> Get(string userName);
        public Task DeleteUser(User user);
    }
}
