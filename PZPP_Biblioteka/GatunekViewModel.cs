using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using PZPP_Biblioteka;

namespace PZPP_Biblioteka
{
    public class GatunekViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        public ObservableCollection<GatunekKsiążki> Gatunki { get; set; }

        private string _nazwa;
        public string Nazwa
        {
            get => _nazwa;
            set
            {
                _nazwa = value;
                OnPropertyChanged();
            }
        }

        public ICommand PokazDodajGatunekCommand { get; }
        public ICommand EdytujGatunekCommand { get; }
        public ICommand UsunGatunekCommand { get; }
        public ICommand ZapiszGatunekCommand { get; }
        public event Action ZamknijOkno;



        private GatunekKsiążki _selectedGatunek;
        public GatunekKsiążki SelectedGatunek
        {
            get => _selectedGatunek;
            set
            {
                if (_selectedGatunek != value)
                {
                    _selectedGatunek = value;
                    OnPropertyChanged();
                }
            }
        }


        public GatunekViewModel(Biblioteka context)
        {
            _context = context;
            Gatunki = new ObservableCollection<GatunekKsiążki>(_context.GatunkiKsiążek.ToList());
            PokazDodajGatunekCommand = new RelayCommand(PokazDodajGatunek);
            EdytujGatunekCommand = new RelayCommand(EdytujGatunek, _ => SelectedGatunek != null);
            UsunGatunekCommand = new RelayCommand(UsunGatunek, _ => SelectedGatunek != null);
            ZapiszGatunekCommand = new RelayCommand(ZapiszGatunek, _ => SelectedGatunek != null);


        }

        private void PokazDodajGatunek(object obj)
        {
            var okno = new GatunekDodajWindow(_context);
            okno.ShowDialog();
            OdswiezGatunek();
        }
        private void EdytujGatunek(object obj)
        {
            if (SelectedGatunek == null) return;

            var okno = new GatunekEdytujWindow(_context, SelectedGatunek);
            okno.ShowDialog();
            OdswiezGatunek();
        }

        public void ZapiszGatunek(object obj)
        {
            if (SelectedGatunek == null || string.IsNullOrWhiteSpace(SelectedGatunek.Nazwa))
                return;

            var istniejąca = _context.GatunkiKsiążek.FirstOrDefault(k => k.ID == SelectedGatunek.ID);
            if (istniejąca != null)
            {
                istniejąca.Nazwa = SelectedGatunek.Nazwa;
                _context.SaveChanges();
            }

            ZamknijOkno?.Invoke();
        }
        private void UsunGatunek(object obj)
        {
            if (SelectedGatunek == null) return;

            _context.GatunkiKsiążek.Remove(SelectedGatunek);
            _context.SaveChanges();
            OdswiezGatunek();
        }

        public void OdswiezGatunek()
        {
            Gatunki.Clear();
            foreach (var gatunek in _context.GatunkiKsiążek.ToList())
            {
                Gatunki.Add(gatunek);
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}