using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using BusinessObject.BusinessModels;
using ParkingManagementWebApp.Ultilities;
using System.Collections;
using ParkingManagementWebApp.Constants;
using DataAccess.Repository;
using BusinessObject.Validation;

namespace ParkingManagementWebApp.Pages.Vehicles
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IVehicleRepository vehicleRepository = new VehicleRepository();
        private ArrayList CarType = new ArrayList();

        public CreateModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
            CarType.Add("Car");
            CarType.Add("Bike");
        }

        public IActionResult OnGet()
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
            ViewData["Ccid"] = new SelectList(_context.Users, "Ccid", "Ccid");
            ViewData["Type"] = new SelectList(CarType);
            return Page();
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; }
        public string NumberPlateError { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Vehicle.Type.Equals("Car"))
            {
                if (RegexCheck.CheckStringMatchWithRegex(Vehicle.VehicleId, RegexCheck.VehicleCarPlateNumber) == false)
                {
                    NumberPlateError = "Wrong Plate Number Format (Correct Example: 30V-12345)";
                    ViewData["Type"] = new SelectList(CarType);
                    return Page();
                }
            }
            else
            {
                if (RegexCheck.CheckStringMatchWithRegex(Vehicle.VehicleId, RegexCheck.VehicleBikePlateNumber) == false)
                {
                    NumberPlateError = "Wrong Plate Number Format (Correct Example: 59-V1 12345, 59-V1 1234)";
                    ViewData["Type"] = new SelectList(CarType);
                    return Page();
                }
            }

            if (_context.Vehicles.Any(v => v.VehicleId == Vehicle.VehicleId))
            {
                NumberPlateError = "Number Plate is taken";
                ViewData["Type"] = new SelectList(CarType);
                return Page();
            }

            Vehicle.Ccid = userInformation.UserId;
            //_context.Vehicles.Add(Vehicle);
            //await _context.SaveChangesAsync();
            await vehicleRepository.AddVehicle(Vehicle);
            return RedirectToPage("./Index");
        }
    }
}
