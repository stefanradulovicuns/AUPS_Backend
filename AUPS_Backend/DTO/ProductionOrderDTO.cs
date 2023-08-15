﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public Guid EmployeeId { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public int TotalCount { get; set; }
    }
}
