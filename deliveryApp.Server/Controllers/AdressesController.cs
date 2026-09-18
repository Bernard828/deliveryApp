using deliveryApp.Server.DTOs;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class AdressesController : BaseApiController
    {
        private readonly IAddressService _service;
        public AdressesController(IAddressService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> Create([FromQuery] int restaurantId, AddressCreateDto dto)
        {
            var result = await _service.CreateAsync(restaurantId, dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int restaurantId, AddressUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(restaurantId, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int restaurantId)
        {
            var deleted = await _service.DeleteAsync(restaurantId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
