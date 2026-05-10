using Microsoft.EntityFrameworkCore;
using PZPP_Biblioteka;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    public class KsiążkaViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        private readonly ApiService _api;
        public ObservableCollection<Książka> Książki { get; set; }
    

        public ICommand PokazDodajKsiążkaCommand { get; }
        public ICommand EdytujKsiążkaCommand { get; }
        public ICommand UsunKsiążkaCommand { get; }
        public ICommand ZapiszKsiążkaCommand { get; }
        public ICommand SortujCommand { get; }
        public event Action ZamknijOkno;

        private Książka _selectedKsiążka;
        public Książka SelectedKsiążka
        {
            get => _selectedKsiążka;
            set
            {
                if (_selectedKsiążka != value)
                {
                    _selectedKsiążka = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _tytuł;
        public string Tytuł
        {
            get => _tytuł;
            set
            {
                _tytuł = value;
                OnPropertyChanged();
            }
        }
        private async void ZaladujDostepnosc()
        {
            var lista = Książki.ToList();

            foreach (var k in lista)
            {
                var wynik = await _api.PobierzDostepnosc(k.Tytuł);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    k.DostepnoscWBibliotece2 = wynik;
                });
            }

            OnPropertyChanged(nameof(Książki));
        }

        private int _dostepnosc;
        public int DostepnoscWBibliotece2
        {
            get => _dostepnosc;
            set
            {
                _dostepnosc = value;
                OnPropertyChanged();
            }
        }
        private string _wyszukaj;

        public string Wyszukaj
        {
            get => _wyszukaj;
            set
            {
                _wyszukaj = value;
                OnPropertyChanged();
                FiltrujKsiążki();
                
            }
        }

        public KsiążkaViewModel(Biblioteka context)
        {
            _context = context;
            _api = new ApiService();
            Książki = new ObservableCollection<Książka>(_context.Książki
                            .Include(k => k.Autor)
                            .Include(k => k.GatunekKsiążki)
                            .ToList());
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

            var istniejacy = _context.Książki.FirstOrDefault(p => p.ISBN == SelectedKsiążka.ISBN);
            if (istniejacy != null)
            {
                istniejacy.Tytuł = SelectedKsiążka.Tytuł;
                istniejacy.IloscNaStanie = SelectedKsiążka.IloscNaStanie;
                istniejacy.ISBN = SelectedKsiążka.ISBN;
                istniejacy.Autor = SelectedKsiążka.Autor;
                //istniejacy.CenaJednostkowa = SelectedProdukt.CenaJednostkowa;
                //istniejacy.VAT = SelectedProdukt.VAT;
                istniejacy.GatunekID = SelectedKsiążka.GatunekID;

                _context.SaveChanges();
            }

            if (string.IsNullOrWhiteSpace(SelectedKsiążka.Tytuł))
            {
                MessageBox.Show("Tytuł książki jest wymagany!");
                return;
            }

            ZamknijOkno?.Invoke();
        }

        private void UsunKsiążka(object obj)
        {
            if (SelectedKsiążka == null) return;

            var wynik = MessageBox.Show($"Czy na pewno chcesz usunąć książkę:\n{SelectedKsiążka.Tytuł}?","Potwierdzenie usunięcia",MessageBoxButton.YesNo,MessageBoxImage.Warning);

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
            foreach (var ksiazka in _context.Książki.Include(k => k.GatunekKsiążki).Include(k => k.Autor).ToList())
            {
                Książki.Add(ksiazka);
            }
        }

        private void FiltrujKsiążki()
        {
            if (string.IsNullOrWhiteSpace(Wyszukaj))
            {
                OdswiezKsiążki();
                return;
            }

            Książki.Clear();

            var tekst = Wyszukaj?.ToLower() ?? "";

            var wynik = _context.Książki
                .Include(k => k.Autor)
                .Include(k => k.GatunekKsiążki)
                .Where(k =>

                    k.Tytuł.ToLower().Contains(tekst)

                    || k.Autor.Imię.ToLower().Contains(tekst)

                    || k.Autor.Nazwisko.ToLower().Contains(tekst)

                    || k.GatunekKsiążki.Nazwa.ToLower().Contains(tekst)
                )
                .ToList();

            foreach (var k in wynik)
            {
                Książki.Add(k);
            }
        }

        private void SortujAlfabetycznie()
        {
            var posortowane = Książki
                .OrderBy(k => k.Tytuł)
                .ToList();

            Książki.Clear();

            foreach (var k in posortowane)
            {
                Książki.Add(k);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }


}

