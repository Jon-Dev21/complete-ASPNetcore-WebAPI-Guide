using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        // Database context variable
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        // Method to get all publishers in the database
        public List<Publisher> GetAllPublishers() => _context.Publishers.ToList();

        // Method to add data to the database
        public void AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }

        // Method to get a publisher by its ID
        public Publisher GetPublisherById(int publisherId)
        {
            var _publisher = _context.Publishers.Where(n => n.Id == publisherId).Select(publisher => new Publisher()
            {
                Id = publisher.Id,
                Name = publisher.Name
                //Books = publisher.
            }).FirstOrDefault();
            return _publisher;
        }

        // Method to get a publisher, its published books and a list of authors.
        // For each book, a list of authors is returned.
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        Authors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return _publisherData; 
        }

        // Method to delete a publisher by its Id
        public void DeletePublisherById(int publisherId)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == publisherId);
            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
        }
    }
}
