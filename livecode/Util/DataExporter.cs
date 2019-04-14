using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.wpf.Components.Logs;

namespace livecode.wpf.Util
{
    public class DataExporter : IDownloadOperator<bool>
    {
        public async Task<bool> Download()
        {
            return await Task.Run(() =>
            {
                DBExportAgent.Instance.SaveToDisk();

                return true;
            });
        }
    }
}
