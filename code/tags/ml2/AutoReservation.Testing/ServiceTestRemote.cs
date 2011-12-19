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
        private IAutoReservationService _target;

        protected override IAutoReservationService Target
        {
            get
            {
                if (_target == null)
                {
                    var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                    _target = channelFactory.CreateChannel();
                }
                return _target;
            }
        }
    }
}