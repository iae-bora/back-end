using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IaeBoraLibrary.Model
{
    public class Route
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime RouteDate { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public List<Enums.PlacesEnum> RouteCategories { get; set; }

        public Route()
        {
            RouteCategories = new List<Enums.PlacesEnum>();
        }

        public Route(List<Enums.PlacesEnum> inputList)
        {
            RouteCategories = inputList;
        }
    }
}
