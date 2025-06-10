using Microsoft.EntityFrameworkCore;
using APBD_Tutorial12.Models;

namespace APBD_Tutorial12.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ClientTrip> ClientTrips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for ClientTrip
            modelBuilder.Entity<ClientTrip>()
                .HasKey(ct => new { ct.IdClient, ct.IdTrip });

            // ClientTrip => Client
            modelBuilder.Entity<ClientTrip>()
                .HasOne(ct => ct.Client)
                .WithMany(c => c.ClientTrips)
                .HasForeignKey(ct => ct.IdClient)
                .OnDelete(DeleteBehavior.Cascade);

            // ClientTrip => Trip
            modelBuilder.Entity<ClientTrip>()
                .HasOne(ct => ct.Trip)
                .WithMany(t => t.ClientTrips)
                .HasForeignKey(ct => ct.IdTrip)
                .OnDelete(DeleteBehavior.Cascade);

            // Trip => Many-to-many with Country 
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Countries)
                .WithMany(c => c.Trips)
                .UsingEntity(j => j.ToTable("Country_Trip"));

            // Additional configs from PDF:
            modelBuilder.Entity<Client>()
                .Property(c => c.FirstName)
                .HasMaxLength(120)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(c => c.LastName)
                .HasMaxLength(120)
                .IsRequired();

            modelBuilder.Entity<Trip>()
                .Property(t => t.Name)
                .HasMaxLength(120)
                .IsRequired();
            
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Countries)
                .WithMany(c => c.Trips)
                .UsingEntity(j => j.ToTable("Country_Trip"));


            modelBuilder.Entity<Trip>()
                .Property(t => t.Description)
                .HasMaxLength(220);

            modelBuilder.Entity<Country>()
                .Property(c => c.Name)
                .HasMaxLength(120)
                .IsRequired();
        }
    }
}
