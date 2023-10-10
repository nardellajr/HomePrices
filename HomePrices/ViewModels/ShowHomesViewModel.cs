using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dashboard.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dashboard.ViewModels;

public partial class ShowHomesViewModel : ObservableObject, IRecipient<HomeUpdateMessage>
{
    private HomePricesContext Context { get; }
    private Func<Home, HomeItemViewModel> HomeViewModelFactory { get; }
    public ObservableCollection<HomeItemViewModel> Homes { get; } = new();

    public ShowHomesViewModel(HomePricesContext context,
        IMessenger messenger,
        Func<Home, HomeItemViewModel> homeViewModelFactory)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        messenger.RegisterAll(this);
        HomeViewModelFactory = homeViewModelFactory;

        // TODO: Load the homes from the database, do not do this, syncronously, use async
        //Context.Homes.Load();

        BindingOperations.EnableCollectionSynchronization(Homes, new object());
    }

    [RelayCommand]
    private async Task OnRefresh()
    {
        Homes.Clear();

        // TODO: This just here to simulate delay
        await Task.Delay(1_000);
        await foreach (var home in Context.Homes.Where(x => x.Address == null).AsAsyncEnumerable())
        {
            Homes.Add(HomeViewModelFactory(home));
        }
    }

    public async void Receive(HomeUpdateMessage message)
    {
        await RefreshCommand.ExecuteAsync(null);
    }
}
