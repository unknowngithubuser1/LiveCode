using System;
using System.Collections.Generic;
using System.Linq;
using Assisticant;
using livecode.wpf.MVVM.Models;

namespace livecode.wpf.MVVM.ViewModels.Locators
{
    public class PSPFormsVMLocator : ViewModelLocatorBase
    {
        private DefectFormBrowserViewModel _defectBrowser = new DefectFormBrowserViewModel(new DefectFormViewModel[0]);
        private TimeFormBrowserViewModel _timeBrowser = new TimeFormBrowserViewModel(new TimeFormViewModel[0]);
        private SizeFormBrowserViewModel _sizeBrowser = new SizeFormBrowserViewModel(new SizeFormViewModel[0]);

        public static event Action SizeFormsUpdated;
        public static event Action TimeFormsUpdated;
        public static event Action DefectFormsUpdated;

        public object DefectForms
        {
            get { return ViewModel(() => _defectBrowser); }
        }

        public object TimeForms
        {
            get { return ViewModel(() => _timeBrowser); }
        }

        public object SizeForms
        {
            get { return ViewModel(() => _sizeBrowser); }
        }

        public void PopulateDefectForms(DefectFormModel[] forms)
        {
            var newSet = forms.Select(f => new DefectFormViewModel(f, (DefectFormModel)f.Clone())).ToArray();

            _defectBrowser.AddToItems(newSet);

            DefectFormsUpdated?.Invoke();
        }

        public void PopulateTimeForms(TimeFormModel[] forms)
        {
            var newSet = forms.Select(f => new TimeFormViewModel(f, (TimeFormModel)f.Clone())).ToArray();

            _timeBrowser.AddToItems(newSet);

            TimeFormsUpdated?.Invoke();
        }

        public void PopulateSizeForms(SizeFormModel[] forms)
        {
            var newSet = forms.Select(f => new SizeFormViewModel(f, (SizeFormModel)f.Clone())).ToArray();

            _sizeBrowser.AddToItems(newSet);

            SizeFormsUpdated?.Invoke();
        }

    }
}
