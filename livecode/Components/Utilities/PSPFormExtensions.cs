using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.ComponentBase.FilePoint;
using livecode.ComponentBase.Measures;
using livecode.wpf.Logs;
using livecode.wpf.MVVM.Models;

namespace livecode.wpf.Components.Utilities
{
    public static class PSPFormExtensions
    {
        private static List<Guid> _visitedHistories = new List<Guid>();

        public static SizeFormModel[] ToPSPForm(this List<ComponentBase.FilePoint.CodeFile> files)
        {
            List<SizeFormModel> forms = new List<SizeFormModel>();

            foreach (var codeFile in files)
            {
                CodeHistory[] newChanges = new CodeHistory[0];
                PSPSizeMeasure[] measures = new PSPSizeMeasure[0];

                try
                {
                    newChanges = codeFile.Changes.SkipWhile(h => _visitedHistories.Contains(h.Id)).ToArray();
                    measures = newChanges.Select(h => h.GetMeasurement(typeof(PSPSizeMeasure).FullName)).Cast<PSPSizeMeasure>().ToArray();

                    var measure = consolidateMeasures(measures);

                    if (measure == null || measure.NewAndChanged + measure.Deleted == 0)
                        continue;

                    forms.Add(new SizeFormModel(Guid.NewGuid())
                    {
                        Program = codeFile.Name,
                        CreatedAt = DateTime.Now,
                        Base = measure.Base,
                        Added = measure.Added,
                        Modified = measure.Modified,
                        Deleted = measure.Deleted,
                        Reused = measure.Reused,
                        Total = measure.Total,
                        Solution = GetProjectName()
                    });
                }
                catch (Exception e)
                {
                    Logger.Instance.Log(e.ToString());
                }

                for (var i = 0; i < newChanges.Length - 1 ; i++)
                {
                    _visitedHistories.Add(newChanges[i].Id);
                }
            }

            return forms.ToArray();
        }

        private static string GetProjectName()
        {
            string dir = Properties.Settings.Default.ProjectDirectory;

            return dir.Substring(dir.LastIndexOf("\\") + 1);
        }

        private static PSPSizeMeasure consolidateMeasures(PSPSizeMeasure[] measures)
        {
            if (measures.Length == 2)
                return measures[1];
            else if (measures.Length == 1)
                return null;
            else if (measures.Length == 0)
                return null;

            var first = measures.First();
            var last = measures.Last();

            PSPSizeMeasure result = new PSPSizeMeasure()
            {
                Base = first.Base,
                Total = last.Total
            };

            for (int i = 1; i < measures.Length; i++)
            {
                result.Added += measures[i].Added;
                result.Modified += measures[i].Modified;
                result.Deleted += measures[i].Deleted;
            }

            return result;
        }
    }
}
