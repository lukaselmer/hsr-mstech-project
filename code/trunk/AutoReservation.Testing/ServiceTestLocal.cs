#region

using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace AutoReservation.Testing
{
    [TestClass]
    public class ServiceTestLocal : ServiceTestBase
    {
        private IAutoReservationService _target;

        protected override IAutoReservationService Target
        {
            get { return _target ?? (_target = new AutoReservationService()); }
        }
    }
}