using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IProductionPlanRepository
    {
        Task<List<ProductionPlan>> GetAllProductionPlans();

        Task<ProductionPlan?> GetProductionPlanById(Guid id);

        Task<ProductionPlan> AddProductionPlan(ProductionPlan productionPlan);

        Task<ProductionPlan> UpdateProductionPlan(ProductionPlan productionPlan);

        Task<bool> DeleteProductionPlan(Guid id);
    }
}
