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
            var model = new AutoViewModel();
            var command = model.LoadCommand;

            Assert.IsTrue(command.CanExecute(null));

            command.Execute(null);

            Assert.AreEqual(3, model.Autos.Count);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var model = new KundeViewModel();
            var command = model.LoadCommand;

            Assert.IsTrue(command.CanExecute(null));
            command.Execute(null);
            Assert.AreEqual(4, model.Kunden.Count);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            TestEnvironmentHelper.InitializeTestData();
            var model = new ReservationViewModel();
            var command = model.LoadCommand;

            Assert.IsTrue(command.CanExecute(null));

            command.Execute(null);

            Assert.AreEqual(1, model.Reservationen.Count);
        }
    }
}