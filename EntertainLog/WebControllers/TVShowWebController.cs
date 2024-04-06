using EntertainLog.Models.Repos;
using EntertainLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.WebControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowWebController : ControllerBase
    {
        private IEntertainLogRepo _entertainLogRepo;
        public TVShowWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }
        //Create
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
        [HttpGet]
        public IEnumerable<TVShow> Get()
        {
            return _entertainLogRepo.TVShows;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TVShow>> Get(long id)
        {
            var tvshow = await _entertainLogRepo.GetTVShowByIDAsync(id);

            return (tvshow == null) ? NotFound() : tvshow;
        }

        //Update
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
