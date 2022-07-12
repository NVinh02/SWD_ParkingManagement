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

namespace ParkingManagementWebApp.Pages.Pricings
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IPricingRepository pricingRepository = new PricingRepository();

        public IndexModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public IList<Pricing> Pricing { get;set; }

        public async Task<IActionResult> OnGetAsync()
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
            //Pricing = await _context.Pricings.ToListAsync();
            Pricing = (IList<Pricing>)await pricingRepository.GetPricings();

            return Page();
        }
    }
}
