using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.BusinessModels;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParkingManagementWebApp.Constants;
using ParkingManagementWebApp.Ultilities;

namespace ParkingManagementWebApp.Pages.ParkingInfos
{
    public class ChooseSlotModel : PageModel
    {
        private readonly ParkingManagementContext _context;
        private readonly ISlotRepository slotRepository = new SlotRepository();
        private readonly IVehicleRepository vehicleRepository = new VehicleRepository();
        private readonly IParkingInfoRepository parkingInfoRepository = new ParkingInfoRepository();
        public ChooseSlotModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }
        public IList<Slot> Slot { get; set; }
        public IList<SlotForm> SlotForm { get; set; }
        [BindProperty]
        public ChooseSlotForm ChosenSlot { get; set; }
        public async Task<IActionResult> OnGetAsync()
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

            ParkingInfoForm parkingInfoForm = SessionHelper.GetFromSession<ParkingInfoForm>(HttpContext.Session, SessionValue.ParkingInfoForm);
            Vehicle vehicle = (Vehicle)await vehicleRepository.GetVehicleByVehicleId(parkingInfoForm.VehicleId);

            switch (parkingInfoForm.ParkType)
            {
                case 1:
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType1(vehicle.Type);
                    break;
                case 2:
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType2(vehicle.Type, parkingInfoForm.CheckInTime, (DateTime)parkingInfoForm.CheckOutTime);
                    break;
                case 3:
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType3(vehicle.Type, parkingInfoForm.CheckInTime, (DateTime)parkingInfoForm.CheckOutTime);
                    break;
                case 4:
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType4(vehicle.Type, parkingInfoForm.CheckInTime, (DateTime)parkingInfoForm.CheckOutTime);
                    break;
                default:
                    return RedirectToPage("/ParkingInfos/Booking");
            }

            //Slot = await _context.Slots.ToListAsync();
            //Slot = (IList<Slot>)await slotRepository.GetSlots();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
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

            ParkingInfoForm parkingInfoForm = SessionHelper.GetFromSession<ParkingInfoForm>(HttpContext.Session, SessionValue.ParkingInfoForm);
            Vehicle vehicle = (Vehicle)await vehicleRepository.GetVehicleByVehicleId(parkingInfoForm.VehicleId);

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please choose an available slot!";
                return Page();
            }
            if (ChosenSlot.ChosenSlotId == 0 && parkingInfoForm.ParkType == 1)
            {
                SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType1(vehicle.Type);
                TempData["ErrorMessage"] = "Please choose an available slot!";
                return Page();
            }
            if (ChosenSlot.ChosenSlotId == 0 && parkingInfoForm.ParkType == 2)
            {
                SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType2(vehicle.Type, parkingInfoForm.CheckInTime, (DateTime)parkingInfoForm.CheckOutTime);
                TempData["ErrorMessage"] = "Please choose an available slot!";
                return Page();
            }
            if (ChosenSlot.ChosenSlotId == 0 && parkingInfoForm.ParkType == 3)
            {
                SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType3(vehicle.Type, parkingInfoForm.CheckInTime, (DateTime)parkingInfoForm.CheckOutTime);
                TempData["ErrorMessage"] = "Please choose an available slot!";
                return Page();
            }
            if (ChosenSlot.ChosenSlotId == 0 && parkingInfoForm.ParkType == 4)
            {
                SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType4(vehicle.Type, parkingInfoForm.CheckInTime, (DateTime)parkingInfoForm.CheckOutTime);
                TempData["ErrorMessage"] = "Please choose an available slot!";
                return Page();
            }

            parkingInfoForm.SlotId = ChosenSlot.ChosenSlotId;
            SessionHelper.SaveToSession<ParkingInfoForm>(HttpContext.Session, parkingInfoForm, SessionValue.ParkingInfoForm);
            return RedirectToPage("/ParkingInfos/Create");

            //Slot = await _context.Slots.ToListAsync();
            //Slot = (IList<Slot>)await slotRepository.GetSlots();
        }
    }
}
