using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IVehicleRepository
    {
        public Task<IEnumerable<Vehicle>> GetVehicles();
        public Task<IEnumerable<Vehicle>> GetVehiclesByUserId(string id);
        public Task<Vehicle> GetVehicleByVehicleId(string id);
        public Task AddVehicle(Vehicle vehicle);
        public Task UpdateVehicle(Vehicle vehicle);
    }
}
