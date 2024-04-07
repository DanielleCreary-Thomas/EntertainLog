using EntertainLog.Models.Repos;
using EntertainLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.WebControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieWebController : ControllerBase
    {
        private IEntertainLogRepo _entertainLogRepo;
        public MovieWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }
        //Create
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
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _entertainLogRepo.Movies;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(long id)
        {
            var movie = await _entertainLogRepo.GetMovieByIDAsync(id);

            return (movie == null) ? NotFound() : movie;
        }

        //Update
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
