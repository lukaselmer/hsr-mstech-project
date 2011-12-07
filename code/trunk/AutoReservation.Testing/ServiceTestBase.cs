#region

using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Exceptions;
using AutoReservation.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

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

            IList<AutoDto> actual = Target.Autos;

            Assert.AreEqual(3, actual.Count);
        }

        [TestMethod]
        public void KundenTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            IList<KundeDto> actual = Target.Kunden;

            Assert.AreEqual(4, actual.Count);
        }

        [TestMethod]
        public void ReservationenTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            IList<ReservationDto> actual = Target.Reservationen;

            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void GetAutoByIdTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var auto = new AutoDto {Marke = "Opel Astra", AutoKlasse = AutoKlasse.Standard, Tagestarif = 45};

            var autoId = Target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            var actual = Target.GetAutoById(autoId);

            Assert.IsNotNull(actual);
            Assert.AreEqual(autoId, actual.Id);
            Assert.AreEqual(auto.Marke, actual.Marke);
            Assert.AreEqual(auto.AutoKlasse, actual.AutoKlasse);
            Assert.AreEqual(auto.Tagestarif, actual.Tagestarif);
            Assert.AreEqual(auto.Basistarif, actual.Basistarif);
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var kunde = new KundeDto {Nachname = "Jewo", Vorname = "Sara", Geburtsdatum = new DateTime(1961, 6, 21)};

            var kundeId = Target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            var actual = Target.GetKundeById(kundeId);

            Assert.IsNotNull(actual);
            Assert.AreEqual(kundeId, actual.Id);
            Assert.AreEqual(kunde.Nachname, actual.Nachname);
            Assert.AreEqual(kunde.Vorname, actual.Vorname);
            Assert.AreEqual(kunde.Geburtsdatum, actual.Geburtsdatum);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var reservationen = Target.Reservationen;
            Assert.AreNotEqual(reservationen.Count, 0);

            var expected = reservationen[0];
            var actual = Target.GetReservationByNr(expected.ReservationNr);

            Assert.AreEqual(expected.ReservationNr, actual.ReservationNr);
        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            TestEnvironmentHelper.InitializeTestData();

            var actual = Target.GetReservationByNr(-1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var auto = new AutoDto {Marke = "Seat Ibiza", AutoKlasse = AutoKlasse.Standard, Tagestarif = 40};

            var autoId = Target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            var result = Target.GetAutoById(autoId);
            Assert.AreEqual(autoId, result.Id);
            Assert.AreEqual(auto.Marke, result.Marke);
            Assert.AreEqual(auto.AutoKlasse, result.AutoKlasse);
            Assert.AreEqual(auto.Tagestarif, result.Tagestarif);
            Assert.AreEqual(auto.Basistarif, result.Basistarif);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var kunde = new KundeDto {Nachname = "Kolade", Vorname = "Joe", Geburtsdatum = new DateTime(1911, 1, 27)};

            var kundeId = Target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            var result = Target.GetKundeById(kundeId);
            Assert.AreEqual(kundeId, result.Id);
            Assert.AreEqual(kunde.Nachname, result.Nachname);
            Assert.AreEqual(kunde.Vorname, result.Vorname);
            Assert.AreEqual(kunde.Geburtsdatum, result.Geburtsdatum);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var reservation = new ReservationDto
                              {
                                  Auto = Target.Autos[0], Kunde = Target.Kunden[0], Von = DateTime.Today,
                                  Bis = DateTime.Today.AddDays(10)
                              };

            var reservationNr = Target.InsertReservation(reservation);
            Assert.AreNotEqual(0, reservationNr);

            var result = Target.GetReservationByNr(reservationNr);
            Assert.AreEqual(reservationNr, result.ReservationNr);
            Assert.AreEqual(reservation.Auto.Id, result.Auto.Id);
            Assert.AreEqual(reservation.Kunde.Id, result.Kunde.Id);
            Assert.AreEqual(reservation.Von, result.Von);
            Assert.AreEqual(reservation.Bis, result.Bis);
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var auto = new AutoDto {Marke = "Renault Clio", AutoKlasse = AutoKlasse.Standard, Tagestarif = 65};

            var autoId = Target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            var org = Target.GetAutoById(autoId);
            var mod = Target.GetAutoById(autoId);

            mod.Marke = "Fiat 500";

            Target.UpdateAuto(mod, org);

            var result = Target.GetAutoById(autoId);
            Assert.AreEqual(mod.Id, result.Id);
            Assert.AreEqual(mod.Marke, result.Marke);
            Assert.AreEqual(mod.AutoKlasse, result.AutoKlasse);
            Assert.AreEqual(mod.Tagestarif, result.Tagestarif);
            Assert.AreEqual(mod.Basistarif, result.Basistarif);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var kunde = new KundeDto {Nachname = "Wand", Vorname = "Andi", Geburtsdatum = new DateTime(1955, 4, 12)};

            var kundeId = Target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            var org = Target.GetKundeById(kundeId);
            var mod = Target.GetKundeById(kundeId);

            mod.Nachname = "Stein";
            mod.Vorname = "Sean";

            Target.UpdateKunde(mod, org);

            var result = Target.GetKundeById(kundeId);
            Assert.AreEqual(mod.Id, result.Id);
            Assert.AreEqual(mod.Nachname, result.Nachname);
            Assert.AreEqual(mod.Vorname, result.Vorname);
            Assert.AreEqual(mod.Geburtsdatum, result.Geburtsdatum);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var reservation = new ReservationDto
                              {
                                  Auto = Target.Autos[0], Kunde = Target.Kunden[0], Von = DateTime.Today,
                                  Bis = DateTime.Today.AddDays(10)
                              };

            var reservationNr = Target.InsertReservation(reservation);
            Assert.AreNotEqual(0, reservationNr);

            var org = Target.GetReservationByNr(reservationNr);
            var mod = Target.GetReservationByNr(reservationNr);

            mod.Von = DateTime.Today.AddYears(1);
            mod.Bis = DateTime.Today.AddDays(10).AddYears(1);

            Target.UpdateReservation(mod, org);

            var result = Target.GetReservationByNr(reservationNr);
            Assert.AreEqual(mod.ReservationNr, result.ReservationNr);
            Assert.AreEqual(mod.Auto.Id, result.Auto.Id);
            Assert.AreEqual(mod.Kunde.Id, result.Kunde.Id);
            Assert.AreEqual(mod.Von, result.Von);
            Assert.AreEqual(mod.Bis, result.Bis);
        }

        [TestMethod, ExpectedException(typeof (FaultException<OptimisticConcurrencyException<AutoDto>>))]
        
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

            // Client 1
            var originalAuto1 = Target.Autos[0];
            var modifiedAuto1 = (AutoDto) originalAuto1.Clone();
            modifiedAuto1.Marke = "Citroen Saxo";

            // Client 2
            var originalAuto2 = Target.Autos[0];
            var modifiedAuto2 = (AutoDto) originalAuto2.Clone();
            modifiedAuto2.Marke = "Peugot 106";

            //Client 1 Update
            Target.UpdateAuto(modifiedAuto1, originalAuto1);

            //Client 2 Update
            Target.UpdateAuto(modifiedAuto2, originalAuto2);
        }

        [TestMethod, ExpectedException(typeof (FaultException<OptimisticConcurrencyException<KundeDto>>))]
        
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

            // Client 1
            var originalKunde1 = Target.Kunden[0];
            var modifiedKunde1 = (KundeDto) originalKunde1.Clone();
            modifiedKunde1.Nachname = "Hardegger";

            // Client 2
            var originalKunde2 = Target.Kunden[0];
            var modifiedKunde2 = (KundeDto) originalKunde2.Clone();
            modifiedKunde2.Nachname = "Schmid";

            //Client 1 Update
            Target.UpdateKunde(modifiedKunde1, originalKunde1);

            //Client 2 Update
            Target.UpdateKunde(modifiedKunde2, originalKunde2);
        }

        [TestMethod, ExpectedException(typeof (FaultException<OptimisticConcurrencyException<ReservationDto>>))]
        
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

            // Client 1
            var res = Target.Reservationen;
            var originalReservation1 = Target.Reservationen[0];
            var modifiedReservation1 = (ReservationDto) originalReservation1.Clone();
            modifiedReservation1.Bis = DateTime.Today.AddSeconds(10);

            // Client 2
            var originalReservation2 = Target.Reservationen[0];
            var modifiedReservation2 = (ReservationDto) originalReservation2.Clone();
            modifiedReservation2.Bis = DateTime.Today.AddSeconds(20);

            //Client 1 Update
            Target.UpdateReservation(modifiedReservation1, originalReservation1);

            //Client 2 Update
            Target.UpdateReservation(modifiedReservation2, originalReservation2);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var actual = Target.Kunden[0];
            Target.DeleteKunde(actual);

            var result = Target.GetKundeById(actual.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var actual = Target.Autos[0];
            Target.DeleteAuto(actual);

            var result = Target.GetAutoById(actual.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var actual = Target.Reservationen[0];
            Target.DeleteReservation(actual);

            var result = Target.GetReservationByNr(actual.ReservationNr);

            Assert.IsNull(result);
        }
    }
}