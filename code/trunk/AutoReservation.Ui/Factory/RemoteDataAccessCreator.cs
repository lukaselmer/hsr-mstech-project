using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Ui.Factory
{
    class RemoteDataAccessCreator : Creator
    {
        public override IAutoReservationService CreateBusinessLayerInstance()
        {
            var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            return channelFactory.CreateChannel();
        }
    }
}
