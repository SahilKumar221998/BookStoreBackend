using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Entites;
using Model.Request;
using Model.Response;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController:ControllerBase
    {
        private readonly IBookBL _bookService;
        public BookController(IBookBL _bookService)
        {
            this._bookService = _bookService;
        }

        [HttpPost]
        public IActionResult AddBook(BookRequest request)
        {
            try
            {
                var result = _bookService.addBook(request);
                if (result != null)
                {
                    return Ok(new ResponseDto<bool> { Success = true, Message = "Book added successfully.", Data = result });
                }
                return BadRequest(new ResponseDto<Book> { Success = false, Message = "Failed to add book." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<Book> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _bookService.getAllBook();
                return Ok(new ResponseDto<List<Book>> { Success = true, Data = books, Message = "Fetched Data Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<List<Book>> { Success = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("/api/Id")]
        public IActionResult GetBookById(int bId)
        {
            try
            {
                var book = _bookService.getBookById(bId);
                if (book != null)
                {
                    return Ok(new ResponseDto<Book> { Success = true, Data = book, Message = "Book Retrived By Id" });
                }
                return NotFound(new ResponseDto<Book> { Success = false, Message = "Book not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<Book> { Success = false, Message = ex.Message });
            }
        }
    }
}
