using CurrieTechnologies.Razor.SweetAlert2;

namespace HrmsSolution.Components.Layout
{
    public partial class NavMenu
    {
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private async Task ShowLogoutConfirmation()
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "ออกจากระบบ",
                Text = "ขอบคุณที่ใช้บริการ กำลังเข้าสู่หน้าเข้าสู่ระบบ",
                Icon = SweetAlertIcon.Success,
                Timer = 3000,
                ShowConfirmButton = false
            });
        }
    }
}
