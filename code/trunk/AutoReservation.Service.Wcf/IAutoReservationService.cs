using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Service.Wcf
{
    public interface IAutoReservationService
    {
        IEnumerable<AutoDto> Autos { get; set; }
        IEnumerable<KundeDto> Kunden { get; set; }
        void DeleteAuto(AutoDto selectedAuto);
        void DeleteKunde(KundeDto selectedKunde);
        void InsertAuto(AutoDto auto);
        void InsertKunde(KundeDto kunde);
        void UpdateAuto(AutoDto auto, AutoDto original);
        void UpdateKunde(KundeDto kunde, KundeDto original);
    }
}