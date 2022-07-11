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
using ParkingManagementWebApp.Constants;
using ParkingManagementWebApp.Ultilities;

namespace ParkingManagementWebApp.Pages.Vehicles
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IVehicleRepository vehicleRepository = new VehicleRepository();

        public DetailsModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public Vehicle Vehicle { get; set; }
        public bool IsStaff { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            IsStaff = false;
            if(userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            
            if(userInformation.IsStaff == true)
            {
                IsStaff = true;
            }

            if (id == null)
            {
                return NotFound();
            }

            //Vehicle = await _context.Vehicles
            //    .Include(v => v.Cc).FirstOrDefaultAsync(m => m.VehicleId == id);
            Vehicle = await vehicleRepository.GetVehicleByVehicleId(id);

            if (Vehicle == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
