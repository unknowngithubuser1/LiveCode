using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using livecode.wpf.Listeners;

namespace livecode.wpf.Util
{
    public class ListenerStarter : IDownloadOperator<bool>
    {

        public async Task<bool> Download()
        {
            return await Task.Run(() =>
            {
                Listener.Start();
                return true;
            });
        }

    }
}
