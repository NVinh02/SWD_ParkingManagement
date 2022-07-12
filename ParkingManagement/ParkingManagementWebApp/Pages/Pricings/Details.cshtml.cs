using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using BusinessObject.BusinessModels;
using ParkingManagementWebApp.Ultilities;
using ParkingManagementWebApp.Constants;

namespace ParkingManagementWebApp.Pages.Pricings
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IPricingRepository pricingRepository = new PricingRepository();

        public DetailsModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public Pricing Pricing { get; set; }

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
            if (id == null)
            {
                return NotFound();
            }

            //Pricing = await _context.Pricings.FirstOrDefaultAsync(m => m.PricingId == id);
            Pricing = await pricingRepository.GetPricingById(id);

            if (Pricing == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
