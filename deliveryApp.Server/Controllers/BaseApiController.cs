using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController:ControllerBase
    {
    }
}
