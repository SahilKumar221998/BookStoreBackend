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
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL _service;
        public AddressController(IAddressBL _service)
        {
            this._service = _service;
        }

        [HttpPost]
        public IActionResult AddAddress(AddressRequest addressRequest)
        {
            try
            {
                int userId= Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                bool result = _service.addAddress(addressRequest, userId);
                if (result)
                {
                    return Ok(new ResponseDto<Object> { Success = true, Message = "Address added successfully." });
                }
                return BadRequest(new { Success = false, Message = "Failed to add address." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<Object> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var addresses = _service.getAllAddress(userId);
                return Ok(new ResponseDto<List<Address>> { Success = true, Data = addresses, Message = "Address Retrived Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<Object> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("/api/update")]
        public IActionResult UpdateAddress(AddressRequest addressRequest, int addressId)
        {
            try
            {
                bool result = _service.updateAddress(addressRequest, addressId);
                if (result)
                {
                    return Ok(new ResponseDto<Object> { Success = true, Message = "Address updated successfully." });
                }
                return NotFound(new ResponseDto<Object> { Success = false, Message = "Address not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<Object> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("/api/Delete")]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                bool result = _service.deleteAddress(addressId);
                if (result)
                {
                    return Ok(new ResponseDto<Object> { Success = true, Message = "Address deleted successfully." });
                }
                return NotFound(new ResponseDto<Object> { Success = false, Message = "Address not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<Object> { Success = false, Message = ex.Message });
            }
        }
    }
}
