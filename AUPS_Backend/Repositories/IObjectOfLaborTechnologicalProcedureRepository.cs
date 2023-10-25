using AUPS_Backend.Entities;

namespace AUPS_Backend.Repositories
{
    public interface IObjectOfLaborTechnologicalProcedureRepository
    {
        Task<List<ObjectOfLaborTechnologicalProcedure>> GetAllObjectOfLaborTechnologicalProcedures();

        Task<List<ObjectOfLaborTechnologicalProcedure>> GetObjectOfLaborTechnologicalProceduresByObjectOfLaborId(Guid objectOfLaborId);

        Task<ObjectOfLaborTechnologicalProcedure?> GetObjectOfLaborTechnologicalProcedureById(Guid id);

        Task<ObjectOfLaborTechnologicalProcedure> AddObjectOfLaborTechnologicalProcedure(ObjectOfLaborTechnologicalProcedure objectOfLaborTechnologicalProcedure);

        Task<ObjectOfLaborTechnologicalProcedure> UpdateObjectOfLaborTechnologicalProcedure(ObjectOfLaborTechnologicalProcedure objectOfLaborTechnologicalProcedure);

        Task<bool> DeleteObjectOfLaborTechnologicalProcedure(Guid id);
    }
}
