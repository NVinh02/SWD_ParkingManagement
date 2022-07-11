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
    public class LoginModel : PageModel
    {
        private readonly ParkingManagementContext _context;
        private readonly IManagerRepository managerRepository = new ManagerRepository();
        private readonly IUserRepository userRepository = new UserRepository();

        [BindProperty]
        public LoginForm LoginForm { get; set; }

        public LoginModel(ParkingManagementContext context)
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

            UserInformation userInfo = null;
            Manager manager = await managerRepository.Login(LoginForm.UserName, LoginForm.Password);
            if (manager != null)
            {
                userInfo = new()
                {
                    UserId = manager.Username,
                    Fullname = manager.Username,
                    IsStaff = true,
                    Status = manager.Status,
                    IsResident = true,
                    ManagerType = manager.Type
                };
            }
            else
            {
                User user = await userRepository.Login(LoginForm.UserName, LoginForm.Password);
                if (user != null)
                {
                    userInfo = new()
                    {
                        UserId = user.Ccid,
                        Fullname = user.Fullname,
                        IsStaff = false,
                        Status = user.Status,
                        IsResident = user.IsResident,
                        ManagerType = ""
                    };
                }
            }

            if (userInfo == null)
            {
                return Page();
            }
            SessionHelper.SaveToSession<UserInformation>(HttpContext.Session, userInfo, SessionValue.Authentication);
            return RedirectToPage("/Index");
        }
    }
}
