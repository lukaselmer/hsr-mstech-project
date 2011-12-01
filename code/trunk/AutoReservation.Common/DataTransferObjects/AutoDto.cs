using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    //[KnownType(typeof(LuxusAuto)), KnownType(typeof(BoredStudent))]
    public class AutoDto : DtoBase
    {
        private int _id;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set
            {
                if (Id == value) return;
                SendPropertyChanging(() => Id);
                _id = value;
                SendPropertyChanged(() => Id);
            }
        }

        private string _marke;

        [DataMember]
        public string Marke
        {
            get { return _marke; }
            set
            {
                if (Marke == value) return;
                SendPropertyChanging(() => Marke);
                _marke = value;
                SendPropertyChanged(() => Marke);
            }
        }

        private int _tagestarif;

        [DataMember]
        public int Tagestarif
        {
            get { return _tagestarif; }
            set
            {
                if (Tagestarif == value) return;
                SendPropertyChanging(() => Tagestarif);
                _tagestarif = value;
                SendPropertyChanged(() => Tagestarif);
            }
        }

        private int _basistarif;

        [DataMember]
        public int Basistarif
        {
            get { return _basistarif; }
            set
            {
                if (Basistarif == value) return;
                SendPropertyChanging(() => Basistarif);
                _basistarif = value;
                SendPropertyChanged(() => Basistarif);
            }
        }

        private AutoKlasse _autoKlasse;

        [DataMember]
        public AutoKlasse AutoKlasse
        {
            get { return _autoKlasse; }
            set
            {
                if (AutoKlasse == value) return;
                SendPropertyChanging(() => AutoKlasse);
                _autoKlasse = value;
                SendPropertyChanged(() => AutoKlasse);
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (Tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && Basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new AutoDto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
                AutoKlasse = AutoKlasse,
                Basistarif = Basistarif
            };
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}; {4}",
                Id,
                Marke,
                Tagestarif,
                Basistarif,
                AutoKlasse);
        }

    }

    [DataContract]
    public enum AutoKlasse
    {
        [EnumMember]
        Luxusklasse = 0,
        [EnumMember]
        Mittelklasse = 1,
        [EnumMember]
        Standard = 2
    }
}
