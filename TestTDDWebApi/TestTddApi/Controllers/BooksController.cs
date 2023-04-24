using Microsoft.AspNetCore.Mvc;
using TestTddCore.Interfaces;
using TestTddCore.Models.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestTddApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookQueryService _bookQueryService;

        public BooksController(IBookQueryService bookQueryService)
        {
            _bookQueryService = bookQueryService;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public GetBooksQueryResult Get()
        {
            return _bookQueryService.GetBooks();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public GetBookQueryResult Get(int id)
        {
            return _bookQueryService.GetBook(id);
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
