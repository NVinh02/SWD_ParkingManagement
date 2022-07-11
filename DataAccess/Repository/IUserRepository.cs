using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IUserRepository
    {
        public Task<User> Login(string username, string password);
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUserById(string ccid);
        public Task UpdateUserStatus(User user);
        public Task AddUser(User user);
        public Task UpdateUser(User user);
    }
}
