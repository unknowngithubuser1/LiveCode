using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using livecode.wpf.Components.Logs;
using livecode.wpf.Dialogs.Mini;
using livecode.wpf.Listeners;
using livecode.wpf.MVVM.ViewModels.Locators;
using livecode.wpf.Util;
using MaterialDesignThemes.Wpf;

namespace livecode.wpf.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : UserControl
    {
        DispatcherTimer t = new DispatcherTimer(){Interval = TimeSpan.FromSeconds(1)};

        public DashboardPage()
        {
            InitializeComponent();

            Loaded += OnLoaded;

            t.Tick += (sender, args) => _TimerText.Text = Listener.Instance.MinutesLeft.ToString();
            if (!DesignerProperties.GetIsInDesignMode(this))
                t.Start();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            checkFiles();

           if (Listener.IsEnabled)
                _Enabled(this, null);
           else
                _Disabled(this, null);

        }

        private void checkFiles()
        {
            _CheckEnabled.IsEnabled = Listener.CanStart();
            _CheckEnabled.IsChecked = Listener.IsEnabled;
        }

        private async void _Enabled(object sender, RoutedEventArgs e)
        {
            _EnabledText.Text = "Enabled";
            _EnabledColorZone.Mode = ColorZoneMode.Accent;

            if (Listener.IsEnabled) return;

            AsyncDataDownloadAgent<bool> a = new AsyncDataDownloadAgent<bool>(new ListenerStarter());
            await a.PromptDownload("MainDialog");
        }

        private void _Disabled(object sender, RoutedEventArgs e)
        {
            _EnabledText.Text = "Disabled";
            _EnabledColorZone.Mode = ColorZoneMode.Standard;

            if (!Listener.IsEnabled) return;

            Listener.Stop();
        }

        private async void _PromptFormsClick(object sender, RoutedEventArgs e)
        {
            AsyncDataDownloadAgent<bool> a = new AsyncDataDownloadAgent<bool>(new ManualPrompter());
            await a.PromptDownload("MainDialog");
        }

        private async void _ExportClick(object sender, RoutedEventArgs e)
        {
            AsyncDataDownloadAgent<bool> a = new AsyncDataDownloadAgent<bool>(new DataExporter());
            await a.PromptDownload("MainDialog");
        }

        private async void _ClearClick(object sender, RoutedEventArgs e)
        {
            var d = MessageBox.Show("My Wise Dude, Are you sure?", "Clear Database", MessageBoxButton.YesNo);
            if (d == MessageBoxResult.No)
                return;

            AsyncDataDownloadAgent<bool> a = new AsyncDataDownloadAgent<bool>(new DataWiper());
            await a.PromptDownload("MainDialog");
        }

        private void _DefectReportClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void _ExitClick(object sender, RoutedEventArgs e)
        {
            var d = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo);
            if (d == MessageBoxResult.No)
                return;

            App.Current.Shutdown();
        }

        private void _TimeExpanderExpanded(object sender, RoutedEventArgs e)
        {
            App.VMLocator<ReportsVMLocator>().ReloadTimeReports();
        }

        private void _SizeExpanderExpanded(object sender, RoutedEventArgs e)
        {
            App.VMLocator<ReportsVMLocator>().ReloadSizeReports();
        }
    }
}
