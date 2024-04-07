using EntertainLog.Models.Repos;
using EntertainLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.WebControllers
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// API for the Music Entity, Performs CRUD Actions through Http Request Methods
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MusicWebController : ControllerBase
    {
        /// <summary>
        /// reference to the EntertainLog Repository
        /// </summary>
        private IEntertainLogRepo _entertainLogRepo;

        /// <summary>
        /// Initializes the Book API with the EntertainLogRepo getting constructor Injected
        /// </summary>
        /// <param name="entertainLogRepo"></param>
        public MusicWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }


        //Create
        /// <summary>
        /// Creates a new Music and adds it to the Database
        /// </summary>
        /// <param name="music">New Music to be Added</param>
        /// <returns>The newly created Music in JSON Format</returns>
        [HttpPost]
        public async Task<ActionResult<Music>> Post([FromBody] Music music)
        {
            var duplicatedMusic = await _entertainLogRepo.GetMusicByIDAsync(music.MusicID);

            if (duplicatedMusic == null)//new music
            {
                if (music.UserID != null)//validate userID with existing users
                {
                    var validatedUser = await _entertainLogRepo.GetUserByIDAsync((long)music.UserID);
                    if (validatedUser != null)
                    {
                        music.UserID = validatedUser.UserID;
                        return _entertainLogRepo.AddMusic(music);
                    }
                    return StatusCode(500, "An error occurred while processing your request: User not Found.");
                }
                return StatusCode(500, "An error occurred while processing your request: User not Provided.");
            }
            else//duplicate
            {
                return StatusCode(500, "An error occurred while processing your request: Duplicate Music.");
            }
        }


        //Read
        /// <summary>
        /// Gives all the Musics in the Database
        /// </summary>
        /// <returns>JSON collection of music</returns>
        [HttpGet]
        public IEnumerable<Music> Get()
        {
            return _entertainLogRepo.Musics;
        }

        /// <summary>
        /// Gives the Music with the matching ID
        /// </summary>
        /// <param name="id">ID of the requested Music</param>
        /// <returns>The requested Msuic in JSON format</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Music>> Get(long id)
        {
            var music = await _entertainLogRepo.GetMusicByIDAsync(id);

            return (music == null) ? NotFound() : music;
        }

        //Update
        /// <summary>
        /// Updates the given Music
        /// </summary>
        /// <param name="music">The Music to be updated</param>
        /// <returns>The updated music in JSON format</returns>
        [HttpPut]
        public async Task<ActionResult<Music>> Put(Music music)
        {
            var validatedMusic = await _entertainLogRepo.GetMusicByIDAsync(music.MusicID);

            if (validatedMusic != null)//new music
            {
                if (music.UserID != null)//validate routeID with existing routes
                {
                    var validatedUser = await _entertainLogRepo.GetUserByIDAsync((long)music.UserID);
                    if (validatedUser != null)
                    {
                        music.UserID = validatedUser.UserID;
                        return _entertainLogRepo.AddMusic(music);
                    }
                    return StatusCode(500, "An error occurred while processing your request: User not Found.");
                }
                return StatusCode(500, "An error occurred while processing your request: User not Provided.");
            }
            else//validated
            {
                return StatusCode(500, "An error occurred while processing your request: Music not Found.");
            }
        }

        //Delete
        /// <summary>
        /// Deletes the Music with the matching ID
        /// </summary>
        /// <param name="id">The ID of the Music to be Deleted</param>
        /// <returns>Status Message and Code for OK</returns>
        [HttpDelete]
        public async Task<ActionResult<Music>> Delete(long id)
        {
            var validatedMusic = await _entertainLogRepo.GetMusicByIDAsync(id);
            if (validatedMusic != null)
            {
                _entertainLogRepo.DeleteMusic(validatedMusic);
                return Ok();
            }
            else//truck not in db
            {
                return NotFound(id);
            }
        }
    }
}
