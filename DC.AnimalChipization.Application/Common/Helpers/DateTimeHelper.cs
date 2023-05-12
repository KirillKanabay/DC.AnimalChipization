namespace DC.AnimalChipization.Application.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetTimeStamp()
        {
            var utcNow = DateTime.UtcNow;

            return DateTime.SpecifyKind
            (
                new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, utcNow.Minute, utcNow.Second),
                DateTimeKind.Utc
            );
        }
    }
}
