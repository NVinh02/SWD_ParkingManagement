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
using DataAccess.Repository;
using ParkingManagementWebApp.Constants;

namespace ParkingManagementWebApp.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IVehicleRepository vehicleRepository = new VehicleRepository();
        private readonly IParkingInfoRepository parkingInfoRepository = new ParkingInfoRepository();

        public IndexModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get;set; }
        public bool IsStaff { get; set; }
        public bool CheckVehicle(string vehicleId)
        {
            bool check = false;
            check = parkingInfoRepository.CheckVehicleBookingStatus(vehicleId);
            return check;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IsStaff = false;
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if(userInformation.IsStaff == true)
            {
                //user can onli view their own vehicles list 
                Vehicle = (IList<Vehicle>)await vehicleRepository.GetVehicles();
                IsStaff = true;
            }
            else
            {
                //only manager acc can view all the vehicles
                Vehicle = (IList<Vehicle>)await vehicleRepository.GetVehiclesByUserId(userInformation.UserId);
                IsStaff = false;
            }
            //Vehicle = await _context.Vehicles
            //    .Include(v => v.Cc).ToListAsync();
            return Page();
        }
    }
}
