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
    public class ChangeSlotModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IParkingInfoRepository parkingInfoRepository = new ParkingInfoRepository();
        private readonly IPricingRepository pricingRepository = new PricingRepository();
        private readonly IVehicleRepository vehicleRepository = new VehicleRepository();
        private readonly ISlotRepository slotRepository = new SlotRepository();
        public ChangeSlotModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public ParkingInfo ParkingInfo { get; set; }
        public IList<SlotForm> SlotForm { get; set; }
        [BindProperty] public int ParkingInfoId { get; set; }

        [BindProperty]
        public ChooseSlotForm ChosenSlot { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            ParkingInfo = await parkingInfoRepository.GetParkingInfoById((int)id);

            if (ParkingInfo == null)
            {
                return NotFound();
            }

            if (ParkingInfo.Status.Equals("Booked"))
            {
                if (ParkingInfo.IsMonthly)
                {
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType4(ParkingInfo.Vehicle.Type, DateTime.Now, (DateTime)ParkingInfo.CheckOutTime);
                }
                else
                {
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType3(ParkingInfo.Vehicle.Type, DateTime.Now, (DateTime)ParkingInfo.CheckOutTime);
                }
            }
            else if(ParkingInfo.Status.Equals("Active") && ParkingInfo.CheckInTime.CompareTo(ParkingInfo.CheckOutTime) < 0)
            {
                if (ParkingInfo.IsMonthly)
                {
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType4(ParkingInfo.Vehicle.Type, DateTime.Now, (DateTime)ParkingInfo.CheckOutTime);
                }
                else
                {
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType3(ParkingInfo.Vehicle.Type, DateTime.Now, (DateTime)ParkingInfo.CheckOutTime);
                }
            }
            else if (ParkingInfo.Status.Equals("Active") && ParkingInfo.CheckInTime.CompareTo(ParkingInfo.CheckOutTime) > 0)
            {
                SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType1(ParkingInfo.Vehicle.Type);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            if (userInformation == null)
            {
                return RedirectToPage("/Accounts/Login");
            }

            ParkingInfo = await parkingInfoRepository.GetParkingInfoById(ParkingInfoId);
            if (ParkingInfo == null)
            {
                return NotFound();
            }

            //Prepare Slot in case Invalid
            if (ParkingInfo.Status.Equals("Booked"))
            {
                if (ParkingInfo.IsMonthly)
                {
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType4(ParkingInfo.Vehicle.Type, DateTime.Now, (DateTime)ParkingInfo.CheckOutTime);
                }
                else
                {
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType3(ParkingInfo.Vehicle.Type, DateTime.Now, (DateTime)ParkingInfo.CheckOutTime);
                }
            }
            else if (ParkingInfo.Status.Equals("Active") && ParkingInfo.CheckInTime.CompareTo(ParkingInfo.CheckOutTime) < 0)
            {
                if (ParkingInfo.IsMonthly)
                {
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType4(ParkingInfo.Vehicle.Type, DateTime.Now, (DateTime)ParkingInfo.CheckOutTime);
                }
                else
                {
                    SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType3(ParkingInfo.Vehicle.Type, DateTime.Now, (DateTime)ParkingInfo.CheckOutTime);
                }
            }
            else if (ParkingInfo.Status.Equals("Active") && ParkingInfo.CheckInTime.CompareTo(ParkingInfo.CheckOutTime) > 0)
            {
                SlotForm = (IList<SlotForm>)await slotRepository.GetSlotsByFloorType1(ParkingInfo.Vehicle.Type);
            }


            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please choose an available slot!";
                return Page();
            }
            if (ChosenSlot.ChosenSlotId == 0)
            {
                TempData["ErrorMessage"] = "Please choose an available slot!";
                return Page();
            }

            ParkingInfo.SlotId = ChosenSlot.ChosenSlotId;
            ParkingInfo.Slot = await slotRepository.GetSlotById(ChosenSlot.ChosenSlotId);

            /*_context.ParkingInfos.Update(ParkingInfo);
            await _context.SaveChangesAsync();*/

            await parkingInfoRepository.Update(ParkingInfo, "");
            return RedirectToPage("/ParkingInfos/Details", new { id = ParkingInfo.ParkingInfoId });
        }
    }
}
