using Azure;
using Blog.Data.Core.Extensions;
using Blog.Data.Mappings;
using Blog.Domain.Aggreagates.Posts;
using Blog.Domain.Aggreagates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blog.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new UserMap());
            modelBuilder.AddConfiguration(new PostMap());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), opt => opt.CommandTimeout(120));
        }
    }
}
