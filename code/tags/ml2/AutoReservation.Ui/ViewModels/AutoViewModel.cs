#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;

#endregion

namespace AutoReservation.Ui.ViewModels
{
    public class AutoViewModel : ViewModelBase
    {
        private readonly List<AutoDto> _autosOriginal = new List<AutoDto>();
        private ObservableCollection<AutoDto> _autos;

        private AutoDto _selectedAuto;

        public ObservableCollection<AutoDto> Autos
        {
            get { return _autos ?? (_autos = new ObservableCollection<AutoDto>()); }
        }

        public AutoDto SelectedAuto
        {
            get { return _selectedAuto; }
            set
            {
                if (_selectedAuto != value)
                {
                    SendPropertyChanging(() => SelectedAuto);
                    _selectedAuto = value;
                    SendPropertyChanged(() => SelectedAuto);
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
            Autos.Clear();
            _autosOriginal.Clear();
            foreach (var auto in Service.Autos)
            {
                Autos.Add(auto);
                _autosOriginal.Add((AutoDto) auto.Clone());
            }
            SelectedAuto = Autos.FirstOrDefault();
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
            foreach (var auto in Autos)
            {
                if (auto.Id == default(int))
                {
                    Service.InsertAuto(auto);
                }
                else
                {
                    var original = _autosOriginal.Where(ao => ao.Id == auto.Id).FirstOrDefault();
                    Service.UpdateAuto(auto, original);
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
            foreach (var auto in Autos)
            {
                var error = auto.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(auto.ToString());
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
            Autos.Add(new AutoDto());
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
            Service.DeleteAuto(SelectedAuto);
            Load();
        }

        private bool CanDelete()
        {
            return SelectedAuto != null && SelectedAuto.Id != default(int) && Service != null;
        }

        #endregion
    }
}