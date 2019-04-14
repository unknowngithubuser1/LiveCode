using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using Assisticant;
using livecode.wpf.MVVM.ViewModels;

namespace livecode.wpf.Dialogs.Mini
{
    /// <summary>
    /// Interaction logic for MiniTimeLogDialog.xaml
    /// </summary>
    public partial class MiniTimeLogDialog : Window
    {
        private static MiniTimeLogDialog _instance;
        public static void CreateDialog()
        {
            if (_instance == null)
            {
                _instance = new MiniTimeLogDialog();
                _instance.ShowDialog();
            }
        }

        public MiniTimeLogDialog()
        {
            InitializeComponent();
            manualPlacement();

            KeyUp += OnKeyUp;
            Closing += OnClosing;

            ForView.Unwrap<TimeFormBrowserViewModel>(DataContext).OnEmpty += OnOnEmpty;
        }

        private void OnOnEmpty()
        {
            Dispatcher.Invoke(Close);
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _instance = null;
        }

        private void manualPlacement()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width - 12;
            Top = desktopWorkingArea.Bottom - Height - 12;
        }

        private void OnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Escape)
                Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
