using CurrieTechnologies.Razor.SweetAlert2;
using Datamodels.Hrms;
using HrmsSolution.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
// (ลบ using ที่ไม่ได้ใช้ออก)

namespace HrmsSolution.Components.Pages
{
    public partial class EmployeeList
    {
        // (แก้ไข 1) Inject Service ที่ถูกต้อง (IVEmployeeDetailsService)
        [Inject]
        private IVEmployeeDetailsService EmployeeViewService { get; set; }
        [Inject]
        private IJSRuntime JS { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        // (แก้ไข 2) เพิ่ม Inject ของ SweetAlertService
        [Inject]
        private SweetAlertService Swal { get; set; }

        private List<VEmployeeDetail> employees;
        private bool isLoading = true;

        // (แก้ไข 3) เพิ่มตัวแปรสำหรับเก็บ Error Message (สำหรับ Prerendering)
        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                // (โค้ดนี้ถูกต้องแล้ว เพราะเรา Inject 'EmployeeViewService' มาแล้ว)
                employees = await EmployeeViewService.GetAllEmployeeDetailsAsync();
            }
            catch (Exception ex)
            {
                // (แก้ไข 3) อย่าเพิ่งเรียก Swal!
                // ให้เก็บ Error Message ไว้ในตัวแปรแทน
                errorMessage = $"ไม่สามารถโหลดข้อมูลพนักงานได้: {ex.Message}";
            }
            finally
            {
                isLoading = false;
            }
        }

        // (แก้ไข 3) เพิ่ม OnAfterRenderAsync เพื่อเรียก Swal (JavaScript)
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // เช็คว่าเป็นการ Render ครั้งแรก และมี Error ค้างอยู่หรือไม่
            if (firstRender && !string.IsNullOrEmpty(errorMessage))
            {
                // (ปลอดภัย) ตอนนี้สามารถเรียก JavaScript (Swal) ได้แล้ว
                await Swal.FireAsync(
                    "เกิดข้อผิดพลาด",
                    errorMessage,
                    SweetAlertIcon.Error);

                errorMessage = null; // เคลียร์ Error
            }
        }

        private Task OnViewDetails(VEmployeeDetail item)
        {
            // (Logic การดูรายละเอียด)
            // (เช่น navigationManager.NavigateTo($"/employee/edit/{item.EmployeeId}"))
            return Task.CompletedTask; // คืนค่า Task
        }

        private async Task ToAddEmployee()
        {
            await JS.InvokeVoidAsync("console.log", "Navigating to Add Employee page...");
        }
    }
}