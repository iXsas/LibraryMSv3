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
            // Konfigūruojame rysių nustatymus tarp "User" ir "Image" lentelių

            modelBuilder.Entity<User>()
            .HasOne(u => u.Image) // Nurodome, kad "User" turi vieną "Image" objektą
            .WithOne(up => up.User) // Nurodome, kad "Image" priklauso vienam "User" objektui
            .HasForeignKey<Image>(up => up.UserId); // Nurodome, kad "Image" lentelėje esantis stulpelis "UserId" yra išorinis raktas, rodantis į "User" objektą

            // Konfigūruojame rysių nustatymus tarp "User" ir "Address" lentelių

            modelBuilder.Entity<User>()
            .HasOne(u => u.Address) // Nurodome, kad "User" turi vieną "Address" objektą
            .WithOne(ua => ua.User) // Nurodome, kad "Address" priklauso vienam "User" objektui
            .HasForeignKey<Address>(ua => ua.UserId); // Nurodome, kad "Address" lentelėje esantis stulpelis "UserId" yra išorinis raktas, rodantis į "User" objektą

            // Konfigūruojame rysių nustatymus tarp "User" ir "UserInfo" lentelių

            modelBuilder.Entity<User>()
            .HasOne(u => u.UserInfo) // Nurodome, kad "User" turi vieną "UserInfo" objektą
            .WithOne(ua => ua.User) // Nurodome, kad "UserInfo" priklauso vienam "User" objektui
            .HasForeignKey<UserInfo>(ua => ua.UserId); // Nurodome, kad "UserInfo" lentelėje esantis stulpelis "UserId" yra išorinis raktas, rodantis į "User" objektą
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
  
    }
}
