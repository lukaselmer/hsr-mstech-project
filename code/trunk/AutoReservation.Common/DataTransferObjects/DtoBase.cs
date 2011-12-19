using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public abstract class DtoBase : INotifyPropertyChanged, INotifyPropertyChanging, ICloneable
    {
        public abstract string Validate();
        public abstract object Clone();
        public abstract override bool Equals(object obj);

        private PropertyChangingEventHandler _propertyChangingEvent;
        private PropertyChangedEventHandler _propertyChangedEvent;

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { _propertyChangingEvent += value; }
            remove { _propertyChangingEvent -= value; }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChangedEvent += value; }
            remove { _propertyChangedEvent -= value; }
        }

        protected void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            var propertyName = ExtractPropertyName(expression);
            if (_propertyChangedEvent != null)
            {
                _propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void SendPropertyChanging<T>(Expression<Func<T>> expression)
        {
            var propertyName = ExtractPropertyName(expression);
            if (_propertyChangingEvent != null)
            {
                _propertyChangingEvent(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        private string ExtractPropertyName<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Der Ausdruck ist kein Member-Lamda-Ausdruck (MemberExpression).", "expression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("Der Member-Ausdruck greift nicht auf eine Eigenschaft zu.", "expression");
            }

            if (!property.DeclaringType.IsAssignableFrom(this.GetType()))
            {
                throw new ArgumentException("Die referenzierte Eigenschaft gehört nicht zum gewünschten Typ.", "expression");
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod == null)
            {
                throw new ArgumentException("Die referenzierte Eigenschaft hat keine 'get' - Methode.", "expression");
            }

            if (getMethod.IsStatic)
            {
                throw new ArgumentException("Die refrenzierte Eigenschaft ist statisch.", "expression");
            }

            return memberExpression.Member.Name;
        }

    }
}
