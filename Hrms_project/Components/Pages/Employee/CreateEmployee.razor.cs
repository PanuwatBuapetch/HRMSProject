using CurrieTechnologies.Razor.SweetAlert2;
using Datamodels.Hrms;
using Hrms_project.Service;
using HrmsSolution.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Hrms_project.Components.Pages.Employee
{
    public partial class CreateEmployee
    {
        [Inject] public IEmployeeService EmployeeService { get; set; } = default!;
        [Inject] public IOrganizationService OrganizationService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public SweetAlertService Swal { get; set; } = default!;
        [Inject] public JsonLocalizationService locallizer { get; set; } = default!;

        public Datamodels.Hrms.Employee NewEmployee { get; set; } = new();
        private EditContext? editContext;

        private int current = 0;
        private List<Division> divisions = new();
        private List<Department> departments = new();
        private List<WorkUnit> workUnits = new();

        [SupplyParameterFromQuery] public string? DivisionId { get; set; }
        [SupplyParameterFromQuery] public string? DeptId { get; set; }

        public List<StepItem> Steps = new List<StepItem>
        {
            new StepItem { Title = "ข้อมูลส่วนตัว", Icon = "user" },
            new StepItem { Title = "การจ้างงาน", Icon = "idcard" },
            new StepItem { Title = "ติดต่อ", Icon = "phone" },
            new StepItem { Title = "สรุป", Icon = "check-circle" }
        };

        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(NewEmployee);
            try { divisions = await OrganizationService.GetAllDivisionsAsync() ?? new(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            if (!string.IsNullOrEmpty(DivisionId))
            {
                NewEmployee.DivisionId = DivisionId;
                departments = await OrganizationService.GetDepartmentsByDivisionIdAsync(DivisionId) ?? new();
                if (!string.IsNullOrEmpty(DeptId))
                {
                    NewEmployee.DeptId = DeptId;
                    workUnits = await OrganizationService.GetWorkUnitsByDepartmentIdAsync(DeptId) ?? new();
                }
            }
        }

        private async Task OnDivisionChanged(string divisionId)
        {
            NewEmployee.DivisionId = divisionId;
            NewEmployee.DeptId = null;
            NewEmployee.UnitId = null;
            departments.Clear();
            workUnits.Clear();
            if (!string.IsNullOrEmpty(divisionId))
                departments = await OrganizationService.GetDepartmentsByDivisionIdAsync(divisionId) ?? new();
        }

        private async Task OnDepartmentChanged(string deptId)
        {
            NewEmployee.DeptId = deptId;
            NewEmployee.UnitId = null;
            workUnits.Clear();
            if (!string.IsNullOrEmpty(deptId))
                workUnits = await OrganizationService.GetWorkUnitsByDepartmentIdAsync(deptId) ?? new();
        }

        private void Next() { if (ValidateCurrentStep()) current++; }
        private void Prev() { if (current > 0) current--; }

        private bool ValidateCurrentStep()
        {
            if (current == 0)
            {
                if (string.IsNullOrWhiteSpace(NewEmployee.FirstNameThai) || string.IsNullOrWhiteSpace(NewEmployee.LastNameThai))
                {
                    Swal.FireAsync("ข้อมูลไม่ครบถ้วน", "กรุณาระบุ ชื่อ-นามสกุล (ภาษาไทย)", SweetAlertIcon.Warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(NewEmployee.CitizenId) || NewEmployee.CitizenId.Length != 13)
                {
                    Swal.FireAsync("ข้อมูลผิดพลาด", "กรุณาระบุเลขบัตรประชาชนให้ครบ 13 หลัก", SweetAlertIcon.Warning);
                    return false;
                }
            }
            else if (current == 1)
            {
                if (string.IsNullOrWhiteSpace(NewEmployee.DivisionId))
                {
                    Swal.FireAsync("ข้อมูลไม่ครบถ้วน", "กรุณาเลือกสังกัด (สำนัก/กอง)", SweetAlertIcon.Warning);
                    return false;
                }
            }
            else if (current == 2)
            {
                if (string.IsNullOrWhiteSpace(NewEmployee.Email))
                {
                    Swal.FireAsync("ข้อมูลไม่ครบถ้วน", "กรุณาระบุอีเมล", SweetAlertIcon.Warning);
                    return false;
                }
                if (!NewEmployee.Email.Contains("@"))
                {
                    Swal.FireAsync("รูปแบบไม่ถูกต้อง", "กรุณาระบุอีเมลให้ถูกต้อง", SweetAlertIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private string GetDivisionName(string? id) => divisions.FirstOrDefault(d => d.DivisionId == id)?.DivisionNameThai ?? "-";
        private string GetDeptName(string? id) => departments.FirstOrDefault(d => d.DeptId == id)?.DeptNameThai ?? "-";
        private string GetUnitName(string? id) => workUnits.FirstOrDefault(u => u.UnitId == id)?.UnitNameThai ?? "-";

        private async Task HandleSubmit()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "ยืนยันการบันทึก?", Text = "กรุณาตรวจสอบข้อมูล", Icon = SweetAlertIcon.Question, ShowCancelButton = true, ConfirmButtonText = "ยืนยัน" });
            if (!string.IsNullOrEmpty(confirm.Value))
            {
                try
                {
                    if (string.IsNullOrEmpty(NewEmployee.EmployeeId)) NewEmployee.EmployeeId = Guid.NewGuid().ToString();

                    // แปลงค่าว่างให้เป็น null ก่อนส่ง (กัน Error Database)
                    if (string.IsNullOrWhiteSpace(NewEmployee.UnitId)) NewEmployee.UnitId = null;
                    if (string.IsNullOrWhiteSpace(NewEmployee.PositionId)) NewEmployee.PositionId = null;

                    await EmployeeService.AddEmployeeAsync(NewEmployee);
                    await Swal.FireAsync("สำเร็จ", "เพิ่มบุคลากรเรียบร้อยแล้ว", SweetAlertIcon.Success);
                    NavigationManager.NavigateTo("/organization");
                }
                catch (Exception ex)
                {
                    await Swal.FireAsync("ผิดพลาด", $"บันทึกไม่สำเร็จ: {ex.Message}", SweetAlertIcon.Error);
                }
            }
        }

        public class StepItem { public string Title { get; set; } public string Icon { get; set; } }
    }
}