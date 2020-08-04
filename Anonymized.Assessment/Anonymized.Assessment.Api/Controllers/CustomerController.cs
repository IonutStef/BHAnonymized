using System.Threading.Tasks;
using AutoMapper;
using Anonymized.Assessment.Api.Models.Responses;
using Anonymized.Assessment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Anonymized.Assessment.Api.Controllers
{
    /// <summary>
    /// Controller for customer actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a customer with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Unique identifier of the customer.</param>
        /// <response code="200">Returns the customer.</response>
        /// <response code="404">Invalid <paramref name="id"/>.</response>
        /// <returns>The customer associated to the <paramref name="id"/>.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> Get(string id)
            => _mapper.Map<CustomerModel>(await _customerService.GetCustomerAsync(id));
    }
}