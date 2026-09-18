using deliveryApp.Server.DTOs;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class OperatingHoursController:BaseApiController
    {
        private readonly IOperatingHoursService _service;
        public OperatingHoursController(OperatingHoursService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OperatingHourDto>>> GetByRestaurant(int restaurantId)
        {
            return Ok(await _service.GetByRestaurantAsync(restaurantId))
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, OperatingHourUpdateDto dto)
        {
            var update = await _service.UpdateAsync(id, dto);
            return update ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
