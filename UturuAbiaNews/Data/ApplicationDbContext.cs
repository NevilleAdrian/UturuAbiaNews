using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UturuAbiaNews.Models;

namespace UturuAbiaNews.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UturuAbiaNews.Models.Category> Category { get; set; }
        public DbSet<UturuAbiaNews.Models.Content> Content { get; set; }
        public DbSet<UturuAbiaNews.Models.Advertisement> Advertisement { get; set; }
        public DbSet<UturuAbiaNews.Models.Comment> Comment { get; set; }

	}
}
