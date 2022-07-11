using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ParkingInfoRepository : IParkingInfoRepository
    {
        public Task Add(ParkingInfo parkingInfo)
            => ParkingInfoDAO.Instance.Add(parkingInfo);

        public Task ChangeHaveVehicle(ParkingInfo parkingInfo)
            => ParkingInfoDAO.Instance.ChangeHaveVehicle(parkingInfo);

        public bool CheckVehicleBookingStatus(string vehicleId)
            => ParkingInfoDAO.Instance.CheckVehicleBookingStatus(vehicleId);

        public Task<ParkingInfo> GetParkingInfoById(int id)
            => ParkingInfoDAO.Instance.GetParkingInfoById(id);

        public Task<IEnumerable<ParkingInfo>> GetParkingInfos(string status)
            => ParkingInfoDAO.Instance.GetParkingInfos(status);

        public Task Update(ParkingInfo parkingInfo, string action)
            => ParkingInfoDAO.Instance.Update(parkingInfo, action);
    }
}
