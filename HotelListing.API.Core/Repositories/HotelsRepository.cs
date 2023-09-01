using AutoMapper;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Core.Repositories
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        private readonly HotelListingDbContext _context;

        public HotelsRepository(HotelListingDbContext context,IMapper mapper) : base(context,mapper)
        {
            this._context = context;
        }
    }
}
