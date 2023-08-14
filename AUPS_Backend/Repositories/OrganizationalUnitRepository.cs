using AUPS_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Repositories
{
    public class OrganizationalUnitRepository : IOrganizationalUnitRepository
    {
        private readonly AupsContext _context;

        public OrganizationalUnitRepository(AupsContext context)
        {
            _context = context;
        }

        public async Task<OrganizationalUnit> AddOrganizationalUnit(OrganizationalUnit organizationalUnit)
        {
            organizationalUnit.OrganizationalUnitId = Guid.NewGuid();
            await _context.OrganizationalUnits.AddAsync(organizationalUnit);
            await _context.SaveChangesAsync();
            return organizationalUnit;
        }

        public async Task<bool> DeleteOrganizationalUnit(Guid id)
        {
            _context.OrganizationalUnits.RemoveRange(_context.OrganizationalUnits.Where(ou => ou.OrganizationalUnitId == id));
            int rowsDeleted = await _context.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<OrganizationalUnit>> GetAllOrganizationalUnits()
        {
            return await _context.OrganizationalUnits.ToListAsync();
        }

        public async Task<OrganizationalUnit?> GetOrganizationalUnitById(Guid id)
        {
            return await _context.OrganizationalUnits.FirstOrDefaultAsync(ou => ou.OrganizationalUnitId == id);
        }

        public async Task<OrganizationalUnit> UpdateOrganizationalUnit(OrganizationalUnit organizationalUnit)
        {
            OrganizationalUnit? matchingOrganizationalUnit = await GetOrganizationalUnitById(organizationalUnit.OrganizationalUnitId);

            if (matchingOrganizationalUnit == null)
            {
                return organizationalUnit;
            }

            matchingOrganizationalUnit.OrganizationalUnitName = organizationalUnit.OrganizationalUnitName;

            await _context.SaveChangesAsync();
            return matchingOrganizationalUnit;
        }
    }
}
