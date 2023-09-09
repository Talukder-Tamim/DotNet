using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportingClass
{
    public class DateTimeFormatter
    {
        public string ConvertTime(string time, string version)
        {
            int hour = int.Parse(time.Substring(0, 2));
            int minute = int.Parse(time.Substring(3, 2));
            string timeSuffix = hour < 12 ? "AM" : "PM";

            Dictionary<int, string> digitDictionary = new Dictionary<int, string>()
            {
                { 0, "০" },
                { 1, "১" },
                { 2, "২" },
                { 3, "৩" },
                { 4, "৪" },
                { 5, "৫" },
                { 6, "৬" },
                { 7, "৭" },
                { 8, "৮" },
                { 9, "৯" }
            };

            string banglaDigit(string value)
            {

                string numberString = value;
                int firstDigit = int.Parse(value.Substring(0, 1));
                int secondDigit = int.Parse(value.Substring(1, 1));
                return $"{digitDictionary[firstDigit]}{digitDictionary[secondDigit]}";

            }

            if (version == "BN")
            {
                string banglaSuffix = hour < 12 ? "পূর্বাহ্ণ" : "অপরাহ্ণ";

                if (hour == 0)
                {
                    hour = 12;
                }
                else if (hour > 12)
                {
                    hour -= 12;
                }

                string banglaHour = banglaDigit(hour.ToString().PadLeft(2, '0'));
                string banglaMinute = banglaDigit(minute.ToString().PadLeft(2, '0'));

                return $"{banglaHour}:{banglaMinute} {banglaSuffix}";
            }
            else
            {
                if (hour == 0)
                {
                    hour = 12;
                }
                else if (hour > 12)
                {
                    hour -= 12;
                }

                string englishHour = hour.ToString().PadLeft(2, '0');
                string englishMinute = minute.ToString().PadLeft(2, '0');

                return $"{englishHour}:{englishMinute} {timeSuffix}";
            }
        }
        public string ConvertDate(string dateString, string version)
        {
            Dictionary<int, string> digitDictionary = new Dictionary<int, string>()
            {
                { 0, "০" },
                { 1, "১" },
                { 2, "২" },
                { 3, "৩" },
                { 4, "৪" },
                { 5, "৫" },
                { 6, "৬" },
                { 7, "৭" },
                { 8, "৮" },
                { 9, "৯" }
            };
            string banglaDigit(string value)
            {
                if (value.Length == 2)
                {
                    string numberString = value;
                    int firstDigit = int.Parse(value.Substring(0, 1));
                    int secondDigit = int.Parse(value.Substring(1, 1));
                    return $"{digitDictionary[firstDigit]}{digitDictionary[secondDigit]}";
                }
                if (value.Length == 4)
                {
                    string numberString = value;
                    int firstDigit = int.Parse(value.Substring(0, 1));
                    int secondDigit = int.Parse(value.Substring(1, 1));
                    int thirdDigit = int.Parse(value.Substring(2, 1));
                    int fourthDigit = int.Parse(value.Substring(3, 1));
                    return $"{digitDictionary[firstDigit]}{digitDictionary[secondDigit]}{digitDictionary[thirdDigit]}{digitDictionary[fourthDigit]}";
                }
                return digitDictionary[1];
            }

            DateTime date;
            if (!DateTime.TryParse(dateString, out date))
            {
                return "Invalid date format";
            }

            CultureInfo culture;
            if (version.Equals("EN", StringComparison.OrdinalIgnoreCase))
            {
                culture = new CultureInfo("en-US");
                string englishDate = date.ToString("dd", culture);
                string englishMonth = culture.DateTimeFormat.GetMonthName(date.Month);
                string englishYear = date.ToString("yyyy", culture);
                return $"{englishDate} {englishMonth} {englishYear}";
            }
            else if (version.Equals("BN", StringComparison.OrdinalIgnoreCase))
            {
                culture = new CultureInfo("bn-BD");
                string banglaDate = banglaDigit(date.ToString("dd", culture));
                string banglaMonth = culture.DateTimeFormat.GetMonthName(date.Month);
                string banglaYear = banglaDigit(date.ToString("yyyy", culture));
                return $"{banglaDate} {banglaMonth} {banglaYear}";
            }
            else
            {
                return "Invalid version";
            }
        }
    }
}
