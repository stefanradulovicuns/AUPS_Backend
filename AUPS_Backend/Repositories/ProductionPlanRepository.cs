using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class ProductionPlanRepository : IProductionPlanRepository
    {
        private readonly AupsContext _context;

        public ProductionPlanRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<ProductionPlan> AddProductionPlan(ProductionPlan productionPlan)
        {
            productionPlan.ProductionPlanId = Guid.NewGuid();
            await _context.ProductionPlans.AddAsync(productionPlan);
            await _context.SaveChangesAsync();
            return productionPlan;
        }

        public async Task<bool> DeleteProductionPlan(Guid id)
        {
            _context.ProductionPlans.RemoveRange(_context.ProductionPlans.Where(pp => pp.ProductionPlanId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<ProductionPlan>> GetAllProductionPlans()
        {
            return await _context.ProductionPlans.ToListAsync();
        }

        public async Task<ProductionPlan?> GetProductionPlanById(Guid id)
        {
            return await _context.ProductionPlans.FirstOrDefaultAsync(pp => pp.ProductionPlanId == id);
        }

        public async Task<ProductionPlan> UpdateProductionPlan(ProductionPlan productionPlan)
        {
            ProductionPlan? matchingProductionPlan = await GetProductionPlanById(productionPlan.ProductionPlanId);

            if (matchingProductionPlan == null)
            {
                return productionPlan;
            }

            matchingProductionPlan.ProductionPlanName = productionPlan.ProductionPlanName;
            matchingProductionPlan.Description = productionPlan.Description;
            matchingProductionPlan.ObjectOfLaborId = productionPlan.ObjectOfLaborId;

            await _context.SaveChangesAsync();
            return matchingProductionPlan;
        }
    }
}
