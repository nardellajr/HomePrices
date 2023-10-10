using System.Windows;
using System.Windows.Controls;

namespace Dashboard.Views
{
    /// <summary>
    /// Interaction logic for ShowHomesView.xaml
    /// </summary>
    public partial class ShowHomesView : UserControl
    {
        public ShowHomesView()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.ShowHomesViewModel vm)
            {
                await vm.RefreshCommand.ExecuteAsync(null);
            }
        }
    }
}
