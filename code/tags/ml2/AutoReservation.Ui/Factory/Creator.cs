#region

using System;
using AutoReservation.Common.Interfaces;
using AutoReservation.Ui.Properties;

#endregion

namespace AutoReservation.Ui.Factory
{
    public abstract class Creator
    {
        public abstract IAutoReservationService CreateBusinessLayerInstance();

        public static Creator GetCreatorInstance()
        {
            var businessLayerType = Type.GetType(Settings.Default.BusinessLayerType);

            if (businessLayerType == null)
            {
                return new BusinessLayerCreator();
            }

            return (Creator) Activator.CreateInstance(businessLayerType);
        }
    }
}