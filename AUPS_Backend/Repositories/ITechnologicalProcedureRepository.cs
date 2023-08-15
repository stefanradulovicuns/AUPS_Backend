using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface ITechnologicalProcedureRepository
    {
        Task<List<TechnologicalProcedure>> GetAllTechnologicalProcedures();

        Task<TechnologicalProcedure?> GetTechnologicalProcedureById(Guid id);

        Task<TechnologicalProcedure> AddTechnologicalProcedure(TechnologicalProcedure technologicalProcedure);

        Task<TechnologicalProcedure> UpdateTechnologicalProcedure(TechnologicalProcedure technologicalProcedure);

        Task<bool> DeleteTechnologicalProcedure(Guid id);
    }
}
