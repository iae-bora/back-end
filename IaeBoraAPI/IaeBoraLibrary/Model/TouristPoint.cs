using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IaeBoraLibrary.Model
{
    public class TouristPoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Place Place { get; set; }
        public Opening_Hours OpeningHours { get; set; }
        public int Index { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public Route Route { get; set; }
    }
}
