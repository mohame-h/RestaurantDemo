using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class DBEntities : DbContext
    {
        public DBEntities(DbContextOptions<DBEntities> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemIngredient> ItemIngredients { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<UserFeatures> UserFeatures { get; set; }



    }
}
