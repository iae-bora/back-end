using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IaeBoraLibrary.Model
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Enums.FoodsEnum Food { get; set; }
        public Enums.MusicsEnum Musics { get; set; }
        public Enums.MoviesEnum Movies { get; set; }
        public Enums.ReligionsEnum Religion { get; set; }
        public Enums.SportsEnum Sports { get; set; }
        public Enums.TeamsEnum Teams { get; set; }
        public Enums.HaveChildrenEnum HaveChildren { get; set; }
        public int UserAge { get; set; }
        public int PlacesCount { get; set; }
        public DateTime RouteDateAndTime { get; set; }

        public User User { get; set; }
    }
}