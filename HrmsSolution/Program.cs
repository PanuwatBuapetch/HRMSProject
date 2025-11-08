using CurrieTechnologies.Razor.SweetAlert2;
using HrmsSolution.Client.Pages;
using HrmsSolution.Components;
using HrmsSolution.Service; // 👈 (อย่าลืม using Service ของคุณ)

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// --- 1. เพิ่ม Services ที่จำเป็น ---
builder.Services.AddSweetAlert2();
builder.Services.AddAntDesign();


// --- (แก้ไขจุดนี้ครับ) ---
// 
// ❌ (ลบบรรทัดนี้ทิ้ง)
// builder.Services.AddScoped(sp => new HttpClient
// {
//     BaseAddress = new Uri("https://localhost:7160") 
// });

// ✅ (ให้ใช้บรรทัดนี้แทน)
// นี่คือการลงทะเบียน IHttpClientFactory และตั้งชื่อ Client ว่า "Api"
// Service ทั้ง 13 ตัวของคุณ (ที่ใช้ CreateClient("Api")) จะทำงานได้ทันที
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7160"); // ✅ URL ของ HRMS_API
});
// --- (สิ้นสุดการแก้ไข) ---


// --- 2. ลงทะเบียน Client Services (ส่วนนี้ถูกต้องแล้ว) ---
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IDivisionService, DivisionService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IJobPositionService, JobPositionService>();
builder.Services.AddScoped<IEmployeeTitleService, EmployeeTitleService>();
builder.Services.AddScoped<IManagementService, ManagementService>();
builder.Services.AddScoped<IManagementPositionService, ManagementPositionService>();
builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<IWorkUnitService, WorkUnitService>();
builder.Services.AddScoped<IVEmployeeDetailsService, VEmployeeDetailsService>();
builder.Services.AddScoped<IVManagementDetailsService, VManagementDetailsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// (ส่วนของ Localization ย้ายมาอยู่ตรงนี้ ถูกต้องแล้ว)
var cultures = builder.Configuration.GetSection("Cultures").GetChildren().ToDictionary(x => x.Key, x => x.Value);
var supportedCultures = cultures.Keys.ToArray();
var localizationOptions = new RequestLocalizationOptions().AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);


// --- 3. Map Razor Components ---
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(HrmsSolution.Client._Imports).Assembly);

app.Run();