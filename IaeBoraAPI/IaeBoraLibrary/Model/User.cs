using System.ComponentModel.DataAnnotations;

namespace IaeBoraLibrary.Model
{
    public class User
    {
        [Key]
        public string GoogleId { get; set; }
        public string Address { get; set; }
    }
}
