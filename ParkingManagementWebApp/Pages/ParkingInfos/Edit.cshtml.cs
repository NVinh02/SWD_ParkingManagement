using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace ParkingManagementWebApp.Pages.ParkingInfos
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;

        public EditModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParkingInfo ParkingInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParkingInfo = await _context.ParkingInfos
                .Include(p => p.Cc)
                .Include(p => p.Pricing)
                .Include(p => p.Slot)
                .Include(p => p.Vehicle).FirstOrDefaultAsync(m => m.ParkingInfoId == id);

            if (ParkingInfo == null)
            {
                return NotFound();
            }
           ViewData["Ccid"] = new SelectList(_context.Users, "Ccid", "Ccid");
           ViewData["PricingId"] = new SelectList(_context.Pricings, "PricingId", "DurationType");
           ViewData["SlotId"] = new SelectList(_context.Slots, "SlotId", "Position");
           ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "VehicleId");
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

            _context.Attach(ParkingInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingInfoExists(ParkingInfo.ParkingInfoId))
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

        private bool ParkingInfoExists(int id)
        {
            return _context.ParkingInfos.Any(e => e.ParkingInfoId == id);
        }
    }
}
