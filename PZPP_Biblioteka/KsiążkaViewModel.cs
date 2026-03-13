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
        public ObservableCollection<Książka> Książki { get; set; }

        public ICommand PokazDodajKsiążkaCommand { get; }
        public ICommand EdytujKsiążkaCommand { get; }
        public ICommand UsunKsiążkaCommand { get; }
        public ICommand ZapiszKsiążkaCommand { get; }
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

        public KsiążkaViewModel(Biblioteka context)
        {
            _context = context;
            Książki = new ObservableCollection<Książka>(_context.Książki.ToList());
            PokazDodajKsiążkaCommand = new RelayCommand(PokazDodajKsiążka);
            EdytujKsiążkaCommand = new RelayCommand(EdytujKsiążka, _ => SelectedKsiążka != null);
            UsunKsiążkaCommand = new RelayCommand(UsunKsiążka, _ => SelectedKsiążka != null);
            ZapiszKsiążkaCommand = new RelayCommand(ZapiszKsiążka, _ => SelectedKsiążka != null);
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

            ZamknijOkno?.Invoke();
        }

        private void UsunKsiążka(object obj)
        {
            if (SelectedKsiążka == null) return;

            _context.Książki.Remove(SelectedKsiążka);
            _context.SaveChanges();
            OdswiezKsiążki();
        }

        public void OdswiezKsiążki()
        {
            Książki.Clear();
            foreach (var ksiazka in _context.Książki.Include(k => k.GatunekKsiążki).ToList())
            {
                Książki.Add(ksiazka);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }


}

