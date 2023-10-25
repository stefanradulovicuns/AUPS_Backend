using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class WorkplaceRepository : IWorkplaceRepository
    {
        private readonly AupsContext _context;

        public WorkplaceRepository(AupsContext context)
        {
            _context = context;
        }
    
        public async Task<Workplace> AddWorkplace(Workplace workplace)
        {
            workplace.WorkplaceId = Guid.NewGuid();
            await _context.Workplaces.AddAsync(workplace);
            await _context.SaveChangesAsync();
            return workplace;
        }

        public async Task<bool> DeleteWorkplace(Guid id)
        {
            _context.Workplaces.RemoveRange(_context.Workplaces.Where(w => w.WorkplaceId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Workplace>> GetAllWorkplaces()
        {
            return await _context.Workplaces.ToListAsync();
        }

        public async Task<Workplace?> GetWorkplaceById(Guid id)
        {
            return await _context.Workplaces.FirstOrDefaultAsync(w => w.WorkplaceId == id);
        }

        public async Task<Workplace?> GetWorkplaceByName(string name)
        {
            return await _context.Workplaces.FirstOrDefaultAsync(w => w.WorkplaceName == name);
        }

        public async Task<Workplace> UpdateWorkplace(Workplace workplace)
        {
            Workplace? matchingWorkplace = await GetWorkplaceById(workplace.WorkplaceId);

            if (matchingWorkplace == null)
            {
                return workplace;
            }

            matchingWorkplace.WorkplaceName = workplace.WorkplaceName;

            await _context.SaveChangesAsync();
            return matchingWorkplace;
        }
    }
}
