using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Collections;
using Assisticant.Fields;

namespace livecode.wpf.MVVM.Models
{
    public class TimeFormModel : ICloneable
    {
        private Observable<DateTime> _createdAt = new Observable<DateTime>(DateTime.Now);

        private Observable<string> _solution = new Observable<string>("livecode");
        private Observable<DateTime> _from = new Observable<DateTime>(DateTime.Now);
        private Observable<DateTime> _to = new Observable<DateTime>(DateTime.Now.AddMinutes(40));
        private Observable<int> _interruption = new Observable<int>(6);

        private Observable<int> _codingTime = new Observable<int>(25);
        private Observable<int> _testingTime = new Observable<int>(10);
        private Observable<int> _debuggingTime = new Observable<int>(4);

        public Guid Id { get; }

        public TimeFormModel(Guid id)
        {
            Id = id;
        }

        public TimeFormModel()
        {
            
        }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt.Value = value; }
        }

        public int Duration => (int)_to.Value.Subtract(_from.Value).TotalMinutes;

        public ObservableList<DocumentActivityModel> Documents { get; set; } = new ObservableList<DocumentActivityModel>();

        public string Solution
        {
            get { return _solution; }
            set { _solution.Value = value; }
        }

        public DateTime From
        {
            get { return _from; }
            set { _from.Value = value; }
        }

        public DateTime To
        {
            get { return _to; }
            set { _to.Value = value; }
        }

        public int Interruption
        {
            get { return _interruption; }
            set { _interruption.Value = value; }
        }

        public int CodingTime
        {
            get { return _codingTime; }
            set { _codingTime.Value = value; }
        }

        public int TestingTime
        {
            get { return _testingTime; }
            set { _testingTime.Value = value; }
        }

        public int DebuggingTime
        {
            get { return _debuggingTime; }
            set { _debuggingTime.Value = value; }
        }

        public object Clone()
        {
            return new TimeFormModel(Id)
            {
                CreatedAt = CreatedAt,
                CodingTime = CodingTime,
                TestingTime = TestingTime,
                DebuggingTime = DebuggingTime,
                Documents = Documents,
                From = From,
                To = To,
                Interruption = Interruption,
                Solution = Solution,
            };
        }
    }
}
