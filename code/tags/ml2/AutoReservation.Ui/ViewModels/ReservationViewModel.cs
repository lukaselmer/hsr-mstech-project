#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;

#endregion

namespace AutoReservation.Ui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly List<ReservationDto> _reservationenOriginal = new List<ReservationDto>();
        private ObservableCollection<AutoDto> _autos;
        private ObservableCollection<KundeDto> _kunden;
        private ObservableCollection<ReservationDto> _reservationen;
        private int _selectedAutoId;
        private int _selectedKundeId;

        private ReservationDto _selectedReservation;

        public ObservableCollection<ReservationDto> Reservationen
        {
            get { return _reservationen ?? (_reservationen = new ObservableCollection<ReservationDto>()); }
        }

        public ReservationDto SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                if (_selectedReservation != value)
                {
                    SendPropertyChanging(() => SelectedReservation);

                    _selectedReservation = value;
                    SelectedAutoId = value != null && value.Auto != null ? value.Auto.Id : 0;
                    SelectedKundeId = value != null && value.Kunde != null ? value.Kunde.Id : 0;

                    SendPropertyChanged(() => SelectedReservation);
                }
            }
        }

        public int SelectedAutoId
        {
            get { return _selectedAutoId; }
            set
            {
                if (_selectedAutoId != value)
                {
                    SendPropertyChanging(() => SelectedAutoId);

                    _selectedAutoId = value;
                    if (SelectedReservation != null)
                    {
                        SelectedReservation.Auto = Autos.Where(a => a.Id == value).SingleOrDefault();
                    }

                    SendPropertyChanged(() => SelectedAutoId);
                }
            }
        }

        public int SelectedKundeId
        {
            get { return _selectedKundeId; }
            set
            {
                if (_selectedKundeId != value)
                {
                    SendPropertyChanging(() => SelectedKundeId);

                    _selectedKundeId = value;
                    if (SelectedReservation != null)
                    {
                        SelectedReservation.Kunde = Kunden.Where(k => k.Id == value).SingleOrDefault();
                    }

                    SendPropertyChanged(() => SelectedKundeId);
                }
            }
        }

        public ObservableCollection<AutoDto> Autos
        {
            get { return _autos ?? (_autos = new ObservableCollection<AutoDto>()); }
        }

        public ObservableCollection<KundeDto> Kunden
        {
            get { return _kunden ?? (_kunden = new ObservableCollection<KundeDto>()); }
        }

        #region Load-Command

        private RelayCommand _loadCommand;

        public ICommand LoadCommand
        {
            get { return _loadCommand ?? (_loadCommand = new RelayCommand(param => Load(), param => CanLoad())); }
        }

        protected override void Load()
        {
            Kunden.Clear();
            foreach (var kunde in Service.Kunden)
            {
                Kunden.Add(kunde);
            }

            Autos.Clear();
            foreach (var auto in Service.Autos)
            {
                Autos.Add(auto);
            }

            Reservationen.Clear();
            _reservationenOriginal.Clear();
            foreach (var reservation in Service.Reservationen)
            {
                Reservationen.Add(reservation);
                _reservationenOriginal.Add((ReservationDto) reservation.Clone());
            }
            SelectedReservation = Reservationen.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return Service != null;
        }

        #endregion

        #region Save-Command

        private RelayCommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(param => SaveData(), param => CanSaveData())); }
        }

        private void SaveData()
        {
            foreach (var reservation in Reservationen)
            {
                if (reservation.ReservationNr == default(int))
                {
                    Service.InsertReservation(reservation);
                }
                else
                {
                    var original =
                        _reservationenOriginal.Where(ao => ao.ReservationNr == reservation.ReservationNr).FirstOrDefault();
                    Service.UpdateReservation(reservation, original);
                }
            }
            Load();
        }

        private bool CanSaveData()
        {
            if (Service == null)
            {
                return false;
            }

            var errorText = new StringBuilder();
            foreach (var reservation in Reservationen)
            {
                var error = reservation.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(reservation.ToString());
                    errorText.AppendLine(error);
                }
            }

            ErrorText = errorText.ToString();
            return string.IsNullOrEmpty(ErrorText);
        }

        #endregion

        #region New-Command

        private RelayCommand _newCommand;

        public ICommand NewCommand
        {
            get { return _newCommand ?? (_newCommand = new RelayCommand(param => New(), param => CanNew())); }
        }

        private void New()
        {
            Reservationen.Add(new ReservationDto {Von = DateTime.Today, Bis = DateTime.Today});
        }

        private bool CanNew()
        {
            return Service != null;
        }

        #endregion

        #region Delete-Command

        private RelayCommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(param => Delete(), param => CanDelete())); }
        }

        private void Delete()
        {
            Service.DeleteReservation(SelectedReservation);
            Load();
        }

        private bool CanDelete()
        {
            return SelectedReservation != null && SelectedReservation.ReservationNr != default(int) && Service != null;
        }

        #endregion
    }
}