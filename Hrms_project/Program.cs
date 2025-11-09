using CurrieTechnologies.Razor.SweetAlert2;
using Hrms_project.Client.Pages;
using Hrms_project.Components;
using Hrms_project.Service;
using HrmsSolution.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSweetAlert2();
builder.Services.AddAntDesign();

// Configure a named API HttpClient (kept for other services)
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"] ?? throw new InvalidOperationException("API base address is not configured."));
});

// Register typed clients for services that need HttpClient
// OrganizationService requires HttpClient; register as a typed client so DI injects configured HttpClient
builder.Services.AddHttpClient<IOrganizationService, OrganizationService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"] ?? throw new InvalidOperationException("API base address is not configured."));
});

// Other service registrations (if they use their own HttpClient or typed clients, consider adding similar registrations)
builder.Services.AddScoped<IEmployeeService,EmployeeService>();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Hrms_project.Client._Imports).Assembly);

app.Run();
