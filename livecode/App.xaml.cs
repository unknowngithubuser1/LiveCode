using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using livecode.wpf.Listeners;
using livecode.wpf.Logs;

namespace livecode.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Instance.Log(e.ToString());
            e.Handled = true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Listener.Stop();

            base.OnExit(e);
        }



        public static T VMLocator<T>()
        {
            string type = typeof(T).Name.Replace("ViewModel", "VM");

            return (T)Current.Resources[type];
        }

        public static T FindResource<T>(string v)
        {
            return (T)Current.Resources[v];
        }


    }
}
