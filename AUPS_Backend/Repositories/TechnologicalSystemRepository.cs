using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class TechnologicalSystemRepository : ITechnologicalSystemRepository
    {
        private readonly AupsContext _context;

        public TechnologicalSystemRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<TechnologicalSystem> AddTechnologicalSystem(TechnologicalSystem technologicalSystem)
        {
            technologicalSystem.TechnologicalSystemId = Guid.NewGuid();
            await _context.TechnologicalSystems.AddAsync(technologicalSystem);
            await _context.SaveChangesAsync();
            return technologicalSystem;
        }

        public async Task<bool> DeleteTechnologicalSystem(Guid id)
        {
            _context.TechnologicalSystems.RemoveRange(_context.TechnologicalSystems.Where(ts => ts.TechnologicalSystemId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<TechnologicalSystem>> GetAllTechnologicalSystems()
        {
            return await _context.TechnologicalSystems.ToListAsync();
        }

        public async Task<TechnologicalSystem?> GetTechnologicalSystemById(Guid id)
        {
            return await _context.TechnologicalSystems.FirstOrDefaultAsync(ts => ts.TechnologicalSystemId == id);
        }

        public async Task<TechnologicalSystem> UpdateTechnologicalSystem(TechnologicalSystem technologicalSystem)
        {
            TechnologicalSystem? matchingTechnologicalSystem = await GetTechnologicalSystemById(technologicalSystem.TechnologicalSystemId);

            if (matchingTechnologicalSystem == null)
            {
                return technologicalSystem;
            }

            matchingTechnologicalSystem.TechnologicalSystemName = technologicalSystem.TechnologicalSystemName;

            await _context.SaveChangesAsync();
            return matchingTechnologicalSystem;
        }
    }
}
