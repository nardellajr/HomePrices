using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HomePrices.data;
using System;
using System.Threading.Tasks;

namespace HomePrices.ViewModels;

public partial class HomeItemViewModel
{  
    private int Id { get;}

    public HomeItemViewModel(Home home, HomePricesContext homePricesContext, IMessenger messenger)
    {
        Id = home.Id;
        Address = home.Address;
        Price = home.Price;
        HomePricesContext = homePricesContext;
        Messenger = messenger;
    }

    public string Address { get; }
    public int Price { get; }
    public HomePricesContext HomePricesContext { get; }
    private IMessenger Messenger { get; }

    [RelayCommand]
    public async Task OnReturned()
    {
        if (await HomePricesContext.Homes.FindAsync(Id) is { } foundHomes)
        {
            foundHomes.PriceWeek = DateTime.Now;
            await HomePricesContext.SaveChangesAsync();
            Messenger.Send(new HomeUpdateMessage(foundHomes));
        }
    }
}

public record class HomeUpdateMessage(Home Home);
