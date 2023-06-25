using LibraryMSv3.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryMSv3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(u => u.Image) 
            .WithOne(up => up.User)
            .HasForeignKey<Image>(up => up.UserId); 

            modelBuilder.Entity<User>()
            .HasOne(u => u.Address) 
            .WithOne(ua => ua.User) 
            .HasForeignKey<Address>(ua => ua.UserId); 

            modelBuilder.Entity<User>()
            .HasOne(u => u.UserInfo) 
            .WithOne(ua => ua.User) 
            .HasForeignKey<UserInfo>(ua => ua.UserId); 
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
  
    }
}
