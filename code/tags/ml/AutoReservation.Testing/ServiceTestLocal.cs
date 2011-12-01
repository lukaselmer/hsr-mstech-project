using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Testing
{
    [TestClass]
    public class ServiceTestLocal : ServiceTestBase
    {

        private IAutoReservationService target;
        protected override IAutoReservationService Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationService();
                }
                return target;
            }
        }

    }
}