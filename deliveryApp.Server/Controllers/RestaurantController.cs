using deliveryApp.Server.DTOs;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{

    public class RestaurantsController : BaseApiController
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll(
            [FromQuery] bool? activeOnly,
            [FromQuery] int? cuisineTypeId)
        {
            var records = await _restaurantService.GetAllAsync(activeOnly, cuisineTypeId);
            return Ok(records);
        }


        [HttpGet("paged")]
        public async Task<ActionResult<PagedResultDto<RestaurantDto>>> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool? isActive = null)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new { Message = "Page parameters must be greater than zero." });
            }

            var result = await _restaurantService.GetPagedAsync(
                page,
                pageSize,
                isActive);

            return Ok(result);
        }

        // GET: api/restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById(int id)
        {
            var record = await _restaurantService.GetByIdAsync(id);
            if (record == null)
            {
                return NotFound(new { Message = $"Restaurant with ID {id} was not found." });
            }
            return Ok(record);
        }

        // POST: api/restaurants
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> Create([FromBody] RestaurantCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRecord = await _restaurantService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdRecord.RestaurantId }, createdRecord);
        }

        // PUT: api/restaurants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RestaurantUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.RestaurantId)
            {
                return BadRequest(new { Message = "Route identifier ID must match target payload ID." });
            }

            var success = await _restaurantService.UpdateAsync(id, dto);
            if (!success)
            {
                return NotFound(new { Message = $"Target modification record ID {id} not found." });
            }

            return NoContent();
        }

        // PATCH: api/restaurants/5/toggle-active
        [HttpPatch("{id}/toggle-active")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var success = await _restaurantService.ToggleActiveStatusAsync(id);
            if (!success)
            {
                return NotFound(new { Message = $"Target toggle record ID {id} not found." });
            }

            return NoContent();
        }

        // DELETE: api/restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _restaurantService.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new { Message = $"Target record removal ID {id} not found." });
            }

            return NoContent();
        }
    }
}
