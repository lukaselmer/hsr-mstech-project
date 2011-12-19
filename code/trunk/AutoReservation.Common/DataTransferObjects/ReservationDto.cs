using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto : DtoBase
    {
        private int _reservationNr;
        public int ReservationNr
        {
            get { return _reservationNr; }
            set { if (_reservationNr != value)
            {
                SendPropertyChanging(() => ReservationNr);
                _reservationNr = value;
                SendPropertyChanged(() => ReservationNr);
            } }
        }

        private DateTime _von;
        public DateTime Von
        {
            get { return _von; }
            set { if(_von != value)
            {
                SendPropertyChanging(() => Von);
                _von = value;
                SendPropertyChanged(() => Von);
            } }
        }

        private DateTime _bis;
        public DateTime Bis
        {
            get { return _bis; }
            set { if(_bis != value)
            {
                SendPropertyChanging(() => Bis);
                _bis = value;
                SendPropertyChanged(() => Bis);
            } }
        }

        private AutoDto _auto;
        public AutoDto Auto
        {
            get { return _auto; }
            set { if(_auto != value)
            {
                SendPropertyChanging(() => Auto);
                _auto = value;
                SendPropertyChanged(() => Auto);
            } }
        }

        private KundeDto _kunde;
        public KundeDto Kunde
        {
            get { return _kunde; }
            set { if(_kunde != value)
            {
                SendPropertyChanging(() => Kunde);
                _kunde = value;
                SendPropertyChanged(() => Kunde);
            } }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (Von == DateTime.MinValue)
            {
                error.AppendLine("- Von-Datum ist nicht gesetzt.");
            }
            if (Bis == DateTime.MinValue)
            {
                error.AppendLine("- Bis-Datum ist nicht gesetzt.");
            }
            if (Von > Bis)
            {
                error.AppendLine("- Von-Datum ist grösser als Bis-Datum.");
            }
            if (Auto == null)
            {
                error.AppendLine("- Auto ist nicht zugewiesen.");
            }
            else
            {
                string autoError = Auto.Validate();
                if (!string.IsNullOrEmpty(autoError))
                {
                    error.AppendLine(autoError);
                }
            }
            if (Kunde == null)
            {
                error.AppendLine("- Kunde ist nicht zugewiesen.");
            }
            else
            {
                string kundeError = Kunde.Validate();
                if (!string.IsNullOrEmpty(kundeError))
                {
                    error.AppendLine(kundeError);
                }
            }


            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new ReservationDto
            {
                ReservationNr = ReservationNr,
                Von = Von,
                Bis = Bis,
                Auto = (AutoDto)Auto.Clone(),
                Kunde = (KundeDto)Kunde.Clone()
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;

            var reservation = (ReservationDto)obj;

            if (ReservationNr != reservation.ReservationNr) return false;
            if (Von != reservation.Von) return false;
            if (Bis != reservation.Bis) return false;
            return true;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}; {4}",
                ReservationNr,
                Von,
                Bis,
                Auto,
                Kunde);
        }

	}
}