using Microsoft.AspNetCore.Components;
using Hrms_project.Service;
using CurrieTechnologies.Razor.SweetAlert2;

namespace Hrms_project.Components.Layout
{
    public partial class NavMenu : IAsyncDisposable
    {
        [Inject] protected SweetAlertService Swal { get; set; } = default!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] protected JsonLocalizationService locallizer { get; set; } = default!;

        private bool navMenuOpen = false;

        protected string NavMenuCssClass => navMenuOpen ? "collapse navbar-collapse show" : "collapse navbar-collapse";

        protected void ToggleNavMenu()
        {
            navMenuOpen = !navMenuOpen;
        }

        protected override void OnInitialized()
        {
            // ลงทะเบียนรับการแจ้งเตือนเมื่อเปลี่ยนภาษา
            locallizer.OnLanguageChanged += HandleLanguageChanged;
        }

        private void HandleLanguageChanged()
        {
            // สั่งให้ UI วาดใหม่เมื่อเปลี่ยนภาษา
            InvokeAsync(StateHasChanged);
        }

        protected async Task ShowLogoutConfirmation()
        {
            var result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = locallizer["ออกจากระบบ"],
                Text = locallizer["คุณต้องการออกจากระบบหรือไม่ ?"],
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                ConfirmButtonText = locallizer["ตกลง"],
                CancelButtonText = locallizer["ยกเลิก"]
            });

            if (result.IsConfirmed)
            {
                NavigationManager.NavigateTo("/login", forceLoad: true);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (locallizer != null)
            {
                // ถอนการลงทะเบียนเพื่อป้องกัน Memory Leak
                locallizer.OnLanguageChanged -= HandleLanguageChanged;
            }
            await ValueTask.CompletedTask;
        }
    }
}