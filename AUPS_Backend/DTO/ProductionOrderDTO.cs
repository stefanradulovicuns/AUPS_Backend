using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUPS_Backend.DTO
{
    public class ProductionOrderDTO
    {
        public Guid ProductionOrderId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Quantity { get; set; }

        public string? Note { get; set; }

        public int CurrentTechnologicalProcedure { get; set; }

        public bool CurrentTechnologicalProcedureExecuted { get; set; }

        public double CurrentState { get; set; }

        public Guid EmployeeId { get; set; }

        public string? Manager { get; set; }

        public string? ManagerEmail { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public string? ObjectOfLaborName { get; set; }

        public int TotalCount { get; set; }
    }
}
