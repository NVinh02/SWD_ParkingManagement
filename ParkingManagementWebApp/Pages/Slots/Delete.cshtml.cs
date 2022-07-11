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

namespace ParkingManagementWebApp.Pages.Slots
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly ISlotRepository slotRepository = new SlotRepository();

        public DeleteModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Slot Slot { get; set; }
        public bool IsActive { get; set; }
        public int Floor { get; set; }
        public IList<ParkingInfo> ParkingInfo { get; set; }

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

            //Slot = await _context.Slots.FirstOrDefaultAsync(m => m.SlotId == id);
            Slot = await slotRepository.GetSlotById(id);
            ParkingInfo = null;
            Floor = Slot.Floor;
            if(Slot.IsActive == true)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }

            if (Slot == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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
            Slot = await slotRepository.GetSlotById(id);
            Floor = Slot.Floor;
            
            ParkingInfo = await _context.ParkingInfos
                .Include(p => p.Cc)
                .Include(p => p.Pricing)
                .Include(p => p.Slot)
                .Include(p => p.Vehicle).Where(p => p.SlotId == Slot.SlotId
                                                    && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToListAsync();

            if (ParkingInfo.Count > 0)
            {
                if (Slot.IsActive == true)
                {
                    IsActive = true;
                }
                else
                {
                    IsActive = false;
                }
                return Page();
            }

            if (Slot != null)
            {
                await slotRepository.ChangeSlotActiveStatus(Slot);
            }

            return RedirectToPage("./Index", new { id = Slot.Floor });
        }
    }
}
