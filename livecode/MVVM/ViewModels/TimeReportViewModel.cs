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
using livecode.wpf.Util;

namespace livecode.wpf.MVVM.ViewModels
{
    public class TimeReportViewModel
    {
        private ObservableList<TimeFormModel> _models = new ObservableList<TimeFormModel>();

        public TimeReportViewModel(TimeFormModel[] models)
        {
            _models.AddRange(models);
        }
        
        public void ReplaceForms(TimeFormModel[] forms)
        {
            _models.Clear();
            _models.AddRange(forms);
        }

        public ICommand Reload => MakeCommand.Do(DoReload);
        private void DoReload()
        {
            App.VMLocator<ReportsVMLocator>().ReloadTimeReports();
        }

        public int Efficiency
        {
            get
            {
                if (_models.Count == 0)
                    return 0;

                double avg = _models.Average(m => 100 - m.Interruption*100 / Math.Max(1, m.Duration));
                avg = Math.Max(Math.Min(100, avg), 0);

                return (int) avg;
            }
        }

        public int CodingPercent
        {
            get
            {
                if (_models.Count == 0)
                    return 0;

                double avg = _models.Average(m => 100 - m.CodingTime * 100 / Math.Max(1, m.Duration));
                avg = Math.Max(Math.Min(100, avg), 0);

                return (int) avg;
            }
        }
        
        public int TestingPercent
        {
            get
            {
                if (_models.Count == 0)
                    return 0;

                double avg = _models.Average(m => 100 - m.TestingTime * 100 / Math.Max(1, m.Duration));
                avg = Math.Max(Math.Min(100, avg), 0);

                return (int)avg;
            }
        }

        public int DebuggingPercent
        {
            get
            {
                if (_models.Count == 0)
                    return 0;

                double avg = _models.Average(m => 100 - m.DebuggingTime * 100 / Math.Max(1, m.Duration));
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
                    return "(w1) You have too many interruptions!";

                if (DebuggingPercent > CodingPercent + 30 ||
                    TestingPercent > CodingPercent + 30)
                    return "(w2) You spend too much time testing/debugging your solution.";

                if (CodingPercent > DebuggingPercent + 30 ||
                    CodingPercent > TestingPercent + 30)
                    return "(w3) You are too confident with your code/design.";

                if (Efficiency > 90)
                    return "(w4) You might be rushing a little bit.";

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
                    return "(a1) At least code some of the parts that you feel confident about.";

                if (Warnings.StartsWith("(w2)"))
                    return "(a2) Spend more time designing the solution before coding. This will make you more confident in your code.";

                if (Warnings.StartsWith("w3"))
                    return "(a3) Make sure a solution works before coding it.";

                if (Warnings.StartsWith("a4"))
                    return "(a4) Discuss design with a partner before jumping into it.";

                return "(--) Keep it up ;)";
            }
        }
    }
}
