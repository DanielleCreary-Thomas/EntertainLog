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
            throw new NotImplementedException();
        }
        //Read
        [HttpGet]
        public async IAsyncEnumerable<Movie> Get()
        {
            yield break;

            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(long id)
        {
            throw new NotImplementedException();
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<Movie>> Put(long id)
        {
            throw new NotImplementedException();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Movie>> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
