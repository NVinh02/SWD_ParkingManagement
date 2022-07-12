using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Pricing
    {
        public Pricing()
        {
            ParkingInfos = new HashSet<ParkingInfo>();
        }

        [Required(ErrorMessage = "{0} is required")]
        public int PricingId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Duration")]
        public string DurationType { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public decimal Value { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required(ErrorMessage = "{0} is required")]
        public string VehicleType { get; set; }

        public virtual ICollection<ParkingInfo> ParkingInfos { get; set; }
    }
}
