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
            throw new NotImplementedException();
        }
        //Read
        [HttpGet]
        public async IAsyncEnumerable<Music> Get()
        {
            yield break;

            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Music>> Get(long id)
        {
            throw new NotImplementedException();
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<Music>> Put(long id)
        {
            throw new NotImplementedException();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Music>> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
