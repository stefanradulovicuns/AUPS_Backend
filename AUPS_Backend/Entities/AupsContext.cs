using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

public partial class AupsContext : DbContext
{
    public AupsContext()
    {
    }

    public AupsContext(DbContextOptions<AupsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ObjectOfLabor> ObjectOfLabors { get; set; }

    public virtual DbSet<ObjectOfLaborTechnologicalProcedure> ObjectOfLaborTechnologicalProcedures { get; set; }

    public virtual DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<ProductionOrder> ProductionOrders { get; set; }

    public virtual DbSet<ProductionPlan> ProductionPlans { get; set; }

    public virtual DbSet<TechnologicalProcedure> TechnologicalProcedures { get; set; }

    public virtual DbSet<TechnologicalSystem> TechnologicalSystems { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<Workplace> Workplaces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:AUPSDb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__C52E0BA8922F078B");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();

            entity.HasOne(d => d.OrganizationalUnit).WithMany(p => p.Employees).HasConstraintName("FK__employee__organi__4979DDF4");

            entity.HasOne(d => d.Workplace).WithMany(p => p.Employees).HasConstraintName("FK__employee__workpl__4885B9BB");
        });

        modelBuilder.Entity<ObjectOfLabor>(entity =>
        {
            entity.HasKey(e => e.ObjectOfLaborId).HasName("PK__object_o__132DFBC9A6505A4D");

            entity.Property(e => e.ObjectOfLaborId).ValueGeneratedNever();

            entity.HasOne(d => d.Warehouse).WithMany(p => p.ObjectOfLabors).HasConstraintName("FK__object_of__wareh__4F32B74A");
        });

        modelBuilder.Entity<ObjectOfLaborTechnologicalProcedure>(entity =>
        {
            entity.HasKey(e => e.ObjectOfLaborTechnologicalProcedureId).HasName("PK__object_o__7D4DF5F1F2053BB6");

            entity.Property(e => e.ObjectOfLaborTechnologicalProcedureId).ValueGeneratedNever();

            entity.HasOne(d => d.ObjectOfLabor).WithMany(p => p.ObjectOfLaborTechnologicalProcedures).HasConstraintName("FK__object_of__objec__642DD430");

            entity.HasOne(d => d.TechnologicalProcedure).WithMany(p => p.ObjectOfLaborTechnologicalProcedures).HasConstraintName("FK__object_of__techn__6521F869");
        });

        modelBuilder.Entity<OrganizationalUnit>(entity =>
        {
            entity.HasKey(e => e.OrganizationalUnitId).HasName("PK__organiza__21A883E2859B0F9B");

            entity.Property(e => e.OrganizationalUnitId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("PK__plant__A576B3B4D8535EBE");

            entity.Property(e => e.PlantId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductionOrder>(entity =>
        {
            entity.HasKey(e => e.ProductionOrderId).HasName("PK__producti__C099D22F533A1C24");

            entity.Property(e => e.ProductionOrderId).ValueGeneratedNever();

            entity.HasOne(d => d.Employee).WithMany(p => p.ProductionOrders).HasConstraintName("FK__productio__emplo__520F23F5");

            entity.HasOne(d => d.ObjectOfLabor).WithMany(p => p.ProductionOrders).HasConstraintName("FK__productio__objec__5303482E");
        });

        modelBuilder.Entity<ProductionPlan>(entity =>
        {
            entity.HasKey(e => e.ProductionPlanId).HasName("PK__producti__F3E379D5267BF97C");

            entity.Property(e => e.ProductionPlanId).ValueGeneratedNever();

            entity.HasOne(d => d.ObjectOfLabor).WithMany(p => p.ProductionPlans).HasConstraintName("FK__productio__objec__55DFB4D9");
        });

        modelBuilder.Entity<TechnologicalProcedure>(entity =>
        {
            entity.HasKey(e => e.TechnologicalProcedureId).HasName("PK__technolo__2192D019EB1CFADE");

            entity.Property(e => e.TechnologicalProcedureId).ValueGeneratedNever();

            entity.HasOne(d => d.OrganizationalUnitNavigation).WithMany(p => p.TechnologicalProcedures).HasConstraintName("FK__technolog__organ__5F691F13");

            entity.HasOne(d => d.Plant).WithMany(p => p.TechnologicalProcedures).HasConstraintName("FK__technolog__plant__605D434C");

            entity.HasOne(d => d.TechnologicalSystemNavigation).WithMany(p => p.TechnologicalProcedures).HasConstraintName("FK__technolog__techn__61516785");
        });

        modelBuilder.Entity<TechnologicalSystem>(entity =>
        {
            entity.HasKey(e => e.TechnologicalSystemId).HasName("PK__technolo__8234801770A31571");

            entity.Property(e => e.TechnologicalSystemId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("PK__warehous__734FE6BFD28E32A2");

            entity.Property(e => e.WarehouseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Workplace>(entity =>
        {
            entity.HasKey(e => e.WorkplaceId).HasName("PK__workplac__8E6F41E7144262C9");

            entity.Property(e => e.WorkplaceId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
