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

namespace HomePrices.Views
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
