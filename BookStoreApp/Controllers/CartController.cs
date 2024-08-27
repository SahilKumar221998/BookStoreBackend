using Business.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entites;
using Model.Request;
using Model.Response;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : ControllerBase
    {
        private readonly ICartBL _cartService;

        public CartController(ICartBL _cartService)
        {
            this._cartService = _cartService;
        }

        [HttpPost]
        public IActionResult AddCart(CartRequest request)
        {
            try
            {
                int uId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                request.userId = uId;
                var result = _cartService.addCart(request);
                if (result != null)
                {
                    return Ok(new ResponseDto<int> { Success = true, Message = "Cart added successfully.", Data = result });
                }
                return BadRequest(new ResponseDto<Cart> { Success = false, Message = "Failed to add cart." });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto<Cart> { Success = false, Message = "Failed to add cart." }); ;
            }
        }

        [HttpPut]
        [Route("/UpdateCartQuantity")]
        public IActionResult UpdateCartQuantity(int cartId, int quantity)
        {
            try
            {
                var result = _cartService.updateCartquantity(cartId, quantity);
                if (result)
                {
                    return Ok(new ResponseDto<bool> { Success = true, Message = "Cart quantity updated successfully.", Data = result });
                }
                return NotFound(new ResponseDto<bool> { Success = false, Message = "Cart not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("/UpdateCartOrder")]
        public IActionResult UpdateCartOrder(int cartId, bool isOrdered)
        {
            try
            {
                var result = _cartService.updateCartOrder(cartId, isOrdered);
                if (result)
                {
                    return Ok(new ResponseDto<bool> { Success = true, Message = "Cart order status updated successfully.", Data = result });
                }
                return NotFound(new ResponseDto<bool> { Success = false, Message = "Cart not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPatch]
        [Route("/Uncart")]
        public IActionResult Uncart(int cartId)
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue("userId"));
                var result = _cartService.unCart(cartId, userId);
                if (result)
                {
                    return Ok(new ResponseDto<bool> { Success = true, Message = "Cart uncategorized successfully.", Data = result });
                }
                return NotFound(new ResponseDto<bool> { Success = false, Message = "Cart not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("/GetCartByID")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetCartByUserId()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var carts = _cartService.getByUserId(userId);
                return Ok(new ResponseDto<List<CartResponse>> { Success = true, Data = carts, Message = "Cart retrived" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<List<CartResponse>> { Success = false, Message = ex.Message });
            }
        }
    }
}
