using Demo.Api.CustomEvents;
using Demo.Application.Contracts;
using Demo.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountApplicationService _accountApplicationService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger, IAccountApplicationService accountApplicationService)
        {
            _logger = logger;
            _accountApplicationService = accountApplicationService;
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
            using (_logger.BeginScope("MyLoggingScope: getting accounts list"))
            {
                _logger.LogDebug("Getting all accounts from API");
                var response = await _accountApplicationService.GetAllAsync();

                if (response == null)
                {
                    _logger.LogWarning("No Accounts found.");
                    return NoContent();
                }

                _logger.LogInformation(BankEvents.GettingAllBanks, "Getting all accounts from API with event id.");

                return Ok(response);
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting Account in API based on its ID {id}", id);
            var response = await _accountApplicationService.GetByIdAsync(id);

            if (response == null)
            {
                _logger.LogWarning("No Account found for ID {id}", id);
                return NoContent();
            }

            return Ok(response);
        }

        [HttpPost("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostAsync(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createdAccount = await _accountApplicationService.CreateAsync(model);

                if (createdAccount == null)
                {
                    return BadRequest();
                }

                return Created($"api/accounts/{createdAccount.ID}", createdAccount);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutAsync(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _accountApplicationService.UpdateAsync(model))
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
            var account = await _accountApplicationService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound($"There is no account with the ID: {id}");
            }

            if (!await _accountApplicationService.DeleteAsync(account.ID))
            {
                return BadRequest();
            }

            return Ok($"The account {account.Number} has been deleted.");
        }
    }
}