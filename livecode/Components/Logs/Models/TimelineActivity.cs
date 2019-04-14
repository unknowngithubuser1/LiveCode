using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livecode.wpf.Components.Logs.Models
{
    public class TimelineActivity
    {
        public decimal ActivityMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SessionId { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration => EndTime.Subtract(StartTime);

        public string Details => Description + "\n" + StartTime.ToShortDateString();



        public TimelineActivity(DateTime s, DateTime e)
        {
            StartTime = s;
            EndTime = e;
        }

        public TimelineActivity()
        {

        }

    }
}
