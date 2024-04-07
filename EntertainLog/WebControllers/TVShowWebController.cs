using EntertainLog.Models.Repos;
using EntertainLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.WebControllers
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// API for the TVShow Entity, Performs CRUD Actions through Http Request Methods
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowWebController : ControllerBase
    {
        /// <summary>
        /// reference to the EntertainLog Repository
        /// </summary>
        private IEntertainLogRepo _entertainLogRepo;

        /// <summary>
        /// Initializes the Book API with the EntertainLogRepo getting constructor Injected
        /// </summary>
        /// <param name="entertainLogRepo"></param>
        public TVShowWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }


        //Create
        /// <summary>
        /// Creates a new TVShow and adds it to the Database
        /// </summary>
        /// <param name="tvshow">New TVShow to be Added</param>
        /// <returns>The newly created TVShow in JSON Format</returns>
        [HttpPost]
        public async Task<ActionResult<TVShow>> Post([FromBody] TVShow tvshow)
        {

            var duplicatedTVShow = await _entertainLogRepo.GetTVShowByIDAsync(tvshow.TVShowID);

            if (duplicatedTVShow == null)//new tvshow
            {
                if (tvshow.UserID != null)//validate userID with existing users
                {
                    var validatedUser = await _entertainLogRepo.GetUserByIDAsync((long)tvshow.UserID);
                    if (validatedUser != null)
                    {
                        tvshow.UserID = validatedUser.UserID;
                        return _entertainLogRepo.AddTVShow(tvshow);
                    }
                    return StatusCode(500, "An error occurred while processing your request: User not Found.");
                }
                return StatusCode(500, "An error occurred while processing your request: User not Provided.");
            }
            else//duplicate
            {
                return StatusCode(500, "An error occurred while processing your request: Duplicate TVShow.");
            }
        }


        //Read
        /// <summary>
        /// Gives all the TVShows in the Database
        /// </summary>
        /// <returns>JSON collection of TV Shows</returns>
        [HttpGet]
        public IEnumerable<TVShow> Get()
        {
            return _entertainLogRepo.TVShows;
        }

        /// <summary>
        /// Gives the TVShow with the matching ID
        /// </summary>
        /// <param name="id">ID of the requested TVShow</param>
        /// <returns>The requested TVShow in JSON format</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TVShow>> Get(long id)
        {
            var tvshow = await _entertainLogRepo.GetTVShowByIDAsync(id);

            return (tvshow == null) ? NotFound() : tvshow;
        }

        //Update
        /// <summary>
        /// Updates the given TVShow
        /// </summary>
        /// <param name="tvshow">The TVshow to be updated</param>
        /// <returns>The updated tvshow in JSON format</returns>
        [HttpPut]
        public async Task<ActionResult<TVShow>> Put(TVShow tvshow)
        {
            var validatedTVShow = await _entertainLogRepo.GetTVShowByIDAsync(tvshow.TVShowID);

            if (validatedTVShow != null)//new tvshow
            {
                if (tvshow.UserID != null)//validate routeID with existing routes
                {
                    var validatedUser = await _entertainLogRepo.GetUserByIDAsync((long)tvshow.UserID);
                    if (validatedUser != null)
                    {
                        tvshow.UserID = validatedUser.UserID;
                        return _entertainLogRepo.AddTVShow(tvshow);
                    }
                    return StatusCode(500, "An error occurred while processing your request: User not Found.");
                }
                return StatusCode(500, "An error occurred while processing your request: User not Provided.");
            }
            else//validated
            {
                return StatusCode(500, "An error occurred while processing your request: TVShow not Found.");
            }
        }

        //Delete
        /// <summary>
        /// Deletes the TVShow with the matching ID
        /// </summary>
        /// <param name="id">The ID of the TVSHow to be Deleted</param>
        /// <returns>Status Message and Code for OK</returns>
        [HttpDelete]
        public async Task<ActionResult<TVShow>> Delete(long id)
        {
            var validatedTVShow = await _entertainLogRepo.GetTVShowByIDAsync(id);
            if (validatedTVShow != null)
            {
                _entertainLogRepo.DeleteTVShow(validatedTVShow);
                return Ok();
            }
            else//tvshow not in db
            {
                return NotFound(id);
            }
        }

    }
}
