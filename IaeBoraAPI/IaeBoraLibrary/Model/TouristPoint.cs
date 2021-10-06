﻿
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IaeBoraLibrary.Model
{
    public class TouristPoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Opening_Hours OpeningHours { get; set; }
        public int Index { get; set; }
        public double DistanceFromOrigin { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Route Route { get; set; }
    }
}
