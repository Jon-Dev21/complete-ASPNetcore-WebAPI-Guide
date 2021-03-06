using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Models.ViewModels
{
    // Book view model. This model is used for Http requests. It excludes Id since this is generated by the auto increment.
    // View models are basically a representation of the data that will be presented in a get request
    public class BookVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        //public string Author { get; set; }
        public string CoverURL { get; set; }

        // Updating database schema with Publisher and Author IDs
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }
    }

    public class BookWithAuthorsVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        //public string Author { get; set; }
        public string CoverURL { get; set; }

        // Updating database schema with Publisher and Author IDs
        public string PublisherName { get; set; }
        public List<string> AuthorNames { get; set; }
    }
}
