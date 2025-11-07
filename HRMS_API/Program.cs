using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//✅ Register DbContextFactory & Service
builder.Services.AddDbContextFactory<Hrms_dbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<DivisionService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<JobPositionService>();
builder.Services.AddScoped<EmployeeTitleService>();
builder.Services.AddScoped<ManagementService>();
builder.Services.AddScoped<ManagementPositionService>();
builder.Services.AddScoped<MissionService>();
builder.Services.AddScoped<WorkUnitService>();
// (Services สำหรับ Views)
builder.Services.AddScoped<VEmployeeDetailsService>();
builder.Services.AddScoped<VManagementDetailsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
