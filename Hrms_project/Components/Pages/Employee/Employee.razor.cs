using CurrieTechnologies.Razor.SweetAlert2;
using Hrms_project.Service;
using HrmsSolution.Service;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms_project.Pages
{
    public partial class Employee : ComponentBase, IDisposable
    {
        [Inject] private IEmployeeService EmployeeService { get; set; }
        [Inject] private JsonLocalizationService locallizer { get; set; }
        [Inject] private SweetAlertService Swal { get; set; }
        [Inject] private NavigationManager NavigationContext { get; set; }

        public List<Datamodels.Hrms.Employee> employees = new();
        public string searchText = "";
        public bool isLoading = true;
        public bool isViewModalVisible = false;
        public Datamodels.Hrms.Employee? selectedEmployee;

        protected override async Task OnInitializedAsync()
        {
            locallizer.OnLanguageChanged += HandleLanguageChanged;
            await LoadEmployees();
        }

        private void HandleLanguageChanged() => InvokeAsync(StateHasChanged);

        public void Dispose()
        {
            locallizer.OnLanguageChanged -= HandleLanguageChanged;
        }

        private async Task LoadEmployees()
        {
            try
            {
                isLoading = true;
                employees = await EmployeeService.GetAllEmployeesAsync();
            }
            catch (Exception)
            {
                await Swal.FireAsync(locallizer["เกิดข้อผิดพลาด"], locallizer["เกิดข้อผิดพลาดทางระบบ"], SweetAlertIcon.Error);
            }
            finally { isLoading = false; }
        }

        public IEnumerable<Datamodels.Hrms.Employee> FilteredEmployees
        {
            get
            {
                var filtered = employees.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    filtered = filtered.Where(e =>
                        $"{e.FirstNameThai} {e.LastNameThai}".Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        (e.CitizenId ?? "").Contains(searchText));
                }
                return filtered;
            }
        }

        public void ShowAddModal()
        {
            NavigationContext.NavigateTo("/employees/create");
        }

        public void ViewEmployeeDetail(string employeeId)
        {
            selectedEmployee = employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (selectedEmployee != null)
            {
                isViewModalVisible = true;
                StateHasChanged();
            }
        }

        public async Task DeleteEmployee(Datamodels.Hrms.Employee emp)
        {
            var result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = locallizer["คุณต้องการลบข้อมูลหรือไม่ ?"],
                Text = $"{locallizer["คุณต้องการลบข้อมูล คุณ"]} {emp.FirstNameThai}",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = locallizer["ตกลง"],
                CancelButtonText = locallizer["ยกเลิก"]
            });

            if (result.IsConfirmed)
            {
                var isDeleted = await EmployeeService.DeleteEmployeeAsync(emp.EmployeeId);
                if (isDeleted)
                {
                    await LoadEmployees();
                    await Swal.FireAsync(locallizer["เสร็จสิ้น"], locallizer["คุณลบข้อมูลบุคลากรเสร็จสิ้น"], SweetAlertIcon.Success);
                }
            }
        }
    }
}