using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Collections;
using Assisticant.Fields;

namespace livecode.wpf.MVVM.Models
{
    public class SizeFormModel : ICloneable
    {

        private Observable<DateTime> _createdAt = new Observable<DateTime>(DateTime.Now);

        private Observable<string> _solution = new Observable<string>("livecode");
        private Observable<string> _program = new Observable<string>("DiffHelper.cs");
        
        private Observable<int> _base = new Observable<int>();
        private Observable<int> _deleted = new Observable<int>();
        private Observable<int> _modified = new Observable<int>();
        private Observable<int> _added = new Observable<int>();
        private Observable<int> _reused = new Observable<int>();
        private Observable<int> _total = new Observable<int>();
        
        public Guid Id { get; }

        public SizeFormModel(Guid id)
        {
            Id = id;
        }

        public SizeFormModel()
        {
            
        }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt.Value = value; }
        }

        public string Solution
        {
            get { return _solution; }
            set { _solution.Value = value; }
        }

        public string Program
        {
            get { return _program; }
            set { _program.Value = value; }
        }
        
        public int Base
        {
            get { return _base; }
            set { _base.Value = value; }
        }
        
        public int Deleted
        {
            get { return _deleted; }
            set { _deleted.Value = value; }
        }

        public int Modified
        {
            get { return _modified; }
            set { _modified.Value = value; }
        }

        public int Added
        {
            get { return _added; }
            set { _added.Value = value; }
        }

        public int Reused
        {
            get { return _reused; }
            set { _reused.Value = value; }
        }
        
        public int Total
        {
            get { return _total; }
            set { _total.Value = value; }
        }

        public object Clone()
        {
            return new SizeFormModel(Id)
            {
                Modified = Modified,
                Added = Added,
                Deleted = Deleted,
                Total = Total,
                Base = Base,
                CreatedAt = CreatedAt,
                Program = Program,
                Reused = Reused,
                Solution = Solution
            };
        }
    }
}
