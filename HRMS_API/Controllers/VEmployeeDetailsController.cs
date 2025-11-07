using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/VEmployeeDetails
    public class VEmployeeDetailsController : ControllerBase
    {
        private readonly VEmployeeDetailsService _viewService;

        public VEmployeeDetailsController(VEmployeeDetailsService viewService)
        {
            _viewService = viewService;
        }

        // GET: api/VEmployeeDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VEmployeeDetail>>> GetAllEmployeeDetails()
        {
            var data = await _viewService.GetAllEmployeeDetailsAsync();
            return Ok(data);
        }

        // GET: api/VEmployeeDetails/1234567 (ค้นหาด้วย EmployeeId)
        [HttpGet("{id}")]
        public async Task<ActionResult<VEmployeeDetail>> GetEmployeeDetails(string id)
        {
            var data = await _viewService.GetEmployeeDetailsByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // GET: api/VEmployeeDetails/Search/somchai
        [HttpGet("Search/{name}")]
        public async Task<ActionResult<IEnumerable<VEmployeeDetail>>> SearchEmployees(string name)
        {
            var data = await _viewService.SearchEmployeesByNameAsync(name);
            return Ok(data);
        }
    }
}