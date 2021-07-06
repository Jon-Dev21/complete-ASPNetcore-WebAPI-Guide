using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Models.ViewModels
{
    public class AuthorVM
    {
        // Author Full Name
        public string FullName { get; set; }
    }

    // New View model to represent authors with their written books
    public class AuthorWithBooksVM
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
