using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PricingRepository : IPricingRepository
    {
        public Task<Pricing> GetPricingById(int? id)
            => PricingDAO.Instance.GetPricingById(id);

        public Task<IEnumerable<Pricing>> GetPricings()
            => PricingDAO.Instance.GetPricings();

        public Task UpdatePricing(Pricing pricing)
            => PricingDAO.Instance.Update(pricing);
    }
}
