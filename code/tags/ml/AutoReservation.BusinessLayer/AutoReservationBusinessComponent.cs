using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Objects;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        public IList<Auto> Autos
        {
            get
            {
                using (AutoReservationEntities context = new AutoReservationEntities())
                {
                    return context.Autos.ToList();
                }
            }
        }

        public List<Kunde> Kunden
        {
            get
            {
                using (AutoReservationEntities context = new AutoReservationEntities())
                {
                    return context.Kunden.ToList();
                }
            }
        }

        public List<Reservation> Reservationen
        {
            get
            {
                using (AutoReservationEntities context = new AutoReservationEntities())
                {
                    return context.Reservationen
                        .Include("Auto")
                        .Include("Kunde")
                        .ToList();
                }
            }
        }

        public Kunde GetKundeById(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                Kunde kunde = context.Kunden.Where(k => k.Id == id).FirstOrDefault();
                return kunde;
            }
        }

        public Auto GetAutoById(int id)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                Auto auto = context.Autos.Where(a => a.Id == id).FirstOrDefault();
                return auto;
            }
        }

        public Reservation GetReservationByNr(int reservationNr)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                Reservation reservation = context.Reservationen
                    .Include("Auto")
                    .Include("Kunde")
                    .Where(r => r.ReservationNr == reservationNr)
                    .FirstOrDefault();
                return reservation;
            }
        }

        public int InsertAuto(Auto auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.AddToAutos(auto);
                context.SaveChanges();
                return auto.Id;
            }
        }

        public int InsertKunde(Kunde kunde)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.AddToKunden(kunde);
                context.SaveChanges();
                return kunde.Id;
            }
        }

        public int InsertReservation(Reservation reservation)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.AddToReservationen(reservation);
                context.SaveChanges();
                return reservation.ReservationNr;
            }
        }

        public void UpdateAuto(Auto modified, Auto original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Autos.Attach(original);
                    context.Autos.ApplyCurrentValues(modified);
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {
                    modified.EntityKey = original.EntityKey;

                    context.Refresh(RefreshMode.StoreWins, modified);
                    context.SaveChanges();

                    throw new LocalOptimisticConcurrencyException<Auto>("Update Auto: Concurrency-Fehler") { Entity = modified };
                }
            }
        }

        public void UpdateKunde(Kunde modified, Kunde original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Kunden.Attach(original);
                    context.Kunden.ApplyCurrentValues(modified);
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {
                    modified.EntityKey = original.EntityKey;

                    context.Refresh(RefreshMode.StoreWins, modified);
                    context.SaveChanges();

                    throw new LocalOptimisticConcurrencyException<Kunde>("Update Kunde: Concurrency-Fehler") { Entity = modified };
                }
            }
        }

        public void UpdateReservation(Reservation modified, Reservation original)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                try
                {
                    context.Reservationen.Attach(original);
                    context.Reservationen.ApplyCurrentValues(modified);
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException)
                {
                    modified.EntityKey = original.EntityKey;

                    context.Refresh(RefreshMode.StoreWins, modified);
                    context.SaveChanges();

                    throw new LocalOptimisticConcurrencyException<Reservation>("Update Reservation: Concurrency-Fehler") { Entity = modified };
                }
            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Autos.Attach(auto);
                context.Autos.DeleteObject(auto);
                context.SaveChanges();
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Kunden.Attach(kunde);
                context.Kunden.DeleteObject(kunde);
                context.SaveChanges();
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Reservationen.Attach(reservation);
                context.Reservationen.DeleteObject(reservation);
                context.SaveChanges();
            }
        }

    }
}