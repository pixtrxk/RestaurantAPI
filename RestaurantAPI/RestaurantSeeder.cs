using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurant();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Restaurant> GetRestaurant()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "Sushi Garden",
                    Description = "A paradise for sushi enthusiasts.",
                    Category = "Japanese",
                    hasDelivery = true,
                    ContactEmail = "info@sushigarden.com",
                    ContactNumber = "987-654-321",
                    Address = new Address
                    {
                        City = "Los Angeles",
                        Street = "Elm Street 198",
                        PostalCode="23-436"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "California Roll", Price = 8.99M },
                        new Dish { Name = "Salmon Nigiri", Price = 9.99M }

                    }
                },
                new Restaurant()
                {
                    Name = "Pizza Palace",
                    Description = "The best place for pizza lovers.",
                    Category = "Italian",
                    hasDelivery = true,
                    ContactEmail = "info@pizzapalace.com",
                    ContactNumber = "123-456-789",
                    Address = new Address
                    {
                        City = "New York",
                        Street = "Downstreet 67",
                        PostalCode="11-011"

                    },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Margherita Pizza", Price = 10.99M },
                        new Dish { Name = "Pepperoni Pizza", Price = 14.99M }

                    }
                }
            };
            return restaurants;
        }
    }
}
