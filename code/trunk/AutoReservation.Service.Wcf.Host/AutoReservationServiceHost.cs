using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Host
{
    internal class AutoReservationServiceHost
    {
        internal static ServiceHost myServiceHost;

        internal static void StartService()
        {
            //Instantiate new ServiceHost 
            myServiceHost = new ServiceHost(typeof(AutoReservationService));

            //Open myServiceHost
            myServiceHost.Open();
        }

        internal static void StopService()
        {
            //Call StopService from your shutdown logic (i.e. dispose method)
            if (myServiceHost.State != CommunicationState.Closed)
                myServiceHost.Close();
        }
    }
}
