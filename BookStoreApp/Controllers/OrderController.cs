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

    public class OrderController : ControllerBase
    {
        private readonly IOrderBL _orderBL;
        public OrderController(IOrderBL _orderBL)
        {
            this._orderBL = _orderBL;
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] List<OrderRequest> orderRequests)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = _orderBL.AddOrder(orderRequests, userId);
                if (result != null)
                {
                    return Ok(new ResponseDto<List<Object>> { Success = true, Message = "Order placed successfully.", Data = result });
                }
                return BadRequest(new ResponseDto<Order> { Success = false, Message = "Failed to place order." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<Order> { Success = false, Message = ex.Message });
            }
        }


        [HttpGet]
        [Route("/api/Order")]
        public IActionResult GetOrder()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var orders = _orderBL.GetOrder(userId);
                if (orders != null && orders.Any())
                {
                    return Ok(new ResponseDto<List<Object>> { Success = true, Data = orders, Message = "orders retrived successfully" });
                }
                return NotFound(new ResponseDto<List<Order>> { Success = false, Message = "No orders found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<List<Order>> { Success = false, Message = ex.Message });
            }
        }
    }
}
