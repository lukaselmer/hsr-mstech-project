using AutoReservation.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void AutosLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void KundenLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }
    }
}