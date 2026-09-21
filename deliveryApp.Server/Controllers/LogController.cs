using deliveryApp.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace deliveryApp.Server.Controllers
{
    public class LogController : BaseApiController
    {
        private readonly ILogger<LogController> _logger;
        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult PostLog([FromBody] LogModel model)
        {
            switch (model.Level?.ToUpper())
            {
                case "DEBUG": _logger.LogDebug(model.Message); break;
                case "WARN": _logger.LogWarning(model.Message); break;
                case "ERROR": _logger.LogError(model.Message); break;
                default: _logger.LogInformation(model.Message); break;
            }return Ok();
        }        
    }
}
