namespace Hrms_project.Models
{
    public class AuthState
    {
        public bool IsLoggedIn { get; set; } = false;
        public string Role { get; set; } = "Guest";

        public void Login(string role)
        {
            IsLoggedIn = true;
            Role = role;
        }

        public void Logout()
        {
            IsLoggedIn = false;
            Role = "Guest";
        }

        // เพิ่มอันนี้เข้าไปเพื่อแก้ Error 'SetState'
        public void SetState(bool isLoggedIn, string role)
        {
            IsLoggedIn = isLoggedIn;
            Role = role;
        }
    }
}