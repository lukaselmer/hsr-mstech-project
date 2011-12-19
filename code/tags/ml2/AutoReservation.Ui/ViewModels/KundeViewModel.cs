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
    public class KundeViewModel : ViewModelBase
    {
        private readonly List<KundeDto> _kundenOriginal = new List<KundeDto>();
        private ObservableCollection<KundeDto> _kunden;

        private KundeDto _selectedKunde;

        public ObservableCollection<KundeDto> Kunden
        {
            get { return _kunden ?? (_kunden = new ObservableCollection<KundeDto>()); }
        }

        public KundeDto SelectedKunde
        {
            get { return _selectedKunde; }
            set
            {
                if (_selectedKunde != value)
                {
                    SendPropertyChanging(() => SelectedKunde);
                    _selectedKunde = value;
                    SendPropertyChanged(() => SelectedKunde);
                }
            }
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
            _kundenOriginal.Clear();
            foreach (var kunde in Service.Kunden)
            {
                Kunden.Add(kunde);
                _kundenOriginal.Add((KundeDto) kunde.Clone());
            }
            SelectedKunde = Kunden.FirstOrDefault();
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
            foreach (var kunde in Kunden)
            {
                if (kunde.Id == default(int))
                {
                    Service.InsertKunde(kunde);
                }
                else
                {
                    var original = _kundenOriginal.Where(ao => ao.Id == kunde.Id).FirstOrDefault();
                    Service.UpdateKunde(kunde, original);
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
            foreach (var kunde in Kunden)
            {
                var error = kunde.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(kunde.ToString());
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
            Kunden.Add(new KundeDto {Geburtsdatum = DateTime.Today});
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
            Service.DeleteKunde(SelectedKunde);
            Load();
        }

        private bool CanDelete()
        {
            return SelectedKunde != null && SelectedKunde.Id != default(int) && Service != null;
        }

        #endregion
    }
}