using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ManagerDAO
    {
        private static ManagerDAO instance = null;
        private static readonly object instanceLock = new object();
        private ManagerDAO() { }
        public static ManagerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ManagerDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<Manager> Login(string username, string password)
        {
            Manager manager = null;
            try
            {
                var _context = new ParkingManagementContext();
                manager = _context.Managers.SingleOrDefault
                    (m => m.Username == username && m.Password == password && m.Status == true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return manager;
        }

        public async Task<Manager> GetManagerByUsername(string username)
        {
            Manager manager = null;
            try
            {
                var _context = new ParkingManagementContext();
                manager = await _context.Managers.FirstOrDefaultAsync(m => m.Username == username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return manager;
        }

        public async Task<IEnumerable<Manager>> GetManagers()
        {
            List<Manager> managers = null;
            try
            {
                var _context = new ParkingManagementContext();
                managers = await _context.Managers.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return managers;
        }

        public async Task Add(Manager manager)
        {
            try
            {
                var _context = new ParkingManagementContext();
                await _context.Managers.AddAsync(manager);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Manager manager)
        {
            try
            {
                var _context = new ParkingManagementContext();
                _context.Managers.Update(manager);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateManagerStatus(Manager manager)
        {
            try
            {
                var _context = new ParkingManagementContext();
                manager.Status = !manager.Status;
                _context.Managers.Update(manager);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Remove(Manager manager)
        {
            try
            {
                var _context = new ParkingManagementContext();
                _context.Managers.Remove(manager);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
