using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task AddUser(User user)
            => UserDAO.Instance.Add(user);

        public Task<User> GetUserById(string ccid)
            => UserDAO.Instance.GetUserById(ccid);

        public Task<IEnumerable<User>> GetUsers()
            => UserDAO.Instance.GetUsers();

        public Task<User> Login(string username, string password) 
            => UserDAO.Instance.Login(username, password);

        public Task UpdateUser(User user)
            => UserDAO.Instance.Update(user);

        public Task UpdateUserStatus(User user)
            => UserDAO.Instance.UpdateUserStatus(user);
    }
}
