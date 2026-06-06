using deliveryApp.Server.Models;
using deliveryApp.Server.NewFolder;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class RestaurantController : ControllerBase
    {
        //fetch list of available restaurants and their specific menus
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
            if (restuarant == null)
            {
                return NotFound();
            }
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
    }
}
