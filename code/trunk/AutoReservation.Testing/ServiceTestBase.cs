using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer;

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

            AutoDto auto = new AutoDto
            {
                Marke = "Opel Astra",
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 45
            };

            int autoId = Target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            AutoDto actual = Target.GetAutoById(autoId);

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

            KundeDto kunde = new KundeDto
            {
                Nachname = "Jewo",
                Vorname = "Sara",
                Geburtsdatum = new DateTime(1961, 6, 21)
            };

            int kundeId = Target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            KundeDto actual = Target.GetKundeById(kundeId);

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

            ReservationDto expected = reservationen[0];
            ReservationDto actual = Target.GetReservationByNr(expected.ReservationNr);

            Assert.AreEqual(expected.ReservationNr, actual.ReservationNr);
        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            TestEnvironmentHelper.InitializeTestData();

            ReservationDto actual = Target.GetReservationByNr(-1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            AutoDto auto = new AutoDto
            {
                Marke = "Seat Ibiza",
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 40
            };

            int autoId = Target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            AutoDto result = Target.GetAutoById(autoId);
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

            KundeDto kunde = new KundeDto
            {
                Nachname = "Kolade",
                Vorname = "Joe",
                Geburtsdatum = new DateTime(1911, 1, 27)
            };

            int kundeId = Target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            KundeDto result = Target.GetKundeById(kundeId);
            Assert.AreEqual(kundeId, result.Id);
            Assert.AreEqual(kunde.Nachname, result.Nachname);
            Assert.AreEqual(kunde.Vorname, result.Vorname);
            Assert.AreEqual(kunde.Geburtsdatum, result.Geburtsdatum);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            ReservationDto reservation = new ReservationDto
            {
                Auto = Target.Autos[0],
                Kunde = Target.Kunden[0],
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(10)
            };

            int reservationNr = Target.InsertReservation(reservation);
            Assert.AreNotEqual(0, reservationNr);

            ReservationDto result = Target.GetReservationByNr(reservationNr);
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

            AutoDto auto = new AutoDto
            {
                Marke = "Renault Clio",
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 65
            };

            int autoId = Target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            AutoDto org = Target.GetAutoById(autoId);
            AutoDto mod = Target.GetAutoById(autoId);

            mod.Marke = "Fiat 500";

            Target.UpdateAuto(mod, org);

            AutoDto result = Target.GetAutoById(autoId);
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

            KundeDto kunde = new KundeDto
            {
                Nachname = "Wand",
                Vorname = "Andi",
                Geburtsdatum = new DateTime(1955, 4, 12)
            };

            int kundeId = Target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            KundeDto org = Target.GetKundeById(kundeId);
            KundeDto mod = Target.GetKundeById(kundeId);

            mod.Nachname = "Stein";
            mod.Vorname = "Sean";

            Target.UpdateKunde(mod, org);

            KundeDto result = Target.GetKundeById(kundeId);
            Assert.AreEqual(mod.Id, result.Id);
            Assert.AreEqual(mod.Nachname, result.Nachname);
            Assert.AreEqual(mod.Vorname, result.Vorname);
            Assert.AreEqual(mod.Geburtsdatum, result.Geburtsdatum);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            ReservationDto reservation = new ReservationDto
            {
                Auto = Target.Autos[0],
                Kunde = Target.Kunden[0],
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(10)
            };

            int reservationNr = Target.InsertReservation(reservation);
            Assert.AreNotEqual(0, reservationNr);

            ReservationDto org = Target.GetReservationByNr(reservationNr);
            ReservationDto mod = Target.GetReservationByNr(reservationNr);

            mod.Von = DateTime.Today.AddYears(1);
            mod.Bis = DateTime.Today.AddDays(10).AddYears(1);

            Target.UpdateReservation(mod, org);

            ReservationDto result = Target.GetReservationByNr(reservationNr);
            Assert.AreEqual(mod.ReservationNr, result.ReservationNr);
            Assert.AreEqual(mod.Auto.Id, result.Auto.Id);
            Assert.AreEqual(mod.Kunde.Id, result.Kunde.Id);
            Assert.AreEqual(mod.Von, result.Von);
            Assert.AreEqual(mod.Bis, result.Bis);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyException<AutoDto>>))]
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

            // Client 1
            AutoDto originalAuto1 = Target.Autos[0];
            AutoDto modifiedAuto1 = (AutoDto)originalAuto1.Clone();
            modifiedAuto1.Marke = "Citroen Saxo";

            // Client 2
            AutoDto originalAuto2 = Target.Autos[0];
            AutoDto modifiedAuto2 = (AutoDto)originalAuto2.Clone();
            modifiedAuto2.Marke = "Peugot 106";

            //Client 1 Update
            Target.UpdateAuto(modifiedAuto1, originalAuto1);

            //Client 2 Update
            Target.UpdateAuto(modifiedAuto2, originalAuto2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyException<KundeDto>>))]
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

            // Client 1
            KundeDto originalKunde1 = Target.Kunden[0];
            KundeDto modifiedKunde1 = (KundeDto)originalKunde1.Clone();
            modifiedKunde1.Nachname = "Hardegger";

            // Client 2
            KundeDto originalKunde2 = Target.Kunden[0];
            KundeDto modifiedKunde2 = (KundeDto)originalKunde2.Clone();
            modifiedKunde2.Nachname = "Schmid";

            //Client 1 Update
            Target.UpdateKunde(modifiedKunde1, originalKunde1);

            //Client 2 Update
            Target.UpdateKunde(modifiedKunde2, originalKunde2);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<OptimisticConcurrencyException<ReservationDto>>))]
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();

            // Client 1
            var res = Target.Reservationen;
            ReservationDto originalReservation1 = Target.Reservationen[0];
            ReservationDto modifiedReservation1 = (ReservationDto)originalReservation1.Clone();
            modifiedReservation1.Bis = DateTime.Today.AddSeconds(10);

            // Client 2
            ReservationDto originalReservation2 = Target.Reservationen[0];
            ReservationDto modifiedReservation2 = (ReservationDto)originalReservation2.Clone();
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

            KundeDto actual = Target.Kunden[0];
            Target.DeleteKunde(actual);

            KundeDto result = Target.GetKundeById(actual.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            AutoDto actual = Target.Autos[0];
            Target.DeleteAuto(actual);

            AutoDto result = Target.GetAutoById(actual.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            ReservationDto actual = Target.Reservationen[0];
            Target.DeleteReservation(actual);

            ReservationDto result = Target.GetReservationByNr(actual.ReservationNr);

            Assert.IsNull(result);
        }
    }
}
