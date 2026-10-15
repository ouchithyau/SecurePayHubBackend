using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurePayHub.Application.DTOs;
using SecurePayHub.Application.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurePayHub.API.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly OrderService _orderService;

        public ProductsController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("products")]
        [Authorize] 
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = await _orderService.CreateProductAsync(request);
            return Ok(product);
        }

        [HttpPost("orders")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null) return Unauthorized();

                int userId = int.Parse(userIdClaim);
                var result = await _orderService.CreateOrderAsync(userId, request);
                return Ok(new { message = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}