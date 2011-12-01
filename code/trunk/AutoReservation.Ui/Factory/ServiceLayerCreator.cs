using System.ServiceModel;
using AutoReservation.Service.Wcf;

namespace AutoReservation.Ui.Factory
{
    public class ServiceLayerCreator : Creator
    {
        public override IAutoReservationService CreateBusinessLayerInstance()
        {
            ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            return channelFactory.CreateChannel();
        }
    }
}