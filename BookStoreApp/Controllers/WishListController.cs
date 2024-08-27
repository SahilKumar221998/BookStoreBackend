using Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using System.Security.Claims;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWhishListBL whishListBL;
        public WishListController(IWhishListBL whishListBL)
        {
            this.whishListBL = whishListBL;
        }
        [HttpPost]
        public IActionResult AddWishList(WishListRequest request)
        {
            try
            {
                int uId = int.Parse(User.FindFirstValue("userId"));
                var result = whishListBL.addWishList(request, uId);
                if (result)
                {
                    return Ok(new ResponseDto<bool>
                    {
                        Success = true,
                        Data = result,
                        Message = "Wish list item added successfully."
                    });
                }
                return BadRequest(new ResponseDto<bool>
                {
                    Success = false,
                    Data = result,
                    Message = "Failed to add wish list item."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<bool>
                {
                    Success = false,
                    Data = false,
                    Message = ex.Message
                });
            }

        }
        [HttpGet]
        public IActionResult GetWishList()
        {
            try
            {
                int uId = int.Parse(User.FindFirstValue("userId"));
                var wishList = whishListBL.getWishList(uId);
                return Ok(new ResponseDto<List<Object>>
                {
                    Success = true,
                    Data = wishList,
                    Message = "Wish list retrieved successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<List<Object>>
                {
                    Success = false,
                    Data = null,
                    Message = ex.Message
                });
            }
        }
        [HttpDelete]
        [Route("/Delete")]
        public IActionResult DeleteWishList(int wishListId)
        {
            try
            {
                int uId = int.Parse(User.FindFirstValue("userId"));
                var result = whishListBL.deleteWishList(uId, wishListId);
                if (result)
                {
                    return Ok(new ResponseDto<bool>
                    {
                        Success = true,
                        Data = result,
                        Message = "Wish list item deleted successfully."
                    });
                }
                return NotFound(new ResponseDto<bool>
                {
                    Success = false,
                    Data = result,
                    Message = "Wish list item not found."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<bool>
                {
                    Success = false,
                    Data = false,
                    Message = ex.Message
                });
            }
        }
    }
}
