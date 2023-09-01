using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Grand Hyatt",
                    Address = "BGC",
                    CountryId = 1,
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
    }
}
