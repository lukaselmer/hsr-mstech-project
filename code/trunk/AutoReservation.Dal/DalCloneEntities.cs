using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace AutoReservation.Dal
{
    public partial class Kunde : EntityObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;
            var kunde = (Kunde) obj;

            if (Id != kunde.Id) return false;
            if (!Equals(Nachname, kunde.Nachname)) return false;
            if (!Equals(Vorname, kunde.Vorname)) return false;
            if (!Equals(Geburtsdatum, kunde.Geburtsdatum)) return false;

            return true;
        }

        public Kunde Copy()
        {
            return new Kunde
            {
                Id = Id,
                Nachname = Nachname,
                Vorname = Vorname,
                Geburtsdatum = Geburtsdatum
            };
        }
    }

    public partial class Auto : EntityObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (GetType() != obj.GetType()) return false;

            var auto = (Auto)obj;

            if (Marke != auto.Marke) return false;
            if (Id != auto.Id) return false;
            if (Tagestarif != auto.Tagestarif) return false;

            return true;
        }

        public abstract object Copy();
    }

    public partial class StandardAuto : Auto
    {
        public override object Copy()
        {
            return new StandardAuto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
            };
        }
    }

    public partial class LuxusklasseAuto : Auto
    {
        public override object Copy()
        {
            return new LuxusklasseAuto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
                Basistarif = Basistarif
            };
        }
    }

    public partial class MittelklasseAuto : Auto
    {
        public override object Copy()
        {
            return new MittelklasseAuto
            {
                Id = Id,
                Marke = Marke,
                Tagestarif = Tagestarif,
            };
        }
    }

    public partial class Reservation : EntityObject
    {
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;
            Reservation reservation = (Reservation)obj;

            if (this.ReservationNr != reservation.ReservationNr) return false;
            if (!Object.Equals(this.Von, reservation.Von)) return false;
            if (!Object.Equals(this.Bis, reservation.Bis)) return false;
            return true;
        }

        public Reservation Copy()
        {
            return new Reservation
            {
                ReservationNr = ReservationNr,
                Von = Von,
                Bis = Bis,
                Auto = (Auto)Auto.Copy(),
                Kunde = (Kunde)Kunde.Copy()
            };
        }
    }
}
