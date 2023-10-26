using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AUPS_Backend.DTO
{
    public class TechnologicalProcedureDTO
    {
        public Guid TechnologicalProcedureId { get; set; }

        public string? TechnologicalProcedureName { get; set; }

        public int Duration { get; set; }

        public Guid OrganizationalUnitId { get; set; }

        public string? OrganizationalUnitName { get; set; }

        public Guid PlantId { get; set; }

        public string? PlantName { get; set; }

        public Guid TechnologicalSystemId { get; set; }

        public string? TechnologicalSystemName { get; set; }

        public int TotalCount { get; set; }
    }
}
