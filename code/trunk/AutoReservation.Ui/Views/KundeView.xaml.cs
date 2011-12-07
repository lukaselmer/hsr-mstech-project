﻿#region

using System.Windows.Controls;
using AutoReservation.Ui.ViewModels;

#endregion

namespace AutoReservation.Ui.Views
{
    /// <summary>
    /// Interaction logic for KundeView.xaml
    /// </summary>
    public partial class KundeView : UserControl
    {
        public KundeView()
        {
            InitializeComponent();
            DataContext = new KundeViewModel();
        }
    }
}