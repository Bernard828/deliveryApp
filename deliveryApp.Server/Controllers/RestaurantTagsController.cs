using deliveryApp.Server.DTOs;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class RestaurantTagsController:BaseApiController
    {
        private readonly IRestaurantTagService _service;

        public RestaurantTagsController(IRestaurantTagService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<RestaurantTagDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost("{restaurantId:int}")]
        public async Task<ActionResult<List<RestaurantTagDto>>> GetByRestaurant( int restaurantId)
        {
            return Ok(await _service.GetByRestaurantAsync(restaurantId));
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantTagDto>> Create(RestaurantTagCreateDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, RestaurantTagUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> AssignToRestaurant(RestaurantTagAssignmentDto dto)
        {
            var updated = await _service.AssignToRestaurantAsync(dto);
            if (!updated) return NotFound();
            return Ok(updated);
        }
    }
}
