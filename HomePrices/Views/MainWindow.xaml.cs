using HomePrices.ViewModels;
using HomePrices.Views;
using System;
using System.Windows.Input;

namespace HomePrices;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow(
        ShowHomesViewModel showHomesViewModel,
        NCStateViewModel addHomeViewModel)
    {
        //DataContext = viewModel;
        InitializeComponent();

        AddHomeView.DataContext = addHomeViewModel;
        ShowHomesView.DataContext = showHomesViewModel;

        CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, OnClose));
    }

    private void OnClose(object sender, ExecutedRoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}