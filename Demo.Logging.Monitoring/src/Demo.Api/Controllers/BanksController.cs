using Demo.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("api/banks")]
    public class BanksController : ControllerBase
    {
        private readonly ILogger<BanksController> _logger;
        private readonly IBankApplicationService _bankApplicationService;        

        public BanksController(ILogger<BanksController> logger, IBankApplicationService bankApplicationService)
        {
            _logger = logger;
            _bankApplicationService = bankApplicationService;
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _bankApplicationService.GetAllAsync();

            if(response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }
    }
}