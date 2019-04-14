using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignExtensions.Controls;
using MaterialDesignThemes.Wpf;
using UserControl = System.Windows.Controls.UserControl;

namespace livecode.wpf.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();

            loadValues();
        }

        private void _SaveClick(object sender, RoutedEventArgs e)
        {
            saveValues();
        }

        private void _ResetClick(object sender, RoutedEventArgs e)
        {
            loadValues();
        }

        private void loadValues()
        {
            _ProjectDirectoryTextBox.Text = Properties.Settings.Default.ProjectDirectory;
            _KaVEDirectoryTextBox.Text = Properties.Settings.Default.KaVEDirectory;
            _formIntervalTextBox.Text = Properties.Settings.Default.FormInterval.ToString();
        }

        private void saveValues()
        {
            Properties.Settings.Default.ProjectDirectory = _ProjectDirectoryTextBox.Text;
            Properties.Settings.Default.KaVEDirectory = _KaVEDirectoryTextBox.Text;
            Properties.Settings.Default.FormInterval = Int32.Parse(_formIntervalTextBox.Text);

            Properties.Settings.Default.Save();
        }

        private void _ProjectDirectoryBrowse(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog= new  FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.Cancel) return;

            _ProjectDirectoryTextBox.Text = dialog.SelectedPath;
        }

        private void _KaVEDirectoryBrowse(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.Cancel) return;

            _KaVEDirectoryTextBox.Text = dialog.SelectedPath;
        }
    }
}
