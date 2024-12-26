using Microsoft.AspNetCore.Mvc;
using FluentOrdersValidation.Models;

namespace FluentOrdersValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { Message = "Customer created successfully", Customer = customer });
        }
    }
}