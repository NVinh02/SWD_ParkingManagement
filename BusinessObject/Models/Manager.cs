using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Manager
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} between {2} to {1}", MinimumLength = 5)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} between {2} to {1}", MinimumLength = 3)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public bool Status { get; set; }

        [Display(Name = "Manager Type")]
        [Required(ErrorMessage = "{0} is required")]
        public string Type { get; set; }
    }
}
