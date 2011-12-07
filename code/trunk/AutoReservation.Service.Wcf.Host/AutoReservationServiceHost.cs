#region

using System.ServiceModel;

#endregion

namespace AutoReservation.Service.Wcf.Host
{
    internal class AutoReservationServiceHost
    {
        internal static ServiceHost MyServiceHost;

        internal static void StartService()
        {
            //Instantiate new ServiceHost 
            MyServiceHost = new ServiceHost(typeof (AutoReservationService));

            //Open myServiceHost
            MyServiceHost.Open();
        }

        internal static void StopService()
        {
            //Call StopService from your shutdown logic (i.e. dispose method)
            if (MyServiceHost.State != CommunicationState.Closed) MyServiceHost.Close();
        }
    }
}