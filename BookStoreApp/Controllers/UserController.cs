using Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userService;
        public UserController(IUserBL userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public IActionResult CreateUser(UserRequest request)
        {
            try
            {
                bool result = userService.createUser(request);
                if (result)
                {
                    return Ok(new ResponseDto<bool>
                    {
                        Success = true,
                        Data = result,
                        Message = "User created successfully."
                    });
                }
                return BadRequest(new ResponseDto<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "User creation failed."
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

        [HttpPost]
        [Route("/api/Login")]
        public IActionResult Login(string userEmail, string password)
        {
            try
            {
                string[] token = userService.login(userEmail, password);
                if (!string.IsNullOrEmpty(token[0]))
                {
                    return Ok(new
                    {
                        Success = true,
                        Data = token[0],
                        Name = token[1],
                        Message = "Logged in successfully."
                    });
                }
                return Unauthorized(new ResponseDto<string>
                {
                    Success = false,
                    Data = null,
                    Message = "Login failed. Invalid email or password."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>
                {
                    Success = false,
                    Data = null,
                    Message = ex.Message
                });
            }
        }
    }
}
