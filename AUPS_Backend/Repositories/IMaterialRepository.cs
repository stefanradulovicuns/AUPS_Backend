using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IMaterialRepository
    {
        Task<List<Material>> GetAllMaterials();
        
        Task<Material?> GetMaterialById(Guid id);

        Task<Material> AddMaterial(Material material);

        Task<Material> UpdateMaterial(Material material);

        Task<bool> DeleteMaterial(Guid id);
    }
}
