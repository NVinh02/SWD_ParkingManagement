using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<User> Login(string username, string password)
        {
            User user = null;
            try
            {
                var _context = new ParkingManagementContext();
                user = _context.Users.SingleOrDefault
                    (u => u.Ccid == username && u.Password == password && u.Status == true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            List<User> users = null;
            try
            {
                var _context = new ParkingManagementContext();
                users = await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public async Task<User> GetUserById(string ccid)
        {
            User user = null;
            try
            {
                var _context = new ParkingManagementContext();
                user = await _context.Users.FirstOrDefaultAsync(u => u.Ccid == ccid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task Add(User user)
        {
            try
            {
                var _context = new ParkingManagementContext();
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUserStatus(User user)
        {
            try
            {
                var _context = new ParkingManagementContext();
                user.Status = !user.Status;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(User user)
        {
            try
            {
                var _context = new ParkingManagementContext();
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
