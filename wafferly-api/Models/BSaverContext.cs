using System;
using Microsoft.EntityFrameworkCore;
namespace WafferlyApi.Models
{
    public class BSaverContext: DbContext
    {
        public BSaverContext( DbContextOptions<BSaverContext> options)
            :base(options)
        {
            
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
