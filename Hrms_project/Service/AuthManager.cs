using Hrms_project.Models;
using Microsoft.JSInterop;

namespace Hrms_project.Service
{
    public class AuthManager
    {
        private readonly IJSRuntime _js;
        private readonly AuthState _state;

        public AuthManager(IJSRuntime js, AuthState state)
        {
            _js = js;
            _state = state;
        }

        public async Task Login(string role)
        {
            // บันทึกลง LocalStorage
            await _js.InvokeVoidAsync("localStorage.setItem", "userRole", role);
            await _js.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "true");

            _state.SetState(true, role);
        }

        public async Task Logout()
        {
            // ลบออกจาก LocalStorage
            await _js.InvokeVoidAsync("localStorage.removeItem", "userRole");
            await _js.InvokeVoidAsync("localStorage.removeItem", "isLoggedIn");

            _state.SetState(false, "Guest");
        }

        public async Task Initialize()
        {
            // โหลดข้อมูลจาก LocalStorage ตอนเปิดเว็บ
            var role = await _js.InvokeAsync<string>("localStorage.getItem", "userRole");
            var isLoggedInStr = await _js.InvokeAsync<string>("localStorage.getItem", "isLoggedIn");

            if (isLoggedInStr == "true" && !string.IsNullOrEmpty(role))
            {
                _state.SetState(true, role);
            }
        }
    }
}
