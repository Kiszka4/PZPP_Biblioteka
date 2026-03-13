using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    public class MainViewModel
    {
        private readonly IServiceProvider _serviceProvider;

        public ICommand OpenGatunekWindowCommand { get; }
        public ICommand OpenKsiążkaWindowCommand { get; }
        public ICommand OpenAutorWindowCommand { get; }

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            OpenGatunekWindowCommand = new RelayCommand(OpenGatunekWindow);
            OpenKsiążkaWindowCommand = new RelayCommand(OpenKsiążkaWindow);
            //OpenZamowieniaWindowCommand = new RelayCommand(OpenZamowieniaWindow);
        }

        private void OpenGatunekWindow(object obj)
        {
            var window = _serviceProvider.GetRequiredService<GatunekWindow>();
            var viewModel = _serviceProvider.GetRequiredService<GatunekViewModel>();
            window.DataContext = viewModel;
            viewModel.OdswiezGatunek();
            window.ShowDialog();
        }

        private void OpenKsiążkaWindow(object obj)
        {
            var window = _serviceProvider.GetRequiredService<KsiążkaWindow>();
            var viewModel = _serviceProvider.GetRequiredService<KsiążkaViewModel>();
            window.DataContext = viewModel;
            viewModel.OdswiezKsiążki();
            window.ShowDialog();
        }
        /*
        private void OpenZamowieniaWindow(object obj)
        {
            var window = _serviceProvider.GetRequiredService<ZamowieniaWindow>();
            var viewModel = _serviceProvider.GetRequiredService<ZamowieniaViewModel>();
            window.DataContext = viewModel;
            viewModel.OdswiezZamowienia();
            window.ShowDialog();
        }
        */
    }
}
