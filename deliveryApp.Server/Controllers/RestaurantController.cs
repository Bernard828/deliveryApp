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

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResultDto<RestaurantDto>>> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool? isActive = true,
            CancellationToken cancellationToken = default)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new { Message = "Page parameters must be greater than zero." });
            }

            var result = await _restaurantService.GetPagedAsync(
                page, pageSize, isActive, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message = "Restaurant ID must be greater than zero."
                });
            }

            var restaurant = await _restaurantService.GetByIdAsync(id, cancellationToken);
            
            if (restaurant == null)
            {
                return NotFound(new { Message = $"Restaurant with ID {id} was not found." });
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> Create([FromBody] RestaurantCreateDto dto, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restaurant = await _restaurantService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(
                nameof(GetById),
                new { id = restaurant.RestaurantId },
                restaurant);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RestaurantUpdateDto dto, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    Message = "Restaurant ID must be reater than zero."
                });
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            if (id != dto.RestaurantId)
            {
                return BadRequest(new { Message = "Route identifier ID must match target payload ID." });
            }

            var success = await _restaurantService.UpdateAsync(id, dto, cancellationToken);
            if (!success)
            {
                return NotFound(new { Message = $"Target modification record ID {id} not found." });
            }

            return NoContent();
        }

        [HttpPatch("{id:int}/toggle-active")]
        public async Task<IActionResult> ToggleActive(int id, CancellationToken cancellationToken=default)
        {
            var success = await _restaurantService.ToggleActiveStatusAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound(new { Message = $"Target toggle record ID {id} not found." });
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken=default)
        {
            var success = await _restaurantService.DeleteAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound(new { Message = $"Target record removal ID {id} not found." });
            }

            return NoContent();
        }
    }
}
