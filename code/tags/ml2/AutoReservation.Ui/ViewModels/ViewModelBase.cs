#region

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Threading;
using AutoReservation.Common.Interfaces;
using AutoReservation.Ui.Factory;

#endregion

namespace AutoReservation.Ui.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        protected readonly IAutoReservationService Service;

        private readonly Dispatcher _dispatcher;
        private string _errorText;

        private PropertyChangedEventHandler _propertyChangedEvent;
        private PropertyChangingEventHandler _propertyChangingEvent;

        protected ViewModelBase()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            Service = Creator.GetCreatorInstance().CreateBusinessLayerInstance();
            Load();
        }

        protected ViewModelBase(IAutoReservationService svc)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            Service = svc;
            Load();
        }

        public Dispatcher Dispatcher { get { return _dispatcher; } }

        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                if (_errorText != value)
                {
                    SendPropertyChanging(() => ErrorText);
                    _errorText = value;
                    SendPropertyChanged(() => ErrorText);
                }
            }
        }

        #region Helper Methods

        private string ExtractPropertyName<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Der Ausdruck ist kein Member-Lamda-Ausdruck (MemberExpression).",
                    "expression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("Der Member-Ausdruck greift nicht auf eine Eigenschaft zu.", "expression");
            }

            if (!property.DeclaringType.IsAssignableFrom(GetType()))
            {
                throw new ArgumentException("Die referenzierte Eigenschaft gehört nicht zum gewünschten Typ.",
                    "expression");
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

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged { add { _propertyChangedEvent += value; } remove { _propertyChangedEvent -= value; } }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging { add { _propertyChangingEvent += value; } remove { _propertyChangingEvent -= value; } }

        #endregion

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

        protected abstract void Load();
    }
}