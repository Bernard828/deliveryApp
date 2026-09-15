using deliveryApp.Server.DTOs;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deliveryApp.Server.Controllers
{

    public class MenuItemController : BaseApiController
    {
        private readonly IMenuItemService _service;

        public MenuItemController(IMenuItemService service)
        {
            _service = service;
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<List<MenuItemDto>>> GetByRestaurant(int restaurantId)
        {
            if (restaurantId <= 0)
            {
                return BadRequest(new
                {
                    Message = "Restaurant ID must be a positive integer."
                });
            }
            var items = await _service.GetByRestaurantAsync(restaurantId);

            return Ok(items);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound(new
                {
                    Message = $"Menu item with ID {id} was not found."
                });
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create([FromBody] MenuItemCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = await _service.CreateAsync(dto);

            if (item == null)
            {
                return NotFound(new
                {
                    Message = "Restaurant not found."
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = item.MenuItemId },
                item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuItemUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.MenuItemId) return BadRequest(new { Message = "Route ID parameter does not match payload object ID." });

            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound(new
            {
                Message = "Target modification menu item was not found."
            });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success) return NotFound(new { Message = "Target removal menu item was not found." });

            return NoContent();
        }
    }
}