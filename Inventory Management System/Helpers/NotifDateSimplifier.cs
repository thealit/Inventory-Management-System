using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Helpers
{
    public static class NotifDateSimplifier
    {
        public static string Shorten(DateTime startTime)
        {
            TimeSpan difference = DateTime.Now - startTime;

            int totalSeconds = (int)difference.TotalSeconds;
            int totalMinutes = (int)difference.TotalMinutes;
            int totalHours = (int)difference.TotalHours;
            int totalDays = (int)difference.TotalDays;
            int totalWeeks = totalDays / 7;
            int totalMonths = (int)(totalDays / 30.44);  // avg days per month
            int totalYears = (int)(totalDays / 365.25); // avg days per year

            string date = "";

            // Just now — under 30 seconds
            if (totalSeconds < 30) { 
                date = "Just now";
                return date;
            }
              
            // Seconds ago — under a minute
            if (totalSeconds < 60)
            {
                date = $"{totalSeconds} seconds ago";
                return date;
            }

            // Minutes ago — under an hour
            if (totalMinutes < 60)
            {
                date = totalMinutes == 1 ? "1 minute ago" : $"{totalMinutes} minutes ago";
                return date;
            }

            // Hours ago — under a day
            if (totalHours < 24)
            {
                date = totalHours == 1 ? "1 hour ago" : $"{totalHours} hours ago";
                return date;
            }

            // Yesterday
            if (totalDays == 1)
            {
                date = "Yesterday";
                return date;
            }

            // Days ago — under a week
            if (totalDays < 7)
            {
                date = $"{totalDays} days ago";
                return date;
            }

            // Weeks ago — under a month
            if (totalDays < 30)
            {
                date = totalWeeks == 1 ? "1 week ago" : $"{totalWeeks} weeks ago";
                return date;
            }

            // Months ago — under a year
            if (totalDays < 365)
            {
                date = totalMonths == 1 ? "1 month ago" : $"{totalMonths} months ago";
                return date;
            }

            // Years ago
            date = totalYears == 1 ? "1 year ago" : $"{totalYears} years ago";
            return date;
        }
    }
}
