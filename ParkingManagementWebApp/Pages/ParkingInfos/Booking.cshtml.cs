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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParkingManagementWebApp.Pages.ParkingInfos
{
    public class BookingModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly ISlotRepository slotRepository = new SlotRepository();
        private readonly IVehicleRepository vehicleRepository = new VehicleRepository();
        private readonly IParkingInfoRepository parkingInfoRepository = new ParkingInfoRepository();
        public BookingModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ParkingInfoForm ParkingInfoForm { get; set; }
        [BindProperty] public DateTime? StartAdvanceDate { get; set; }
        [BindProperty] public DateTime? EndAdvanceDate { get; set; }
        //[BindProperty] public DateTime? EndImmediatelyDate { get; set; }
        [BindProperty] public DateTime? StartAdvanceDateMonthly { get; set; }
        [BindProperty] public int AdvanceMonthly { get; set; }
        [BindProperty] public int ImmediatelyMonthly { get; set; }
        [BindProperty] public int ParkingType { get; set; }
        [BindProperty] public string ParkingVehicleId { get; set; }
        List<Vehicle> tempVehicle = new List<Vehicle>();
        List<Vehicle> availableVehicle = new List<Vehicle>();
        /*        public string StartAdvanceDateError { get; set; }
                public string EndAdvanceDateError { get; set; }
                public string StartAdvanceDateMonthlyError { get; set; }*/
        public int NumberOfVehicles { get; set; }
        public string NumberOfVehicleError { get; set; }

        public IList<Slot> Slot { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            NumberOfVehicles = 0;
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if (userInformation.IsStaff)
            {
                return RedirectToPage("/Index");
            }

            //tempVehicle = _context.Vehicles.Include(v => v.Cc).Where(v => v.Ccid == userInformation.UserId).ToList();
            tempVehicle = (List<Vehicle>)await vehicleRepository.GetVehiclesByUserId(userInformation.UserId);

            foreach(Vehicle v in tempVehicle)
            {
                if(!(_context.ParkingInfos.Any(p => p.VehicleId.Equals(v.VehicleId)
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active")))))
                {
                    availableVehicle.Add(v);
                }
            }

            List<Vehicle> vehiclesList = _context.Vehicles.Where(v => v.Ccid == userInformation.UserId).ToList();
            foreach (Vehicle v in vehiclesList)
            {
                if (!_context.ParkingInfos.Any(pk => pk.VehicleId == v.VehicleId && (pk.Status == "Booked" || pk.Status == "Active")))
                {
                    NumberOfVehicles += 1;
                }
            }

            if(NumberOfVehicles <= 0)
            {
                NumberOfVehicleError = "You already booked with all your current Vehicles!";
            }

            ViewData["VehicleId"] = new SelectList(availableVehicle, "VehicleId", "VehicleId");

            //Slot = await _context.Slots.ToListAsync();
            //Slot = (IList<Slot>)await slotRepository.GetSlots();

            return Page();
        }

        public IActionResult OnPost()
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if (userInformation.IsStaff)
            {
                return RedirectToPage("/Index");
            }
            if (!ModelState.IsValid)
            {
                ViewData["VehicleId"] = new SelectList(availableVehicle, "VehicleId", "VehicleId");
                return Page();
            }

            ParkingInfoForm parkingForm = null;

            if (ParkingType == 0) 
            {
                return Page();
            }
            else if (ParkingType == 1)
            {
                parkingForm = new()
                {
                    SlotId = 0,
                    VehicleId = ParkingVehicleId,
                    PricingId = 0,
                    ParkType = ParkingType,
                    Month = 0,
                    IsMonthly = false,
                    CheckInTime = DateTime.Now,
                    CheckOutTime = null,
                    HaveVehicle = true,
                    Status = "Active",
                    TotalPrice = 0
                };
            }
            else if (ParkingType == 2)
            {
                parkingForm = new()
                {
                    SlotId = 0,
                    VehicleId = ParkingVehicleId,
                    PricingId = 0,
                    ParkType = ParkingType,
                    Month = ImmediatelyMonthly,
                    IsMonthly = true,
                    CheckInTime = DateTime.Now,
                    CheckOutTime = DateTime.Now.AddMonths(ImmediatelyMonthly),
                    HaveVehicle = true,
                    Status = "Active",
                    TotalPrice = 0
                };
            }
            else if (ParkingType == 3)
            {
                if (((DateTime)StartAdvanceDate).CompareTo(DateTime.Now) <= 0)
                {
                    TempData["ErrorMessage"] = "Start Date must be in the future";
                    ViewData["VehicleId"] = new SelectList(availableVehicle, "VehicleId", "VehicleId");
                    return Page();
                }
                else if (((DateTime)EndAdvanceDate).CompareTo((DateTime)StartAdvanceDate) <= 0)
                {
                    TempData["ErrorMessage"] = "End Date must be after Start Date";
                    ViewData["VehicleId"] = new SelectList(availableVehicle, "VehicleId", "VehicleId");
                    return Page();
                }

                parkingForm = new()
                {
                    SlotId = 0,
                    VehicleId = ParkingVehicleId,
                    PricingId = 0,
                    ParkType = ParkingType,
                    Month = 0,
                    IsMonthly = false,
                    CheckInTime = (DateTime)StartAdvanceDate,
                    CheckOutTime = EndAdvanceDate,
                    HaveVehicle = false,
                    Status = "Booked",
                    TotalPrice = 0
                };
            }
            else if (ParkingType == 4)
            {
                if (((DateTime)StartAdvanceDateMonthly).CompareTo(DateTime.Now) <= 0)
                {
                    TempData["ErrorMessage"] = "Start Date must be in the future";
                    ViewData["VehicleId"] = new SelectList(availableVehicle, "VehicleId", "VehicleId");
                    return Page();
                }

                parkingForm = new()
                {
                    SlotId = 0,
                    VehicleId = ParkingVehicleId,
                    PricingId = 0,
                    ParkType = ParkingType,
                    Month = AdvanceMonthly,
                    IsMonthly = true,
                    CheckInTime = (DateTime)StartAdvanceDateMonthly,
                    CheckOutTime = ((DateTime)StartAdvanceDateMonthly).AddMonths(AdvanceMonthly),
                    HaveVehicle = false,
                    Status = "Booked",
                    TotalPrice = 0
                };
            }

            SessionHelper.SaveToSession<ParkingInfoForm>(HttpContext.Session, parkingForm, SessionValue.ParkingInfoForm);
            return RedirectToPage("/ParkingInfos/ChooseSlot");
        }
    }
}
