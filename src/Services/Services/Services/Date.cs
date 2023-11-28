using System;
using System.Collections.Generic;
using System.Globalization;

namespace Services.Services
{
    public class Date : IDate
    {
        public string ConvertDate(DateTime date)
        {
            var persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(date).ToString("0000/") + persianCalendar.GetMonth(date).ToString("00/") + persianCalendar.GetDayOfMonth(date).ToString("00");
        }

        public string PersianToEnglish(string persianStr)
        {
            var lettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9',
                ['/'] = '/',
                [':'] = ':',
                [' '] = ' '
            };

            foreach (var item in persianStr)
            {
                persianStr = persianStr.Replace(item, lettersDictionary[item]);
            }

            return persianStr;
        }

        public string BeautyTime(DateTimeOffset date)
        {
            var time = "بیشتر از 1 سال";

            var now = DateTimeOffset.Now;

            if (date - now < new TimeSpan(0, 1, 0, 0))
            {
                time = "کمتر از 1 ساعت";
            }
            else if (date - now < new TimeSpan(0, 12, 0, 0))
            {
                time = "کمتر از 12 ساعت";
            }
            else if (date - now > new TimeSpan(1, 0, 0, 0) &&
                     date - now < new TimeSpan(1, 23, 0, 0))
            {
                time = "1 روز قبل";
            }
            else if (date - now < new TimeSpan(7, 0, 0, 0) &&
                     date - now > new TimeSpan(6, 0, 0, 0))
            {
                time = "کمتر از 1 هفته";
            }
            else if (date - now < new TimeSpan(30, 0, 0, 0) &&
                     date - now > new TimeSpan(7, 0, 0, 0))
            {
                time = "کمتر از 1 ماه";
            }
            else if (date - now > new TimeSpan(30, 0, 0, 0) &&
                     date - now < new TimeSpan(180, 0, 0, 0))
            {
                time = "کمتر از 6 ماه";
            }
            else if (date - now > new TimeSpan(180, 0, 0, 0) &&
                     date - now < new TimeSpan(360, 0, 0, 0))
            {
                time = "کمتر از 1 سال";
            }

            return time;
        }
    }
}