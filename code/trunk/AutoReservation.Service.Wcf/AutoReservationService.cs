using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }

        public IEnumerable<AutoDto> Autos { get; set; }
        public IEnumerable<KundeDto> Kunden { get; set; }
        public void DeleteAuto(AutoDto selectedAuto)
        {
            throw new NotImplementedException();
        }

        public void DeleteKunde(KundeDto selectedKunde)
        {
            throw new NotImplementedException();
        }

        public void InsertAuto(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public void InsertKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuto(AutoDto auto, AutoDto original)
        {
            throw new NotImplementedException();
        }

        public void UpdateKunde(KundeDto kunde, KundeDto original)
        {
            throw new NotImplementedException();
        }
    }
}