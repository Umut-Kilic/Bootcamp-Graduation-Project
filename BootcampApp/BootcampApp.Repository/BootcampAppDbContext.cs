using BootcampApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BootcampApp.Repository
{
    public class BootcampAppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<User> Users { get; set; }

        public BootcampAppDbContext(DbContextOptions<BootcampAppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
