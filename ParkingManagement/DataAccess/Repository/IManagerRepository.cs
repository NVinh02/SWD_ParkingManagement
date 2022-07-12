using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IManagerRepository
    {
        public Task<Manager> Login(string username, string password);
        public Task<IEnumerable<Manager>> GetManagers();
        public Task<Manager> GetManagerByUsername(string username);
        public Task AddManager(Manager manager);
        public Task Update(Manager manager);
        public Task UpdateManagerStatus(Manager manager);
    }
}
