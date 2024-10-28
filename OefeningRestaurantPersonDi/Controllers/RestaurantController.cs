using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OefeningRestaurantPersonDi.Models;
using OefeningRestaurantPersonDi.Services;

namespace OefeningRestaurantPersonDi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> CreateRestaurant(Restaurant item)
        {
            await _restaurantService.CreateRestaurant(item);
            return CreatedAtAction(nameof(GetRestaurant), new { id = item.Id }, item);
        }

        [HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurant(id);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRestaurant(int id, Restaurant item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var restaurant = await _restaurantService.UpdateRestaurant(id, item);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant(int id)
        {
            var restaurant = _restaurantService.GetRestaurant(id);
            if (restaurant is null)
            {
                return NotFound();
            }
            await _restaurantService.DeleteRestaurant(id);
            return NoContent();
        }
    }
}
