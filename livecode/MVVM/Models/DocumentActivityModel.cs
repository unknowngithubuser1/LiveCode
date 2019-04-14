using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Fields;

namespace livecode.wpf.MVVM.Models
{
    public class DocumentActivityModel
    {
        private Observable<string> _documentName = new Observable<string>();
        private Observable<int> _activityDuration = new Observable<int>();

        public string DocumentName
        {
            get { return _documentName; }
            set { _documentName.Value = value; }
        }

        public int ActivityDuration
        {
            get { return _activityDuration; }
            set { _activityDuration.Value = value; }
        }

        public DocumentActivityModel(string documentName, int activityDuration)
        {
            DocumentName = documentName;
            ActivityDuration = activityDuration;
        }

        public DocumentActivityModel()
        {
            
        }
    }
}
