using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers.v1
{
    // [Route("api/v{version:apiVersion}/[controller]")] // For URL Based versioning, just change the API route
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [ApiVersion("1.9")]
    [Route("api/[controller]")] //Query string based route
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult GetV1()
        {
            return Ok("This is TestController V1");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.2")]
        public IActionResult GetV12()
        {
            return Ok("This is TestController V1.2");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.9")]
        public IActionResult GetV19()
        {
            return Ok("This is TestController V1.9");
        }
    }
}
