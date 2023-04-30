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
        public async Task<IActionResult> PostBankAsync(BankViewModel model)
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
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _bankApplicationService.GetAllAsync();

            if(response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _bankApplicationService.GetByIdAsync(id);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpPut("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutBankAsync(BankViewModel model)
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
        public async Task<IActionResult> DeleteBankAsync(Guid id)
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