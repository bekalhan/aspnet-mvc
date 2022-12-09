using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WP.Models
{
    public class WebAppContext: IdentityDbContext<IdentityUser>
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
            
        
    }
}

