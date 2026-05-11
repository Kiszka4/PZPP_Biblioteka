using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    public class KsiążkaViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        private readonly ApiService _api;
        public ObservableCollection<Książka> Książki { get; set; }
        public ObservableCollection<Autor> Autorzy { get; set; }
        public ObservableCollection<GatunekKsiążki> Gatunki { get; set; }
        public ObservableCollection<Filia> Filie { get; set; }

        public ICommand PokazDodajKsiążkaCommand { get; }
        public ICommand EdytujKsiążkaCommand { get; }
        public ICommand UsunKsiążkaCommand { get; }
        public ICommand ZapiszKsiążkaCommand { get; }
        public ICommand SortujCommand { get; }
        public event Action? ZamknijOkno;

        private Książka? _selectedKsiążka;
        public Książka? SelectedKsiążka
        {
            get => _selectedKsiążka;
            set
            {
                if (_selectedKsiążka != value)
                {
                    _selectedKsiążka = value;
                    OnPropertyChanged();
                    WybranyAutor = value?.Autor;
                    WybranyGatunek = value?.GatunekKsiążki;
                }
            }
        }

        private Filia? _wybranaFilia;
        public Filia? WybranaFilia
        {
            get => _wybranaFilia;
            set
            {
                _wybranaFilia = value;
                OnPropertyChanged();
                OdswiezKsiążki();
            }
        }

        private Autor? _wybranyAutor;
        public Autor? WybranyAutor
        {
            get => _wybranyAutor;
            set { _wybranyAutor = value; OnPropertyChanged(); }
        }

        private GatunekKsiążki? _wybranyGatunek;
        public GatunekKsiążki? WybranyGatunek
        {
            get => _wybranyGatunek;
            set { _wybranyGatunek = value; OnPropertyChanged(); }
        }

        private string? _tytuł;
        public string? Tytuł
        {
            get => _tytuł;
            set { _tytuł = value; OnPropertyChanged(); }
        }

        private string? _wyszukaj;
        public string? Wyszukaj
        {
            get => _wyszukaj;
            set
            {
                _wyszukaj = value;
                OnPropertyChanged();
                FiltrujKsiążki();
            }
        }

        public int LiczbaEgzemplarzy => Książki.Sum(k => k.IloscNaStanie);

        private async void ZaladujDostepnosc()
        {
            var lista = Książki.ToList();
            foreach (var k in lista)
            {
                try
                {
                    var wynik = await _api.PobierzDostepnosc(k.Tytuł);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        k.DostepnoscWBibliotece2 = wynik;
                    });
                }
                catch { }
            }
            OnPropertyChanged(nameof(Książki));
        }

        public KsiążkaViewModel(Biblioteka context)
        {
            _context = context;
            _api = new ApiService();
            Autorzy = new ObservableCollection<Autor>(_context.Autorzy.ToList());
            Gatunki = new ObservableCollection<GatunekKsiążki>(_context.GatunkiKsiążek.ToList());
            Filie = new ObservableCollection<Filia>(_context.Filie.ToList());
            Książki = new ObservableCollection<Książka>();

            OdswiezKsiążki();

            Książki.CollectionChanged += (s, e) => OnPropertyChanged(nameof(LiczbaEgzemplarzy));

            PokazDodajKsiążkaCommand = new RelayCommand(PokazDodajKsiążka);
            EdytujKsiążkaCommand = new RelayCommand(EdytujKsiążka, _ => SelectedKsiążka != null);
            UsunKsiążkaCommand = new RelayCommand(UsunKsiążka, _ => SelectedKsiążka != null);
            ZapiszKsiążkaCommand = new RelayCommand(ZapiszKsiążka, _ => SelectedKsiążka != null);
            SortujCommand = new RelayCommand(_ => SortujAlfabetycznie());

            ZaladujDostepnosc();
        }

        private void PokazDodajKsiążka(object obj)
        {
            var okno = new KsiążkaDodajWindow(_context);
            okno.ShowDialog();
            OdswiezKsiążki();
        }

        private void EdytujKsiążka(object obj)
        {
            if (SelectedKsiążka == null) return;
            var okno = new KsiążkaEdytujWindow(_context, SelectedKsiążka);
            okno.ShowDialog();
            OdswiezKsiążki();
        }

        private void ZapiszKsiążka(object obj)
        {
            if (SelectedKsiążka == null) return;

            if (string.IsNullOrWhiteSpace(SelectedKsiążka.Tytuł))
            {
                MessageBox.Show("Tytuł książki jest wymagany!", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedKsiążka.IloscNaStanie < 0)
            {
                MessageBox.Show("Ilość na stanie nie może być ujemna!", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var istniejacy = _context.Książki.FirstOrDefault(p => p.ISBN == SelectedKsiążka.ISBN);
            if (istniejacy != null)
            {
                istniejacy.Tytuł = SelectedKsiążka.Tytuł;
                istniejacy.IloscNaStanie = SelectedKsiążka.IloscNaStanie;
                istniejacy.ISBN = SelectedKsiążka.ISBN;
                istniejacy.Autor = WybranyAutor ?? SelectedKsiążka.Autor;
                istniejacy.GatunekID = SelectedKsiążka.GatunekID;
                _context.SaveChanges();
            }

            ZamknijOkno?.Invoke();
        }

        private void UsunKsiążka(object obj)
        {
            if (SelectedKsiążka == null) return;
            var wynik = MessageBox.Show(
                $"Czy na pewno chcesz usunąć książkę:\n{SelectedKsiążka.Tytuł}?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (wynik == MessageBoxResult.Yes)
            {
                _context.Książki.Remove(SelectedKsiążka);
                _context.SaveChanges();
                OdswiezKsiążki();
            }
        }

        public void OdswiezKsiążki()
        {
            Książki.Clear();
            var lista = _context.Książki
                .Include(k => k.GatunekKsiążki)
                .Include(k => k.Autor)
                .Include(k => k.StanyMagazynowe)
                .ToList();

            foreach (var k in lista)
            {
                if (WybranaFilia != null)
                {
                    var stan = k.StanyMagazynowe
                        .FirstOrDefault(s => s.FiliaID == WybranaFilia.ID);
                    k.IloscNaStanie = stan?.IloscNaStanie ?? 0;
                }
                Książki.Add(k);
            }
            OnPropertyChanged(nameof(LiczbaEgzemplarzy));
        }

        private void FiltrujKsiążki()
        {
            if (string.IsNullOrWhiteSpace(Wyszukaj))
            {
                OdswiezKsiążki();
                return;
            }
            Książki.Clear();
            var tekst = Wyszukaj.ToLower();
            var lista = _context.Książki
                .Include(k => k.Autor)
                .Include(k => k.GatunekKsiążki)
                .Include(k => k.StanyMagazynowe)
                .Where(k =>
                    k.Tytuł.ToLower().Contains(tekst) ||
                    k.Autor.Imię.ToLower().Contains(tekst) ||
                    k.Autor.Nazwisko.ToLower().Contains(tekst) ||
                    k.GatunekKsiążki.Nazwa.ToLower().Contains(tekst))
                .ToList();

            foreach (var k in lista)
            {
                if (WybranaFilia != null)
                {
                    var stan = k.StanyMagazynowe
                        .FirstOrDefault(s => s.FiliaID == WybranaFilia.ID);
                    k.IloscNaStanie = stan?.IloscNaStanie ?? 0;
                }
                Książki.Add(k);
            }
            OnPropertyChanged(nameof(LiczbaEgzemplarzy));
        }

        private void SortujAlfabetycznie()
        {
            var posortowane = Książki.OrderBy(k => k.Tytuł).ToList();
            Książki.Clear();
            foreach (var k in posortowane)
                Książki.Add(k);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}