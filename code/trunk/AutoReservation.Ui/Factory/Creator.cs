using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoReservation.Common.Interfaces;
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
                return new LocalDataAccessCreator();
            }

            return (Creator)Activator.CreateInstance(businessLayerType);
        }
    }
}
