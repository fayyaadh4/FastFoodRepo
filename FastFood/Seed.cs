using FastFood.Data;
using FastFood.Models;
using System.Diagnostics.Metrics;

namespace FastFood
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Restaurants.Any())
            {

                List<Restaurant> restaurants = [new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC (short for Kentucky Fried Chicken) is an american fast food restaurant chain",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                MenuItems =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.",
                        Price = 10.30M
                    },
                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken nuggets (5 pcs.)",
                        Price = 5.30M
                    }
                ],
                Location = new() {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5Du"
                }
            },
            new Restaurant()
            {
                Name = "Mcdonalds",
                Category = "Fast Food",
                Description = "Mcdonalds is an american fast food restaurant chain",
                ContactEmail = "contact@mcdees.com",
                HasDelivery = true,
                Location = new() {
                    City = "New york",
                    Street = "West St 5",
                    PostalCode = "ZZZ 5Du"
                }
            }
                ];
                dataContext.Restaurants.AddRange(restaurants);
                dataContext.SaveChanges();
            }
        }
    }
}
