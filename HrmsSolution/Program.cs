using CurrieTechnologies.Razor.SweetAlert2;
using HrmsAppSolution.Services;
using HrmsSolution.Client.Pages;
using HrmsSolution.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// --- 1. ????? Services ????????? ---
builder.Services.AddSweetAlert2();
builder.Services.AddAntDesign(); // <-- [?????] ????? Service ??? AntDesign ?????????
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7160") // ✅ URL ของ HRMS_API
});

builder.Services.AddScoped<CampusService>(); // ⬅️ อันนี้คือ Blazor Service เรียก API
builder.Services.AddScoped<EmployeeService>(); // ⬅️ อันนี้คือ Blazor Service เรียก API
//builder.Services.AddScoped<StaffDetailService>(); // ⬅️ อันนี้คือ Blazor Service เรียก API

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


var cultures = builder.Configuration.GetSection("Cultures").GetChildren().ToDictionary(x => x.Key, x => x.Value);
var supportedCultures = cultures.Keys.ToArray();
var localizationOptions = new RequestLocalizationOptions()
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);


// --- 3. Map Razor Components ---
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(HrmsSolution.Client._Imports).Assembly);

app.Run();