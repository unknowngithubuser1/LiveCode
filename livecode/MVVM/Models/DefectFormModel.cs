using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Fields;
using livecode.ComponentBase.FilePoint;

namespace livecode.wpf.MVVM.Models
{
    public class DefectFormModel : ICloneable
    {
        private Observable<DateTime> _createdAt = new Observable<DateTime>(DateTime.Now);

        private Observable<int> _identifier = new Observable<int>(4);

        private Observable<string> _solution = new Observable<string>("livecode");
        private Observable<string> _program = new Observable<string>("DiffHelper.cs");

        private Observable<CodeChangeType> _type = new Observable<CodeChangeType>(CodeChangeType.Interface);
        private Observable<CodeChangeReason> _reason = new Observable<CodeChangeReason>(CodeChangeReason.Communication);

        private Observable<int> _fixTime = new Observable<int>(5);
        private Observable<string> _comments = new Observable<string>();

        public Guid Id { get; }

        public DefectFormModel(Guid id)
        {
            Id = id;
        }
        

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt.Value = value; }
        }

        public int Identifier
        {
            get { return _identifier; }
            set { _identifier.Value = value; }
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

        public CodeChangeType Type
        {
            get { return _type; }
            set { _type.Value = value; }
        }
        
        public CodeChangeReason Reason
        {
            get { return _reason; }
            set { _reason.Value = value; }
        }

        public int FixTime
        {
            get { return _fixTime; }
            set { _fixTime.Value = value; }
        }
        
        public string Comments
        {
            get { return _comments; }
            set { _comments.Value = value; }
        }

        public object Clone()
        {
            return new DefectFormModel(Id)
            {
                CreatedAt = CreatedAt,
                Comments = Comments,
                Type = Type,
                FixTime = FixTime,
                Reason = Reason,
                Identifier = Identifier,
                Program = Program,
                Solution = Solution
            };
        }
    }
}
