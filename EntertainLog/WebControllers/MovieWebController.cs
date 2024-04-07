using EntertainLog.Models.Repos;
using EntertainLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertainLog.WebControllers
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
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
