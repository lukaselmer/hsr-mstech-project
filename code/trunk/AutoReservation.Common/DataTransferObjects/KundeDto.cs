using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : DtoBase
    {
        private int _id;

        // TODO: see AutoDto...
        [DataMember]
        public int Id { get { return _id; } set { _id = value; } }

        private string _nachname;

        [DataMember]
        public string Nachname { get { return _nachname; } set { _nachname = value; } }

        private string _vorname;

        [DataMember]
        public string Vorname { get { return _vorname; } set { _vorname = value; } }

        private DateTime _geburtsdatum;

        [DataMember]
        public DateTime Geburtsdatum { get { return _geburtsdatum; } set { _geburtsdatum = value; } }

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
