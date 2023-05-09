using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

using SledgePlus.Data.Models;

namespace SledgePlus.WPF.Views.UserControls.Custom;

public class UsersDataGrid : DataGrid
{
    public UsersDataGrid()
    {
        SelectionChanged += DataGridCustom_SelectionChanged;
    }

    private void DataGridCustom_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (Items.SourceCollection == null) return;
        ItemsList = new ObservableCollection<User>((ObservableCollection<User>)Items.SourceCollection);
    }

    #region ItemsListProperty

    public IEnumerable<User> ItemsList
    {
        get => (IEnumerable<User>)GetValue(ItemsListProperty);
        set => SetValue(ItemsListProperty, value);
    }

    public static readonly DependencyProperty ItemsListProperty =
        DependencyProperty.Register(
            nameof(ItemsList),
            typeof(IEnumerable<User>),
            typeof(UsersDataGrid),
            new PropertyMetadata(null));

    #endregion ItemsListProperty
}