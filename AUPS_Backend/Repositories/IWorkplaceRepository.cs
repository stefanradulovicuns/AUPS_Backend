using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IWorkplaceRepository
    {
        Task<List<Workplace>> GetAllWorkplaces();

        Task<Workplace?> GetWorkplaceById(Guid id);

        Task<Workplace?> GetWorkplaceByName(string name);

        Task<Workplace> AddWorkplace(Workplace workplace);

        Task<Workplace> UpdateWorkplace(Workplace workplace);

        Task<bool> DeleteWorkplace(Guid id);
    }
}
