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
    [ServiceBehavior]
    public class AutoReservationService : IAutoReservationService
    {
        private AutoReservationBusinessComponent component;

        public AutoReservationService()
        {
            component = new AutoReservationBusinessComponent();
        }

        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }

        public IList<AutoDto> GetAutos()
        {
            return component.GetAutos().ConvertToDtos();
        }

        public AutoDto GetAuto(int id)
        {
            return component.GetAuto(id).ConvertToDto();
        }

        public void CreateAuto(AutoDto auto)
        {
            component.CreateAuto(auto.ConvertToEntity());
        }

        public void UpdateAuto(AutoDto modified, AutoDto original)
        {
            {
                try
                {
                    WriteActualMethod();
                    component.UpdateAuto(modified.ConvertToEntity(), original.ConvertToEntity());
                }
                catch (LocalOptimisticConcurrencyException<Auto> ex)
                {
                    var e = new OptimisticConcurrencyException<AutoDto> { Entity = ex.Entity.ConvertToDto() };

                    throw new FaultException<OptimisticConcurrencyException<AutoDto>>(e);
                }
            }
        }

        public void DeleteAuto(AutoDto auto)
        {
            component.DeleteAuto(auto.ConvertToEntity());
        }

        public IList<KundeDto> GetKunden()
        {
            return component.GetKunden().ConvertToDtos();
        }

        public KundeDto getKunde(int id)
        {
            return component.GetKunde(id).ConvertToDto();
        }

        public void CreateKunde(KundeDto kunde)
        {
            component.CreateKunde(kunde.ConvertToEntity());
        }

        public void UpdateKunde(KundeDto modified, KundeDto original)
        {
                            try
                {
                    WriteActualMethod();
                    component.UpdateKunde(modified.ConvertToEntity(), original.ConvertToEntity());
                }
                catch (LocalOptimisticConcurrencyException<Kunde> ex)
                {
                    var e = new OptimisticConcurrencyException<KundeDto> { Entity = ex.Entity.ConvertToDto() };

                    throw new FaultException<OptimisticConcurrencyException<KundeDto>>(e);
                }
        }

        public void DeleteKunde(KundeDto kunde)
        {
            component.DeleteKunde(kunde.ConvertToEntity());
        }

        public IList<ReservationDto> GetReservationen()
        {
            return component.GetReservationen().ConvertToDtos();
        }

        public ReservationDto GetReservation(int id)
        {
            return component.GetReservation(id).ConvertToDto();
        }

        public void CreateReservation(ReservationDto reservation)
        {
            component.CreateReservation(reservation.ConvertToEntity());
        }

        public void UpdateReservation(ReservationDto modified, ReservationDto original)
        {
                try {
                    WriteActualMethod();
                    component.UpdateReservationen(modified.ConvertToEntity(), original.ConvertToEntity());
                }
                catch (LocalOptimisticConcurrencyException<Reservation> ex)
                {
                    var e = new OptimisticConcurrencyException<ReservationDto> { Entity = ex.Entity.ConvertToDto() };

                    throw new FaultException<OptimisticConcurrencyException<ReservationDto>>(e);
                }

        }

        public void DeleteReservation(ReservationDto reservation)
        {
            component.DeleteReservation(reservation.ConvertToEntity());
        }
    }
}