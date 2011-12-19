#region

using System;

#endregion

namespace AutoReservation.BusinessLayer
{
    public class LocalOptimisticConcurrencyException<T> : Exception
    {
        public LocalOptimisticConcurrencyException(string msg) : base(msg) {}
        public T Entity { get; set; }
    }
}