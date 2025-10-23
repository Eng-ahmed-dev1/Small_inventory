using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{
    internal class InventoryDB : DbContext
    {
        public InventoryDB() { }
        public InventoryDB(DbContextOptions<InventoryDB> options) : base(options) { }

        public DbSet<Cart_item> Cart_item { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2008HFE\\SQLEXPRESS;Initial Catalog=Inventory;Integrated Security=True;Trust Server Certificate=True");
            }
        }
    }
}
