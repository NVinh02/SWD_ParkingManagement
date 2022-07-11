using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject.Models
{
    public partial class User
    {
        public User()
        {
            ParkingInfos = new HashSet<ParkingInfo>();
            Vehicles = new HashSet<Vehicle>();
        }

        [Display(Name = "CCID")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Numbers only.")]
        [StringLength(12, ErrorMessage = "{0} has to be between {2}-{1} number", MinimumLength = 9)]
        public string Ccid { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} between {2} to {1}", MinimumLength = 3)]
        public string Password { get; set; }

        [Display(Name = "Fullname")]
        [RegularExpression(@"^(\b[A-Z]{1}\w*\s*)+$", ErrorMessage = "Each letter of {0} has to start with a capitalized letter")]
        [StringLength(50, ErrorMessage = "{0} has to be between {2} to {1}", MinimumLength = 5)]
        public string Fullname { get; set; }

        [Display(Name = "Active Status")]
        public bool Status { get; set; }

        [Display(Name = "Resident Status")]
        public bool IsResident { get; set; }

        public virtual ICollection<ParkingInfo> ParkingInfos { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
