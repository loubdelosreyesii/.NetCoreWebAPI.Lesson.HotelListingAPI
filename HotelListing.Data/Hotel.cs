using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Data
{
    public class Hotel
    {
        //Primary Key
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        //Foreign Key Declaration
        [ForeignKey(nameof(CountryId))] //DataAnnotation
        public int CountryId { get; set; }
        public Country Country{ get; set; }
    }
}
