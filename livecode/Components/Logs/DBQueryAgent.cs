using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Collections;
using KaVEData.Tools;
using livecode.wpf.Components.Logs.Models;
using livecode.wpf.MVVM.Models;

namespace livecode.wpf.Components.Logs
{
    public class DBQueryAgent
    {
        #region Singleton

        private static DBQueryAgent _instance;
        private static object _lock = true;

        public static DBQueryAgent Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ?? (_instance = new DBQueryAgent());
                }
            }
        }

        public static void Initialize()
        {
            if (_instance == null) _instance = new DBQueryAgent();
        }

        #endregion

        private DBQueryAgent()
        {
            
        }

        private KaVEEntities _baseContext = new KaVEEntities();
        private long _maxgap = 180000;

        public TimeFormModel GetTimeLogs(DateTime start, DateTime end)
        {
            var startId = _baseContext.Events.First(e => e.TriggeredAt.Value >= start).EventId;
            var endId = _baseContext.Events.OrderByDescending(e => e.TriggeredAt).First(e => e.TriggeredAt.Value <= end).EventId;

            var total = TotalActivity(startId, endId);
            var code = CodeActivity(startId, endId);
            var debug = DebugActivity(startId, endId);
            var test = TestActivity(startId, endId);
            var doc = DocumentActivity(startId, endId);

            TimeFormModel m = new TimeFormModel(Guid.NewGuid())
            {
                CreatedAt = DateTime.Now,
                From = start,
                To = end,
                Documents = new ObservableList<DocumentActivityModel>(
                    doc.Select(d => new DocumentActivityModel(d.DocumentName, (int)Math.Round(d.Duration.TotalMinutes))).ToArray()),
                Interruption = (int)(end.Subtract(start).TotalMinutes - (int)total.Sum(a => a.ActivityMinutes)),
                CodingTime = (int)getIntersect(total, code),
                DebuggingTime = (int)getIntersect(total, debug),
                TestingTime = (int)getIntersect(total, test)
        };

            return m;
        }

        private double getIntersect(List<TimelineActivity> total, List<TimelineActivity> code)
        {
            double codingTime = 0;
            foreach (var c in code)
            {
                foreach (var t in total)
                {
                    codingTime += IntersectLength(c.StartTime, t.StartTime, c.EndTime, t.EndTime);
                }
            }

            return codingTime;
        }

        private double IntersectLength(DateTime start1, DateTime start2, DateTime end1, DateTime end2)
        {
            if (start1 < start2)
            {
                if (end1 < end2)
                    return Math.Max(0,end1.Subtract(start2).TotalMinutes);
                else
                    return Math.Max(0, end2.Subtract(start2).TotalMinutes);
            }
            else
            {
                if (end1 < end2)
                    return Math.Max(0, end1.Subtract(start1).TotalMinutes);
                else
                    return Math.Max(0, end2.Subtract(start1).TotalMinutes);
            }
        }

        private List<TimelineActivity> TotalActivity(int startId, int endId)
        {
            _baseContext.Database.CommandTimeout = Int32.MaxValue;

            var startidP = new SqlParameter("@startId", startId);
            var endidP = new SqlParameter("@endId", endId);
            var maxgapP = new SqlParameter("@maxGap", _maxgap);

            var result = _baseContext.Database.SqlQuery<TimelineActivity>("CollapseActivities @startId,@endId,@maxGap", startidP, endidP, maxgapP).ToList();

            result.ForEach(i => i.Description = "Activity");

            return result;
        }
        
        private List<TimelineActivity> CodeActivity(int startId, int endId)
        {
            _baseContext.Database.CommandTimeout = Int32.MaxValue;

            var startidP = new SqlParameter("@startId", startId);
            var endidP = new SqlParameter("@endId", endId);
            var maxgapP = new SqlParameter("@maxGap", _maxgap);

            var result = _baseContext.Database.SqlQuery<TimelineActivity>("CollapseCodes @startId,@endId,@maxGap", startidP, endidP, maxgapP).ToList();

            result.ForEach(i => i.Description = "Code");

            return result;
        }

        private List<TimelineActivity> DebugActivity(int startId, int endId)
        {
            _baseContext.Database.CommandTimeout = Int32.MaxValue;

            var startidP = new SqlParameter("@startId", startId);
            var endidP = new SqlParameter("@endId", endId);
            var maxgapP = new SqlParameter("@maxGap", _maxgap);

            var result = _baseContext.Database.SqlQuery<TimelineActivity>("CollapseDebugs @startId,@endId,@maxGap", startidP, endidP, maxgapP).ToList();

            result.ForEach(i => i.Description = "Debug");

            return result;
        }

        private List<TimelineActivity> TestActivity(int startId, int endId)
        {
            _baseContext.Database.CommandTimeout = Int32.MaxValue;
            
            var startidP = new SqlParameter("@startId", startId);
            var endidP = new SqlParameter("@endId", endId);
            var maxgapP = new SqlParameter("@maxGap", _maxgap);

            var result = _baseContext.Database.SqlQuery<TimelineActivity>("CollapseTests @startId,@endId,@maxGap", startidP, endidP, maxgapP).ToList();

            result.ForEach(i => i.Description = "Test");

            return result;
        }
        
        private List<DocumentTimelineActivity> DocumentActivity(int startId, int endId)
        {
            _baseContext.Database.CommandTimeout = Int32.MaxValue;
            
            var startidP = new SqlParameter("@startId", startId);
            var endidP = new SqlParameter("@endId", endId);
            var maxgapP = new SqlParameter("@maxGap", _maxgap/3);
            var minactP = new SqlParameter("@minActivityMinute", 1);

            var result = _baseContext.Database.SqlQuery<DocumentTimelineActivity>("CollapseActivitiesByDocumentName  @startId,@endId,@maxGap, @minActivityMinute", startidP, endidP, maxgapP, minactP)
                .ToList();

            result.ForEach(i => i.Description = $"{i.DocumentName} ({(int)i.Duration.TotalMinutes} mins)");

            return result;
        }
    }
}
