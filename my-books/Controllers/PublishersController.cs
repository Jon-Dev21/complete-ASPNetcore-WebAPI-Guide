using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.ActionResults;
using my_books.Data.Models.ViewModels;
using my_books.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        // Creating Database Endpoint for authors
        private PublishersService _publishersService;
        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        // Http Post request endpoint
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            _publishersService.AddPublisher(publisher);
            return Ok();
        }

        // Http Get request endpoint (get publisher by ID)
        // Changed return type from IActionResult to Publisher
        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _response = _publishersService.GetPublisherById(id);

            if(_response != null)
            {
                //var _responseObject = new CustomActionResultVM()
                //{
                //    Publisher = _response
                //};

                //return new CustomActionResult(_responseObject);
                return Ok(_response);
            } else
            {
                //var _responseObject = new CustomActionResultVM()
                //{
                //    Exception = new Exception("This is coming from publishers controller")
                //};

                //return new CustomActionResult(_responseObject);
                return NotFound();
            }
        }

        // Http Get request endpoint
        // Gets all publishers
        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString)
        {
            // If sortBy = "name_desc", the publishers will return in descending order by name
            try
            {
                var _result = _publishersService.GetAllPublishers(sortBy, searchString);
                return Ok(_result);
            } catch
            {
                return BadRequest("Unable to load publishers");
            }
        }


        // Http Get request endpoint (Get publisher books with authors)
        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        // Http delete request endpoint
        [HttpDelete("delete-publisher-by-id")]
        public IActionResult DeletePublisherById(int id)
        {
            _publishersService.DeletePublisherById(id);
            return Ok();
        }
    }
}
