using Datamodels.Hrms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IDbContextFactory<Hrms_dbContext> _factory;

    public AuthController(IDbContextFactory<Hrms_dbContext> factory)
    {
        _factory = factory;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        using var db = _factory.CreateDbContext();

        var employee = await db.Employees
            .FirstOrDefaultAsync(x =>
                x.Username == request.Username &&
                x.Password == request.Password);

        if (employee == null)
        {
            return Unauthorized(new
            {
                message = "Invalid username or password"
            });
        }

        return Ok(new
        {
            employee.EmployeeId,
            FullNameThai = $"{employee.FirstNameThai} {employee.LastNameThai}",
            employee.Username,
            Role =
            employee.Username == "superadmin"
                ? "SuperAdmin"
                : employee.PositionId == "ADMIN"
                    ? "Admin"
                    : "User"
                });
    }
}

public class LoginRequest
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}