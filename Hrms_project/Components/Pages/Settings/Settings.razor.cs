using AntDesign;
using Datamodels.Hrms;
using HrmsSolution.Service;
using Microsoft.AspNetCore.Components;
using CurrieTechnologies.Razor.SweetAlert2;

namespace HrmsApp.Pages
{
    public partial class Settings : ComponentBase
    {
        [Inject] protected IDepartmentService DepartmentService { get; set; } = default!;
        [Inject] protected IManagementService ManagementService { get; set; } = default!;
        [Inject] protected SweetAlertService Swal { get; set; } = default!;

        protected List<Department> Departments = new();
        protected List<ManagementPosition> Positions = new();
        protected bool IsLoading = true;

        protected bool _deptModalVisible = false;
        protected bool _posModalVisible = false;
        protected bool _isEditMode = false;

        protected Department _editDept = new();
        protected ManagementPosition _editPos = new();

        protected override async Task OnInitializedAsync() => await LoadAllData();

        private async Task LoadAllData()
        {
            IsLoading = true;
            try
            {
                // โหลดเฉพาะแผนกและตำแหน่ง
                Departments = (await DepartmentService.GetAllDepartmentsAsync())?.ToList() ?? new();
                Positions = (await ManagementService.GetAllManagementPositionsAsync())?.ToList() ?? new();
            }
            finally
            {
                IsLoading = false;
            }
        }

        // --- Department Logic ---
        protected void ShowDeptModal(Department? dept = null)
        {
            if (dept == null) { _editDept = new(); _isEditMode = false; }
            else { _editDept = dept; _isEditMode = true; }
            _deptModalVisible = true;
        }

        protected async Task HandleDeptSubmit()
        {
            // เรียกใช้ Service ของพี่ที่เขียนไว้ก่อนหน้านี้
            _deptModalVisible = false;
            await Swal.FireAsync("สำเร็จ", "บันทึกข้อมูลแผนกเรียบร้อย", SweetAlertIcon.Success);
            await LoadAllData();
        }

        protected async Task DeleteDept(string id)
        {
            var res = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "ลบแผนกนี้?",
                Text = "ระวัง: แผนกที่มีพนักงานสังกัดอยู่จะไม่สามารถลบได้",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });
            if (res.IsConfirmed) await LoadAllData();
        }

        // --- Position Logic ---
        protected void ShowPosModal(ManagementPosition? pos = null)
        {
            if (pos == null) { _editPos = new(); _isEditMode = false; }
            else { _editPos = pos; _isEditMode = true; }
            _posModalVisible = true;
        }
    }
}