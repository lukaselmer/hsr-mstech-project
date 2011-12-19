﻿using System;
using System.Data.Objects.DataClasses;
using System.Reflection;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoReservation.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {

        [TestMethod]
        public void GetAutosTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var component = new AutoReservationBusinessComponent();
            var list = component.GetAutos();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void GetAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var component = new AutoReservationBusinessComponent();
            var id = component.GetAutos()[0].Id;
            var auto = component.GetAuto(id);
            Assert.IsNotNull(auto);
        }

        [TestMethod]
        public void CreateAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var component = new AutoReservationBusinessComponent();
            var auto = new LuxusklasseAuto {Basistarif = 10, Marke = "Test Auto", Tagestarif = 15};
            component.CreateAuto(auto);
            Auto resultAuto = component.GetAutos()[component.GetAutos().Count - 1];
            Assert.AreEqual(auto, resultAuto);
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var component = new AutoReservationBusinessComponent();
            var oldAuto = component.GetAutos()[0];
            var newAuto = (Auto) oldAuto.Copy();
            newAuto.Marke = "Test Marke";
            component.UpdateAuto(newAuto, oldAuto);
            Assert.AreEqual(newAuto, component.GetAutos()[0]);
        }

        [TestMethod]
        public void DeleteAutoTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var component = new AutoReservationBusinessComponent();
            var auto = component.GetAutos()[0];
            component.DeleteAuto(auto);
            Assert.IsFalse(component.GetAutos().Contains(auto));
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

    }
}
