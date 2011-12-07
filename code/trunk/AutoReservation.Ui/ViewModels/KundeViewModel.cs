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
        private readonly List<KundeDto> kundenOriginal = new List<KundeDto>();
        private ObservableCollection<KundeDto> kunden;

        private KundeDto selectedKunde;

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

        public KundeDto SelectedKunde
        {
            get { return selectedKunde; }
            set
            {
                if (selectedKunde != value)
                {
                    SendPropertyChanging(() => SelectedKunde);
                    selectedKunde = value;
                    SendPropertyChanged(() => SelectedKunde);
                }
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
                    loadCommand = new RelayCommand(param => Load(), param => CanLoad());
                }
                return loadCommand;
            }
        }

        protected override void Load()
        {
            Kunden.Clear();
            kundenOriginal.Clear();
            foreach (var kunde in Service.Kunden)
            {
                Kunden.Add(kunde);
                kundenOriginal.Add((KundeDto) kunde.Clone());
            }
            SelectedKunde = Kunden.FirstOrDefault();
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
                    saveCommand = new RelayCommand(param => SaveData(), param => CanSaveData());
                }
                return saveCommand;
            }
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
                    var original = kundenOriginal.Where(ao => ao.Id == kunde.Id).FirstOrDefault();
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

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(param => New(), param => CanNew());
                }
                return newCommand;
            }
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

        private RelayCommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(param => Delete(), param => CanDelete());
                }
                return deleteCommand;
            }
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