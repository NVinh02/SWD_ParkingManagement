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

namespace ParkingManagementWebApp.Pages.Managers
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IManagerRepository managerRepository = new ManagerRepository();

        public DetailsModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        public Manager Manager { get; set; }
        public bool Status { get; set; }
        public bool IsAdmin { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
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
            if (id == null)
            {
                return NotFound();
            }

            //Manager = await _context.Managers.FirstOrDefaultAsync(m => m.Username == id);
            Manager = await managerRepository.GetManagerByUsername(id);
            if(Manager.Status == true)
            {
                Status = true;
            }
            else
            {
                Status = false;
            }

            if (Manager.Type.Equals("Admin"))
            {
                IsAdmin = true;
            }
            else
            {
                IsAdmin = false;
            }

            if (Manager == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
