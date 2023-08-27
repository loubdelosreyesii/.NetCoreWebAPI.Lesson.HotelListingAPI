using HotelListingAPI.Models.Hotel;
using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Models.Country;

public class CountryDto :  BaseCountryDto
{
    public int Id { get; set; }
    public List<HotelDto> Hotels { get; set; }
}
