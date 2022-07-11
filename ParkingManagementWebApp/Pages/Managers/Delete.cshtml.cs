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

namespace ParkingManagementWebApp.Pages.Managers
{
    public class DeleteModel : PageModel
    {
        private readonly BusinessObject.Models.ParkingManagementContext _context;
        private readonly IManagerRepository managerRepository = new ManagerRepository();

        public DeleteModel(BusinessObject.Models.ParkingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Manager Manager { get; set; }
        public bool Status { get; set; }

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

            if (Manager == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
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

            Manager = await managerRepository.GetManagerByUsername(id);

            if (Manager != null)
            {
                //_context.Managers.Remove(Manager);
                //await _context.SaveChangesAsync();
                await managerRepository.UpdateManagerStatus(Manager);
            }

            return RedirectToPage("./Index");
        }
    }
}
