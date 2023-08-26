using HotelListingAPI.Contracts;
using HotelListingAPI.Data;

namespace HotelListingAPI.Repositories
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        public CountriesRepository(HotelListingDbContext context) : base(context)
        {

        }


    }
}
