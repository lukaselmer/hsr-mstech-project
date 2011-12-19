using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {

        //Autos
        public IList<Auto> GetAutos()
        {
            using (var context = new AutoReservationEntities())
            {
                var autos = from c in context.Autos
                            select c;
                return autos.ToList();
            }
        }

        public Auto GetAuto(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                var car = (from c in context.Autos
                           where c.Id == id
                           select c).FirstOrDefault();
                return car;
            }
        }

        public void CreateAuto(Auto auto)
        {
            using (var context = new AutoReservationEntities()){
            context.AddToAutos(auto);
            context.SaveChanges();
            }
        }

        public void UpdateAuto(Auto modified, Auto original)
        {
              using (var context = new AutoReservationEntities())
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

                    throw new LocalOptimisticConcurrencyException<Auto>(
                        "Konflikt beim Ändern eines Wertes der Klasse Auto") {Entity = modified};
                }
            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Attach(auto);
                context.Autos.DeleteObject(auto);
                context.SaveChanges();
            }
        }


        //Kunde
        public IList<Kunde> GetKunden()
        {
            using (var context = new AutoReservationEntities())
            {
                var kunden = from c in context.Kunden
                             select c;
                return kunden.ToList();
            }
        }

        public Kunde GetKunde(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                var kunde = (from k in context.Kunden
                             where k.Id == id
                             select k).FirstOrDefault();
                return kunde;
            }
        }

        public void CreateKunde(Kunde kunde)
        {
            using (var context = new AutoReservationEntities())
            {
                context.AddToKunden(kunde);
                context.SaveChanges();
            }
        }

        public void UpdateKunde(Kunde modified, Kunde original)
        {
            using (var context = new AutoReservationEntities())
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

                    throw new LocalOptimisticConcurrencyException<Kunde>(
                        "Konflikt beim Ändern eines Wertes der Klasse Kunde") { Entity = modified };
                }
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kunden.Attach(kunde);
                context.Kunden.DeleteObject(kunde);
                context.SaveChanges();
            }
        }


        //Reservation
        public IList<Reservation> GetReservationen()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Reservationen.Include("Auto").Include("Kunde").ToList();
            }
        }

        public Reservation GetReservation(int id)
        {
            using (var context = new AutoReservationEntities())
            {
                var reservation = (from r in context.Reservationen.Include("Auto").Include("Kunde")
                                   where r.ReservationNr == id
                                   select r).FirstOrDefault();
                return reservation;
            }
        }

        public void CreateReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.AddToReservationen(reservation);
                context.SaveChanges();
            }
        }

        public void UpdateReservationen(Reservation modified, Reservation original)
        {
            using (var context = new AutoReservationEntities())
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

                    throw new LocalOptimisticConcurrencyException<Reservation>(
                        "Konflikt beim Ändern eines Wertes der Klasse Reservation") { Entity = modified };
                }
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservationen.Attach(reservation);
                context.Reservationen.DeleteObject(reservation);
                context.SaveChanges();
            }
        }
    }
}
