using deliveryApp.Server.Models;
using deliveryApp.Server.NewFolder;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    //fetch list of available restaurants and their specific menus
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null) return NotFound($"Restaurant with Id {id} does not exist.");

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostRestaurant(RestaurantDto restaurantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRestaurant = await _restaurantService.CreateRestaurantAsync(restaurantDto);
            return CreatedAtAction(nameof(GetRestaurant), new { id = createdRestaurant.RestaurantId }, createdRestaurant);
        }

        [HttpPut]
        public async Task<IActionResult> PutRestaurant(int id, [FromBody] RestaurantDto restaurantDto)
        {
            if (id != restaurantDto.RestaurantId)
            {
                return BadRequest("Payload Id structural mismatch with resource route identified.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _restaurantService.UpdateRestaurantAsync(id, restaurantDto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var success = await _restaurantService.DeleteRestaurantAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
