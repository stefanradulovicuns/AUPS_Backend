using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class ProductionOrderRepository : IProductionOrderRepository
    {
        private readonly AupsContext _context;

        public ProductionOrderRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<ProductionOrder> AddProductionOrder(ProductionOrder productionOrder)
        {
            productionOrder.ProductionOrderId = Guid.NewGuid();
            await _context.ProductionOrders.AddAsync(productionOrder);
            await _context.SaveChangesAsync();
            return productionOrder;
        }

        public async Task<bool> DeleteProductionOrder(Guid id)
        {
            _context.ProductionOrders.RemoveRange(_context.ProductionOrders.Where(po => po.ProductionOrderId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<ProductionOrder>> GetAllProductionOrders()
        {
            return await _context.ProductionOrders
                .Include("Employee")
                .Include("ObjectOfLabor")
                .ToListAsync();
        }

        public async Task<ProductionOrder?> GetProductionOrderById(Guid id)
        {
            return await _context.ProductionOrders
                .Include("Employee")
                .Include("ObjectOfLabor")
                .FirstOrDefaultAsync(po => po.ProductionOrderId == id);
        }

        public async Task<ProductionOrder> UpdateProductionOrder(ProductionOrder productionOrder)
        {
            ProductionOrder? matchingProductionOrder = await GetProductionOrderById(productionOrder.ProductionOrderId);

            if (matchingProductionOrder == null)
            {
                return productionOrder;
            }

            matchingProductionOrder.StartDate = productionOrder.StartDate;
            matchingProductionOrder.EndDate = productionOrder.EndDate;
            matchingProductionOrder.Quantity = productionOrder.Quantity;
            matchingProductionOrder.Note = productionOrder.Note;
            matchingProductionOrder.CurrentTechnologicalProcedure = productionOrder.CurrentTechnologicalProcedure;
            matchingProductionOrder.CurrentTechnologicalProcedureExecuted = productionOrder.CurrentTechnologicalProcedureExecuted;
            matchingProductionOrder.EmployeeId = productionOrder.EmployeeId;
            matchingProductionOrder.ObjectOfLaborId = productionOrder.ObjectOfLaborId;

            await _context.SaveChangesAsync();
            return matchingProductionOrder;
        }
    }
}
