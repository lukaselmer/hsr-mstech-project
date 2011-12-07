using System;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            Auto auto = new StandardAuto
            {
                Marke = "Renault Clio",
                Tagestarif = 65
            };

            int autoId = Target.InsertAuto(auto);
            Assert.AreNotEqual(0, autoId);

            Auto org = Target.GetAutoById(autoId);
            Auto mod = Target.GetAutoById(autoId);

            mod.Marke = "Fiat 500";

            Target.UpdateAuto(mod, org);

            Auto result = Target.GetAutoById(autoId);
            Assert.AreEqual(mod.GetType(), result.GetType());
            Assert.AreEqual(mod.Id, result.Id);
            Assert.AreEqual(mod.Marke, result.Marke);
            Assert.AreEqual(mod.Tagestarif, result.Tagestarif);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            Kunde kunde = new Kunde
            {
                Nachname = "Wand",
                Vorname = "Andi",
                Geburtsdatum = new DateTime(1955, 4, 12)
            };

            int kundeId = Target.InsertKunde(kunde);
            Assert.AreNotEqual(0, kundeId);

            Kunde org = Target.GetKundeById(kundeId);
            Kunde mod = Target.GetKundeById(kundeId);

            mod.Nachname = "Stein";
            mod.Vorname = "Sean";

            Target.UpdateKunde(mod, org);

            Kunde result = Target.GetKundeById(kundeId);
            Assert.AreEqual(mod.Id, result.Id);
            Assert.AreEqual(mod.Nachname, result.Nachname);
            Assert.AreEqual(mod.Vorname, result.Vorname);
            Assert.AreEqual(mod.Geburtsdatum, result.Geburtsdatum);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            Reservation reservation = new Reservation
            {
                AutoId = Target.Autos[0].Id,
                KundeId = Target.Kunden[0].Id,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(10)
            };

            int reservationNr = Target.InsertReservation(reservation);
            Assert.AreNotEqual(0, reservationNr);

            Reservation org = Target.GetReservationByNr(reservationNr);
            Reservation mod = Target.GetReservationByNr(reservationNr);

            mod.Von = DateTime.Today.AddYears(1);
            mod.Bis = DateTime.Today.AddDays(10).AddYears(1);

            Target.UpdateReservation(mod, org);

            Reservation result = Target.GetReservationByNr(reservationNr);
            Assert.AreEqual(mod.ReservationNr, result.ReservationNr);
            Assert.AreEqual(mod.Auto.Id, result.Auto.Id);
            Assert.AreEqual(mod.Kunde.Id, result.Kunde.Id);
            Assert.AreEqual(mod.Von, result.Von);
            Assert.AreEqual(mod.Bis, result.Bis);
        }

    }
}
