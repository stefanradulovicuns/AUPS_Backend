using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUPS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "organizational_unit",
                columns: table => new
                {
                    organizational_unit_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organizational_unit_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__organiza__21A883E2A0413E58", x => x.organizational_unit_id);
                });

            migrationBuilder.CreateTable(
                name: "plant",
                columns: table => new
                {
                    plant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    plant_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__plant__A576B3B4F4CF2CC4", x => x.plant_id);
                });

            migrationBuilder.CreateTable(
                name: "technological_system",
                columns: table => new
                {
                    technological_system_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    technological_system_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__technolo__823480177CF18B5F", x => x.technological_system_id);
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
                columns: table => new
                {
                    warehouse_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__warehous__734FE6BF2BC6D417", x => x.warehouse_id);
                });

            migrationBuilder.CreateTable(
                name: "workplace",
                columns: table => new
                {
                    workplace_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    workplace_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__workplac__8E6F41E728F334B9", x => x.workplace_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "technological_procedure",
                columns: table => new
                {
                    technological_procedure_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    technological_procedure_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    organizational_unit_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    plant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    technological_system_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__technolo__2192D019DFA30232", x => x.technological_procedure_id);
                    table.ForeignKey(
                        name: "FK__technolog__organ__4F87BD05",
                        column: x => x.organizational_unit_id,
                        principalTable: "organizational_unit",
                        principalColumn: "organizational_unit_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__technolog__plant__507BE13E",
                        column: x => x.plant_id,
                        principalTable: "plant",
                        principalColumn: "plant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__technolog__techn__51700577",
                        column: x => x.technological_system_id,
                        principalTable: "technological_system",
                        principalColumn: "technological_system_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "object_of_labor",
                columns: table => new
                {
                    object_of_labor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    object_of_labor_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    stock_quantity = table.Column<int>(type: "int", nullable: false),
                    warehouse_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__object_o__132DFBC9BD6DAD16", x => x.object_of_labor_id);
                    table.ForeignKey(
                        name: "FK__object_of__wareh__3F51553C",
                        column: x => x.warehouse_id,
                        principalTable: "warehouse",
                        principalColumn: "warehouse_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jmbg = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sallary = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    date_of_employment = table.Column<DateTime>(type: "date", nullable: false),
                    workplace_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    organizational_unit_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__employee__C52E0BA822845859", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK__employee__organi__39987BE6",
                        column: x => x.organizational_unit_id,
                        principalTable: "organizational_unit",
                        principalColumn: "organizational_unit_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__employee__workpl__38A457AD",
                        column: x => x.workplace_id,
                        principalTable: "workplace",
                        principalColumn: "workplace_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "object_of_labor_technological_procedure",
                columns: table => new
                {
                    object_of_labor_technological_procedure_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    object_of_labor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    technological_procedure_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__object_o__7D4DF5F1514AE950", x => x.object_of_labor_technological_procedure_id);
                    table.ForeignKey(
                        name: "FK__object_of__objec__544C7222",
                        column: x => x.object_of_labor_id,
                        principalTable: "object_of_labor",
                        principalColumn: "object_of_labor_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__object_of__techn__5540965B",
                        column: x => x.technological_procedure_id,
                        principalTable: "technological_procedure",
                        principalColumn: "technological_procedure_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_plan",
                columns: table => new
                {
                    production_plan_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    production_plan_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    object_of_labor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producti__F3E379D5CD0F2289", x => x.production_plan_id);
                    table.ForeignKey(
                        name: "FK__productio__objec__45FE52CB",
                        column: x => x.object_of_labor_id,
                        principalTable: "object_of_labor",
                        principalColumn: "object_of_labor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_order",
                columns: table => new
                {
                    production_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employee_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    object_of_labor_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producti__C099D22FCF047D9B", x => x.production_order_id);
                    table.ForeignKey(
                        name: "FK__productio__emplo__422DC1E7",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__productio__objec__4321E620",
                        column: x => x.object_of_labor_id,
                        principalTable: "object_of_labor",
                        principalColumn: "object_of_labor_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_employee_organizational_unit_id",
                table: "employee",
                column: "organizational_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_workplace_id",
                table: "employee",
                column: "workplace_id");

            migrationBuilder.CreateIndex(
                name: "UQ__employee__8C39FC6751DCDFA0",
                table: "employee",
                column: "jmbg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__employee__AB6E61644279CDEF",
                table: "employee",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_object_of_labor_warehouse_id",
                table: "object_of_labor",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "UQ__object_o__55DFE956C20F1A1C",
                table: "object_of_labor",
                column: "object_of_labor_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_object_of_labor_technological_procedure_object_of_labor_id",
                table: "object_of_labor_technological_procedure",
                column: "object_of_labor_id");

            migrationBuilder.CreateIndex(
                name: "IX_object_of_labor_technological_procedure_technological_procedure_id",
                table: "object_of_labor_technological_procedure",
                column: "technological_procedure_id");

            migrationBuilder.CreateIndex(
                name: "UQ__organiza__723090DBF6D2E546",
                table: "organizational_unit",
                column: "organizational_unit_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__plant__2D642453AF980F6C",
                table: "plant",
                column: "plant_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_production_order_employee_id",
                table: "production_order",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_order_object_of_labor_id",
                table: "production_order",
                column: "object_of_labor_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_plan_object_of_labor_id",
                table: "production_plan",
                column: "object_of_labor_id");

            migrationBuilder.CreateIndex(
                name: "IX_technological_procedure_organizational_unit_id",
                table: "technological_procedure",
                column: "organizational_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_technological_procedure_plant_id",
                table: "technological_procedure",
                column: "plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_technological_procedure_technological_system_id",
                table: "technological_procedure",
                column: "technological_system_id");

            migrationBuilder.CreateIndex(
                name: "UQ__technolo__07C953C8CB22F26E",
                table: "technological_procedure",
                column: "technological_procedure_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__technolo__F6E71529B53D3E02",
                table: "technological_system",
                column: "technological_system_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__workplac__F9F5552C94E5B3AF",
                table: "workplace",
                column: "workplace_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "object_of_labor_technological_procedure");

            migrationBuilder.DropTable(
                name: "production_order");

            migrationBuilder.DropTable(
                name: "production_plan");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "technological_procedure");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "object_of_labor");

            migrationBuilder.DropTable(
                name: "plant");

            migrationBuilder.DropTable(
                name: "technological_system");

            migrationBuilder.DropTable(
                name: "organizational_unit");

            migrationBuilder.DropTable(
                name: "workplace");

            migrationBuilder.DropTable(
                name: "warehouse");
        }
    }
}
