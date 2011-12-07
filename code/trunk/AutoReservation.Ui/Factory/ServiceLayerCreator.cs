#region

using System.ServiceModel;
using AutoReservation.Common.Interfaces;

#endregion

namespace AutoReservation.Ui.Factory
{
    public class ServiceLayerCreator : Creator
    {
        public override IAutoReservationService CreateBusinessLayerInstance()
        {
            var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            return channelFactory.CreateChannel();
        }
    }
}