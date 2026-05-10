using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PZPP_Biblioteka;

namespace PZPP_Biblioteka
{
    public class KsiążkaZapiszViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        private Książka _książka;

        private string _tytuł;
        public string Tytuł
        {
            get => _tytuł;
            set { _tytuł = value; OnPropertyChanged(); }
        }

        private int _iloscNaStanie;
        public int IloscNaStanie
        {
            get => _iloscNaStanie;
            set { _iloscNaStanie = value; OnPropertyChanged(); }
        }

        private int _isbn;
        public int ISBN
        {
            get => _isbn;
            set { _isbn = value; OnPropertyChanged(); }
        }


        public ObservableCollection<GatunekKsiążki> Gatunki { get; }
        public GatunekKsiążki WybranyGatunek { get; set; }

        public ObservableCollection<Autor> Autorzy { get; set; }
        public Autor WybranyAutor { get; set; }

        public ICommand ZapiszCommand { get; }
        public event Action ZamknijOkno;

        public KsiążkaZapiszViewModel(Biblioteka context)
        {
            _context = context;
            Gatunki = new ObservableCollection<GatunekKsiążki>(_context.GatunkiKsiążek);
            Autorzy = new ObservableCollection<Autor>(_context.Autorzy);
            ZapiszCommand = new RelayCommand(Zapisz);
            _książka = new Książka();
        }

        private void Zapisz(object obj)
        {
            // WALIDACJA

            if (string.IsNullOrWhiteSpace(Tytuł))
            {
                MessageBox.Show(
                    "Tytuł książki nie może być pusty.",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (WybranyAutor == null)
            {
                MessageBox.Show(
                    "Wybierz autora książki.",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (WybranyGatunek == null)
            {
                MessageBox.Show(
                    "Wybierz gatunek książki.",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (IloscNaStanie < 0)
            {
                MessageBox.Show("Ilość książek nie może być ujemna.");
                return;
            }

            if (ISBN <= 0)
            {
                MessageBox.Show("ISBN musi być poprawny.");
                return;
            }

            // PRZYPISANIE DANYCH

            _książka.Tytuł = Tytuł;
            _książka.IloscNaStanie = IloscNaStanie;
            _książka.GatunekKsiążki = WybranyGatunek;
            _książka.Autor = WybranyAutor;

            try
            {
                if (_książka.ISBN == 0)
                    _context.Książki.Add(_książka);
                else
                    _context.Książki.Update(_książka);

                _context.SaveChanges();

                MessageBox.Show(
                    "Książka została zapisana.",
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
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }
}
