using System;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Exceptions;
using AutoReservation.Common.Interfaces;
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
            var autos = Target.GetAutos();
            Assert.IsNotNull(autos);
        }

        [TestMethod]
        public void KundenTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var kunden = Target.GetKunden();
            Assert.IsNotNull(kunden);
        }

        [TestMethod]
        public void ReservationenTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var reservationen = Target.GetReservationen();
            Assert.IsNotNull(reservationen);
        }

        [TestMethod]
        public void GetAutoByIdTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var autoId = Target.GetAutos()[0].Id;
            var auto = Target.GetAuto(autoId);
            Assert.AreEqual(autoId, auto.Id);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var kundenId = Target.GetKunden()[0].Id;
            var kunde = Target.getKunde(kundenId);
            Assert.AreEqual(kundenId, kunde.Id);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var reservationsNr = Target.GetReservationen()[0].ReservationNr;
            var reservation = Target.GetReservation(reservationsNr);
            Assert.AreEqual(reservationsNr, reservation.ReservationNr);
        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            TestEnvironmentHelper.InitializeTestData();
            var reservation = Target.GetReservation(-1);
            Assert.IsNull(reservation);
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var auto = new AutoDto {Marke = "Test Marke", AutoKlasse = AutoKlasse.Mittelklasse, Tagestarif = 13, Id = Target.GetAutos()[Target.GetAutos().Count-1].Id + 1};
            Target.CreateAuto(auto);
            var autoResult = Target.GetAutos()[Target.GetAutos().Count-1];
            Assert.AreEqual(auto, autoResult);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var kunde = new KundeDto()
                            {
                                Geburtsdatum = new DateTime(2011, 11, 10),
                                Id = Target.GetKunden()[Target.GetKunden().Count - 1].Id + 1,
                                Nachname = "Muster",
                                Vorname = "Peter"
                            };
            Target.CreateKunde(kunde);
            var kundeResult = Target.GetKunden()[Target.GetKunden().Count - 1];
            Assert.AreEqual(kunde, kundeResult);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var reservation = new ReservationDto()
                                  {
                                      Auto = Target.GetAutos()[0],
                                      Bis = new DateTime(2011, 11, 10),
                                      Kunde = Target.GetKunden()[0],
                                      ReservationNr =
                                          Target.GetReservationen()[Target.GetReservationen().Count - 1].ReservationNr + 1,
                                      Von = new DateTime(2011, 10, 10)
                                  };
            Target.CreateReservation(reservation);
            var reservationResult = Target.GetReservationen()[Target.GetReservationen().Count - 1];
            Assert.AreEqual(reservation, reservationResult);
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var original = Target.GetAutos()[0];
            var modified = (AutoDto) original.Clone();
            modified.Marke = "Test Marke";
            Target.UpdateAuto(modified, original);
            Assert.AreEqual(modified, Target.GetAutos()[0]);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var original = Target.GetKunden()[0];
            var modified = (KundeDto) original.Clone();
            Target.UpdateKunde(modified, original);
            Assert.AreEqual(modified, Target.GetKunden()[0]);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var original = Target.GetReservationen()[0];
            var modified = (ReservationDto) original.Clone();
            Target.UpdateReservation(modified, original);
            Assert.AreEqual(modified, Target.GetReservationen()[0]);
        }

       [TestMethod, ExpectedException(typeof(FaultException<OptimisticConcurrencyException<AutoDto>>))]
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();
           var original1 = Target.GetAutos()[0];
           var modified1 = (AutoDto)original1.Clone();
           modified1.Marke = "Test Marke 1";

           var original2 = Target.GetAutos()[0];
           var modified2 = (AutoDto) original2.Clone();
           modified2.Marke = "Test Marke 2";

           Target.UpdateAuto(modified1, original1);
           Target.UpdateAuto(modified2, original2);
        }

        [TestMethod, ExpectedException(typeof(FaultException<OptimisticConcurrencyException<KundeDto>>))]
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();
            var original1 = Target.GetKunden()[0];
            var modified1 = (KundeDto) original1.Clone();
            modified1.Nachname = "Muster 1";

            var original2 = Target.GetKunden()[0];
            var modified2 = (KundeDto)original2.Clone();
            modified2.Nachname = "Muster 2";

            Target.UpdateKunde(modified1, original1);
            Target.UpdateKunde(modified2, original2);
        }

        [TestMethod, ExpectedException(typeof(FaultException<OptimisticConcurrencyException<ReservationDto>>))]
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();
            var original1 = Target.GetReservationen()[0];
            var modified1 = (ReservationDto) original1.Clone();
            modified1.Von = new DateTime(1990, 10, 10);

            var original2 = Target.GetReservationen()[0];
            var modified2 = (ReservationDto)original2.Clone();
            modified2.Von = new DateTime(1990, 10, 11);

            Target.UpdateReservation(modified1, original1);
            Target.UpdateReservation(modified2, original2);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var kunde = Target.GetKunden()[0];
            Target.DeleteKunde(kunde);
            Assert.IsFalse(Target.GetKunden().Contains(kunde));
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var auto = Target.GetAutos()[0];
            Target.DeleteAuto(auto);
            Assert.IsFalse(Target.GetAutos().Contains(auto));
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var reservation = Target.GetReservationen()[0];
            Target.DeleteReservation(reservation);
            Assert.IsFalse(Target.GetReservationen().Contains(reservation));
        }
    }
}
