#region

using System.ServiceModel;
using AutoReservation.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace AutoReservation.Testing
{
    [TestClass]
    public class ServiceTestRemote : ServiceTestBase
    {
        private IAutoReservationService target;

        protected override IAutoReservationService Target
        {
            get
            {
                if (target == null)
                {
                    var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                    target = channelFactory.CreateChannel();
                }
                return target;
            }
        }
    }
}