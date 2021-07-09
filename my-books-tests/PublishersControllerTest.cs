using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using my_books.Controllers;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books_tests
{
    public class PublishersControllerTest
    {
        // Getting db context from my-books project
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbControllerTest")        // This line states that an InMemory db will be used instead of the 
            .Options;

        // Publishers service property
        PublishersService publishersService;

        // Creating App Db context reference
        AppDbContext context;

        // Publishers controller property
        PublishersController publishersController;

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

            // Setting up publishers service
            publishersService = new PublishersService(context);

            // Adding publishers Controller
            publishersController = new PublishersController(publishersService, new NullLogger<PublishersController>());
        }

        // CONTROLLER TESTING METHODS

        [Test, Order(1)]
        public void HTTPGet_GetAllPublishers_WithSortBy_WithSearchString_WithPageNumber_ReturnOk_Test()
        {
            IActionResult actionResult = publishersController.GetAllPublishers("name_desc", "Publisher", 1);

            // To check response for action result
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            // Get the action result as value of okObject. Expecting List of publishers
            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;
            
            // Assert that the first element of the list is equal to "Publisher 6"
            Assert.That(actionResultData.First().Name, Is.EqualTo("Publisher 6"));

            // Assert that the publisher Id returned is equal to 6
            Assert.That(actionResultData.First().Id, Is.EqualTo(6));

            // Assert that the publishers returned is equal to 5 (5 publishers per page returned)
            Assert.That(actionResultData.Count, Is.EqualTo(5));

            // ----------------------------- Second Page ------------------------------------

            IActionResult actionResultSecondPage = publishersController.GetAllPublishers("name_desc", "Publisher", 2);

            // To check response for action result
            Assert.That(actionResultSecondPage, Is.TypeOf<OkObjectResult>());

            // Get the action result as value of okObject. Expecting List of publishers
            var actionResultDataSecondPage = (actionResultSecondPage as OkObjectResult).Value as List<Publisher>;

            // Assert that the first element of the list is equal to "Publisher 6"
            Assert.That(actionResultDataSecondPage.First().Name, Is.EqualTo("Publisher 1"));

            // Assert that the publisher Id returned is equal to 6
            Assert.That(actionResultDataSecondPage.First().Id, Is.EqualTo(1));

            // Assert that the publishers returned is equal to 5 (5 publishers per page returned)
            Assert.That(actionResultDataSecondPage.Count, Is.EqualTo(1));
        }



        //[Test, Order(2)]
        //public void HTTPGet_GetAllPublishers_WithSortBy_WithNoSearchString_WithNoPageNumber_ReturnOk_Test()
        //{

        //}

        //[Test, Order(3)]
        //public void HTTPGet_GetAllPublishers_WithNoSortBy_WithSearchString_WithNoPageNumber_ReturnOk_Test()
        //{

        //}

        //[Test, Order(4)]
        //public void HTTPGet_GetAllPublishers_WithNoSortBy_WithNoSearchString_WithPageNumber_ReturnOk_Test()
        //{

        //}






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
            
            context.SaveChanges(); // This method saves all the data to the database.
        }
    }
}