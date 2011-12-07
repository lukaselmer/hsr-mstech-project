using System.Windows.Input;
using AutoReservation.Testing;
using AutoReservation.Ui.ViewModels;
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

            AutoViewModel target = new AutoViewModel();
            ICommand targetCommand = target.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(3, target.Autos.Count);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            KundeViewModel target = new KundeViewModel();
            ICommand targetCommand = target.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(4, target.Kunden.Count);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            ReservationViewModel target = new ReservationViewModel();
            ICommand targetCommand = target.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(1, target.Reservationen.Count);
        }
    }
}