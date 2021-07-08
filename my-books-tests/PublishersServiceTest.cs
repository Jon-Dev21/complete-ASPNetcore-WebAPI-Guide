using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
        }

        // At the end, we want to destroy the database. 
        // When tests end, this method executes.
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted(); 
        }

        // This function will have some code to unit test the publishers service methods 
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
                    }//,
                    //new Publisher() {
                    //    Id = 4,
                    //    Name = "Publisher 4"
                    //},
                    //new Publisher() {
                    //    Id = 5,
                    //    Name = "Publisher 5"
                    //},
                    //new Publisher() {
                    //    Id = 6,
                    //    Name = "Publisher 6"
                    //},
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