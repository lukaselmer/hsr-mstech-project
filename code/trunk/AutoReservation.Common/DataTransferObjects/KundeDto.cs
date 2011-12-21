using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : DtoBase
    {
        private int _id;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set { if(Id != value)
            {
                SendPropertyChanging(() => Id);
                _id = value;
                SendPropertyChanged(() => Id);
            }}
        }

        private DateTime _geburtsdatum;

        [DataMember]
        public DateTime Geburtsdatum
        {
            get { return _geburtsdatum; }
            set { if (_geburtsdatum != value)
            {
                SendPropertyChanging(() => Geburtsdatum);
                _geburtsdatum = value;
                SendPropertyChanged(() => Geburtsdatum);
            } }
        }

        private string _nachname;

        [DataMember]
        public string Nachname
        {
            get { return _nachname; }
            set { if (_nachname != value)
            {
                SendPropertyChanging(() => Nachname);
                _nachname = value;
                SendPropertyChanged(() => Nachname);
            } }
        }

        private string _vorname;

        [DataMember]
        public string Vorname
        {
            get { return _vorname; }
            set { if (_vorname != value)
                {
                    SendPropertyChanging(() => Vorname);
                    _vorname = value;
                    SendPropertyChanged(() => Vorname);
                } }
        }

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Nachname))
            {
                error.AppendLine("- Nachname ist nicht gesetzt.");
            }
            if (string.IsNullOrEmpty(Vorname))
            {
                error.AppendLine("- Vorname ist nicht gesetzt.");
            }
            if (Geburtsdatum == DateTime.MinValue)
            {
                error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override object Clone()
        {
            return new KundeDto
            {
                Id = Id,
                Nachname = Nachname,
                Vorname = Vorname,
                Geburtsdatum = Geburtsdatum
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;
            var kunde = (KundeDto)obj;

            if (Id != kunde.Id) return false;
            if (Nachname != kunde.Nachname) return false;
            if (Vorname != kunde.Vorname) return false;
            if (Geburtsdatum != kunde.Geburtsdatum) return false;

            return true;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}; {1}; {2}; {3}",
                Id,
                Nachname,
                Vorname,
                Geburtsdatum);
        }

	}
}
