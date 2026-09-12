using deliveryApp.Server.DTOs;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deliveryApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _service;

        public MenuItemController(IMenuItemService service)
        {
            _service = service;
        }

        // POST: api/MenuItem
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuItemCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = item.MenuItemId }, item);
        }

        // PUT: api/MenuItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuItemUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.MenuItemId) return BadRequest(new { Message = "Route ID parameter does not match payload object ID." });

            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "Target modification menu item was not found." });

            return NoContent();
        }

        // DELETE: api/MenuItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Target removal menu item was not found." });

            return NoContent();
        }

        // GET: api/MenuItem/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound(new { Message = $"Menu item with ID {id} was not found." });

            return Ok(item);
        }

        // GET: api/MenuItem/restaurant/5
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetByRestaurant(int restaurantId)
        {
            var items = await _service.GetByRestaurantIdAsync(restaurantId);
            return Ok(items);
        }

        // GET: api/MenuItem/search?query=pizza
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return BadRequest(new { Message = "Query context text string parameters cannot be left blank." });

            var results = await _service.SearchAsync(query);
            return Ok(results);
        }

        // PUT: api/MenuItem/5/tag
        [HttpPut("{id}/tag")]
        public async Task<IActionResult> UpdateTags(int id, [FromBody] List<string> tags)
        {
            if (tags == null || !tags.Any()) return BadRequest(new { Message = "Array payload collection cannot be empty." });

            var success = await _service.UpdateTagsAsync(id, tags);
            if (!success) return NotFound(new { Message = "Target collection record item not found." });

            return NoContent();
        }

        // Logan DELETE endpoint mapping fix
        // DELETE: api/MenuItem/5/tagDelete
        [HttpPost("{id}/tagDelete")]
        public async Task<IActionResult> DeleteTags(int id, [FromBody] List<string> tags)
        {
            if (tags == null || !tags.Any()) return BadRequest(new { Message = "Array removal criteria list must hold values." });

            var success = await _service.DeleteTagsAsync(id, tags);
            if (!success) return NotFound(new { Message = "Target record update criteria item not found." });

            return NoContent();
        }

        // GET: api/MenuItem/paged?page=1&pageSize=10
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var results = await _service.GetPagedAsync(page, pageSize);
            return Ok(results);
        }

        // PUT: api/MenuItem/batch
        [HttpPut("batch")]
        public async Task<IActionResult> BatchUpdate([FromBody] List<MenuItemUpdateDto> items)
        {
            if (items == null || !items.Any()) return BadRequest(new { Message = "Bulk array request payload cannot be empty." });

            var success = await _service.BatchUpdateAsync(items);
            if (!success) return NotFound(new { Message = "No targeted modification records were matched inside persistence storage structures." });

            return NoContent();
        }
    }
}


//using deliveryApp.Server.Data;
//using deliveryApp.Server.Models;
//using deliveryApp.Server.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace deliveryApp.Server.Controllers
//{
//    public class MenuItemController : BaseApiController
//    {
//        private readonly DeliveryAppDbContext _context;
//        public MenuItemController(DeliveryAppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(MenuItemCreateDto dto)
//        {
//            var item = await _context.MenuItems.CreateAsync(dto);
//            return CreatedAtAction(nameof(GetById),
//                new { id = item.MenuItemId },
//                item);
//        }

//        [HttpPut]
//        public async Task<IActionResult> Update(int id, MenuItemUpdateDto dto)
//        {
//            if (id != dto.MenuItemId)
//                return BadRequest("Mismatch ID.");
//            var success = await _service.UpdateAsync(id, dto);
//            if (!success) return NotFound();
//            return Ok(success);
//        }

//        [HttpDelete]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var success = await _service.DeleteAsync(id);
//            if (!success) return NotFound();
//            return NoContent();
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var item = await _service.GetByIdAsync(id);
//            if (item == null) return NotFound();
//            return Ok(item);
//        }

//        [HttpGet("restaurantId")]
//        public async Task<IActionResult> GetByRestaurant(int restaurantId)
//        {
//            var items = await _service.GetByRestaurantIdAsync(restaurantId);
//            return Ok(items);
//        }

//        //[HttpGet]
//        //public async Task<IActionResult> Search([FromBody] string query)
//        //{
//        //    if (string.IsNullOrWhiteSpace(query))
//        //        return BadRequest("Query cannot be empty.");
//        //    var results = await _service.SearchAsync(query);
//        //    return Ok(results);
//        //}



//        //[HttpPut("tag")]
//        //public async Task<IActionResult> UpdateTags(int id, [FromBody] List<string> tags)
//        //{
//        //    if (tags == null || !tags.Any())
//        //        return BadRequest("Tags list cannot be empty.");
//        //    var success = await _service.UpdateTagsAsync(id, tags);
//        //    if (!success) return NotFound();
//        //    return NoContent();
//        //}

//        //[HttpDelete("tagDelete")]
//        //public async Task<IActionResult> DeleteTags(int id, [FromBody] List<string> tags)
//        //{
//        //    if (tags == null || !tags.Any())
//        //        return BadRequest("Tag list cannot be empty.");

//        //    var success = await _service.DeleteTagsAsync(id, tags);
//        //    if (!success) return NotFound();
//        //    return NoContent();
//        //}

//        //[HttpGet("paged")]
//        //public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
//        //{
//        //    var results = await _service.GetPagedAsync(page, pageSize);
//        //    return Ok(results);
//        //}
//        //[HttpPut("batch")]
//        //public async Task<IActionResult> BatchUpdate([FromBody] List<MenuItemUpdateDto> items)
//        //{
//        //    if (items == null || !items.Any())
//        //        return BadRequest("Update list cannot be empty.");
//        //    var success = await _service.BatchUpdateAsync(items);
//        //    if (!success) return NotFound("No matching menu items found.");
//        //    return NoContent();
//        //}
//    }
//}
