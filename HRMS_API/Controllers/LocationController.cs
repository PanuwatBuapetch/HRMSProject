using Datamodels.Hrms;
using HRMS_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Location
    public class LocationController : ControllerBase
    {
        // 1. เปลี่ยน Service ที่ฉีด
        private readonly LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }

        // 2. เปลี่ยน Model ที่รับ/ส่ง
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {
            var items = await _locationService.GetAllLocationsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(string id)
        {
            var item = await _locationService.GetLocationByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Location>> AddLocation(Location location)
        {
            var newItem = await _locationService.AddLocationAsync(location);
            return CreatedAtAction(nameof(GetLocation), new { id = newItem.LocationId }, newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(string id, Location location)
        {
            if (id != location.LocationId) return BadRequest();
            var success = await _locationService.UpdateLocationAsync(id, location);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(string id)
        {
            var success = await _locationService.DeleteLocationAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}