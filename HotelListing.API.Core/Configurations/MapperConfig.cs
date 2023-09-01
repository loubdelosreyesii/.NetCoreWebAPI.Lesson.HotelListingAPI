using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Core.Models.Country;
using HotelListing.API.Core.Models.Hotel;
using HotelListing.API.Core.Models.User;

namespace HotelListing.API.Core.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //Automapping for Country Model
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            //Automapping for Hotel Model
            CreateMap<Hotel,CreateHotelDto>().ReverseMap();
            CreateMap<Hotel,GetHotelDto>().ReverseMap();
            CreateMap<Hotel,HotelDto>().ReverseMap();
            CreateMap<Hotel,UpdateHotelDto>().ReverseMap();

            //Automapping for IAuth
            CreateMap<ApiUser,ApiUserDto>().ReverseMap();
        }
    }
}
