using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestMethod]
        public void AutosTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void KundenTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void ReservationenTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void GetAutoByIdTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void InsertAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void InsertKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void InsertReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

        }
    }
}
