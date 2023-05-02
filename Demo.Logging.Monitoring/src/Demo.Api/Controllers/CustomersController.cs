using Demo.Api.CustomEvents;
using Demo.Application.ApplicationServices;
using Demo.Application.Contracts;
using Demo.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerApplicationService _customerApplicationService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger, ICustomerApplicationService customerApplicationService)
        {
            _logger = logger;
            _customerApplicationService = customerApplicationService;
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
            using (_logger.BeginScope("MyLoggingScope: getting customers list"))
            {
                _logger.LogDebug("Getting all customers from API");
                var response = await _customerApplicationService.GetAllAsync();

                if (response == null)
                {
                    _logger.LogWarning("No Customers found.");
                    return NoContent();
                }

                _logger.LogInformation(BankEvents.GettingAllDataFromDatabaseTable, "Getting all customers from API with event id.");

                return Ok(response);
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Getting Customer in API based on its ID {id}", id);
            var response = await _customerApplicationService.GetByIdAsync(id);

            if (response == null)
            {
                _logger.LogWarning("No Customer found for ID {id}", id);
                return NoContent();
            }

            return Ok(response);
        }

        [HttpPost("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostAsync(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createdCustomer = await _customerApplicationService.CreateAsync(model);

                if (createdCustomer == null)
                {
                    return BadRequest();
                }

                return Created($"api/customers/{createdCustomer.ID}", createdCustomer);
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
        public async Task<IActionResult> PutAsync(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _customerApplicationService.UpdateAsync(model))
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
            var customer = await _customerApplicationService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound($"There is no customer with the ID: {id}");
            }

            if (!await _customerApplicationService.DeleteAsync(customer.ID))
            {
                return BadRequest();
            }

            return Ok($"The customer {customer.Name} has been deleted.");
        }
    }
}