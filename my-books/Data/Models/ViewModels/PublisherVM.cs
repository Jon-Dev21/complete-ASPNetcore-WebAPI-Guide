using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Models.ViewModels
{
    public class PublisherVM
    {
        // Author Full Name
        public string Name { get; set; }
        // Author List of Books
        //public List<BookNameVM> PublishedBooks { get; set; }
    }

    // For an Id provided, we'll get all the books that this publisher has published as well as a list of authors
    // For each book, we get the book authors
    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorVM> BookAuthors { get; set; }
    }

    // Has two properties, bookName and List of Authors
    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> Authors { get; set; }
    }

    public class BookNameVM
    {
        public string PublishedBooks { get; set; }
    }
}
