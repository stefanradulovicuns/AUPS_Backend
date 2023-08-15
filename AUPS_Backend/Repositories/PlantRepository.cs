using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly AupsContext _context;

        public PlantRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<Plant> AddPlant(Plant plant)
        {
            plant.PlantId = Guid.NewGuid();
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();
            return plant;
        }

        public async Task<bool> DeletePlant(Guid id)
        {
            _context.Plants.RemoveRange(_context.Plants.Where(p => p.PlantId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Plant>> GetAllPlants()
        {
            return await _context.Plants.ToListAsync();
        }

        public async Task<Plant?> GetPlantById(Guid id)
        {
            return await _context.Plants.FirstOrDefaultAsync(p => p.PlantId == id);
        }

        public async Task<Plant> UpdatePlant(Plant plant)
        {
            Plant? matchingPlant = await GetPlantById(plant.PlantId);

            if (matchingPlant == null)
            {
                return plant;
            }

            matchingPlant.PlantName = plant.PlantName;

            await _context.SaveChangesAsync();
            return matchingPlant;
        }
    }
}
