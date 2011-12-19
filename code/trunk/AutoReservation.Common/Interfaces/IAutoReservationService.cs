using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        //Auto
        [OperationContract]
        IList<AutoDto> GetAutos();
        [OperationContract]
        AutoDto GetAuto(int id);
        [OperationContract]
        void CreateAuto(AutoDto auto);
        [OperationContract]
        void UpdateAuto(AutoDto modified, AutoDto original);
        [OperationContract]
        void DeleteAuto(AutoDto auto);

        //Kunde
        [OperationContract]
        IList<KundeDto> GetKunden();
        [OperationContract]
        KundeDto getKunde(int id);
        [OperationContract]
        void CreateKunde(KundeDto kunde);
        [OperationContract]
        void UpdateKunde(KundeDto modified, KundeDto original);
        [OperationContract]
        void DeleteKunde(KundeDto kunde);

        //Reservation
        [OperationContract]
        IList<ReservationDto> GetReservationen();
        [OperationContract]
        ReservationDto GetReservation(int id);
        [OperationContract]
        void CreateReservation(ReservationDto reservation);
        [OperationContract]
        void UpdateReservation(ReservationDto modified, ReservationDto original);
        [OperationContract]
        void DeleteReservation(ReservationDto reservation);
    }
}
