using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingManagementWebApp.Constants;
using ParkingManagementWebApp.Ultilities;

namespace ParkingManagementWebApp.Pages.Accounts
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            SessionHelper.RemoveFromSession(HttpContext.Session, SessionValue.Authentication);
            return RedirectToPage("/Index");
        }
    }
}
