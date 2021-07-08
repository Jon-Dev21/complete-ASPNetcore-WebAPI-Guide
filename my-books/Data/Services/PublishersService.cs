using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using my_books.Data.Paging;
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
        // Before adding sort parameter: public List<Publisher> GetAllPublishers() => _context.Publishers.ToList();
        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            // Sort ascending by default
            var allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    // sort descending
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            // If a search string is passed, query for that string
            if (!string.IsNullOrEmpty(searchString))
            {// StringComparison.CurrentCultureIgnoreCase is used to ignore lower & uppercase
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // Paging
            int pageSize = 5;  // Will display 5 publishers per page.
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);

            return allPublishers;
        }

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
        public Publisher GetPublisherById(int publisherId) => _context.Publishers.FirstOrDefault(n => n.Id == publisherId);

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
