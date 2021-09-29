using System;

namespace IaeBoraLibrary.Utils
{
    public static class DaysOfWeekTools
    {
        public static DayOfWeek TranslateDay(string day)
        {
            return day switch
            {
                "domingo" => DayOfWeek.Sunday,
                "segunda-feira" => DayOfWeek.Monday,
                "terça-feira" => DayOfWeek.Tuesday,
                "quarta-feira" => DayOfWeek.Wednesday,
                "quinta-feira" => DayOfWeek.Thursday,
                "sexta-feira" => DayOfWeek.Friday,
                "sábado" => DayOfWeek.Saturday,
                _ => throw new Exception("Esse dia não está cadastrado no sistema. Dia: " + day),
            };
        }
    }
}
