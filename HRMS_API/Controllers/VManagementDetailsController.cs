using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VManagementDetailsController : ControllerBase
    {
        private readonly VManagementDetailsService _viewService;

        public VManagementDetailsController(VManagementDetailsService viewService)
        {
            _viewService = viewService;
        }

        // GET: api/VManagementDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VManagementDetail>>> GetAllManagementDetails()
        {
            var data = await _viewService.GetAllManagementDetailsAsync();
            return Ok(data);
        }

        // GET: api/VManagementDetails/ByStaff/12345 (ค้นหาด้วย StaffId)
        [HttpGet("ByStaff/{staffId}")]
        public async Task<ActionResult<VManagementDetail>> GetManagementDetailsByStaff(string staffId)
        {
            var data = await _viewService.GetManagementDetailsByStaffIdAsync(staffId);
            return data == null ? NotFound() : Ok(data);
        }

        // GET: api/VManagementDetails/ByKey/guid-id (ค้นหาด้วย ManagementId)
        [HttpGet("ByKey/{key}")]
        public async Task<ActionResult<VManagementDetail>> GetManagementDetailsByKey(string key)
        {
            var data = await _viewService.GetManagementDetailsByKeyAsync(key);
            return data == null ? NotFound() : Ok(data);
        }

        // GET: api/VManagementDetails/Search/ภานุวัฒน์
        [HttpGet("Search/{name}")]
        public async Task<ActionResult<IEnumerable<VManagementDetail>>> SearchManagement(string name)
        {
            var data = await _viewService.SearchManagementByNameAsync(name);
            return Ok(data);
        }
    }
}