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

        public ICommand OpenKategorieWindowCommand { get; }
        public ICommand OpenProduktyWindowCommand { get; }
        public ICommand OpenZamowieniaWindowCommand { get; }

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            OpenKategorieWindowCommand = new RelayCommand(OpenKategorieWindow);
            OpenProduktyWindowCommand = new RelayCommand(OpenProduktyWindow);
            OpenZamowieniaWindowCommand = new RelayCommand(OpenZamowieniaWindow);
        }

        private void OpenKategorieWindow(object obj)
        {
            var window = _serviceProvider.GetRequiredService<KategorieWindow>();
            var viewModel = _serviceProvider.GetRequiredService<KategorieViewModel>();
            window.DataContext = viewModel;
            viewModel.OdswiezKategorie();
            window.ShowDialog();
        }

        private void OpenProduktyWindow(object obj)
        {
            var window = _serviceProvider.GetRequiredService<ProduktyWindow>();
            var viewModel = _serviceProvider.GetRequiredService<ProduktyViewModel>();
            window.DataContext = viewModel;
            viewModel.OdswiezProdukty();
            window.ShowDialog();
        }

        private void OpenZamowieniaWindow(object obj)
        {
            var window = _serviceProvider.GetRequiredService<ZamowieniaWindow>();
            var viewModel = _serviceProvider.GetRequiredService<ZamowieniaViewModel>();
            window.DataContext = viewModel;
            viewModel.OdswiezZamowienia();
            window.ShowDialog();
        }
    }
}
