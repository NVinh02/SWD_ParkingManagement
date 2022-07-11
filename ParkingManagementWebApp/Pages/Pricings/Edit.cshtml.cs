using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using BusinessObject.BusinessModels;
using ParkingManagementWebApp.Ultilities;
using ParkingManagementWebApp.Constants;

namespace ParkingManagementWebApp.Pages.Pricings
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IPricingRepository pricingRepository = new PricingRepository();

        public EditModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pricing Pricing { get; set; }
        public string ValueError { get; set; }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("Accounts/Login");
            }
            if (!userInformation.IsStaff)
            {
                return RedirectToPage("Home/Index");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(Pricing.DurationType.Equals("Temporary") && Pricing.VehicleType.Equals("Bike"))
            {
                if (Pricing.Value > _context.Pricings.FirstOrDefault(p => p.DurationType.Equals("Monthly") && p.VehicleType.Equals("Bike")).Value)
                {
                    ValueError = "Temporary Value cant be bigger than Monthly Value " + _context.Pricings.FirstOrDefault(p => p.DurationType.Equals("Monthly") && p.VehicleType.Equals("Bike")).Value;
                    return Page();
                }
            }
            else if (Pricing.DurationType.Equals("Monthly") && Pricing.VehicleType.Equals("Bike"))
            {
                if (Pricing.Value < _context.Pricings.FirstOrDefault(p => p.DurationType.Equals("Temporary") && p.VehicleType.Equals("Bike")).Value)
                {
                    ValueError = "Monthly Value cant be smaller than Temporary Value " + _context.Pricings.FirstOrDefault(p => p.DurationType.Equals("Temporary") && p.VehicleType.Equals("Bike")).Value;
                    return Page();
                }
            }
            else if (Pricing.DurationType.Equals("Temporary") && Pricing.VehicleType.Equals("Car"))
            {
                if (Pricing.Value > _context.Pricings.FirstOrDefault(p => p.DurationType.Equals("Monthly") && p.VehicleType.Equals("Car")).Value)
                {
                    ValueError = "Temporary Value cant be bigger than Monthly Value " + _context.Pricings.FirstOrDefault(p => p.DurationType.Equals("Monthly") && p.VehicleType.Equals("Car")).Value;
                    return Page();
                }
            }
            else if (Pricing.DurationType.Equals("Monthly") && Pricing.VehicleType.Equals("Car"))
            {
                if (Pricing.Value < _context.Pricings.FirstOrDefault(p => p.DurationType.Equals("Temporary") && p.VehicleType.Equals("Car")).Value)
                {
                    ValueError = "Monthly Value cant be smaller than Temporary Value " + _context.Pricings.FirstOrDefault(p => p.DurationType.Equals("Temporary") && p.VehicleType.Equals("Car")).Value;
                    return Page();
                }
            }

            //_context.Attach(Pricing).State = EntityState.Modified;
            await pricingRepository.UpdatePricing(Pricing);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PricingExists(Pricing.PricingId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PricingExists(int id)
        {
            return _context.Pricings.Any(e => e.PricingId == id);
        }
    }
}
