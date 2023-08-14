using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IOrganizationalUnitRepository
    {
        Task<List<OrganizationalUnit>> GetAllOrganizationalUnits();

        Task<OrganizationalUnit?> GetOrganizationalUnitById(Guid id);

        Task<OrganizationalUnit> AddOrganizationalUnit(OrganizationalUnit organizationalUnit);

        Task<OrganizationalUnit> UpdateOrganizationalUnit(OrganizationalUnit organizationalUnit);

        Task<bool> DeleteOrganizationalUnit(Guid id);
    }
}
