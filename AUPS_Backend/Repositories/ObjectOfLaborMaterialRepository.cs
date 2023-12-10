using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class ObjectOfLaborMaterialRepository : IObjectOfLaborMaterialRepository
    {
        private readonly AupsContext _context;

        public ObjectOfLaborMaterialRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<ObjectOfLaborMaterial> AddObjectOfLaborMaterial(ObjectOfLaborMaterial objectOfLaborMaterial)
        {
            objectOfLaborMaterial.ObjectOfLaborMaterialId = Guid.NewGuid();
            await _context.ObjectOfLaborMaterials.AddAsync(objectOfLaborMaterial);
            await _context.SaveChangesAsync();
            return objectOfLaborMaterial;
        }

        public async Task<bool> DeleteObjectOfLaborMaterial(Guid id)
        {
            _context.ObjectOfLaborMaterials
               .RemoveRange(_context.ObjectOfLaborMaterials
               .Where(oolm => oolm.ObjectOfLaborMaterialId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<ObjectOfLaborMaterial>> GetAllObjectOfLaborMaterials()
        {
            return await _context.ObjectOfLaborMaterials
                .Include("ObjectOfLabor")
                .Include("Material")
                .ToListAsync();
        }

        public async Task<ObjectOfLaborMaterial?> GetObjectOfLaborMaterialById(Guid id)
        {
            return await _context.ObjectOfLaborMaterials
             .FirstOrDefaultAsync(oolm => oolm.ObjectOfLaborMaterialId == id);
        }

        public async Task<List<ObjectOfLaborMaterial>> GetObjectOfLaborMaterialsByObjectOfLaborId(Guid objectOfLaborId)
        {
            return await _context.ObjectOfLaborMaterials.Where(oolm => oolm.ObjectOfLaborId == objectOfLaborId).ToListAsync();
        }

        public async Task<ObjectOfLaborMaterial> UpdateObjectOfLaborMaterial(ObjectOfLaborMaterial objectOfLaborMaterial)
        {
            ObjectOfLaborMaterial? matchingObjectOfLaborMaterial = await GetObjectOfLaborMaterialById(objectOfLaborMaterial.ObjectOfLaborMaterialId);

            if (matchingObjectOfLaborMaterial == null)
            {
                return objectOfLaborMaterial;
            }

            matchingObjectOfLaborMaterial.Quantity = objectOfLaborMaterial.Quantity;
            matchingObjectOfLaborMaterial.ObjectOfLaborId = objectOfLaborMaterial.ObjectOfLaborId;
            matchingObjectOfLaborMaterial.MaterialId = objectOfLaborMaterial.MaterialId;

            await _context.SaveChangesAsync();
            return matchingObjectOfLaborMaterial;
        }
    }
}
