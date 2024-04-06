using EntertainLog.Models;
using EntertainLog.Models.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EntertainLog.WebControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookWebController : ControllerBase
    {
        private IEntertainLogRepo _entertainLogRepo;
        public BookWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }
        //Create
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            var duplicatedBook = await _entertainLogRepo.GetBookByIDAsync(book.BookID);

            if (duplicatedBook == null)//new book
            {
                if (book.UserID != null)//validate userID with existing users
                {
                    var validatedUser = await _entertainLogRepo.GetUserByIDAsync((long)book.UserID);
                    if(validatedUser != null)
                    {
                        book.UserID = validatedUser.UserID;
                        return _entertainLogRepo.AddBook(book);
                    }
                    return StatusCode(500, "An error occurred while processing your request: User not Found.");
                }
                return StatusCode(500, "An error occurred while processing your request: User not Provided.");
            }
            else//duplicate
            {
                return StatusCode(500, "An error occurred while processing your request: Duplicate Book.");
            }
        }
        //Read
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _entertainLogRepo.Books;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(long id)
        {
            var book = await _entertainLogRepo.GetBookByIDAsync(id);

            return (book == null) ? NotFound() : book;
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<Book>> Put(Book book)
        {
            var validatedBook = await _entertainLogRepo.GetBookByIDAsync(book.BookID);

            if (validatedBook != null)//new book
            {
                if (book.UserID != null)//validate routeID with existing routes
                {
                    var validatedUser = await _entertainLogRepo.GetUserByIDAsync((long)book.UserID);
                    if (validatedUser != null)
                    {
                        book.UserID = validatedUser.UserID;
                        return _entertainLogRepo.AddBook(book);
                    }
                    return StatusCode(500, "An error occurred while processing your request: User not Found.");
                }
                return StatusCode(500, "An error occurred while processing your request: User not Provided.");
            }
            else//validated
            {
                return StatusCode(500, "An error occurred while processing your request: Book not Found.");
            }
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Book>> Delete(long id)
        {
            var validatedBook = await _entertainLogRepo.GetBookByIDAsync(id);
            if (validatedBook != null)
            {
                _entertainLogRepo.DeleteBook(validatedBook);
                return Ok();
            }
            else//book not in db
            {
                return NotFound(id);
            }
        }
    }
}
