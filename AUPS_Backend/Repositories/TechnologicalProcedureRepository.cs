using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class TechnologicalProcedureRepository : ITechnologicalProcedureRepository
    {
        private readonly AupsContext _context;

        public TechnologicalProcedureRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<TechnologicalProcedure> AddTechnologicalProcedure(TechnologicalProcedure technologicalProcedure)
        {
            technologicalProcedure.TechnologicalProcedureId = Guid.NewGuid();
            await _context.TechnologicalProcedures.AddAsync(technologicalProcedure);
            await _context.SaveChangesAsync();
            return technologicalProcedure;
        }

        public async Task<bool> DeleteTechnologicalProcedure(Guid id)
        {
            _context.TechnologicalProcedures.RemoveRange(_context.TechnologicalProcedures.Where(tp => tp.TechnologicalProcedureId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<TechnologicalProcedure>> GetAllTechnologicalProcedures()
        {
            return await _context.TechnologicalProcedures
                .Include("OrganizationalUnit")
                .Include("TechnologicalSystem")
                .Include("Plant")
                .ToListAsync();
        }

        public async Task<TechnologicalProcedure?> GetTechnologicalProcedureById(Guid id)
        {
            return await _context.TechnologicalProcedures.FirstOrDefaultAsync(tp => tp.TechnologicalProcedureId == id);
        }

        public async Task<TechnologicalProcedure> UpdateTechnologicalProcedure(TechnologicalProcedure technologicalProcedure)
        {
            TechnologicalProcedure? matchingTechnologicalProcedure = await GetTechnologicalProcedureById(technologicalProcedure.TechnologicalProcedureId);

            if (matchingTechnologicalProcedure == null)
            {
                return technologicalProcedure;
            }

            matchingTechnologicalProcedure.TechnologicalProcedureName = technologicalProcedure.TechnologicalProcedureName;
            matchingTechnologicalProcedure.Duration = technologicalProcedure.Duration;
            matchingTechnologicalProcedure.OrganizationalUnitId = technologicalProcedure.OrganizationalUnitId;
            matchingTechnologicalProcedure.PlantId = technologicalProcedure.PlantId;
            matchingTechnologicalProcedure.TechnologicalSystemId = technologicalProcedure.TechnologicalSystemId;

            await _context.SaveChangesAsync();
            return matchingTechnologicalProcedure;
        }
    }
}
