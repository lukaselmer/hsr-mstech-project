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
            set { if(_selectedReservation != value)
            {
                SendPropertyChanging(() => SelectedReservation);
                _selectedReservation = value;
                SendPropertyChanged(() => SelectedReservation);
            } }
        }

        private RelayCommand _loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(param => Load(), param => CanLoad());
                }
                return _loadCommand;
            }
        }

        protected override void Load()
        {
            Reservationen.Clear();
            _reservationenOriginal.Clear();
            foreach (var reservationDto in Service.GetReservationen())
            {
                Reservationen.Add(reservationDto);
                _reservationenOriginal.Add((ReservationDto) reservationDto.Clone());
            }
            SelectedReservation = Reservationen.FirstOrDefault();
        }

        private bool CanLoad()
        {
            return Service != null;
        }

        private RelayCommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if(_saveCommand != null)
                {
                    _saveCommand = new RelayCommand(param => SaveData(), param => CanSaveData());
                }
                return _saveCommand;
            }
        }
 
        public void SaveData()
        {
            foreach (var reservationDto in Reservationen)
            {
                if (reservationDto.ReservationNr == default(int))
                {
                    Service.CreateReservation(reservationDto);
                }
                else
                {
                    ReservationDto original =
                        _reservationenOriginal.Where(ro => ro.ReservationNr == reservationDto.ReservationNr).
                            FirstOrDefault();
                    Service.UpdateReservation(reservationDto, original);
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
            foreach (ReservationDto reservationDto in Reservationen)
            {
                string error = reservationDto.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(reservationDto.ToString());
                    errorText.AppendLine(error);
                }
            }

            ErrorText = errorText.ToString();
            return string.IsNullOrEmpty(ErrorText);
        }

        private RelayCommand _newCommand;
        public ICommand NewCommand
        {
            get
            {
                if (_newCommand == null)
                {
                    _newCommand = new RelayCommand(param => New(), param => CanNew());
                }
                return _newCommand;
            }
        }

        private void New()
        {
            Reservationen.Add(new ReservationDto());
        }

        private bool CanNew()
        {
            return Service != null;
        }

        private RelayCommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(param => Delete(), param => CanDelete());
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
            return SelectedReservation != null && SelectedReservation.ReservationNr != default(int) && Service != null;
        }
    }
}
