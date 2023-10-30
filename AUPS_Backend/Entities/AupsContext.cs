using System;
using System.Collections.Generic;
using AUPS_Backend.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AUPS_Backend.Entities;

public partial class AupsContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__C52E0BA822845859");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();

            entity.HasOne(d => d.OrganizationalUnit)
                .WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employee__organi__39987BE6");

            entity.HasOne(d => d.Workplace)
                .WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employee__workpl__38A457AD");
        });

        modelBuilder.Entity<ObjectOfLabor>(entity =>
        {
            entity.HasKey(e => e.ObjectOfLaborId).HasName("PK__object_o__132DFBC9BD6DAD16");

            entity.Property(e => e.ObjectOfLaborId).ValueGeneratedNever();

            entity.HasOne(d => d.Warehouse).WithMany(p => p.ObjectOfLabors).HasConstraintName("FK__object_of__wareh__3F51553C");
        });

        modelBuilder.Entity<ObjectOfLaborTechnologicalProcedure>(entity =>
        {
            entity.HasKey(e => e.ObjectOfLaborTechnologicalProcedureId).HasName("PK__object_o__7D4DF5F1514AE950");

            entity.Property(e => e.ObjectOfLaborTechnologicalProcedureId).ValueGeneratedNever();

            entity.HasOne(d => d.ObjectOfLabor).WithMany(p => p.ObjectOfLaborTechnologicalProcedures).HasConstraintName("FK__object_of__objec__544C7222");

            entity.HasOne(d => d.TechnologicalProcedure).WithMany(p => p.ObjectOfLaborTechnologicalProcedures).HasConstraintName("FK__object_of__techn__5540965B");
        });

        modelBuilder.Entity<OrganizationalUnit>(entity =>
        {
            entity.HasKey(e => e.OrganizationalUnitId).HasName("PK__organiza__21A883E2A0413E58");

            entity.Property(e => e.OrganizationalUnitId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("PK__plant__A576B3B4F4CF2CC4");

            entity.Property(e => e.PlantId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductionOrder>(entity =>
        {
            entity.HasKey(e => e.ProductionOrderId).HasName("PK__producti__C099D22FCF047D9B");

            entity.Property(e => e.ProductionOrderId).ValueGeneratedNever();

            entity.HasOne(d => d.Employee).WithMany(p => p.ProductionOrders).HasConstraintName("FK__productio__emplo__422DC1E7");

            entity.HasOne(d => d.ObjectOfLabor).WithMany(p => p.ProductionOrders).HasConstraintName("FK__productio__objec__4321E620");
        });

        modelBuilder.Entity<ProductionPlan>(entity =>
        {
            entity.HasKey(e => e.ProductionPlanId).HasName("PK__producti__F3E379D5CD0F2289");

            entity.Property(e => e.ProductionPlanId).ValueGeneratedNever();

            entity.HasOne(d => d.ObjectOfLabor).WithMany(p => p.ProductionPlans).HasConstraintName("FK__productio__objec__45FE52CB");
        });

        modelBuilder.Entity<TechnologicalProcedure>(entity =>
        {
            entity.HasKey(e => e.TechnologicalProcedureId).HasName("PK__technolo__2192D019DFA30232");

            entity.Property(e => e.TechnologicalProcedureId).ValueGeneratedNever();

            entity.HasOne(d => d.OrganizationalUnit).WithMany(p => p.TechnologicalProcedures).HasConstraintName("FK__technolog__organ__4F87BD05");

            entity.HasOne(d => d.Plant).WithMany(p => p.TechnologicalProcedures).HasConstraintName("FK__technolog__plant__507BE13E");

            entity.HasOne(d => d.TechnologicalSystem).WithMany(p => p.TechnologicalProcedures).HasConstraintName("FK__technolog__techn__51700577");
        });

        modelBuilder.Entity<TechnologicalSystem>(entity =>
        {
            entity.HasKey(e => e.TechnologicalSystemId).HasName("PK__technolo__823480177CF18B5F");

            entity.Property(e => e.TechnologicalSystemId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("PK__warehous__734FE6BF2BC6D417");

            entity.Property(e => e.WarehouseId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Workplace>(entity =>
        {
            entity.HasKey(e => e.WorkplaceId).HasName("PK__workplac__8E6F41E728F334B9");

            entity.Property(e => e.WorkplaceId).ValueGeneratedNever();
        });

        modelBuilder.Entity<OrganizationalUnit>().HasData(
            new OrganizationalUnit()
            {
                OrganizationalUnitId = Guid.NewGuid(),
                OrganizationalUnitName = "Administracija"
            });

        modelBuilder.Entity<Workplace>().HasData(new List<Workplace>()
            {
                new Workplace()
                {
                    WorkplaceId = Guid.NewGuid(),
                    WorkplaceName = "Admin"
                },
                new Workplace()
                {
                    WorkplaceId = Guid.NewGuid(),
                    WorkplaceName = "Menadzer"
                },
                new Workplace()
                {
                    WorkplaceId = Guid.NewGuid(),
                    WorkplaceName = "Radnik u proizvodnji"
                }
            });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
