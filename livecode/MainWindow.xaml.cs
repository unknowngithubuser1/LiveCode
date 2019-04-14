using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assisticant.Collections;
using livecode.wpf.Components;
using livecode.wpf.Dialogs.Mini;
using livecode.wpf.Listeners;
using livecode.wpf.MVVM.Models;
using livecode.wpf.MVVM.ViewModels.Locators;
using livecode.wpf.Util;
using ManagedWinapi.Windows;

namespace livecode.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> NavItems { get; set; } =
            new List<string>()
            {
                "Dashboard",
                "Components",
                "Settings",
                "About",
                "Exit"
            };

        public MainWindow()
        {
            InitializeComponent();

            Listener.Initialize();
            
            PSPFormsVMLocator.SizeFormsUpdated += OnSizeFormsUpdated;
            PSPFormsVMLocator.TimeFormsUpdated += OnTimeFormsUpdated;
                
        }

        private void OnTimeFormsUpdated()
        {
            App.Current.Dispatcher.Invoke(MiniTimeLogDialog.CreateDialog);
        }

        private void OnSizeFormsUpdated()
        {
            App.Current.Dispatcher.Invoke(MiniSizeLogDialog.CreateDialog);
        }

        private async void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AsyncDataDownloadAgent<bool> a = new AsyncDataDownloadAgent<bool>(new ContentChanger()
            {
                Control = _Content,
                List = _NavListBox,
                Button = MenuToggleButton
            });

            await a.PromptDownload("MainDialog");
        }
    }
}
