using System;

namespace IaeBoraLibrary.Utils
{
    public static class DaysOfWeekTools
    {
        public static DayOfWeek TranslateDay(string day)
        {
            switch (day)
            {
                case "domingo":
                    return DayOfWeek.Sunday;
                case "segunda-feira":
                    return DayOfWeek.Monday;
                case "terça-feira":
                    return DayOfWeek.Tuesday;
                case "quarta-feira":
                    return DayOfWeek.Wednesday;
                case "quinta-feira":
                    return DayOfWeek.Thursday;
                case "sexta-feira":
                    return DayOfWeek.Friday;
                case "sábado":
                    return DayOfWeek.Saturday;
                default:
                    throw new Exception("Esse dia não está cadastrado no sistema. Dia: " + day);
            }
        }
    }
}
