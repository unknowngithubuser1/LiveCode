using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace livecode.wpf.Util
{
    public class ContentChanger : IDownloadOperator<bool>
    {
        public ContentControl Control { get; set; }
        public ListBox List { get; set; }
        public ToggleButton Button { get; set; }


        public async Task<bool> Download()
        {
            return await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                        //until we had a StaysOpen glag to Drawer, this will help with scroll bars
                        var dependencyObject = Mouse.Captured as DependencyObject;
                    while (dependencyObject != null)
                    {
                        if (dependencyObject is ScrollBar) return;
                        dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
                    }

                    Button.IsChecked = false;

                        //TODO: go to pages
                        var contentType = TypeHelper.TypeOf(List.SelectedItem + "Page");
                    Control.Content = Activator.CreateInstance(contentType);
                });
                
                return true;
            });
        }
    }
}

