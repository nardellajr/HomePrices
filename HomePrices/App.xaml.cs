using CommunityToolkit.Mvvm.Messaging;
using HomePrices.data;
using HomePrices.ViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using System.Windows.Threading;

namespace HomePrices;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    [STAThread]
    public static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        host.Start();

        using ( var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        using (HomePricesContext context = scope.ServiceProvider.GetRequiredService<HomePricesContext>())
        {
            // Create the EF database if it doesn't exist
            // syncronously, will hang the UI, while DB created
            context.Database.Migrate();
        }

        App app = new();
        app.InitializeComponent();
        app.MainWindow = host.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder)
            => configurationBuilder.AddUserSecrets(typeof(App).Assembly))
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<NCStateViewModel>();
            services.AddSingleton<ShowHomesViewModel>();

            //Factory for creating HomeItemViewModels
            services.AddSingleton<Func<Home, HomeItemViewModel>>(serviceProvider =>
            {
                HomePricesContext context = serviceProvider.GetRequiredService<HomePricesContext>();
                IMessenger messenger = serviceProvider.GetRequiredService<IMessenger>();
                return home => new HomeItemViewModel(home, context, messenger);
            });


            services.AddSingleton<WeakReferenceMessenger>();
            services.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());

            services.AddSingleton(_ => Current.Dispatcher);

            services.AddDbContext<HomePricesContext>();

            services.AddTransient<ISnackbarMessageQueue>(provider =>
            {
                Dispatcher dispatcher = provider.GetRequiredService<Dispatcher>();
                return new SnackbarMessageQueue(TimeSpan.FromSeconds(2.0), dispatcher);
            });
        });
}