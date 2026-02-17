using CurrieTechnologies.Razor.SweetAlert2;
using Hrms_project.Client.Pages;
using Hrms_project.Components;
using Hrms_project.Models;
using Hrms_project.Service;
using HrmsSolution.Service;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Registration of Services (ต้องอยู่ก่อน builder.Build()) ---

// Default HttpClient สำหรับโหลดไฟล์ Static เช่น JSON ใน wwwroot
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7177/")
});

// Add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Third-party Services
builder.Services.AddSweetAlert2();
builder.Services.AddAntDesign();
builder.Services.AddControllers();

// Named API HttpClient
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"] ?? throw new InvalidOperationException("API base address is not configured."));
});

// Typed Client: OrganizationService
builder.Services.AddHttpClient<IOrganizationService, OrganizationService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"] ?? throw new InvalidOperationException("API base address is not configured."));
});

// Business Logic Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeTitleService, EmployeeTitleService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IJobPositionService, JobPositionService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IManagementPositionService, ManagementPositionService>();
builder.Services.AddScoped<IManagementService, ManagementService>();
builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IVEmployeeDetailsService, VEmployeeDetailsService>();
builder.Services.AddScoped<IVManagementDetailsService, VManagementDetailsService>();
builder.Services.AddScoped<IWorkUnitService, WorkUnitService>();

// Localization & Auth
builder.Services.AddScoped<JsonLocalizationService>();
builder.Services.AddSingleton<AuthState>();
builder.Services.AddScoped<AuthManager>();

// ตั้งค่า Localization มาตรฐาน (ย้ายมาไว้ก่อน Build)
builder.Services.AddLocalization(options => options.ResourcesPath = "Resource");

// --- 2. Build Application ---
var app = builder.Build();

// --- 3. Configure HTTP request pipeline (Middleware) ---

// ตั้งค่า Supported Cultures
var supportedCultures = new[] { "en-US", "th-TH" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1]) // ตั้งค่าเริ่มต้นเป็น th-TH
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Hrms_project.Client._Imports).Assembly);

app.Run();