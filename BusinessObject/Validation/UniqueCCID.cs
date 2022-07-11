using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Validation
{
    public class UniqueCCID : ValidationAttribute
    {
        public UniqueCCID()
        {
            ErrorMessage = " is already taken.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace((string)value))
            {
                return new ValidationResult($"{validationContext.DisplayName} cannot be empty. ");
            }
            ParkingManagementContext context = (ParkingManagementContext)validationContext
                .GetService(typeof(ParkingManagementContext));
            bool isExisted = context.Users
                .Where(user => user.Ccid == (string)value).Any();
            return !isExisted ? ValidationResult.Success : new ValidationResult($"{validationContext.DisplayName}{ErrorMessage}");
        }
    }
}
