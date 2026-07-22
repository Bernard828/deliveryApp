using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController:ControllerBase
    {
    }
}
