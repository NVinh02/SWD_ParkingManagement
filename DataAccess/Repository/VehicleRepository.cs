using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        public Task AddVehicle(Vehicle vehicle)
            => VehicleDAO.Instance.Add(vehicle);

        public Task<Vehicle> GetVehicleByVehicleId(string id)
            => VehicleDAO.Instance.GetVehicleByVId(id);

        public Task<IEnumerable<Vehicle>> GetVehicles()
            => VehicleDAO.Instance.GetVehicles();

        public Task<IEnumerable<Vehicle>> GetVehiclesByUserId(string id)
            => VehicleDAO.Instance.GetVehiclesByUserId(id);

        public Task UpdateVehicle(Vehicle vehicle)
            => VehicleDAO.Instance.Update(vehicle);
    }
}
