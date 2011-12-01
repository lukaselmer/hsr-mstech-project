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
        //public AutoKlasse AutoKlasse{get
        //{
        //    return AutoKlasse.Luxusklasse;
        //}}

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Marke { get; set; }
        [DataMember]
        public int Tagestarif { get; set; }
        [DataMember]
        public int? Basistarif { get; set; }
        [DataMember]
        public AutoKlasse AutoKlasse { get; set; }

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
        //    if (string.IsNullOrEmpty(marke))
        //    {
        //        error.AppendLine("- Marke ist nicht gesetzt.");
        //    }
        //    if (tagestarif <= 0)
        //    {
        //        error.AppendLine("- Tagestarif muss grösser als 0 sein.");
        //    }
        //    if (AutoKlasse == AutoKlasse.Luxusklasse && basistarif <= 0)
        //    {
        //        error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
        //    }

        //    if (error.Length == 0) { return null; }

        //    return error.ToString();
        //}

        //public override object Clone()
        //{
        //    return new AutoDto
        //    {
        //        Id = Id,
        //        Marke = Marke,
        //        Tagestarif = Tagestarif,
        //        AutoKlasse = AutoKlasse,
        //        Basistarif = Basistarif
        //    };
        //}

        //public override string ToString()
        //{
        //    return string.Format(
        //        "{0}; {1}; {2}; {3}; {4}",
        //        Id,
        //        Marke,
        //        Tagestarif,
        //        Basistarif,
        //        AutoKlasse);
        //}

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
