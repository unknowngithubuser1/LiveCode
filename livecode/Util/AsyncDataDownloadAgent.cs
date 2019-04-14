using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using livecode.wpf.Dialogs.Progress;
using MaterialDesignThemes.Wpf;

namespace livecode.wpf.Util
{
    /// <summary>
    /// An agent that automates the process of downloading dialogs using <see cref="IDownloadOperator{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the downloaded data</typeparam>
    public class AsyncDataDownloadAgent<T>
    {
        
        private IDownloadOperator<T> _operator;

        public AsyncDataDownloadAgent(IDownloadOperator<T> @operator)
        {
            _operator = @operator;
        }

        public async Task<T> PromptDownload()
        {
            var dialog = new AsyncProgressView { Padding = new Thickness(15) };

            object result = await DialogHost.Show(dialog, "RootDialog", OpenedEventHandler);

            return (T)result;

        }

        public async Task<T> PromptDownload(string hostIdentifier)
        {
            var dialog = new AsyncProgressView { Padding = new Thickness(15) };

            object result = await DialogHost.Show(dialog, hostIdentifier, OpenedEventHandler);

            return (T)result;
        }

        private async void OpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            T result = await _operator.Download();

            eventArgs.Session.Close(result);
        }


    }
}
