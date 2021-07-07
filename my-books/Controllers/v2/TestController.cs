using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers.v2
{
    // [Route("api/v{version:apiVersion}/[controller]")] // For URL Based versioning, just change the API route
     
    [ApiVersion("2.0")]
    [ApiVersion("2.1")]
    [ApiVersion("2.9")]
    [Route("api/[controller]")] //Query string based route
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult GetV2()
        {
            return Ok("This is TestController V2.0");
        }

        // Use the MapToApiVersion parameter to map route to a specified Api version
        [HttpGet("get-test-data"), MapToApiVersion("2.1")]
        public IActionResult GetV21()
        {
            return Ok("This is TestController V2.1");
        }

        [HttpGet("get-test-data"), MapToApiVersion("2.9")]
        public IActionResult GetV29()
        {
            return Ok("This is TestController V2.9");
        }
    }
}
