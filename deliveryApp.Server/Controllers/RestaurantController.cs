using deliveryApp.Server.DTOs;
using deliveryApp.Server.Services;
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


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RestaurantCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.RestaurantId },
                created
            );
        }
     

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _service.GetByIdAsync(id);
            if (r == null) return NotFound($"Restaurant with Id {id} does not exist.");

            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
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

        //[HttpGet("search")]
        //public async Task<IActionResult> Search([FromQuery] string query)
        //{
        //    if (string.IsNullOrWhiteSpace(query)) return BadRequest("Query is empty");
        //    var results = await _service.SearchAsync(query);
        //    return Ok(results);
        //}


        //[HttpPut("batch")]
        //public async Task<IActionResult> UpdateMultiple([FromBody] RestaurantUpdateMultipleDto dto)
        //{
        //    var success = await _service.EditMultipleAsync(dto);
        //    if (!success) return NotFound("No restaurants found for the provided IDs.");
        //    return NoContent();
        //}

        //[HttpDelete("tag")]
        //public async Task<IActionResult> DeleteTags(int id, [FromBody] DeleteRestaurantSearchTagsDto dto)
        //{
        //    if (dto.TagNames == null || !dto.TagNames.Any())
        //        return BadRequest("TagNames list cannot be empty.");

        //    var success = await _service.DeleteTagAsync(id, dto.TagNames);
        //    if (!success) return NotFound($"Restaurant with ID {id} not found.");
        //    return NoContent();
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var success = await _service.DeleteRestaurantAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        //[HttpPut("hours")]
        //public async Task<IActionResult> UpdateHours(int id, List<RestaurantHourDto> hours)
        //{
        //    var success = await _service.UpdateOperatingHoursAsync(id, hours);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}

        //[HttpGet("paged")]
        //public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        //{
        //    var results = await _service.GetPagedAsync(page, pageSize);
        //    return Ok(results);
        //}
    }
}
