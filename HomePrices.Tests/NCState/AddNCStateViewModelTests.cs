using HomePrices.data;
using HomePrices.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HomePrices.Tests.NCState;

public class AddNCStateViewModelTests
{
    [Fact]
    public async void OnSubmit_WithValidData_AddNewRentalToDatabase() 
    {
        // Arrange
        AutoMocker mocker = new();
        using var factory = mocker.WithDBScope();
        
        NCStateViewModel vm = mocker.CreateInstance<NCStateViewModel>();

        // Act
        await vm.SubmitCommand.ExecuteAsync(null);
        vm.Address = "Test Stuff";
        vm.Price = 1000;

        //Assert
        using var assertContext = factory.CreateDbContext();
        Home home = await assertContext.Homes.SingleAsync();
        Assert.Equal("Test Stuff", home.Address);
        Assert.Equal(1000, home.Price);     
    }
}
