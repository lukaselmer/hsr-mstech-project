using System;
using AutoReservation.Service.Wcf;
using AutoReservation.Ui.Properties;

namespace AutoReservation.Ui.Factory
{
    public class Creator
    {
        public static Creator GetCreatorInstance()
        {
            Type businessLayerType = Type.GetType(Settings.Default.BusinessLayerType);
            return (Creator)Activator.CreateInstance(businessLayerType);
        }

        public IAutoReservationService CreateBusinessLayerInstance()
        {
            return new AutoReservationService();
        }
    }
}