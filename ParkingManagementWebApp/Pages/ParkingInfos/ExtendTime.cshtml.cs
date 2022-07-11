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
    public class ExtendTimeModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IParkingInfoRepository parkingInfoRepository = new ParkingInfoRepository();
        private readonly IPricingRepository pricingRepository = new PricingRepository();
        public ExtendTimeModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public ParkingInfo ParkingInfo { get; set; }
        [BindProperty] public int ParkingInfoId { get; set; }
        [BindProperty] public int ExtendMonthly { get; set; }
        [BindProperty] public DateTime? ExtendEndDate { get; set; }
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

            if (ParkingInfo.IsMonthly)
            {
                DateTime newEndDate = ParkingInfo.CheckOutTime;
                newEndDate = newEndDate.AddMonths(ExtendMonthly);
                bool isAllowed = true;
                List<ParkingInfo> tempPK = new List<ParkingInfo>();
                tempPK = _context.ParkingInfos.Where(p => p.SlotId == ParkingInfo.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToList();
                if (tempPK.Count > 0)
                {
                    foreach (ParkingInfo p in tempPK)
                    {
                        if (p.ParkingInfoId == ParkingInfo.ParkingInfoId)
                        {
                            continue;
                        }

                        if ((ParkingInfo.CheckInTime.CompareTo(p.CheckInTime) < 0 && newEndDate.CompareTo(p.CheckInTime) < 0) ||
                            (ParkingInfo.CheckInTime.CompareTo(p.CheckOutTime) > 0 && newEndDate.CompareTo(p.CheckOutTime) > 0))
                        {
                            isAllowed = true;
                        }
                        else
                        {
                            isAllowed = false;
                            break;
                        }
                    }
                }
                else
                {
                    isAllowed = true;
                }

                if (isAllowed)
                {
                    ParkingInfo.CheckOutTime = newEndDate;

                    if (userInformation.IsResident)
                    {
                        ParkingInfo.TotalPrice += ParkingInfo.Pricing.Value * ExtendMonthly * (decimal)(0.9);
                    }
                    else
                    {
                        ParkingInfo.TotalPrice += ParkingInfo.Pricing.Value * ExtendMonthly;
                    }

                    await parkingInfoRepository.Update(ParkingInfo, "");
                    return RedirectToPage("/ParkingInfos/Details", new { id = ParkingInfo.ParkingInfoId });
                }
                else
                {
                    TempData["ErrorMessage"] = "Someone already booked this slot during that time";
                    return Page();
                }
            }
            else
            {
                DateTime newEndDate = (DateTime)ExtendEndDate;

                if(newEndDate.CompareTo(ParkingInfo.CheckOutTime) <= 0)
                {
                    TempData["ErrorMessage"] = "Your new End Date must be later than the current End Date!";
                    return Page();
                }

                bool isAllowed = true;
                List<ParkingInfo> tempPK = new List<ParkingInfo>();
                tempPK = _context.ParkingInfos.Where(p => p.SlotId == ParkingInfo.SlotId
                                                     && (p.Status.Trim().Equals("Booked") || p.Status.Trim().Equals("Active"))).ToList();
                if (tempPK.Count > 0)
                {
                    foreach (ParkingInfo p in tempPK)
                    {
                        if (p.ParkingInfoId == ParkingInfo.ParkingInfoId)
                        {
                            continue;
                        }

                        if ((ParkingInfo.CheckInTime.CompareTo(p.CheckInTime) < 0 && newEndDate.CompareTo(p.CheckInTime) < 0) ||
                            (ParkingInfo.CheckInTime.CompareTo(p.CheckOutTime) > 0 && newEndDate.CompareTo(p.CheckOutTime) > 0))
                        {
                            isAllowed = true;
                        }
                        else
                        {
                            isAllowed = false;
                            break;
                        }
                    }
                }
                else
                {
                    isAllowed = true;
                }

                if (isAllowed)
                {
                    ParkingInfo.CheckOutTime = newEndDate;
                    int times = (int)((ParkingInfo.CheckOutTime - ParkingInfo.CheckInTime).TotalHours / 8 + 1);
                    ParkingInfo.TotalPrice = times * ParkingInfo.Pricing.Value;

                    if (userInformation.IsResident)
                    {
                        ParkingInfo.TotalPrice = ParkingInfo.TotalPrice * (decimal)(0.9);
                    }

                    await parkingInfoRepository.Update(ParkingInfo, "");
                    return RedirectToPage("/ParkingInfos/Details", new { id = ParkingInfo.ParkingInfoId });
                }
                else
                {
                    TempData["ErrorMessage"] = "Someone already booked this slot during that time";
                    return Page();
                }
            }

            return Page();
        }
    }
}
