using Datamodels.Hrms;
using HRMS_API.Service;
using HRMS_API.Workflows; // อย่าลืมใส่ Namespace ของ Workflow ที่คุณสร้างไว้
using HRMS_API.Workflows.Steps;
using Microsoft.EntityFrameworkCore;
using WorkflowCore.Interface;
Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

// --- 1. ส่วนของ Services (ต้องอยู่ก่อน builder.Build) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ส่วนของ Workflow Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddWorkflow(cfg =>
{
    cfg.UsePostgreSQL(connectionString, true, true);
});

// ส่วนของ DbContext
builder.Services.AddDbContextFactory<Hrms_dbContext>(options =>
    options.UseNpgsql(connectionString));

// ส่วนของ Service อื่นๆ
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
builder.Services.AddScoped<VEmployeeDetailsService>();
builder.Services.AddScoped<VManagementDetailsService>();
builder.Services.AddScoped<IOrganizationStructureService, OrganizationStructureService>();



builder.Services.AddTransient<UpdateStatusStep>();


// --- 2. Build Application ---
var app = builder.Build();

// --- 3. ลงทะเบียน Workflow และ Start Host ---
var host = app.Services.GetService<IWorkflowHost>();
host.RegisterWorkflow<EmployeeDocumentWorkflow>(); // ชื่อ Class Workflow ของคุณ
host.Start();

// --- 4. Configure Pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();