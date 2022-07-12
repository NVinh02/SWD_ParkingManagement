using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PricingDAO
    {
        private static PricingDAO instance = null;
        private static readonly object instanceLock = new object();
        private PricingDAO() { }
        public static PricingDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PricingDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<IEnumerable<Pricing>> GetPricings()
        {
            List<Pricing> pricings = null;
            try
            {
                var _context = new ParkingManagementContext();
                pricings = await _context.Pricings.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pricings;
        }

        public async Task<Pricing> GetPricingById(int? id)
        {
            Pricing price = null;
            try
            {
                var _context = new ParkingManagementContext();
                price = await _context.Pricings.FirstOrDefaultAsync(m => m.PricingId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return price;
        }

        public async Task Update(Pricing pricing)
        {
            try
            {
                var _context = new ParkingManagementContext();
                _context.Pricings.Update(pricing);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
