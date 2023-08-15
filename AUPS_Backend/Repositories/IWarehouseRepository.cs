using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IWarehouseRepository
    {
        Task<List<Warehouse>> GetAllWarehouses();

        Task<Warehouse?> GetWarehouseById(Guid id);

        Task<Warehouse> AddWarehouse(Warehouse warehouse);

        Task<Warehouse> UpdateWarehouse(Warehouse warehouse);

        Task<bool> DeleteWarehouse(Guid id);
    }
}
