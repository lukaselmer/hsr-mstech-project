#region

using System;

#endregion

namespace AutoReservation.Service.Wcf.Host
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("AutoReservationService starting...");
            AutoReservationServiceHost.StartService();
            Console.WriteLine("AutoReservationService started...");
            Console.ReadLine();
            AutoReservationServiceHost.StopService();
        }
    }
}