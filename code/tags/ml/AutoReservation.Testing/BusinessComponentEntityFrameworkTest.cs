using System;
using System.Collections.Generic;
using AutoReservation.BusinessLayer.EntityFramework;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.BusinessLayer.Test
{
    [TestClass]
    public class BusinessComponentEntityFrameworkTest
    {
        [TestMethod]
        public void AutosTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();
            IList<AutoDto> actual = target.Autos;

            Assert.AreEqual(3, actual.Count);
        }

        [TestMethod]
        public void KundenTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();
            IList<KundeDto> actual = target.Kunden;

            Assert.AreEqual(4, actual.Count);
        }

        [TestMethod]
        public void ReservationenTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();
            IList<ReservationDto> actual = target.Reservationen;

            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void GetAutoByIdTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            AutoDto auto = new AutoDto
            {
                Marke = "Opel Astra",
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 45
            };

            int autoId = target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            AutoDto actual = target.GetAutoById(autoId);

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
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            KundeDto kunde = new KundeDto
            {
                Nachname = "Jewo",
                Vorname = "Sara",
                Geburtsdatum = new DateTime(1961, 6, 21)
            };

            int kundeId = target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            KundeDto actual = target.GetKundeById(kundeId);

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
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            var reservationen = target.Reservationen;
            Assert.AreNotEqual(reservationen.Count, 0);

            ReservationDto expected = reservationen[0];
            ReservationDto actual = target.GetReservationByNr(expected.ReservationNr);

            Assert.AreEqual(expected.ReservationNr, actual.ReservationNr);
        }

        [TestMethod]
        public void GetReservationByIllegalNr()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            ReservationDto actual = target.GetReservationByNr(-1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void InsertAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            AutoDto auto = new AutoDto
            {
                Marke = "Seat Ibiza",
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 40
            };

            int autoId = target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            AutoDto result = target.GetAutoById(autoId);
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
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            KundeDto kunde = new KundeDto
            {
                Nachname = "Kolade",
                Vorname = "Joe",
                Geburtsdatum = new DateTime(1911, 1, 27)
            };

            int kundeId = target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            KundeDto result = target.GetKundeById(kundeId);
            Assert.AreEqual(kundeId, result.Id);
            Assert.AreEqual(kunde.Nachname, result.Nachname);
            Assert.AreEqual(kunde.Vorname, result.Vorname);
            Assert.AreEqual(kunde.Geburtsdatum, result.Geburtsdatum);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            ReservationDto reservation = new ReservationDto
            {
                Auto = target.Autos[0],
                Kunde = target.Kunden[0],
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(10)
            };

            int reservationNr = target.InsertReservation(reservation);
            Assert.AreNotEqual(0, reservationNr);

            ReservationDto result = target.GetReservationByNr(reservationNr);
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
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            AutoDto auto = new AutoDto
            {
                Marke = "Renault Clio",
                AutoKlasse = AutoKlasse.Standard,
                Tagestarif = 65
            };

            int autoId = target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            AutoDto org = target.GetAutoById(autoId);
            AutoDto mod = target.GetAutoById(autoId);

            mod.Marke = "Fiat 500";

            target.UpdateAuto(mod, org);

            AutoDto result = target.GetAutoById(autoId);
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
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            KundeDto kunde = new KundeDto
            {
                Nachname = "Wand",
                Vorname = "Andi",
                Geburtsdatum = new DateTime(1955, 4, 12)
            };

            int kundeId = target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            KundeDto org = target.GetKundeById(kundeId);
            KundeDto mod = target.GetKundeById(kundeId);

            mod.Nachname = "Stein";
            mod.Vorname = "Sean";

            target.UpdateKunde(mod, org);

            KundeDto result = target.GetKundeById(kundeId);
            Assert.AreEqual(mod.Id, result.Id);
            Assert.AreEqual(mod.Nachname, result.Nachname);
            Assert.AreEqual(mod.Vorname, result.Vorname);
            Assert.AreEqual(mod.Geburtsdatum, result.Geburtsdatum);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            ReservationDto reservation = new ReservationDto
            {
                Auto = target.Autos[0],
                Kunde = target.Kunden[0],
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(10)
            };

            int reservationNr = target.InsertReservation(reservation);
            Assert.AreNotEqual(0, reservationNr);

            ReservationDto org = target.GetReservationByNr(reservationNr);
            ReservationDto mod = target.GetReservationByNr(reservationNr);

            mod.Von = DateTime.Today.AddYears(1);
            mod.Bis = DateTime.Today.AddDays(10).AddYears(1);

            target.UpdateReservation(mod, org);

            ReservationDto result = target.GetReservationByNr(reservationNr);
            Assert.AreEqual(mod.ReservationNr, result.ReservationNr);
            Assert.AreEqual(mod.Auto.Id, result.Auto.Id);
            Assert.AreEqual(mod.Kunde.Id, result.Kunde.Id);
            Assert.AreEqual(mod.Von, result.Von);
            Assert.AreEqual(mod.Bis, result.Bis);
        }

        [TestMethod]
        public void UpdateAutoTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            // Client 1
            AutoDto originalAuto1 = target.Autos[0];
            AutoDto modifiedAuto1 = (AutoDto) originalAuto1.Clone();
            modifiedAuto1.Marke = "Citroen Saxo";

            // Client 2
            AutoDto originalAuto2 = target.Autos[0];
            AutoDto modifiedAuto2 = (AutoDto)originalAuto2.Clone();
            modifiedAuto2.Marke = "Peugot 106";

            //Client 1 Update
            target.UpdateAuto(modifiedAuto1, originalAuto1);

            //Client 2 Update
            try
            {
                target.UpdateAuto(modifiedAuto2, originalAuto2);
                Assert.Fail();
            }
            catch (OptimisticConcurrencyException<AutoDto>) { }
            catch { Assert.Fail("Unexpected Exception"); }
        }

        [TestMethod]
        public void UpdateKundeTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            // Client 1
            KundeDto originalKunde1 = target.Kunden[0];
            KundeDto modifiedKunde1 = (KundeDto)originalKunde1.Clone();
            modifiedKunde1.Nachname = "Hardegger";

            // Client 2
            KundeDto originalKunde2 = target.Kunden[0];
            KundeDto modifiedKunde2 = (KundeDto)originalKunde2.Clone();
            modifiedKunde2.Nachname = "Schmid";

            //Client 1 Update
            target.UpdateKunde(modifiedKunde1, originalKunde1);

            //Client 2 Update
            try
            {
                target.UpdateKunde(modifiedKunde2, originalKunde2);
                Assert.Fail();
            }
            catch (OptimisticConcurrencyException<KundeDto>) { }
            catch{ Assert.Fail("Unexpected Exception"); }
        }

        [TestMethod]
        public void UpdateReservationTestWithOptimisticConcurrency()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            // Client 1
            ReservationDto originalReservation1 = target.Reservationen[0];
            ReservationDto modifiedReservation1 = (ReservationDto)originalReservation1.Clone();
            modifiedReservation1.Bis = DateTime.Today.AddSeconds(10);

            // Client 2
            ReservationDto originalReservation2 = target.Reservationen[0];
            ReservationDto modifiedReservation2 = (ReservationDto)originalReservation2.Clone();
            modifiedReservation2.Bis = DateTime.Today.AddSeconds(20);

            //Client 1 Update
            target.UpdateReservation(modifiedReservation1, originalReservation1);

            //Client 2 Update
            try
            {
                target.UpdateReservation(modifiedReservation2, originalReservation2);
                Assert.Fail();
            }
            catch (OptimisticConcurrencyException<ReservationDto>) { }
            catch { Assert.Fail("Unexpected Exception"); }
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            KundeDto actual = target.Kunden[0];
            target.DeleteKunde(actual);

            KundeDto result = target.GetKundeById(actual.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            AutoDto actual = target.Autos[0];
            target.DeleteAuto(actual);

            AutoDto result = target.GetAutoById(actual.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            AutoReservationBusinessComponent target = new AutoReservationBusinessComponent();

            ReservationDto actual = target.Reservationen[0];
            target.DeleteReservation(actual);

            ReservationDto result = target.GetReservationByNr(actual.ReservationNr);

            Assert.IsNull(result);
        }
    }
}