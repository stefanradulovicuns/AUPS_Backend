using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AupsContext _context;

        public MaterialRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<Material> AddMaterial(Material material)
        {
            material.MaterialId = Guid.NewGuid();
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
            return material;
        }

        public async Task<bool> DeleteMaterial(Guid id)
        {
            _context.Materials.RemoveRange(_context.Materials.Where(m => m.MaterialId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Material>> GetAllMaterials()
        {
            return await _context.Materials.ToListAsync();
        }

        public async Task<Material?> GetMaterialById(Guid id)
        {
            return await _context.Materials.FirstOrDefaultAsync(m => m.MaterialId == id);
        }

        public async Task<Material> UpdateMaterial(Material material)
        {
            Material? matchingMaterial = await GetMaterialById(material.MaterialId);

            if (matchingMaterial == null)
            {
                return material;
            }

            matchingMaterial.MaterialName = material.MaterialName;
            matchingMaterial.StockQuantity = material.StockQuantity;

            await _context.SaveChangesAsync();
            return matchingMaterial;
        }
    }
}
