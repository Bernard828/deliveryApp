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

        [HttpGet("{restaurantId:int}")]
        public async Task<ActionResult<List<MenuItemDto>>> GetByRestaurant(int restaurantId)
        {
            return Ok(await _service.GetByRestaurantAsync(restaurantId);
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
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            //var item = await _service.CreateAsync(dto);

            //if (item == null)
            //{
            //    return NotFound(new
            //    {
            //        Message = "Restaurant not found."
            //    });
            //}

            //return CreatedAtAction(
            //    nameof(GetById),
            //    new { id = item.MenuItemId },
            //    item);
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] MenuItemUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success) return NotFound(new { Message = "Target removal menu item was not found." });

            return NoContent();
        }
    }
}