using OdeToFood.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public void Add(Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
        }

        public void Edit(Restaurant restaurant)
        {
            var entry = db.Entry(restaurant);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            //uses DbContext Find method with our ID passed in.
            var restaurant = db.Restaurants.Find(id);
            //calls DbContext Remove method to remove our found result
            db.Restaurants.Remove(restaurant);
            //Saves
            db.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return db.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            //Orders by restaurant name in ascending order and selects each restaurant.
            return from r in db.Restaurants orderby r.Name select r;
        }
    }
}
