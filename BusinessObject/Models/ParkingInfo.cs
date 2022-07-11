using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject.Models
{
    public partial class ParkingInfo
    {
        [Display(Name = "Parking Id")]
        public int ParkingInfoId { get; set; }

        [Display(Name = "User CCID")]
        public string Ccid { get; set; }

        [Display(Name = "Vehicle Id")]
        public string VehicleId { get; set; }

        [Display(Name = "Slot Id")]
        public int? SlotId { get; set; }

        [Display(Name = "Pricing Id")]
        public int? PricingId { get; set; }

        [Display(Name = "Start Time")]
        public DateTime CheckInTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime CheckOutTime { get; set; }

        [Display(Name = "Monthly")]
        public bool IsMonthly { get; set; }

        [Display(Name = "Have Vehicle")]
        public bool HaveVehicle { get; set; }
        public string Status { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public virtual User Cc { get; set; }
        public virtual Pricing Pricing { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
