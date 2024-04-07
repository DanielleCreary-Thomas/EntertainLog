using EntertainLog.Models;
using EntertainLog.Models.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EntertainLog.WebControllers
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// API for the Book Entity, Performs CRUD Actions through Http Request Methods
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookWebController : ControllerBase
    {
        /// <summary>
        /// reference to the EntertainLog Repository
        /// </summary>
        private IEntertainLogRepo _entertainLogRepo;

        /// <summary>
        /// Initializes the Book API with the EntertainLogRepo getting constructor Injected
        /// </summary>
        /// <param name="entertainLogRepo"></param>
        public BookWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }


        //Create
        /// <summary>
        /// Creates a new Book and adds it to the Database
        /// </summary>
        /// <param name="book">New Book to be Added</param>
        /// <returns>The newly created Book in JSON Format</returns>
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
        /// <summary>
        /// Gives all the Books in the Database
        /// </summary>
        /// <returns>JSON collection of books</returns>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _entertainLogRepo.Books;
        }

        /// <summary>
        /// Gives the Book with the matching ID
        /// </summary>
        /// <param name="id">ID of the requested Book</param>
        /// <returns>The requested Book in JSON format</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(long id)
        {
            var book = await _entertainLogRepo.GetBookByIDAsync(id);

            return (book == null) ? NotFound() : book;
        }

        //Update
        /// <summary>
        /// Updates the given Book
        /// </summary>
        /// <param name="book">The Book to be updated</param>
        /// <returns>The updated book in JSON format</returns>
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
        /// <summary>
        /// Deletes the Book with the matching ID
        /// </summary>
        /// <param name="id">The ID of the Book to be Deleted</param>
        /// <returns>Status Message and Code for OK</returns>
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
