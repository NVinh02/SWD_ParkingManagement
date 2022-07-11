using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IParkingInfoRepository
    {
        public Task<IEnumerable<ParkingInfo>> GetParkingInfos(string status);
        public Task<ParkingInfo> GetParkingInfoById(int id);
        public Task ChangeHaveVehicle(ParkingInfo parkingInfo);
        public Task Add(ParkingInfo parkingInfo);
        public Task Update(ParkingInfo parkingInfo, string action);
        public bool CheckVehicleBookingStatus(string vehicleId);
    }
}
