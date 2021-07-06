using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models.ViewModels;
using my_books.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    // Api route definition.
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // Injecting BooksService to enable using booksService methods for http requests
        public BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        // Get Endpoint for returning all books
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allBooks = _booksService.GetAllBooks();
            return Ok(allBooks);
        }

        // Get Endpoint for returning a single book by id
        [HttpGet("get-book-by-id/{BookID}")]
        public IActionResult GetBookById(int BookID)
        {
            var book = _booksService.GetBookById(BookID);
            return Ok(book);
        }

        // Creating Post endpoint
        // For custom endpoint, add route between parenthesis
        [HttpPost("add-book-with-authors")]
        public IActionResult Addbook([FromBody] BookVM book)
        {
            _booksService.AddBookWithAuthors(book);
            return Ok();
        }

        // Book returned from request body
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody]BookVM book)
        {
            var updatedBook = _booksService.UpdateBookById(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            _booksService.DeleteBookById(id);
            return Ok();            
        }
        
    }
}
