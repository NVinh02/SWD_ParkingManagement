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
using System.Collections;
using BusinessObject.BusinessModels;
using ParkingManagementWebApp.Ultilities;
using ParkingManagementWebApp.Constants;

namespace ParkingManagementWebApp.Pages.Vehicles
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IVehicleRepository vehicleRepository = new VehicleRepository();
        private readonly ArrayList CarType = new ArrayList();

        public EditModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
            CarType.Add("Car");
            CarType.Add("Bike");
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if (userInformation.IsStaff == true)
            {
                return RedirectToPage("/Vehicles/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Type"] = new SelectList(CarType);

            //Vehicle = await _context.Vehicles
            //    .Include(v => v.Cc).FirstOrDefaultAsync(m => m.VehicleId == id);
            Vehicle = await vehicleRepository.GetVehicleByVehicleId(id);

            if (Vehicle == null)
            {
                return NotFound();
            }
           ViewData["Ccid"] = new SelectList(_context.Users, "Ccid", "Ccid");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Vehicle).State = EntityState.Modified;
            await vehicleRepository.UpdateVehicle(Vehicle);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(Vehicle.VehicleId))
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

        private bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
