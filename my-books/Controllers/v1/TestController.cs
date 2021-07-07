using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers.v1
{
    // [Route("api/[controller]")] Query string based route
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")] // For URL Based versioning, just change the API route
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is TestController V1");
        }
    }
}
