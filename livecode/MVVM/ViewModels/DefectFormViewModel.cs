using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.FilePoint;
using livecode.wpf.MVVM.Models;

namespace livecode.wpf.MVVM.ViewModels
{
    public class DefectFormViewModel
    {
        private readonly DefectFormModel _model;
        private readonly DefectFormModel _clone;
        
        public DefectFormViewModel(DefectFormModel model, DefectFormModel clone)
        {
            _model = model;
            _clone = clone;
        }

        public string CreationDate => _model.CreatedAt.ToString("d");
        public string CreationTime => _model.CreatedAt.ToString("hh:mm tt");

        public string Solution => _model.Solution;
        public string Program => _model.Program;

        public int Identifier => _model.Identifier;

        public CodeChangeType Type
        {
            get { return _model.Type; }
            set { _model.Type = value; }
        }

        public void ResetType()
        {
            Type = _clone.Type;
        }

        public CodeChangeReason Reason
        {
            get { return _model.Reason; }
            set { _model.Reason = value; }
        }

        public void ResetReason()
        {
            Reason = _clone.Reason;
        }
        
        public int FixTime
        {
            get { return _model.FixTime; }
            set { _model.FixTime = value; }
        }

        public void ResetFixTime()
        {
            FixTime = _clone.FixTime;
        }

        public string Comments
        {
            get { return _model.Comments; }
            set { _model.Comments = value; }
        }
    }
}
