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

    }
}
