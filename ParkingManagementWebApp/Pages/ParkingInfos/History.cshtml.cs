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

namespace ParkingManagementWebApp.Pages.ParkingInfos
{
    public class HistoryModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;

        public HistoryModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public IList<ParkingInfo> ParkingInfo { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if (userInformation.IsStaff)
            {
                return RedirectToPage("/Index");
            }

            ParkingInfo = await _context.ParkingInfos
                .Include(p => p.Cc)
                .Include(p => p.Pricing)
                .Include(p => p.Slot)
                .Include(p => p.Vehicle).Where(p => p.Ccid == userInformation.UserId).ToListAsync();

            return Page();
        }
    }
}
