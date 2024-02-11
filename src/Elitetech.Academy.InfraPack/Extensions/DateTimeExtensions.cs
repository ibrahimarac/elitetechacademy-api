namespace Elitetech.Academy.InfraPack.Extensions
{
    public static class DateTimeExtensions
    {
        //TimeZone bilgisi Linux işletim sisteminde TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time") ile elde edilemiyor.
        //immutable olduğundan aşağıdaki string yardımıyla bu değeri elde ediyoruz.
        //Bu değeri TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time").ToSerializedString() ile elde edildi.
        private static readonly TimeZoneInfo TurkeyTimeZone = TimeZoneInfo.FromSerializedString("Turkey Standard Time;180;(UTC+03:00) İstanbul;Türkiye Standart Saati;Türkiye Yaz Saati;[01:01:0001;12:31:2010;60;[0;03:00:00;3;5;0;];[0;04:00:00;10;5;0;];-60;][01:01:2011;12:31:2011;60;[0;03:00:00;3;5;1;];[0;04:00:00;10;5;0;];-60;][01:01:2012;12:31:2012;60;[0;03:00:00;3;5;0;];[0;04:00:00;10;5;0;];-60;][01:01:2013;12:31:2013;60;[0;03:00:00;3;5;0;];[0;04:00:00;10;5;0;];-60;][01:01:2014;12:31:2014;60;[0;03:00:00;3;5;1;];[0;04:00:00;10;5;0;];-60;][01:01:2015;12:31:2015;60;[0;03:00:00;3;5;0;];[0;04:00:00;11;2;0;];-60;][01:01:2016;12:31:2016;-60;[0;00:00:00;1;1;5;];[0;03:00:00;3;5;0;];];");

        public static DateTime ToTurkeyLocalTime(this DateTime utcTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, TurkeyTimeZone);
        }
    }
}
