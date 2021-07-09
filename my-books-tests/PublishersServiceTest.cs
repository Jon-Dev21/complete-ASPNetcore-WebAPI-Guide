using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Models.ViewModels;
using my_books.Data.Services;
using my_books.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books_tests
{
    public class PublishersServiceTest
    {
        // Getting db context from my-books project
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")        // This line states that an InMemory db will be used instead of the 
            .Options;

        // Creating App Db context reference
        AppDbContext context;

        // Publishers service propery
        PublishersService publishersService;

        // Setup Decorator
        // OneTimeSetUp is used to set up database One time.
        [OneTimeSetUp]
        public void Setup()
        {
            // This code runs each time a method is executed.
            context = new AppDbContext(dbContextOptions); // Passing in-memory database
            context.Database.EnsureCreated();             // Ensures that the database has been created.

            // After the database is created, seed the database.
            SeedDatabase();

            // Adding publishers service
            publishersService = new PublishersService(context); 
        }

        // We can set the Test order of execution by adding Order(#) to the [Test] decoration as shown below
        
        // Test method for testing GetAllPublishers service method without any parameters.
        [Test, Order(1)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            // Will use publishers service here.
            var result = publishersService.GetAllPublishers("", "", null);


            // Verify that there are 3 publishers
            Assert.That(result.Count, Is.EqualTo(5));
            // Assert.AreEqual(result.Count, 3); This does the same as the method above
        }

        // Test method for testing GetAllPublishers service method with only search string parameter.
        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithSearchString_WithNoPageNumber_Test()
        {
            // Will use publishers service here.
            var result = publishersService.GetAllPublishers("", "3", null);


            // Verify that there are 3 publishers
            Assert.That(result.Count, Is.EqualTo(1));   // Check if result count == 1
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 3")); // Check if returned result name is "Publisher 3"
        }

        // Test method for testing GetAllPublishers service method with only SortBy parameter.
        [Test, Order(2)]
        public void GetAllPublishers_WithSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            // Will use publishers service here.
            var result = publishersService.GetAllPublishers("name_desc", "", null);

            // Verify that there are 3 publishers
            Assert.That(result.Count, Is.EqualTo(5));   // Check if result count == 5
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 6")); // Check if returned result name is "Publisher 6" since results are returned descending
        }

        // Test method for testing GetAllPublishers service method with only page number parameter.
        [Test, Order(4)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithPageNumber_Test()
        {
            // Will use publishers service here.
            var result = publishersService.GetAllPublishers("", "", 2);


            // Verify that there are 3 publishers
            Assert.That(result.Count, Is.EqualTo(1));
            
        }

        // Unit test challenge: Test the publishersService.GetPublisherById method
        [Test, Order(5)]
        public void GetPublishersById_WithResponse_Test()
        {
            // Will use publishers service here.
            var result = publishersService.GetPublisherById(1);


            // Verify that there are 3 publishers
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
            // Assert.AreEqual(result.Count, 3); This does the same as the method above
        }

        [Test, Order(6)]
        public void GetPublishersById_WithoutResponse_Test()
        {
            // Will use publishers service here.
            var result = publishersService.GetPublisherById(99);

            Assert.That(result, Is.Null);
        }
        // ------------------ Unit test challenge End ------------------

        // Test method for testing AddPublishers service method with a thrown exception
        [Test, Order(7)]
        public void AddPublisher_WithException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "123 With Exception"
            };

            // Assert that this method throws an exception with message "Name starts with a number"
            Assert.That(() => publishersService.AddPublisher(newPublisher), 
                Throws.Exception.TypeOf<PublisherNameException>().With.Message.EqualTo("Name starts with a number"));
        }

        // Test method for testing AddPublishers service method without a thrown exception
        [Test, Order(8)]
        public void AddPublisher_WithoutException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "Without Exception Publisher"
            };

            var result = publishersService.AddPublisher(newPublisher);

            // Assert that result is not null
            Assert.That(result, Is.Not.Null);

            // Assert that result name starts with "Without"
            Assert.That(result.Name, Does.StartWith("Without"));

            // Assert that Id is not null
            Assert.That(result.Id, Is.Not.Null);
        }

        // Test method for testing GetPublisherData service method
        [Test, Order(9)]
        public void GetPublisherData_Test()
        {
            var result = publishersService.GetPublisherData(1);

            // Assert that the result name is equal to "Publisher 1
            Assert.That(result.Name, Is.EqualTo("Publisher 1"));

            // Assert that the BookAuthors is not empty
            Assert.That(result.BookAuthors, Is.Not.Empty);

            // Assert that the BookAuthors result count is greater than 0
            Assert.That(result.BookAuthors.Count, Is.GreaterThan(0));

            // Getting first book name from BookAuthors
            var firstBookName = result.BookAuthors.OrderBy(n => n.BookName).FirstOrDefault().BookName;
            
            // Assert that firstBookName is equal to "Book 1 Title"
            Assert.That(firstBookName, Is.EqualTo("Book 1 Title"));
        }

        // At the end, we want to destroy the database. 
        // When tests end, this method executes.
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted(); 
        }

        // This function will have some code to unit test the publishers service methods.
        // It creates some test data for the database in order to test the API.
        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                // Creating publishers
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
            context.Publishers.AddRange(publishers); // Adding Publishers

            // Creating List of Authors
            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);


            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverURL = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverURL = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            context.Books.AddRange(books);

            // Creating Books_Authors relationship
            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            context.Books_Authors.AddRange(books_authors);


            context.SaveChanges(); // This method saves all the data to the database.
        }
    }
}