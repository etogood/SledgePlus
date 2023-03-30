using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace SledgePlus.WPF.Models.Converters;

public class IndexConverter : IValueConverter 
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var item = (DataGridRow)value;
        var dataGrid = ItemsControl.ItemsControlFromItemContainer(item) as DataGrid;
        var index = dataGrid.ItemContainerGenerator.IndexFromContainer(item) + 1;
        return index.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}