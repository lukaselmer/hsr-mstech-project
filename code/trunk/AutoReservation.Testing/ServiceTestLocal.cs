using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Testing
{
    [TestClass]
    public class ServiceTestLocal : ServiceTestBase
    {

        private IAutoReservationService _target;
        protected override IAutoReservationService Target
        {
            get
            {
                if (_target == null)
                {
                    _target = new AutoReservationService();
                }
                return _target;
            }
        }

    }
}