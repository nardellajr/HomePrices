using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dashboard.data;
using System.Threading.Tasks;
using System;
using MaterialDesignThemes.Wpf;

namespace Dashboard.ViewModels;

public partial class NCStateViewModel : ObservableObject
{
    private HomePricesContext DbContext { get; }
    public ISnackbarMessageQueue SnackbarMessageQueue { get; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private int _price = 0;
    
        
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _address;

    public NCStateViewModel(HomePricesContext dbContext, ISnackbarMessageQueue snackbarMessageQueue)
    {
        DbContext = dbContext?? throw new ArgumentNullException(nameof(dbContext));
        SnackbarMessageQueue = snackbarMessageQueue?? throw new ArgumentException(nameof(snackbarMessageQueue));
    }

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private async Task OnSubmit()
    {
        if (string.IsNullOrWhiteSpace(Address))
        {
            return;
        }
        Home home = new()
        {
            Address = Address!,
            Price = Price
        };

        DbContext.Homes.Add(home);
        await DbContext.SaveChangesAsync();
        Address = string.Empty;
        Price = 0;        
        SnackbarMessageQueue.Enqueue("Home added");
    }
    private bool CanSubmit() => !string.IsNullOrWhiteSpace(Address) & Price > 0;
}
