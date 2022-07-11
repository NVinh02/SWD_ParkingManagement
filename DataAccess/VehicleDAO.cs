using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VehicleDAO
    {
        private static VehicleDAO instance = null;
        private static readonly object instanceLock = new object();
        private VehicleDAO() { }
        public static VehicleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VehicleDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            List<Vehicle> vehicles;
            try
            {
                var _context = new ParkingManagementContext();
                vehicles = await _context.Vehicles
                                .Include(v => v.Cc).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vehicles;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByUserId(string id)
        {
            List<Vehicle> vehicles;
            try
            {
                var _context = new ParkingManagementContext();
                vehicles = await _context.Vehicles
                                .Include(v => v.Cc)
                                .Where(v => v.Ccid == id)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleByVId(string vId)
        {
            Vehicle vehicle = null;
            try
            {
                var _context = new ParkingManagementContext();
                vehicle = await _context.Vehicles
                    .Include(v => v.Cc).FirstOrDefaultAsync(m => m.VehicleId == vId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vehicle;
        }


        public async Task Add(Vehicle vehicle)
        {
            try
            {
                var _context = new ParkingManagementContext();
                _context.Vehicles.Add(vehicle);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Vehicle vehicle)
        {
            try
            {
                var _context = new ParkingManagementContext();
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
