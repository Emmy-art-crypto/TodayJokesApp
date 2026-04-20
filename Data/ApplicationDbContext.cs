using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodayJokesApp.Models;

namespace TodayJokesApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ✅ Jokes table
        public DbSet<Jokes> Jokes { get; set; }

        // ✅ ADD THIS (Likes table)
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}