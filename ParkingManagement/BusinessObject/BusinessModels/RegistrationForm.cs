using BusinessObject.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.BusinessModels
{
    public class RegistrationForm
    {
        [Display(Name = "CCID")]
        [Required(ErrorMessage = "{0} is required. ")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Numbers only.")]
        [StringLength(12, ErrorMessage = "{0} has to be between {2}-{1} number" , MinimumLength = 9)]
        [UniqueCCID]
        public string UserName { get; set; }

        [Display(Name = "Fullname")]
        [RegularExpression(@"^(\b[A-Z]{1}\w*\s*)+$", ErrorMessage = "Each letter of {0} has to start with a capitalized letter")]
        [StringLength(50, ErrorMessage = "{0} has to be between {2} to {1}", MinimumLength = 10)]
        [Required(ErrorMessage = "{0} is required. ")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "{0} is required. ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required. ")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} is required. ")]
        [Display(Name = "Are you a Resident")]
        public bool IsResident { get; set; }
    }
}
