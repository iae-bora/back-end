using System;
using System.ComponentModel.DataAnnotations;

namespace IaeBoraLibrary.Model
{
    public class Opening_Hours
    {
        [Key]
        public int Id { get; set; }
        public string Day_of_Week { get; set; }
        public bool Open { get; set; }
        public TimeSpan Start_Hour { get; set; }
        public TimeSpan End_Hour { get; set; }
        public int Place_Id { get; set; }
    }
}
