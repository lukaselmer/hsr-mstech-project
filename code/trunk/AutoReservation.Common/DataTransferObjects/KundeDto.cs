using System;
using System.Runtime.Serialization;
using System.Text;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : DtoBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nachname { get; set; }
        [DataMember]
        public string Vorname { get; set; }
        [DataMember]
        public DateTime Geburtsdatum { get; set; }

        public override string Validate()
        {
            return null;
        }

        public override object Clone()
        {
            return this;
        }

        //public override string Validate()
        //{
        //    StringBuilder error = new StringBuilder();
        //    if (string.IsNullOrEmpty(Nachname))
        //    {
        //        error.AppendLine("- Nachname ist nicht gesetzt.");
        //    }
        //    if (string.IsNullOrEmpty(Vorname))
        //    {
        //        error.AppendLine("- Vorname ist nicht gesetzt.");
        //    }
        //    if (Geburtsdatum == DateTime.MinValue)
        //    {
        //        error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
        //    }

        //    if (error.Length == 0) { return null; }

        //    return error.ToString();
        //}

        //public override object Clone()
        //{
        //    return new KundeDto
        //    {
        //        Id = Id,
        //        Nachname = Nachname,
        //        Vorname = Vorname,
        //        Geburtsdatum = Geburtsdatum
        //    };
        //}

        //public override string ToString()
        //{
        //    return string.Format(
        //        "{0}; {1}; {2}; {3}",
        //        Id,
        //        Nachname,
        //        Vorname,
        //        Geburtsdatum);
        //}

    }
}
