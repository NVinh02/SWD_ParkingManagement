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
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;

        public IndexModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public IList<ParkingInfo> ParkingInfo { get;set; }
        public string Status { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
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

            switch (id)
            {
                case 0:
                    Status = "All";
                    break;
                case 1:
                    Status = "Active";
                    break;
                case 2:
                    Status = "Booked";
                    break;
                case 3:
                    Status = "Finished";
                    break;
                case 4:
                    Status = "Cancelled";
                    break;
                default:
                    Status = "All";
                    break;
            }

            if (Status.Equals("All"))
            {
                ParkingInfo = await _context.ParkingInfos
                .Include(p => p.Cc)
                .Include(p => p.Pricing)
                .Include(p => p.Slot)
                .Include(p => p.Vehicle).ToListAsync();
            }
            else
            {
            ParkingInfo = await _context.ParkingInfos
                .Include(p => p.Cc)
                .Include(p => p.Pricing)
                .Include(p => p.Slot)
                .Include(p => p.Vehicle).Where(p => p.Status.Equals(Status)).ToListAsync();
            }

            return Page();
        }
    }
}
