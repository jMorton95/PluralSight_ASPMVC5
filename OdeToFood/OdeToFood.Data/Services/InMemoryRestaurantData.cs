﻿using OdeToFood.Data.Models;
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
