using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.Models.Country;
using HotelListingAPI.Models.Hotel;

namespace HotelListingAPI.Configurations
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
        }
    }
}
