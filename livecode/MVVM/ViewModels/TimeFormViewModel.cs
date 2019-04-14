using System;
using System.Collections.Generic;
using Assisticant.Fields;
using livecode.wpf.MVVM.Models;
using livecode.wpf.Util;

namespace livecode.wpf.MVVM.ViewModels
{
    public class TimeFormViewModel
    {
        private readonly TimeFormModel _model;
        private readonly TimeFormModel _clone;

        public TimeFormViewModel(TimeFormModel model, TimeFormModel clone)
        {
            _model = model;
            _clone = clone;
        }

        public string SerializeForm()
        {
            var models = new[] {_model, _clone};

            return TextSerializer.XmlSerializeToString(models);
        }

        public string CreationDate => _model.CreatedAt.ToString("d");
        public string CreationTime => _model.CreatedAt.ToString("hh:mm tt");

        public string Solution => _model.Solution;

        public string From => _model.From.ToString("hh:mm tt");
        public string To => _model.To.ToString("hh:mm tt");
        public int Duration => (int)_model.To.Subtract(_model.From).TotalMinutes;

        public IEnumerable<DocumentActivityModel> Documents => _model.Documents;
        
        public int CodingTime
        {
            get { return _model.CodingTime; }
            set { _model.CodingTime = value; }
        }

        public int TestingTime
        {
            get { return _model.TestingTime; }
            set { _model.TestingTime = value; }
        }

        public int DebuggingTime
        {
            get { return _model.DebuggingTime; }
            set { _model.DebuggingTime = value; }
        }

        public int Interruption
        {
            get { return _model.Interruption; }
            set { _model.Interruption = value; }
        }

        public void ResetInterruption()
        {
            Interruption = _clone.Interruption;
        }

        public void ResetCodingTime()
        {
            CodingTime = _clone.CodingTime;
        }

        public void ResetTestingTime()
        {
            TestingTime = _clone.TestingTime;
        }

        public void ResetDebuggingTime()
        {
            DebuggingTime = _clone.DebuggingTime;
        }
        
    }
}
