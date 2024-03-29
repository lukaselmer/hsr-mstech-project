﻿using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : DtoBase
    {
        private int id;
        [DataMember]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    SendPropertyChanging(() => Id);
                    id = value;
                    SendPropertyChanged(() => Id);
                }
            }
        }

        private string marke;
        [DataMember]
        public string Marke
        {
            get
            {
                return marke;
            }
            set
            {
                if (marke != value)
                {
                    SendPropertyChanging(() => Marke);
                    marke = value;
                    SendPropertyChanged(() => Marke);
                }
            }
        }

        private int tagestarif;
        [DataMember]
        public int Tagestarif
        {
            get
            {
                return tagestarif;
            }
            set
            {
                if (tagestarif != value)
                {
                    SendPropertyChanging(() => Tagestarif);
                    tagestarif = value;
                    SendPropertyChanged(() => Tagestarif);
                }
            }
        }

        private int basistarif;
        [DataMember]
        public int Basistarif
        {
            get
            {
                return basistarif;
            }
            set
            {
                if (basistarif != value)
                {
                    SendPropertyChanging(() => Basistarif);
                    basistarif = value;
                    SendPropertyChanged(() => Basistarif);
                }
            }
        }

        private AutoKlasse autoKlasse;
        [DataMember]
        public AutoKlasse AutoKlasse
        {
            get
            {
                return autoKlasse;
            }
            set
            {
                if (autoKlasse != value)
                {
                    SendPropertyChanging(() => AutoKlasse);
                    autoKlasse = value;
                    SendPropertyChanged(() => AutoKlasse);
                }
            }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && basistarif <= 0)
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
