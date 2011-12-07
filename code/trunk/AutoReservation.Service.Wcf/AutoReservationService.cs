using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Exceptions;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }

        private AutoReservationBusinessComponent _businessComponent;
        private AutoReservationBusinessComponent BusinessComponent
        {
            get { return _businessComponent ?? (_businessComponent = new AutoReservationBusinessComponent()); }
        }

        public List<AutoDto> Autos
        {
            get
            {
                WriteActualMethod();
                return BusinessComponent.Autos.ConvertToDtos();
            }
        }

        public List<ReservationDto> Reservationen
        {
            get
            {
                WriteActualMethod();
                return BusinessComponent.Reservationen.ConvertToDtos();
            }
        }

        public List<KundeDto> Kunden
        {
            get
            {
                WriteActualMethod();
                return BusinessComponent.Kunden.ConvertToDtos();
            }
        }

        public AutoDto GetAutoById(int id)
        {
            WriteActualMethod();
            return BusinessComponent.GetAutoById(id).ConvertToDto();
        }

        public KundeDto GetKundeById(int id)
        {
            WriteActualMethod();
            Kunde k = BusinessComponent.GetKundeById(id);
            return k.ConvertToDto();
        }

        public ReservationDto GetReservationByNr(int reservationNr)
        {
            WriteActualMethod();
            return BusinessComponent.GetReservationByNr(reservationNr).ConvertToDto();
        }

        public int InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            return BusinessComponent.InsertAuto(auto.ConvertToEntity());
        }

        public int InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            return BusinessComponent.InsertKunde(kunde.ConvertToEntity());
        }

        public int InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return BusinessComponent.InsertReservation(reservation.ConvertToEntity());
        }

        public void UpdateAuto(AutoDto modified, AutoDto original)
        {
            try
            {
                WriteActualMethod();
                BusinessComponent.UpdateAuto(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Auto> ex)
            {
                OptimisticConcurrencyException<AutoDto> enThrow = new OptimisticConcurrencyException<AutoDto>();
                enThrow.Entity = ex.Entity.ConvertToDto();

                throw new FaultException<OptimisticConcurrencyException<AutoDto>>(enThrow);
            }
        }


        public void UpdateKunde(KundeDto modified, KundeDto original)
        {
            try
            {
                WriteActualMethod();
                BusinessComponent.UpdateKunde(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Kunde> ex)
            {
                OptimisticConcurrencyException<KundeDto> enThrow = new OptimisticConcurrencyException<KundeDto>();
                enThrow.Entity = ex.Entity.ConvertToDto();

                throw new FaultException<OptimisticConcurrencyException<KundeDto>>(enThrow);
            }
        }

        public void UpdateReservation(ReservationDto modified, ReservationDto original)
        {
            try
            {
                WriteActualMethod();
                BusinessComponent.UpdateReservation(modified.ConvertToEntity(), original.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Reservation> ex)
            {
                OptimisticConcurrencyException<ReservationDto> enThrow = new OptimisticConcurrencyException<ReservationDto>();
                enThrow.Entity = ex.Entity.ConvertToDto();

                throw new FaultException<OptimisticConcurrencyException<ReservationDto>>(enThrow);
            }
        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            BusinessComponent.DeleteAuto(auto.ConvertToEntity());
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            BusinessComponent.DeleteKunde(kunde.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            BusinessComponent.DeleteReservation(reservation.ConvertToEntity());
        }
    }
}