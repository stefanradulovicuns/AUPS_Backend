using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class ObjectOfLaborRepository : IObjectOfLaborRepository
    {
        private readonly AupsContext _context;

        public ObjectOfLaborRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<ObjectOfLabor> AddObjectOfLabor(ObjectOfLabor objectOfLabor)
        {
            objectOfLabor.ObjectOfLaborId = Guid.NewGuid();
            await _context.ObjectOfLabors.AddAsync(objectOfLabor);
            await _context.SaveChangesAsync();
            return objectOfLabor;
        }

        public async Task<bool> DeleteObjectOfLabor(Guid id)
        {
            _context.ObjectOfLabors.RemoveRange(_context.ObjectOfLabors.Where(ool => ool.ObjectOfLaborId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<ObjectOfLabor?> GetObjectOfLaborById(Guid id)
        {
            return await _context.ObjectOfLabors.FirstOrDefaultAsync(ool => ool.ObjectOfLaborId == id);
        }

        public async Task<List<ObjectOfLabor>> GetAllObjectOfLabors()
        {
            return await _context.ObjectOfLabors
                .Include("Warehouse")
                .ToListAsync();
        }

        public async Task<ObjectOfLabor> UpdateObjectOfLabor(ObjectOfLabor objectOfLabor)
        {
            ObjectOfLabor? matchingObjectOfLabor = await GetObjectOfLaborById(objectOfLabor.ObjectOfLaborId);

            if (matchingObjectOfLabor == null)
            {
                return objectOfLabor;
            }

            matchingObjectOfLabor.ObjectOfLaborName = objectOfLabor.ObjectOfLaborName;
            matchingObjectOfLabor.Description = objectOfLabor.Description;
            matchingObjectOfLabor.Price = objectOfLabor.Price;
            matchingObjectOfLabor.StockQuantity = objectOfLabor.StockQuantity;
            matchingObjectOfLabor.WarehouseId = objectOfLabor.WarehouseId;

            await _context.SaveChangesAsync();
            return matchingObjectOfLabor;
        }
    }
}
