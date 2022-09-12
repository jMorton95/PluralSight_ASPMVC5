using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant{ Id = 1, Name = "Scott's Pizza", Cuisine = CuisineType.Italian },
                new Restaurant{ Id = 2, Name = "Terisguels", Cuisine = CuisineType.French },
                new Restaurant{ Id = 3, Name = "Mango Grove", Cuisine = CuisineType.Indian }
            };
        }
        public void Add(Restaurant restaurant)
        {
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(restaurant);
        }

        public void Delete(int id)
        {
            var restaurant = Get(id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
        }

        public void Edit(Restaurant restaurant)
        {
            var dbRes = Get(restaurant.Id);
            if (dbRes != null)
            {  
                dbRes.Name = restaurant.Name;
                dbRes.Cuisine = restaurant.Cuisine;
            }
        }

        public Restaurant Get(int id)
        {
            //Search inside the DB for an entry that's ID matches the ID parameter passed, or return the first result.
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants.OrderBy(r => r.Name);
        }

    }
}
