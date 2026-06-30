using deliveryApp.Server.Models;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class MenuItemController : BaseApiController
    {
        private readonly IMenuItemService _service;

        public MenuItemController(IMenuItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetByRestaurant(int restaurantId)
        {
            var items = await _service.GetByRestaurantIdAsync(restaurantId);
            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromBody] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query cannot be empty.");
            var results = await _service.SearchAsync(query);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuItemCreateDto dto)
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), 
                new { id = item.MenuItemId },
                item);
        }

        [HttpPut]
        public async Task<IActionResult>Update(int id,MenuItemUpdateDto dto)
        {
            if (id != dto.MenuItemId)
                return BadRequest("Mismatch ID.");
            var success = await _service.UpdateAsync(dto);
            if (!success) return NotFound();
            return Ok(success);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if(!success) return NotFound();
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTags(int id, [FromBody]List<string> tags)
        {
            if (tags == null || !tags.Any())
                return BadRequest("Tags list cannot be empty.");
            var success = await _service.Up
        }
    }
}
