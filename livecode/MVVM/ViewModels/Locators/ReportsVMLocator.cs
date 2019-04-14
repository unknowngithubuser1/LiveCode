using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant;
using livecode.wpf.MVVM.Models;
using livecode.wpf.Util;

namespace livecode.wpf.MVVM.ViewModels.Locators
{
    public class ReportsVMLocator : ViewModelLocatorBase
    {
        private TimeReportViewModel _timeReport = new TimeReportViewModel(new TimeFormModel[0]);
        private SizeReportViewModel _sizeReport = new SizeReportViewModel(new SizeFormModel[0]);

        public object TimeReport => ViewModel(() => _timeReport);
        public object SizeReport => ViewModel(() => _sizeReport);

        public void PopulateTimeReports(TimeFormModel[] forms)
        {
            _timeReport.ReplaceForms(forms);
        }

        private void PopulateSizeReports(SizeFormModel[] forms)
        {
            _sizeReport.ReplaceForms(forms);
        }

        public void ReloadTimeReports()
        {
            PopulateTimeReports(LoadFormData.LoadTimeForms());
        }

        public void ReloadSizeReports()
        {
            PopulateSizeReports(LoadFormData.LoadSizeForms());
        }

    }
}
