using System;
using AutoReservation.Service.Wcf;
using AutoReservation.Ui.Properties;

namespace AutoReservation.Ui.Factory
{

    public abstract class Creator
    {
        public abstract IAutoReservationService CreateBusinessLayerInstance();
        public static Creator GetCreatorInstance()
        {
            Type businessLayerType = Type.GetType(Settings.Default.BusinessLayerType);

            if (businessLayerType == null)
            {
                return new BusinessLayerCreator();
            }

            return (Creator)Activator.CreateInstance(businessLayerType);
        }
    }
}