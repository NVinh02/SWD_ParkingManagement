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
using ParkingManagementWebApp.Constants;
using DataAccess.Repository;

namespace ParkingManagementWebApp.Pages.ParkingInfos
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IParkingInfoRepository parkingInfoRepository = new ParkingInfoRepository();
        private readonly IPricingRepository pricingRepository = new PricingRepository();
        public DetailsModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public ParkingInfo ParkingInfo { get; set; }
        public bool IsStaff { get; set; } = false;

        [BindProperty]
        public EditParkingInfoForm EditParkingInfoForm { get; set; }

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
            IsStaff = userInformation.IsStaff;
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
            if (!userInformation.IsStaff && !EditParkingInfoForm.EditAction.Equals("Extend") && !EditParkingInfoForm.EditAction.Equals("Slot"))
            {
                return RedirectToPage("/Index");
            }
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            ParkingInfo = await parkingInfoRepository.GetParkingInfoById(EditParkingInfoForm.ParkingInfoId);

            if (EditParkingInfoForm.EditAction.Equals("Toggle"))
            {
                await parkingInfoRepository.ChangeHaveVehicle(ParkingInfo);
            }
            else if (EditParkingInfoForm.EditAction.Equals("Extend"))
            {
                return RedirectToPage("/ParkingInfos/ExtendTime", new { id = ParkingInfo.ParkingInfoId });
            }
            else if (EditParkingInfoForm.EditAction.Equals("Slot"))
            {
                return RedirectToPage("/ParkingInfos/ChangeSlot", new { id = ParkingInfo.ParkingInfoId });
            }
            else
            {
                await parkingInfoRepository.Update(ParkingInfo, EditParkingInfoForm.EditAction);
            }
            
            IsStaff = userInformation.IsStaff;
            return Page();
        }
    }
}
