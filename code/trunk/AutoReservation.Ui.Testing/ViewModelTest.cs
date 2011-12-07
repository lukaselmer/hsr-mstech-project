#region

using AutoReservation.Testing;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace AutoReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void AutosLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var target = new AutoViewModel();
            var targetCommand = target.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(3, target.Autos.Count);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var target = new KundeViewModel();
            var targetCommand = target.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(4, target.Kunden.Count);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();

            var target = new ReservationViewModel();
            var targetCommand = target.LoadCommand;

            Assert.IsTrue(targetCommand.CanExecute(null));

            targetCommand.Execute(null);

            Assert.AreEqual(1, target.Reservationen.Count);
        }
    }
}