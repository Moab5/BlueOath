using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlueOath.Models;

namespace BlueOath.Data
{
    public class BlueOathContext : DbContext
    {
        public BlueOathContext (DbContextOptions<BlueOathContext> options)
            : base(options)
        {
        }

        public DbSet<BlueOath.Models.Room> Room { get; set; } = default!;
    }
}
