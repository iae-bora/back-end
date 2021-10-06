using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IaeBoraLibrary.Model
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string BusinessStatus { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Rating { get; set; }   
        public Enums.CityEnum City { get; set; }
        public Enums.PlacesEnum Category { get; set; }
        public Enums.FoodsEnum? RestaurantCategory { get; set; }
    }
}
