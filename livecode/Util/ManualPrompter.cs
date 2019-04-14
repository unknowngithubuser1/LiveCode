using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.wpf.Listeners;

namespace livecode.wpf.Util
{
    public class ManualPrompter : IDownloadOperator<bool>
    {
        public async Task<bool> Download()
        {
            return await Task.Run(() =>
            {
                Listener.ManualPrompt();

                return true;
            });
        }
    }
}
