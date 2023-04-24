using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTddCore.Interfaces;
using TestTddCore.Models.Queries;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestTddApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IBookQueryService _bookQueryService;

        public AuthorsController(IBookQueryService bookQueryService)
        {
            _bookQueryService = bookQueryService;
        }


        // GET: /<controller>/
        [HttpGet("{id}")]
        public GetAuthorQueryResult Index(int id)
        {
            return _bookQueryService.GetAuthor(id);
        }
    }
}

