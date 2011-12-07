#region

using System;
using System.Runtime.Serialization;
using System.Text;

#endregion

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : DtoBase
    {
        private DateTime _geburtsdatum;
        private int _id;

        private string _nachname;

        private string _vorname;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    SendPropertyChanging(() => Id);
                    _id = value;
                    SendPropertyChanged(() => Id);
                }
            }
        }

        [DataMember]
        public string Nachname
        {
            get { return _nachname; }
            set
            {
                if (_nachname != value)
                {
                    SendPropertyChanging(() => Nachname);
                    _nachname = value;
                    SendPropertyChanged(() => Nachname);
                }
            }
        }

        [DataMember]
        public string Vorname
        {
            get { return _vorname; }
            set
            {
                if (_vorname != value)
                {
                    SendPropertyChanging(() => Vorname);
                    _vorname = value;
                    SendPropertyChanged(() => Vorname);
                }
            }
        }

        [DataMember]
        public DateTime Geburtsdatum
        {
            get { return _geburtsdatum; }
            set
            {
                if (_geburtsdatum != value)
                {
                    SendPropertyChanging(() => Geburtsdatum);
                    _geburtsdatum = value;
                    SendPropertyChanged(() => Geburtsdatum);
                }
            }
        }

        public override string Validate()
        {
            var error = new StringBuilder();
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

            if (error.Length == 0)
            {
                return null;
            }

            return error.ToString();
        }

        public override object Clone()
        {
            return new KundeDto {Id = Id, Nachname = Nachname, Vorname = Vorname, Geburtsdatum = Geburtsdatum};
        }

        public override string ToString()
        {
            return string.Format("{0}; {1}; {2}; {3}", Id, Nachname, Vorname, Geburtsdatum);
        }
    }
}