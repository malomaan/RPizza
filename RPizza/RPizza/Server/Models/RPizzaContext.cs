using Microsoft.EntityFrameworkCore;
using RPizza.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPizza.Server.Models
{
    public class RPizzaContext : DbContext
    {
        public RPizzaContext(DbContextOptions<RPizzaContext> options): base(options)
        {
        }
        public DbSet<PizzaSpecial> Special { get; set; }
        public DbSet<Topping> Topping { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaTopping>()
                .HasKey(pst => new { pst.PizzaId, pst.ToppingId });
            
            modelBuilder.Entity<PizzaTopping>()
                .HasOne<Pizza>().WithMany(ps => ps.Toppings);
        }


    }
}
