using Dashboard.ViewModels;
using Dashboard.Views;
using System;
using System.Windows.Input;

namespace Dashboard;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow(
        HomesViewModel homesViewModel,
        ShowHomesViewModel showHomesViewModel,
        NCStateViewModel addHomeViewModel)
    {
        //DataContext = viewModel;
        InitializeComponent();

        //HomesViewModel.DataContext = homesViewModel;

        //AddHomeView.DataContext = addHomeViewModel;
        //ShowHomesView.DataContext = showHomesViewModel;

        CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, OnClose));
    }

    private void OnClose(object sender, ExecutedRoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}