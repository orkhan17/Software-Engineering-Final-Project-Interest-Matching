using Microsoft.EntityFrameworkCore;
using Project.API.Models;

namespace Project.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<Account> Accounts { get; set; } 
        public DbSet<Music_type_account> Music_type_accounts { get; set; } 
        public DbSet<Music_type> Music_types { get; set; } 
        public DbSet<Post> Posts { get; set; } 
        public DbSet<Visited_profile> Visited_profiles { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Music_type_account>()
                .HasKey(k => new {k.Account_Id, k.Music_type_id});

            builder.Entity<Music_type_account>()
                .HasOne(u => u.Account)
                .WithMany(u => u.Accounts)
                .HasForeignKey(u => u.Account_Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Music_type_account>()
                .HasOne(u => u.Music_type)
                .WithMany(u => u.Music_types)
                .HasForeignKey(u => u.Music_type_id)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}