using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace livecode.wpf.Converters
{
    public class IconKindConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string v = value?.ToString() ?? "PlusNetwork";
            PackIconKind kind;

            if (Enum.TryParse(v, out kind))
                return kind;
            else
                return PackIconKind.PlusNetwork;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
