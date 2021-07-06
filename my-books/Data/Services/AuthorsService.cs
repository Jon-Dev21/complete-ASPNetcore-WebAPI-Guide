using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        // Database context variable
        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        // Method to add data to the database
        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }
    }
}
