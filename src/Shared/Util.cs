namespace ken_lo.Shared;
public static class Util
{
    public static DateTime HorarioOficialBrasilia() => TimeZoneInfo.ConvertTime(
        DateTime.Now,
        TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")
    );
   
}
