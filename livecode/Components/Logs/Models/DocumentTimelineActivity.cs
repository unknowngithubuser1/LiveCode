using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livecode.wpf.Components.Logs.Models
{
    public class DocumentTimelineActivity
    {
        public int SessionId { get; set; }

        public decimal ActivityMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DocumentName { get; set; }


        public string Description { get; set; }

        public TimeSpan Duration => EndTime.Subtract(StartTime);

        public string Details => Description + "\n" + StartTime.ToShortDateString();

        public DocumentTimelineActivity(DateTime s, DateTime e)
        {
            StartTime = s;
            EndTime = e;
        }

        public DocumentTimelineActivity()
        {

        }
    }
}
