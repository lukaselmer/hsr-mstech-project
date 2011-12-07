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
        private readonly List<ReservationDto> reservationenOriginal = new List<ReservationDto>();
        private ObservableCollection<ReservationDto> reservationen;
        public ObservableCollection<ReservationDto> Reservationen
        {
            get
            {
                if (reservationen == null)
                {
                    reservationen = new ObservableCollection<ReservationDto>();
                }
                return reservationen;
            }
        }

        private ReservationDto selectedReservation;
        public ReservationDto SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    SendPropertyChanging(() => SelectedReservation);

                    selectedReservation = value;
                    SelectedAutoId = value != null && value.Auto != null ? value.Auto.Id : 0;
                    SelectedKundeId = value != null && value.Kunde != null ? value.Kunde.Id : 0;

                    SendPropertyChanged(() => SelectedReservation);
                }
            }
        }

        private int selectedAutoId;
        public int SelectedAutoId
        {
            get { return selectedAutoId; }
            set
            {
                if (selectedAutoId != value)
                {
                    SendPropertyChanging(() => SelectedAutoId);

                    selectedAutoId = value;
                    if (SelectedReservation != null)
                    {
                        SelectedReservation.Auto = Autos.Where(a => a.Id == value).SingleOrDefault();
                    }

                    SendPropertyChanged(() => SelectedAutoId);
                }
            }
        }

        private int selectedKundeId;
        public int SelectedKundeId
        {
            get { return selectedKundeId; }
            set
            {
                if (selectedKundeId != value)
                {
                    SendPropertyChanging(() => SelectedKundeId);

                    selectedKundeId = value;
                    if (SelectedReservation != null)
                    {
                        SelectedReservation.Kunde = Kunden.Where(k => k.Id == value).SingleOrDefault();
                    }

                    SendPropertyChanged(() => SelectedKundeId);
                }
            }
        }

        private ObservableCollection<AutoDto> autos;
        public ObservableCollection<AutoDto> Autos
        {
            get
            {
                if (autos == null)
                {
                    autos = new ObservableCollection<AutoDto>();
                }
                return autos;
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

        private RelayCommand loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new RelayCommand(
                        param => Load(),
                        param => CanLoad()
                    );
                }
                return loadCommand;
            }
        }

        protected override void Load()
        {
            Kunden.Clear();
            foreach (KundeDto kunde in Service.Kunden) { Kunden.Add(kunde); }

            Autos.Clear();
            foreach (AutoDto auto in Service.Autos) { Autos.Add(auto); }

            Reservationen.Clear();
            reservationenOriginal.Clear();
            foreach (ReservationDto reservation in Service.Reservationen)
            {
                Reservationen.Add(reservation);
                reservationenOriginal.Add((ReservationDto)reservation.Clone());
            }
            SelectedReservation = Reservationen.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return Service != null;
        }

        #endregion

        #region Save-Command

        private RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(
                        param => SaveData(),
                        param => CanSaveData()
                    );
                }
                return saveCommand;
            }
        }

        private void SaveData()
        {
            foreach (ReservationDto reservation in Reservationen)
            {
                if (reservation.ReservationNr == default(int))
                {
                    Service.InsertReservation(reservation);
                }
                else
                {
                    ReservationDto original = reservationenOriginal.Where(ao => ao.ReservationNr == reservation.ReservationNr).FirstOrDefault();
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

            StringBuilder errorText = new StringBuilder();
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

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(
                        param => New(),
                        param => CanNew()
                    );
                }
                return newCommand;
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

        private RelayCommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(
                        param => Delete(),
                        param => CanDelete()
                    );
                }
                return deleteCommand;
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