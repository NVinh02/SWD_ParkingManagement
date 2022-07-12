using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessObject.BusinessModels;
using ParkingManagementWebApp.Ultilities;
using ParkingManagementWebApp.Constants;
using DataAccess.Repository;

namespace ParkingManagementWebApp.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IUserRepository userRepository = new UserRepository();

        public DeleteModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }
        public bool Status { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if (!userInformation.IsStaff)
            {
                return RedirectToPage("/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            //User = await _context.Users.FirstOrDefaultAsync(m => m.Ccid == id);
            User = await userRepository.GetUserById(id);
            if (User.Status == true)
            {
                Status = true;
            }
            else
            {
                Status = false;
            }

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if (!userInformation.IsStaff)
            {
                return RedirectToPage("/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            //User = await _context.Users.FindAsync(id);
            User = await userRepository.GetUserById(id);

            if (User != null)
            {
                //_context.Users.Remove(User);
                //await _context.SaveChangesAsync();
                await userRepository.UpdateUserStatus(User);
            }

            return RedirectToPage("./Index");
        }
    }
}
