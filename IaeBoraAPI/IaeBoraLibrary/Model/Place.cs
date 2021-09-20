using System.ComponentModel.DataAnnotations;

namespace IaeBoraLibrary.Model
{
    public class Place
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Business_status { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Rating { get; set; }   
        public Enums.CityEnum City_id { get; set; }
        public Enums.PlacesEnum Category_id { get; set; }
        public Enums.FoodsEnum? Restaurant_category_id { get; set; }
    }
}
