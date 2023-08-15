using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IPlantRepository
    {
        Task<List<Plant>> GetAllPlants();

        Task<Plant?> GetPlantById(Guid id);

        Task<Plant> AddPlant(Plant plant);

        Task<Plant> UpdatePlant(Plant plant);

        Task<bool> DeletePlant(Guid id);
    }
}
