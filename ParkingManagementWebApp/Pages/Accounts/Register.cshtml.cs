using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.BusinessModels;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingManagementWebApp.Constants;
using ParkingManagementWebApp.Ultilities;

namespace ParkingManagementWebApp.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        private readonly ParkingManagementContext _context;
        private IUserRepository userRepository = new UserRepository();
        [BindProperty]
        public RegistrationForm RegistrationForm { get; set; }

        public string Error { get; set; }

        public RegisterModel(ParkingManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return (SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication) == null)
                    ? Page()
                    : RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!RegistrationForm.ConfirmPassword.Equals(RegistrationForm.Password))
            {
                Error = "Confrim Password has to be the same as Password";
                return Page();
            }
            else
            {
                User user = new User()
                {
                    Ccid = RegistrationForm.UserName,
                    Fullname = RegistrationForm.Fullname,
                    Password = RegistrationForm.Password,
                    Status = true,
                    IsResident = RegistrationForm.IsResident
                };
                await userRepository.AddUser(user);

                User login = await userRepository.Login(user.Ccid, user.Password);
                UserInformation userInformation = null;
                if (login != null)
                {
                    userInformation = new()
                    {
                        UserId = user.Ccid,
                        Fullname = user.Fullname,
                        IsStaff = false,
                        IsResident = user.IsResident,
                        Status = user.Status
                    };
                }
                if (userInformation == null)
                {
                    Error = "Error Register";
                    return Page();
                }
                SessionHelper.SaveToSession<UserInformation>(HttpContext.Session, userInformation, SessionValue.Authentication);
            }
            return RedirectToPage("/Index");
        }
    }
}
