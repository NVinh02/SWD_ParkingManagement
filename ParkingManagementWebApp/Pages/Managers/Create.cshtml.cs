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
using ParkingManagementWebApp.Constants;
using DataAccess.Repository;

namespace ParkingManagementWebApp.Pages.Managers
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IManagerRepository managerRepository = new ManagerRepository();

        public CreateModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
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
            return Page();
        }

        [BindProperty]
        public Manager Manager { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Managers.Add(Manager);
            //await _context.SaveChangesAsync();
            Manager.Type = "Staff";
            await managerRepository.AddManager(Manager);

            return RedirectToPage("./Index");
        }
    }
}
