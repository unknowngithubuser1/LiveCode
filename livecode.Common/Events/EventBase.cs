using System;

namespace livecode.Common.Events
{
    public class EventBase
    {
        private static long _seqId = 0;

        public EventBase()
        {
            Identifier = Guid.NewGuid();
            SequenceId = ++_seqId;

            TriggeredAt = DateTime.Now;
            TimeStamp = DateTime.Now.Subtract(new DateTime(2000, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            Duration = TimeSpan.Zero;

            TriggeredBy = "Unknown";
            Type = GetType().FullName;
        }

        public Guid Identifier { get; set; }
        public long SequenceId { get; set; }

        public DateTime TriggeredAt { get; set; }
        public double TimeStamp { get; set; }
        public TimeSpan Duration { get; set; }

        public string TriggeredBy { get; set; }

        public string Type { get; set; }
        public virtual string Details { get; set; }

    }
}
