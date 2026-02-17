using HrmsSolution.Service;
using Microsoft.AspNetCore.Components;

namespace Hrms_project.Components.Pages.Orignization
{
    public partial class Organization
    {
        [Inject]
        public IOrganizationService OriganiztionService { get; set; } = default!;
        public List<Datamodels.Hrms.Division> Divisions { get; set; } = new();

        //protected override void OnInitialized()
        //{
        //    divisions = OriganiztionService.GetAllDivisionsAsync();
        //}
    }
}
