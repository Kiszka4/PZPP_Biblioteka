using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using PZPP_Biblioteka;


namespace PZPP_Biblioteka
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder() // Fully qualify the Host class
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<Biblioteka>(options =>
                        options.UseInMemoryDatabase("BibliotekaDB"));

                    services.AddTransient<MainViewModel>();
                    services.AddTransient<GatunekViewModel>();
                    services.AddTransient<GatunekZapiszViewModel>();
                    services.AddTransient<KsiążkaViewModel>();
                    services.AddTransient<KsiążkaZapiszViewModel>();

                    //services.AddTransient<ZamowieniaViewModel>();
                    //services.AddTransient<ZamowieniaDodajViewModel>();

                    services.AddSingleton<MainWindow>();
                    services.AddTransient<GatunekWindow>();
                    services.AddTransient<KsiążkaWindow>();

                    //services.AddTransient<ZamowieniaWindow>();

                    services.AddTransient<GatunekDodajWindow>();
                    services.AddTransient<KsiążkaDodajWindow>();

                    //services.AddTransient<ZamowieniaDodajWindow>();

                    services.AddTransient<GatunekEdytujWindow>();
                    services.AddTransient<KsiążkaEdytujWindow>();

                    //services.AddTransient<ZamowieniaEdytujWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            using (var scope = AppHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Biblioteka>();
                InicjujDane.Inicjuj(context);
            }


            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}

