using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListingAPI.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions options) : base(options)
        {
            
        }
        //Table Declarations
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //seeding data
            modelBuilder.Entity<Country>().HasData(
                new Country 
                { 
                    Id = 1,
                    Name="Philippines",
                    ShortName="PH"
                },
                new Country
                {
                    Id = 2,
                    Name = "United States of America",
                    ShortName = "USA"
                },
                new Country
                {
                    Id = 3,
                    Name = "Japan",
                    ShortName = "JP"
                }
                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { 
                    Id=1,
                    Name="Grand Hyatt",
                    Address="BGC",
                    CountryId=1,
                    Rating = 4.8
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Ritz Hotel",
                    Address = "New York",
                    CountryId = 2,
                    Rating = 5.0
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Azure",
                    Address = "Kyoto",
                    CountryId = 3,
                    Rating = 4.0
                }
                );
        }
        //Then run below in the Packager Manager Console.(Tools -> Nuget Packager Manager->Package Manager Console. 
        ////add-migration InitialMigration
        /////update-database
    }
}
