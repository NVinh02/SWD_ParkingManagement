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
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly ISlotRepository slotRepository = new SlotRepository();

        public IndexModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public IList<Slot> Slot { get;set; }
        public int? Floor { get; set; }

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

            Floor = id;
            Slot = (IList<Slot>)await slotRepository.GetSlotsByFloor(id);

            return Page();
        }
    }
}
