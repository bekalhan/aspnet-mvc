using System;
using Microsoft.EntityFrameworkCore;

namespace BackUp.Models
{
	public class WebAppContext :DbContext
	{

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Crud;User=SA;Password=MyPassword123#;Trusted_Connection=false;Encrypt=false;");
        }

    }
}

