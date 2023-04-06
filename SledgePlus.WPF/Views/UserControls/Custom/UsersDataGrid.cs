using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SledgePlus.WPF.Models.DTOs;

namespace SledgePlus.WPF.Views.UserControls.Custom;

public class UsersDataGrid : DataGrid
{
    public UsersDataGrid()
    {
        SelectionChanged += DataGridCustom_SelectionChanged;
    }

    void DataGridCustom_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ItemsList = new ObservableCollection<UserDTO>((ObservableCollection<UserDTO>)Items.SourceCollection);
    }
    #region ItemsListProperty

    public IEnumerable<UserDTO> ItemsList
    {
        get => (IEnumerable<UserDTO>)GetValue(ItemsListProperty);
        set => SetValue(ItemsListProperty, value);
    }

    public static readonly DependencyProperty ItemsListProperty =
        DependencyProperty.Register(
            nameof(ItemsList), 
            typeof(IEnumerable<UserDTO>), 
            typeof(UsersDataGrid), 
            new PropertyMetadata(null));

    #endregion
}