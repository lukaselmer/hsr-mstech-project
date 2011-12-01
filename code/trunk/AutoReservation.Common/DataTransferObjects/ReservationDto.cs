using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto : DtoBase
    {
        private int _reservationNr;

        [DataMember]
        public int ReservationNr { get { return _reservationNr; } set { _reservationNr = value; } }

        private int _autoId;

        [DataMember]
        public int AutoId { get { return _autoId; } set { _autoId = value; } }

        private int _kundeId;

        [DataMember]
        public int KundeId { get { return _kundeId; } set { _kundeId = value; } }

        private DateTime _von;

        [DataMember]
        public DateTime Von { get { return _von; } set { _von = value; } }

        private DateTime _bis;

        [DataMember]
        public DateTime Bis { get { return _bis; } set { _bis = value; } }

        //[DataMember(IsReference = true)]
        [DataMember]
        public AutoDto Auto { get; set; }
        [DataMember]
        public KundeDto Kunde { get; set; }

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
