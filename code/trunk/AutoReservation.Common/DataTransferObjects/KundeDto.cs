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
        private DateTime geburtsdatum;
        private int id;

        private string nachname;

        private string vorname;

        [DataMember]
        public int Id
        {
            get { return id; }
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

        [DataMember]
        public string Nachname
        {
            get { return nachname; }
            set
            {
                if (nachname != value)
                {
                    SendPropertyChanging(() => Nachname);
                    nachname = value;
                    SendPropertyChanged(() => Nachname);
                }
            }
        }

        [DataMember]
        public string Vorname
        {
            get { return vorname; }
            set
            {
                if (vorname != value)
                {
                    SendPropertyChanging(() => Vorname);
                    vorname = value;
                    SendPropertyChanged(() => Vorname);
                }
            }
        }

        [DataMember]
        public DateTime Geburtsdatum
        {
            get { return geburtsdatum; }
            set
            {
                if (geburtsdatum != value)
                {
                    SendPropertyChanging(() => Geburtsdatum);
                    geburtsdatum = value;
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