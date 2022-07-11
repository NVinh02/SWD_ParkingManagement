using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ParkingInfoDAO
    {
        private static ParkingInfoDAO instance = null;
        private static readonly object instanceLock = new object();
        private ParkingInfoDAO() { }
        public static ParkingInfoDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ParkingInfoDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<IEnumerable<ParkingInfo>> GetParkingInfos(string status)
        {
            List<ParkingInfo> parkingInfos;
            try
            {
                var _context = new ParkingManagementContext();
                if (status.Equals("All"))
                {
                    parkingInfos = await _context.ParkingInfos
                    .Include(p => p.Cc)
                    .Include(p => p.Pricing)
                    .Include(p => p.Slot)
                    .Include(p => p.Vehicle).ToListAsync();
                }
                else
                {
                    parkingInfos = await _context.ParkingInfos
                        .Include(p => p.Cc)
                        .Include(p => p.Pricing)
                        .Include(p => p.Slot)
                        .Include(p => p.Vehicle).Where(p => p.Status.Equals(status)).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return parkingInfos;
        }

        public async Task<ParkingInfo> GetParkingInfoById(int id)
        {
            ParkingInfo parkingInfo;
            try
            {
                var _context = new ParkingManagementContext();
                parkingInfo = await _context.ParkingInfos
                .Include(p => p.Cc)
                .Include(p => p.Pricing)
                .Include(p => p.Slot)
                .Include(p => p.Vehicle).FirstOrDefaultAsync(m => m.ParkingInfoId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return parkingInfo;
        }

        public async Task ChangeHaveVehicle(ParkingInfo parkingInfo)
        {
            try
            {
                var _context = new ParkingManagementContext();
                parkingInfo.HaveVehicle = !parkingInfo.HaveVehicle;
                _context.ParkingInfos.Update(parkingInfo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Add(ParkingInfo parkingInfo)
        {
            try
            {
                var _context = new ParkingManagementContext();
                _context.ParkingInfos.Update(parkingInfo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(ParkingInfo parkingInfo, string action)
        {
            try
            {
                var _context = new ParkingManagementContext();

                if (action.Equals("Finished"))
                {
                    if(parkingInfo.CheckOutTime.Year < 2000)
                    {
                        parkingInfo.CheckOutTime = DateTime.Now;
                        int times = (int)((parkingInfo.CheckOutTime - parkingInfo.CheckInTime).TotalHours / 8 + 1);
                        parkingInfo.TotalPrice = times * parkingInfo.Pricing.Value;
                        parkingInfo.HaveVehicle = false;
                        parkingInfo.Status = "Finished";
                    }
                    else
                    {
                        if(parkingInfo.CheckOutTime.CompareTo(DateTime.Now) >= 0)
                        {
                            parkingInfo.CheckOutTime = DateTime.Now;
                            int times = (int)((parkingInfo.CheckOutTime - parkingInfo.CheckInTime).TotalHours / 8 + 1);
                            parkingInfo.TotalPrice = times * parkingInfo.Pricing.Value;
                            parkingInfo.HaveVehicle = false;
                            parkingInfo.Status = "Finished";
                        }
                        else
                        {
                            parkingInfo.HaveVehicle = false;
                            parkingInfo.Status = "Finished";
                        }
                    }
                }

                if (action.Equals("Active"))
                {
                    parkingInfo.Status = "Active";
                    parkingInfo.HaveVehicle = true;
                }

                if (action.Equals("Cancelled"))
                {
                    parkingInfo.Status = "Cancelled";
                    parkingInfo.HaveVehicle = false;
                    parkingInfo.TotalPrice = 0;
                }

                _context.ParkingInfos.Update(parkingInfo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CheckVehicleBookingStatus(string vehicleId)
        {
            bool check = false;
            try
            {
                var _context = new ParkingManagementContext();
                check = _context.ParkingInfos.Any(pk => pk.VehicleId == vehicleId && (pk.Status == "Booked" || pk.Status == "Active"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }
    }
}
