using System;
using System.Collections.Generic;
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

namespace SledgePlus.WPF.Views.UserControls.UserPanels
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelUserControl.xaml
    /// </summary>
    public partial class AdminPanelUserControl : UserControl
    {
        public AdminPanelUserControl()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ContentVerticalOffset - e.Delta);
        }

        private void ToSignInButton_OnClick(object sender, RoutedEventArgs e)
        {
            DataGrid.CancelEdit();
        }
    }
}