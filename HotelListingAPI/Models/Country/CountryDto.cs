using HotelListingAPI.Models.Hotel;
using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Models.Country;

public class CountryDto 
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string ShortName { get; set; }

    public List<HotelDto> Hotels { get; set; }
}
