using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPricingRepository
    {
        public Task<IEnumerable<Pricing>> GetPricings();
        public Task<Pricing> GetPricingById(int? id);
        public Task UpdatePricing(Pricing pricing);
    }
}
