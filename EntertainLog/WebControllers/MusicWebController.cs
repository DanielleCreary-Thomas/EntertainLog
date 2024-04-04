using EntertainLog.Models.Repos;
using EntertainLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.WebControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicWebController : ControllerBase
    {
        private IEntertainLogRepo _entertainLogRepo;
        public MusicWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }
        //Create
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
        [HttpGet]
        public IEnumerable<Music> Get()
        {
            return _entertainLogRepo.Musics;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Music>> Get(long id)
        {
            var music = await _entertainLogRepo.GetMusicByIDAsync(id);

            return (music == null) ? NotFound() : music;
        }

        //Update
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
