using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;

namespace AutoReservation.Ui.Factory
{
    class LocalDataAccessCreator : Creator
    {
        public override IAutoReservationService CreateBusinessLayerInstance()
        {
            return new AutoReservationService();
        }
    }
}
