using deliveryApp.Server.Models;
using deliveryApp.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = created.UserId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            if (id != user.UserId) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _userService.UpdateUserAsync(id, user);
            if(!success) return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
