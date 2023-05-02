using Demo.Api.CustomEvents;
using Demo.Application.Contracts;
using Demo.Application.ViewModels;
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

        [HttpPost("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostAsync(BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createdBank = await _bankApplicationService.CreateAsync(model);

                if (createdBank == null)
                {
                    return BadRequest();
                }

                return Created($"api/banks/{createdBank.ID}", createdBank);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllAsync()
        {
            // using scopes is a way to apply semantic logging.
            // with this approach we can add key/values (paramters) that can be searched in the logs
            // which help us to improve the support we deliver to our customers
            using (_logger.BeginScope("MyLoggingScope: getting banks list"))
            {
                _logger.LogDebug("Getting all banks from API");
                var response = await _bankApplicationService.GetAllAsync();

                if (response == null)
                {
                    _logger.LogWarning("No Banks found.");
                    return NoContent();
                }

                _logger.LogInformation(BankEvents.GettingAllBanks, "Getting all banks from API with event id.");

                return Ok(response);
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting Bank in API based on its ID {id}", id);
            var response = await _bankApplicationService.GetByIdAsync(id);

            if (response == null)
            {
                _logger.LogWarning("No Bank found for ID {id}", id);
                return NoContent();
            }

            return Ok(response);
        }

        [HttpPut("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutAsync(BankViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(!await _bankApplicationService.UpdateAsync(model))
                {
                    return BadRequest();
                }

                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var bank = await _bankApplicationService.GetByIdAsync(id);

            if(bank == null)
            {
                return NotFound($"There is no bank with the ID: {id}");
            }

            if(!await _bankApplicationService.DeleteAsync(bank.ID))
            {
                return BadRequest();
            }

            return Ok($"The bank {bank.Name} has been deleted.");
        }
    }
}