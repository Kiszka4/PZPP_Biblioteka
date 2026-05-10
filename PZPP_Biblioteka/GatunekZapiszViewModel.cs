using Microsoft.EntityFrameworkCore;
using PZPP_Biblioteka;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    public class GatunekZapiszViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        private GatunekKsiążki _gatunek;

        private string _nazwa;
        public string Nazwa
        {
            get { return _nazwa; }
            set
            {
                if (_nazwa != value)
                {
                    _nazwa = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ZapiszCommand { get; }
        public event Action ZamknijOkno;

        public GatunekZapiszViewModel(Biblioteka context)
        {
            _context = context;
            ZapiszCommand = new RelayCommand(Zapisz);
            _gatunek = new GatunekKsiążki();
        }

        private void Zapisz(object obj)
        {
            // WALIDACJA

            if (string.IsNullOrWhiteSpace(Nazwa))
            {
                MessageBox.Show(
                    "Nazwa gatunku nie może być pusta.",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // PRZYPISANIE DANYCH

            _gatunek.Nazwa = Nazwa;

            try
            {
                if (_gatunek.ID == 0)
                    _context.GatunkiKsiążek.Add(_gatunek);
                else
                    _context.GatunkiKsiążek.Update(_gatunek);

                _context.SaveChanges();

                MessageBox.Show(
                    "Gatunek został zapisany.",
                    "Sukces",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                ZamknijOkno?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Wystąpił błąd podczas zapisu:\n{ex.Message}",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
