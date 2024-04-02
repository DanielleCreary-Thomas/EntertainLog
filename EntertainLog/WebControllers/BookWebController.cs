using EntertainLog.Models;
using EntertainLog.Models.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EntertainLog.WebControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookWebController : ControllerBase
    {
        private IEntertainLogRepo _entertainLogRepo;
        public BookWebController(IEntertainLogRepo entertainLogRepo)
        {
            _entertainLogRepo = entertainLogRepo;
        }
        //Create
        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            throw new NotImplementedException();
        }
        //Read
        [HttpGet]
        public async IAsyncEnumerable<Book> Get()
        {
            yield break;

            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(long id)
        {
            throw new NotImplementedException();
        }

        //Update
        [HttpPut]
        public async Task<ActionResult<Book>> Put(long id)
        {
            throw new NotImplementedException();
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<Book>> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
