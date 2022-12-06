using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WP.Models
{
    public class WebAppContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Crud;User=SA;Password=MyPassword123#;Trusted_Connection=false;Encrypt=false;");
        }
    }
}

