using deliveryApp.Server.Models;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemservice;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemservice = menuItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetByRestaurant(int restaurantId)
        {
            return Ok(await _menuItemservice.GetMenuByRestaurantIdAsync(restaurantId));
        }

        [HttpGet]
        public async Task<ActionResult<MenuItem>> Get(int id)
        {
            var item = await _menuItemservice.GetByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItem>> Create([FromBody] MenuItem item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _menuItemservice.CreateAsync(item);
            return CreatedAtAction(nameof(Get), new
            {
                id = created.MenuItemId
            }, created);
        }

        [HttpPut]
        public async Task<IActionResult>Update(int id, [FromBody] MenuItem item)
        {
            var success = await _menuItemservice.UpdateAsync(id, item);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _menuItemservice.DeleteAsync(id);
            if(!success) return BadRequest();
            return NoContent();

        }
    }
}
