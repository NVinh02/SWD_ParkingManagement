using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            ParkingInfos = new HashSet<ParkingInfo>();
        }

        [Display(Name = "Vehicle ID")]
        [StringLength(12, ErrorMessage = "{0} has to be between {2} to {1}", MinimumLength = 9)]
        [Required(ErrorMessage = "{0} is required")]
        public string VehicleId { get; set; }

        [Display(Name = "Vehicle Type")]
        [Required(ErrorMessage = "{0} is required")]
        public string Type { get; set; }

        [Display(Name = "CCID")]
        public string Ccid { get; set; }

        public virtual User Cc { get; set; }
        public virtual ICollection<ParkingInfo> ParkingInfos { get; set; }
    }
}
