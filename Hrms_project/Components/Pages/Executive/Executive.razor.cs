using AntDesign;
using Datamodels.Hrms;
using HrmsSolution.Service;
using Microsoft.AspNetCore.Components;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Hrms_project.Components.Pages.Executive
{
    public partial class Executive : ComponentBase, IAsyncDisposable
    {
        [Inject] protected IManagementService ManagementService { get; set; } = default!;
        [Inject] protected IEmployeeService EmployeeService { get; set; } = default!;
        [Inject] protected SweetAlertService Swal { get; set; } = default!;

        protected List<VManagementDetail> ExecutivesList = new();
        protected List<Personnel> PersonnelList = new();
        protected Management CurrentManagement = new();

        protected bool IsLoading = true;
        protected bool IsSubmitting = false;
        protected bool IsEditing = false;
        protected bool ShowSelectionModal = false;
        protected bool ShowManagementForm = false;
        protected string PersonnelSearchTerm = "";
        protected string SelectedEmployeeName = "";

        protected override async Task OnInitializedAsync() => await LoadExecutives();

        protected async Task LoadExecutives()
        {
            IsLoading = true;
            try
            {
                var result = await ManagementService.GetAllManagementDetailsAsync();
                ExecutivesList = result?.ToList() ?? new();
            }
            finally { IsLoading = false; }
        }

        protected void OpenSelectionModal()
        {
            ShowSelectionModal = true;
            PersonnelSearchTerm = "";
            PersonnelList.Clear();
        }

        protected async Task LoadPersonnelForSelection()
        {
            var emps = await EmployeeService.GetAllEmployeesAsync();
            if (emps != null)
            {
                PersonnelList = emps
                    .Where(x => (x.FirstNameThai + " " + x.LastNameThai).Contains(PersonnelSearchTerm ?? ""))
                    .Select(x => new Personnel
                    {
                        EmployeeId = x.EmployeeId ?? "",
                        FullNameThai = $"{x.FirstNameThai} {x.LastNameThai}"
                    }).ToList();
            }
        }

        protected void SelectEmployeeForManagement(Personnel p)
        {
            IsEditing = false;
            SelectedEmployeeName = p.FullNameThai;
            CurrentManagement = new Management
            {
                ManagementId = Guid.NewGuid().ToString(),
                EmployeeId = p.EmployeeId,
                Isactive = "Y"
            };
            ShowSelectionModal = false;
            ShowManagementForm = true;
        }

        protected async Task EditExecutive(VManagementDetail view)
        {
            try
            {
                if (string.IsNullOrEmpty(view.Key)) return;

                var entity = await ManagementService.GetManagementByIdAsync(view.Key);
                if (entity != null)
                {
                    IsEditing = true;
                    SelectedEmployeeName = view.StaffNameThai ?? "";
                    CurrentManagement = entity;
                    ShowManagementForm = true;
                }
            }
            catch (Exception ex)
            {
                await Swal.FireAsync("Error", $"ไม่สามารถดึงข้อมูลได้: {ex.Message}", SweetAlertIcon.Error);
            }
        }

        protected async Task HandleSubmit()
        {
            if (string.IsNullOrEmpty(CurrentManagement.ManagementPositionId))
            {
                await Swal.FireAsync("แจ้งเตือน", "กรุณาระบุรหัสตำแหน่งบริหาร", SweetAlertIcon.Warning);
                return;
            }

            IsSubmitting = true;
            try
            {
                bool success = IsEditing
                    ? await ManagementService.UpdateManagementAsync(CurrentManagement.ManagementId!, CurrentManagement)
                    : (await ManagementService.AddManagementAsync(CurrentManagement) != null);

                if (success)
                {
                    ShowManagementForm = false;
                    await Swal.FireAsync(new SweetAlertOptions
                    {
                        Title = "สำเร็จ!",
                        Text = IsEditing ? "อัปเดตข้อมูลเรียบร้อยแล้ว" : "เพิ่มข้อมูลผู้บริหารเรียบร้อยแล้ว",
                        Icon = SweetAlertIcon.Success,
                        Timer = 2000,
                        ShowConfirmButton = false
                    });
                    await LoadExecutives();
                }
            }
            catch (Exception ex)
            {
                await Swal.FireAsync("เกิดข้อผิดพลาด", ex.Message, SweetAlertIcon.Error);
            }
            finally { IsSubmitting = false; }
        }

        protected async Task DeleteExecutive(VManagementDetail view)
        {
            var result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "ยืนยันการลบ?",
                Text = $"คุณต้องการถอดถอนคุณ {view.StaffNameThai} ออกจากตำแหน่งบริหารใช่หรือไม่?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "ใช่, ลบเลย",
                CancelButtonText = "ยกเลิก",
                ConfirmButtonColor = "#d33",
                CancelButtonColor = "#3085d6"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                if (await ManagementService.DeleteManagementAsync(view.Key!))
                {
                    await Swal.FireAsync("ลบสำเร็จ!", "ถอดถอนตำแหน่งเรียบร้อยแล้ว", SweetAlertIcon.Success);
                    await LoadExecutives();
                }
            }
        }

        public async ValueTask DisposeAsync() => await ValueTask.CompletedTask;
    }

    public class Personnel
    {
        public string EmployeeId { get; set; } = "";
        public string FullNameThai { get; set; } = "";
    }
}