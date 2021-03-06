using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    // This class is used to configure the Http services for the Books class
    public class BooksService
    {
        // Database context variable
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        // Method used to add books to the database (POST)
        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverURL = book.CoverURL,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            // Assigning Author Ids to To the Books_Authors Object
            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id, // This ID comes from the book object
                    AuthorId = id // This ID comes from the Book View model
                };
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }

        }

        // Returns all books from the database
        public List<Book> GetAllBooks() => _context.Books.ToList();

        // Returns a single book using specified ID
        public BookWithAuthorsVM GetBookById(int bookId) {
            // Old line of code: public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(n => n.Id == bookId);
            // Updating method to get the book with authors.
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverURL = book.CoverURL,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();
            return _bookWithAuthors;
        }

        // Updates book using specified ID
        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            // IF the book exists, update it.
            if(_book != null)
            {
                // added ternary condition to not update table if the update value is null
                _book.Title = book.Title!= null ? book.Title : _book.Title;
                _book.Description = book.Description!= null ? book.Description: _book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre != null ? book.Genre : _book.Genre;
                //_book.Author = book.Author;
                _book.CoverURL = book.CoverURL != null ? book.CoverURL : _book.CoverURL;
                _book.PublisherId = book.PublisherId;
                _context.SaveChanges();
            };
            return _book;
        }

        // Deletes book using specified ID
        public void DeleteBookById(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if(_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }
    }
}
