using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AupsContext _context;

        public WarehouseRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<Warehouse> AddWarehouse(Warehouse warehouse)
        {
            warehouse.WarehouseId = Guid.NewGuid();
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<bool> DeleteWarehouse(Guid id)
        {
            _context.Warehouses.RemoveRange(_context.Warehouses.Where(w => w.WarehouseId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Warehouse>> GetAllWarehouses()
        {
            return await _context.Warehouses.ToListAsync();
        }

        public async Task<Warehouse?> GetWarehouseById(Guid id)
        {
            return await _context.Warehouses.FirstOrDefaultAsync(w => w.WarehouseId == id);
        }

        public async Task<Warehouse> UpdateWarehouse(Warehouse warehouse)
        {
            Warehouse? matchingWarehouse = await GetWarehouseById(warehouse.WarehouseId);

            if (matchingWarehouse == null)
            {
                return warehouse;
            }

            matchingWarehouse.Address = warehouse.Address;
            matchingWarehouse.City = warehouse.City;
            matchingWarehouse.Capacity = warehouse.Capacity;
           
            await _context.SaveChangesAsync();
            return matchingWarehouse;
        }
    }
}
