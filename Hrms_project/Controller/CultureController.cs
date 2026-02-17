using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Hrms_project.Controllers
{
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (culture != null)
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1),
                        Path = "/",
                        SameSite = SameSiteMode.Lax
                    }
                );
            }

            // Decode redirectUri และถ้าว่างให้กลับไปหน้าแรก
            if (string.IsNullOrEmpty(redirectUri))
            {
                redirectUri = "/";
            }
            else
            {
                redirectUri = HttpUtility.UrlDecode(redirectUri);
            }

            return Redirect(redirectUri);
        }
    }
}