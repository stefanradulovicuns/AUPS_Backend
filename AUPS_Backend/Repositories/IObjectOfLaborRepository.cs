using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IObjectOfLaborRepository
    {
        Task<List<ObjectOfLabor>> GetAllObjectOfLabors();

        Task<ObjectOfLabor?> GetObjectOfLaborById(Guid id);

        Task<ObjectOfLabor> AddObjectOfLabor(ObjectOfLabor objectOfLabor);

        Task<ObjectOfLabor> UpdateObjectOfLabor(ObjectOfLabor objectOfLabor);

        Task<bool> DeleteObjectOfLabor(Guid id);
    }
}
