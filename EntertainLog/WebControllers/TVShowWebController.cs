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
        public async Task<ActionResult<TVShow>> Post([FromBody] TVShow tvShow)
        {

            throw new NotImplementedException();
        }
        //Read
        [HttpGet]
        public async IAsyncEnumerable<TVShow> Get()
        {
            yield break;
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TVShow>> Get(long id)
        {
            throw new NotImplementedException();
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<TVShow>> Put(long id)
        {
            throw new NotImplementedException();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<TVShow>> Delete(long id)
        {
            throw new NotImplementedException();
        }

    }
}
