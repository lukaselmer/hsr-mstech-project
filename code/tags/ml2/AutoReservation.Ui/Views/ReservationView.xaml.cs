using System.Windows.Controls;
using AutoReservation.Ui.ViewModels;

namespace AutoReservation.Ui.Views
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : UserControl
    {
        public ReservationView()
        {
            InitializeComponent();
            DataContext = new ReservationViewModel();
        }
    }
}
