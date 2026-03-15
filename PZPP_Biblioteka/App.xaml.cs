using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PZPP_Biblioteka
{
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<Biblioteka>(options =>
                        options.UseInMemoryDatabase("BibliotekaDB"));

                    services.AddTransient<MainViewModel>();
                    services.AddTransient<GatunekViewModel>();
                    services.AddTransient<GatunekZapiszViewModel>();
                    services.AddTransient<KsiążkaViewModel>();
                    services.AddTransient<KsiążkaZapiszViewModel>();
                    services.AddTransient<AutorViewModel>();

                    services.AddSingleton<MainWindow>();
                    services.AddTransient<GatunekWindow>();
                    services.AddTransient<KsiążkaWindow>();
                    services.AddTransient<AutorWindow>();
                    services.AddTransient<GatunekDodajWindow>();
                    services.AddTransient<KsiążkaDodajWindow>();
                    services.AddTransient<GatunekEdytujWindow>();
                    services.AddTransient<KsiążkaEdytujWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            using (var scope = AppHost.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<Biblioteka>();
                InicjujDane.Inicjuj(ctx);
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
