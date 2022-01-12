using System;

namespace CourseLibrary.API.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - dateTimeOffset.Year;

            // for leap year
            if (today < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }


    }
}
