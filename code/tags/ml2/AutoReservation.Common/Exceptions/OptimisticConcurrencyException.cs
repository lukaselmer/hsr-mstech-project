#region

using System.Runtime.Serialization;
using AutoReservation.Common.DataTransferObjects;

#endregion

namespace AutoReservation.Common.Exceptions
{
    [DataContract]
    public class OptimisticConcurrencyException<T> where T : DtoBase
    {
        [DataMember]
        public T Entity { get; set; }
    }
}