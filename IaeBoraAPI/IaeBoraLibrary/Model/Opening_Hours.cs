using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IaeBoraLibrary.Model
{
    public class Opening_Hours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Day_of_Week { get; set; }
        public bool Open { get; set; }
        public DateTime? Start_Hour { get; set; }
        public DateTime? End_Hour { get; set; }
        public Place Place { get; set; }
    }
}
