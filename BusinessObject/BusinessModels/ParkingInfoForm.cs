using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.BusinessModels
{
    public class ParkingInfoForm
    {
        public string VehicleId { get; set; }
        public int SlotId { get; set; }
        public int PricingId { get; set; }
        public int ParkType { get; set; }
        public int Month { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public bool IsMonthly { get; set; }
        public bool HaveVehicle { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
