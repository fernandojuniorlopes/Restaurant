using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Models
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext (DbContextOptions<RestaurantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant.Models.FoodMenu> FoodMenu { get; set; }
    }
}
