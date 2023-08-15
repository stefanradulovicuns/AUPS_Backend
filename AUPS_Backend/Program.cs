using AUPS_Backend.Entities;
using AUPS_Backend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IWorkplaceRepository, WorkplaceRepository>();
builder.Services.AddScoped<IOrganizationalUnitRepository, OrganizationalUnitRepository>();
builder.Services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
builder.Services.AddScoped<IObjectOfLaborRepository, ObjectOfLaborRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddDbContext<AupsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AUPSDb"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
