using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using DataAccess.Repository;
using ParkingManagementWebApp.Constants;
using BusinessObject.BusinessModels;
using ParkingManagementWebApp.Ultilities;

namespace ParkingManagementWebApp.Pages.ParkingInfos
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly ISlotRepository slotRepository = new SlotRepository();
        private readonly IParkingInfoRepository parkingInfoRepository = new ParkingInfoRepository();
        private readonly IVehicleRepository vehicleRepository = new VehicleRepository();
        private readonly IPricingRepository pricingRepository = new PricingRepository();

        [BindProperty]
        public ParkingInfo ParkingInfoConfirm { get; set; }

        public CreateModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
            ParkingInfoConfirm = new ParkingInfo();
        }

        [BindProperty]
        public ParkingInfo ParkingInfo { get; set; }
        public string CurrentUserId { get; set; }
        public int SlotId { get; set; }
        public string Position { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            UserInformation userInformation = SessionHelper.GetFromSession<UserInformation>(HttpContext.Session, SessionValue.Authentication);
            CurrentUserId = userInformation.UserId;
            ParkingInfoForm parkingInfoForm = SessionHelper.GetFromSession<ParkingInfoForm>(HttpContext.Session, SessionValue.ParkingInfoForm);
            Vehicle vehicle = (Vehicle)await vehicleRepository.GetVehicleByVehicleId(parkingInfoForm.VehicleId);

            ParkingInfoConfirm.VehicleId = parkingInfoForm.VehicleId;
            ParkingInfoConfirm.Ccid = CurrentUserId;
            ParkingInfoConfirm.SlotId = parkingInfoForm.SlotId;
            if (vehicle.Type.Equals("Bike"))
            {
                if (parkingInfoForm.IsMonthly)
                {
                    ParkingInfoConfirm.PricingId = 2;
                    ParkingInfoConfirm.IsMonthly = true;
                }
                else
                {
                    ParkingInfoConfirm.PricingId = 1;
                    ParkingInfoConfirm.IsMonthly = false;
                }
            }
            else if (vehicle.Type.Equals("Car"))
            {
                if (parkingInfoForm.IsMonthly)
                {
                    ParkingInfoConfirm.PricingId = 4;
                    ParkingInfoConfirm.IsMonthly = true;
                }
                else
                {
                    ParkingInfoConfirm.PricingId = 3;
                    ParkingInfoConfirm.IsMonthly = false;
                }
            }
            ParkingInfoConfirm.CheckInTime = parkingInfoForm.CheckInTime;

            if (parkingInfoForm.CheckOutTime.HasValue)
            {
                ParkingInfoConfirm.CheckOutTime = (DateTime)parkingInfoForm.CheckOutTime;
            }
            else
            {
                ParkingInfoConfirm.CheckOutTime = (DateTime)parkingInfoForm.CheckInTime.AddYears(-200);
            }

            if (ParkingInfoConfirm.CheckInTime <= DateTime.Now)
            {
                ParkingInfoConfirm.HaveVehicle = true;
                ParkingInfoConfirm.Status = "Active";
            }
            else if (ParkingInfoConfirm.CheckInTime > DateTime.Now)
            {
                ParkingInfoConfirm.HaveVehicle = false;
                ParkingInfoConfirm.Status = "Booked";
            }
            if (parkingInfoForm.ParkType == 1)
            {
                ParkingInfoConfirm.TotalPrice = 0;
            }
            else if(parkingInfoForm.ParkType == 2 || parkingInfoForm.ParkType == 4)
            {
                decimal price = _context.Pricings.Find(ParkingInfoConfirm.PricingId).Value;
                ParkingInfoConfirm.TotalPrice = price * parkingInfoForm.Month;
            }
            else
            {
                decimal price = _context.Pricings.Find(ParkingInfoConfirm.PricingId).Value;
                int times = (int)((ParkingInfoConfirm.CheckOutTime - ParkingInfoConfirm.CheckInTime).TotalHours / 8 + 1);
                ParkingInfoConfirm.TotalPrice = times * price;
            }

            if (userInformation.IsResident)
            {
                ParkingInfoConfirm.TotalPrice = ParkingInfoConfirm.TotalPrice * (decimal)(0.9);
            }

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/
            ParkingInfoForm parkingInfoForm = SessionHelper.GetFromSession<ParkingInfoForm>(HttpContext.Session, SessionValue.ParkingInfoForm);

            ParkingInfo.CheckInTime = parkingInfoForm.CheckInTime;
            if (parkingInfoForm.CheckOutTime.HasValue)
            {
                ParkingInfo.CheckOutTime = (DateTime)parkingInfoForm.CheckOutTime;
            }
            else
            {
                ParkingInfo.CheckOutTime = (DateTime)parkingInfoForm.CheckInTime.AddYears(-200);
            }

            ParkingInfo.HaveVehicle = parkingInfoForm.HaveVehicle;
            ParkingInfo.IsMonthly = parkingInfoForm.IsMonthly;

            /*_context.ParkingInfos.Add(ParkingInfo);
            await _context.SaveChangesAsync();*/

            await parkingInfoRepository.Add(ParkingInfo);

            /*if(parkingInfoForm.ParkType == 1 || parkingInfoForm.ParkType == 2)
            {
                Slot tempSlot = (Slot)_context.Slots.SingleOrDefault(s => s.SlotId == parkingInfoForm.SlotId);
                tempSlot.IsOccupied = true;
                tempSlot.IsBook = true;
                _context.Slots.Update(tempSlot);
                await _context.SaveChangesAsync();
            }
            else if(parkingInfoForm.ParkType == 3 || parkingInfoForm.ParkType == 4)
            {
                Slot tempSlot = (Slot)_context.Slots.SingleOrDefault(s => s.SlotId == parkingInfoForm.SlotId);
                tempSlot.IsBook = true;
                _context.Slots.Update(tempSlot);
                await _context.SaveChangesAsync();
            }*/

            return RedirectToPage("/Index");
        }
    }
}
