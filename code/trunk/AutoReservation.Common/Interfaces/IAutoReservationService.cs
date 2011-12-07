using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Exceptions;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract(Name = "Auto Reservation Service")]
    public interface IAutoReservationService
    {
        List<AutoDto> Autos
        {
            [OperationContract]
            get;
        }

        List<KundeDto> Kunden
        {
            [OperationContract]
            get;
        }
  
        List<ReservationDto> Reservationen
        {
            [OperationContract]
            get;
        }

        [OperationContract]
        AutoDto GetAutoById(int id);

        [OperationContract]
        KundeDto GetKundeById(int id);

        [OperationContract]
        ReservationDto GetReservationByNr(int reservationNr);
    
        [OperationContract]
        int InsertAuto(AutoDto auto);

        [OperationContract]
        int InsertKunde(KundeDto kunde);

        [OperationContract]
        int InsertReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyException<AutoDto>))]
        void UpdateAuto(AutoDto modified, AutoDto original);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyException<KundeDto>))]
        void UpdateKunde(KundeDto modified, KundeDto original);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyException<ReservationDto>))]
        void UpdateReservation(ReservationDto modified, ReservationDto original);
        
        [OperationContract]
        void DeleteAuto(AutoDto auto);

        [OperationContract]
        void DeleteKunde(KundeDto kunde);
        
        [OperationContract]
        void DeleteReservation(ReservationDto reservation);
    }
}
