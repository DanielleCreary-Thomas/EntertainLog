using EntertainLog.Models.Repos;
using EntertainLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.WebControllers
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// Edit By: Yuanlong Song
    /// API for the Movie Entity, Performs CRUD Actions through Http Request Methods
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MovieWebController : ControllerBase
    {
        /// <summary>
        /// reference to the EntertainLog Repository
        /// </summary>
        private IEntertainLogRepo _entertainLogRepo;

        /// <summary>
        /// Initializes the Book API with the EntertainLogRepo getting constructor Injected
        /// </summary>
        /// <param name="entertainLogRepo"></param>
        public MovieWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }
        //Create
        /// <summary>
        /// Creates a new Movie and adds it to the Database
        /// </summary>
        /// <param name="movie">New movie to be Added</param>
        /// <returns>The newly created moive in JSON Format</returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> Post([FromBody] Movie movie)
        {
            var duplicatedMovie = await _entertainLogRepo.GetMovieByIDAsync(movie.MovieID);

            if (duplicatedMovie == null)//new movie
            {
                if (movie.UserID != null)//validate userID with existing users
                {
                    var validatedUser = await _entertainLogRepo.GetUserByIDAsync((long)movie.UserID);
                    if (validatedUser != null)
                    {
                        movie.UserID = validatedUser.UserID;
                        return _entertainLogRepo.AddMovie(movie);
                    }
                    return StatusCode(500, "An error occurred while processing your request: User not Found.");
                }
                return StatusCode(500, "An error occurred while processing your request: User not Provided.");
            }
            else//duplicate
            {
                return StatusCode(500, "An error occurred while processing your request: Duplicate movie.");
            }
        }
        //Watched
        /// <summary>
        /// Gives all the Movies in the Database
        /// </summary>
        /// <returns>JSON collection of movie</returns>
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _entertainLogRepo.Movies;
        }

        /// <summary>
        /// Gives the Movie with the matching ID
        /// </summary>
        /// <param name="id">ID of the requested Movie</param>
        /// <returns>The requested Msuic in JSON format</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(long id)
        {
            var movie = await _entertainLogRepo.GetMovieByIDAsync(id);

            return (movie == null) ? NotFound() : movie;
        }

        //Update
        /// <summary>
        /// Updates the given Movie
        /// </summary>
        /// <param name="movie">The Movie to be updated</param>
        /// <returns>The updated movie in JSON format</returns>
        [HttpPut]
        public async Task<ActionResult<Movie>> Put(Movie movie)
        {
            var validatedMovie = await _entertainLogRepo.GetBookByIDAsync(movie.MovieID);

            if (validatedMovie != null)//new movie
            {
                if (movie.UserID != null)//validate routeID with existing routes
                {
                    var validatedUser = await _entertainLogRepo.GetUserByIDAsync((long)movie.UserID);
                    if (validatedUser != null)
                    {
                        movie.UserID = validatedUser.UserID;
                        return _entertainLogRepo.AddMovie(movie);
                    }
                    return StatusCode(500, "An error occurred while processing your request: User not Found.");
                }
                return StatusCode(500, "An error occurred while processing your request: User not Provided.");
            }
            else//validated
            {
                return StatusCode(500, "An error occurred while processing your request: Movie not Found.");
            }
        }

        //Delete
        /// <summary>
        /// Deletes the Movie with the matching ID
        /// </summary>
        /// <param name="id">The ID of the Movie to be Deleted</param>
        /// <returns>Status Message and Code for OK</returns>
        [HttpDelete]
        public async Task<ActionResult<Movie>> Delete(long id)
        {
            var validatedMovie = await _entertainLogRepo.GetMovieByIDAsync(id);
            if (validatedMovie != null)
            {
                _entertainLogRepo.DeleteMovie(validatedMovie);
                return Ok();
            }
            else//movie not in db
            {
                return NotFound(id);
            }
        }
    }
}
