using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Assisticant;
using Assisticant.Collections;
using livecode.wpf.MVVM.Models;
using livecode.wpf.MVVM.ViewModels.Locators;

namespace livecode.wpf.MVVM.ViewModels
{
    public class SizeReportViewModel
    {
        private ObservableList<SizeFormModel> _models = new ObservableList<SizeFormModel>();

        public SizeReportViewModel(SizeFormModel[] models)
        {
            _models.AddRange(models);
        }

        public void ReplaceForms(SizeFormModel[] forms)
        {
            _models.Clear();
            _models.AddRange(forms);
        }

        public ICommand Reload => MakeCommand.Do(DoReload);
        private void DoReload()
        {
            App.VMLocator<ReportsVMLocator>().ReloadSizeReports();
        }

        public int Efficiency
        {
            get
            {
                if (_models.Count == 0)
                    return 0;

                double avg = _models.Average(m => 100 - m.Deleted * 100 / Math.Max(1, m.Added + m.Modified));
                avg = Math.Max(Math.Min(100, avg), 0);

                return (int)avg;
            }
        }

        public int AddedPercent
        {
            get
            {
                if (_models.Count == 0)
                    return 0;

                double avg = _models.Average(m =>  m.Added * 100 / Math.Max(1, m.Added + m.Modified + m.Deleted));
                avg = Math.Max(Math.Min(100, avg), 0);

                return (int)avg;
            }
        }

        public int DeletedPercent
        {
            get
            {
                if (_models.Count == 0)
                    return 0;

                double avg = _models.Average(m => m.Deleted * 100 / Math.Max(1, m.Added + m.Modified + m.Deleted));
                avg = Math.Max(Math.Min(100, avg), 0);

                return (int)avg;
            }
        }

        public int ModifiedPercent
        {
            get
            {
                if (_models.Count == 0)
                    return 0;

                double avg = _models.Average(m => m.Modified * 100 / Math.Max(1, m.Added + m.Modified + m.Deleted));
                avg = Math.Max(Math.Min(100, avg), 0);

                return (int)avg;
            }
        }

        public string Warnings
        {
            get
            {
                if (_models.Count == 0)
                    return "Refresh to see warnings.";

                if (Efficiency < 50)
                    return "(w1) You throw away much of your code!";

                if (ModifiedPercent > 70)
                    return "(w2) You spend too much time modifying your code.";

                if (ModifiedPercent + DeletedPercent > AddedPercent + 30)
                    return "(w3) You write code that is not maintainable.";


                return "(--) Nothing seems to be wrong.";
            }
        }

        public string Advice
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Warnings))
                    return "";

                if (_models.Count == 0)
                    return "Refresh to see advices.";

                if (Warnings.StartsWith("(--)"))
                    return "(--) Keep it up :)";

                if (Warnings.StartsWith("w1"))
                    return "(a1) Spend more time designing/reviewing the solution before coding.";

                if (Warnings.StartsWith("(w2)"))
                    return "(a2) Do not rush into coding only to test a solution.";

                if (Warnings.StartsWith("w3"))
                    return "(a3) Write shorter classes/functions and spread duties.";


                return "(--) Keep it up ;)";
            }
        }
    }
}
