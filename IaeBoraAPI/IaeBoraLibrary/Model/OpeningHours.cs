using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IaeBoraLibrary.Model
{
    public class OpeningHours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public bool Open { get; set; }
        public DateTime? StartHour { get; set; }
        public DateTime? EndHour { get; set; }
        public Place Place { get; set; }
    }
}
