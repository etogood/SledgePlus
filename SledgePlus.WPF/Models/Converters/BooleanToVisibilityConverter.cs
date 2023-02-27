using System.Globalization;
using System.Windows.Data;

namespace SledgePlus.WPF.Models.Converters;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value switch
        {
            true => Visibility.Visible,
            false => Visibility.Collapsed
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Visibility)value switch
        {
            Visibility.Visible => true,
            Visibility.Collapsed => false,
            Visibility.Hidden => false,
            _ => false
        };
    }
}