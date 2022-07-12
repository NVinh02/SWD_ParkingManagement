using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.BusinessModels
{
    public class LoginForm
    {
        [Display(Name = "Username or CCID")]
        [Required(ErrorMessage = "{0} is required. ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required. ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
