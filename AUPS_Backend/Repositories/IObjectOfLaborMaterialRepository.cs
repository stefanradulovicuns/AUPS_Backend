using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IObjectOfLaborMaterialRepository
    {
        Task<List<ObjectOfLaborMaterial>> GetAllObjectOfLaborMaterials();

        Task<List<ObjectOfLaborMaterial>> GetObjectOfLaborMaterialsByObjectOfLaborId(Guid objectOfLaborId);

        Task<ObjectOfLaborMaterial?> GetObjectOfLaborMaterialById(Guid id);

        Task<ObjectOfLaborMaterial> AddObjectOfLaborMaterial(ObjectOfLaborMaterial objectOfLaborMaterial);

        Task<ObjectOfLaborMaterial> UpdateObjectOfLaborMaterial(ObjectOfLaborMaterial objectOfLaborMaterial);

        Task<bool> DeleteObjectOfLaborMaterial(Guid id);
    }
}
