using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using my_books.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder) 
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // Getting appDBCOntext reference
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                // Check if there aren't any books in our database
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "The Lord of The Rings, The Fellowship of the Ring",
                        Description = "When the eccentric hobbit Bilbo Baggins leaves his home in the Shire, he gives his greatest treasure to his heir Frodo: a magic ring that makes its wearer invisible. Because of the difficulty Bilbo has in giving the ring away, his friend the wizard Gandalf the Grey suspects that the ring is more than it appears.",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 5,
                        Genre = "High Fantasy",
                        //Author = "J.R.R. Tolkien",
                        CoverURL = "https://images-na.ssl-images-amazon.com/images/I/91jBdaRVqML.jpg",
                        DateAdded = DateTime.Now
                    }, new Book()
                    {
                        Title = "The Lord of The Rings, The Two Towers",
                        Description = "The Two Towers opens with the disintegration of the Fellowship, as Merry and Pippin are taken captive by Orcs after the death of Boromir in battle. The Orcs, having heard a prophecy that a Hobbit will bear a Ring that gives universal power to its owner, wrongly think that Merry and Pippin are the Ring-bearers.",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-9),
                        Rate = 5,
                        Genre = "High Fantasy",
                        //Author = "J.R.R. Tolkien",
                        CoverURL = "https://images-na.ssl-images-amazon.com/images/I/31hpHUdg3OL._SX310_BO1,204,203,200_.jpg",
                        DateAdded = DateTime.Now
                    }, new Book()
                    {
                        Title = "The Lord of The Rings, The Return of The King",
                        Description = "The Return of the King, the third and final volume in The Lord of the Rings, opens as Gandalf and Pippin ride east to the city of Minas Tirith in Gondor, just after parting with King Théoden and the Riders of Rohan at the end of The Two Towers. In Minas Tirith, Gandalf and Pippin meet Denethor, the city’s Steward, or ruler, who clearly dislikes Gandalf. Pippin offers Denethor his sword in service to Gondor, out of gratitude for the fact that Denethor’s son Boromir gave his life for the hobbits earlier in the quest.",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-8),
                        Rate = 5,
                        Genre = "High Fantasy",
                        //Author = "J.R.R. Tolkien",
                        CoverURL = "https://i.harperapps.com/covers/9780261103597/y648.jpg",
                        DateAdded = DateTime.Now
                    });

                    // Saves the changes made in the dbcontext
                    context.SaveChanges();
                }
            }

        }
    }
}
