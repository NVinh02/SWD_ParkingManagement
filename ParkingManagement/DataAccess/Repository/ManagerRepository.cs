using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        public Task AddManager(Manager manager)
            => ManagerDAO.Instance.Add(manager);

        public Task<Manager> GetManagerByUsername(string username)
            => ManagerDAO.Instance.GetManagerByUsername(username);

        public Task<IEnumerable<Manager>> GetManagers()
            => ManagerDAO.Instance.GetManagers(); 

        public Task<Manager> Login(string username, string password) 
            => ManagerDAO.Instance.Login(username, password);

        public Task Update(Manager manager)
            => ManagerDAO.Instance.Update(manager);

        public Task UpdateManagerStatus(Manager manager)
            => ManagerDAO.Instance.UpdateManagerStatus(manager);
    }
}
