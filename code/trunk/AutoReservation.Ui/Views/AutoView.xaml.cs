#region

using System.Windows.Controls;
using AutoReservation.Ui.ViewModels;

#endregion

namespace AutoReservation.Ui.Views
{
    /// <summary>
    /// Interaction logic for AutoView.xaml
    /// </summary>
    public partial class AutoView
    {
        public AutoView()
        {
            InitializeComponent();
            DataContext = new AutoViewModel();
        }
    }
}