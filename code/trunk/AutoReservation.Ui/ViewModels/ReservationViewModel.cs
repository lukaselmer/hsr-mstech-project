using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly List<ReservationDto> _reservationenOriginal = new List<ReservationDto>();
        private ObservableCollection<ReservationDto> _reservationen;
        public ObservableCollection<ReservationDto> Reservationen
        {
            get
            {
                if (_reservationen == null)
                {
                    _reservationen = new ObservableCollection<ReservationDto>();
                }
                return _reservationen;
            }
        }

        private ReservationDto _selectedReservation;
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

        private int _selectedAutoId;
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

        private int _selectedKundeId;
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

        private ObservableCollection<AutoDto> _autos;
        public ObservableCollection<AutoDto> Autos
        {
            get
            {
                if (_autos == null)
                {
                    _autos = new ObservableCollection<AutoDto>();
                }
                return _autos;
            }
        }

        private ObservableCollection<KundeDto> kunden;
        public ObservableCollection<KundeDto> Kunden
        {
            get
            {
                if (kunden == null)
                {
                    kunden = new ObservableCollection<KundeDto>();
                }
                return kunden;
            }
        }

        #region Load-Command

        private RelayCommand _loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(
                        param => Load(),
                        param => CanLoad()
                    );
                }
                return _loadCommand;
            }
        }

        protected override void Load()
        {
            Kunden.Clear();
            foreach (KundeDto kunde in Service.GetKunden()) { Kunden.Add(kunde); }

            Autos.Clear();
            foreach (AutoDto auto in Service.GetAutos()) { Autos.Add(auto); }

            Reservationen.Clear();
            _reservationenOriginal.Clear();
            foreach (ReservationDto reservation in Service.GetReservationen())
            {
                Reservationen.Add(reservation);
                _reservationenOriginal.Add((ReservationDto)reservation.Clone());
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
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => SaveData(),
                        param => CanSaveData()
                    );
                }
                return _saveCommand;
            }
        }

        private void SaveData()
        {
            foreach (ReservationDto reservation in Reservationen)
            {
                if (reservation.ReservationNr == default(int))
                {
                    Service.CreateReservation(reservation);
                }
                else
                {
                    ReservationDto original = _reservationenOriginal.Where(ao => ao.ReservationNr == reservation.ReservationNr).FirstOrDefault();
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
            foreach (ReservationDto reservation in Reservationen)
            {
                string error = reservation.Validate();
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
            get
            {
                if (_newCommand == null)
                {
                    _newCommand = new RelayCommand(
                        param => New(),
                        param => CanNew()
                    );
                }
                return _newCommand;
            }
        }

        private void New()
        {
            Reservationen.Add(new ReservationDto
            {
                Von = DateTime.Today,
                Bis = DateTime.Today
            });
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
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        param => Delete(),
                        param => CanDelete()
                    );
                }
                return _deleteCommand;
            }
        }

        private void Delete()
        {
            Service.DeleteReservation(SelectedReservation);
            Load();
        }

        private bool CanDelete()
        {
            return
                SelectedReservation != null &&
                SelectedReservation.ReservationNr != default(int) &&
                Service != null;
        }

        #endregion

    }
}