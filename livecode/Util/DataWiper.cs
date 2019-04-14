using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.wpf.Components.Logs;

namespace livecode.wpf.Util
{
    public class DataWiper : IDownloadOperator<bool>
    {
        public Task<bool> Download()
        {
            return Task.Run(() =>
            {
                DBExportAgent.Instance.DeleteEverything();

                return true;
            });
        }
    }
}
