using deliveryApp.Server.Models;
using deliveryApp.Server.NewFolder;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    //fetch list of available restaurants and their specific menus
    public class RestaurantController : BaseApiController
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            var restaurants = await _service.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById(int id)
        {
            var restaurant = await _service.GetRestaurantByIdAsync(id);
            if (restaurant == null) return NotFound($"Restaurant with Id {id} does not exist.");

            return Ok(restaurant);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return BadRequest("Query is empty");
            var results = await _service.SearchAsync(query);
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RestaurantCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurant = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = restaurant.RestaurantId },
                restaurant
            );
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] RestaurantUpdateDto dto)
        {
            if (id != dto.RestaurantId)
                return BadRequest("Mismatch id");

            var success = await _service.UpdateAsync(dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateMultiple([FromBody] RestaurantUpdateMultipleDto dto)
        {
            var success = await _service.EditMultipleAsync(dto);
            if (!success) return NotFound("No restaurants found for the provided IDs.");
            return NoContent();
        }

        [HttpDelete("tag")]
        public async Task<IActionResult> DeleteTags(int id, [FromBody] DeleteRestaurantSearchTagsDto dto)
        {
            if (dto.TagNames == null || !dto.TagNames.Any())
                return BadRequest("TagNames list cannot be empty.");

            var success = await _service.DeleteTagAsync(id, dto.TagNames);
            if (!success) return NotFound($"Restaurant with ID {id} not found.");
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var success = await _service.DeleteRestaurantAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPut("hours")]
        public async Task<IActionResult> UpdateHours(int id, List<RestaurantHourDto> hours)
        {
            var success = await _service.UpdateOperatingHoursAsync(id, hours);
            if(!success) return NotFound();
            return NoContent();
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery]int page =1, [FromQuery] int pageSize = 10)
        {
            var results = await _service.GetPagedAsync(page, pageSize);
            return Ok(results);
        }
    }
}
