using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Validation;
using livecode.wpf.MVVM.Models;
using livecode.wpf.Util;

namespace livecode.wpf.MVVM.ViewModels
{
    public class SizeFormViewModel
    {
        private readonly SizeFormModel _model;
        private readonly SizeFormModel _clone;


        public SizeFormViewModel(SizeFormModel model, SizeFormModel clone)
        {
            _model = model;
            _clone = clone;
        }

        public string SerializeForm()
        {
            var models = new[] { _model, _clone };

            return TextSerializer.XmlSerializeToString(models);
        }

        public string CreationDate => _model.CreatedAt.ToString("d");
        public string CreationTime => _model.CreatedAt.ToString("hh:mm tt");

        public string Solution => _model.Solution;
        public string Program => _model.Program;

        public int Base => _model.Base;
        public int Deleted => _model.Deleted;
        public int Modified => _model.Modified;
        public int Added => _model.Added;
        public int NewAndChanged => _model.Added + _model.Modified;
        public int Total => _model.Total;

        public int Reused
        {
            get { return _model.Reused; }
            set { _model.Reused = value; }
        }
        
        public void ResetReused()
        {
            Reused = _clone.Reused;
        }
    }
}
