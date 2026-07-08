using deliveryApp.Server.Models;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class CuisineTypeController : BaseApiController
    {
        private readonly ICuisineTypeService _service;
        public CuisineTypeController(ICuisineTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CuisineTypeCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, CuisineTypeUpdateDto dto)
        {
            if (id != dto.CuisineTypeId) return BadRequest("Id mismatch.");
            var updated = await _service.UpdateAsync(dto);
            if (!updated) return NotFound();
            return Ok(updated);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuisineType>>> GetAll()
        {
            try
            {
            var result = await _service.GetAllAsync();
            return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}