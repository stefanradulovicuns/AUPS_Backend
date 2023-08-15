using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IProductionOrderRepository
    {
        Task<List<ProductionOrder>> GetAllProductionOrders();

        Task<ProductionOrder?> GetProductionOrderById(Guid id);

        Task<ProductionOrder> AddProductionOrder(ProductionOrder productionOrder);

        Task<ProductionOrder> UpdateProductionOrder(ProductionOrder productionOrder);

        Task<bool> DeleteProductionOrder(Guid id);
    }
}
