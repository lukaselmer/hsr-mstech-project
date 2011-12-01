using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Threading;
//using AutoReservation.Common.Interfaces;
using AutoReservation.Service.Wcf;
using AutoReservation.Ui.Factory;

//using AutoReservation.Ui.Factory;

namespace AutoReservation.Ui.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        protected readonly IAutoReservationService Service;

        private readonly Dispatcher dispatcher;

        private PropertyChangingEventHandler propertyChangingEvent;
        private PropertyChangedEventHandler propertyChangedEvent;

        protected ViewModelBase()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            Service = Creator.GetCreatorInstance().CreateBusinessLayerInstance();
            Load();
        }

        protected ViewModelBase(IAutoReservationService svc)
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            Service = svc;
            Load();
        }

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { propertyChangingEvent += value; }
            remove { propertyChangingEvent -= value; }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChangedEvent += value; }
            remove { propertyChangedEvent -= value; }
        }

        public Dispatcher Dispatcher { get { return dispatcher; } }

        protected void SendPropertyChanged<T>(Expression<Func<T>> expression)
        {
            var propertyName = ExtractPropertyName(expression);
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void SendPropertyChanging<T>(Expression<Func<T>> expression)
        {
            var propertyName = ExtractPropertyName(expression);
            if (propertyChangingEvent != null)
            {
                propertyChangingEvent(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        protected abstract void Load();

        private string errorText;
        public string ErrorText
        {
            get
            {
                return errorText;
            }
            set
            {
                if (errorText != value)
                {
                    SendPropertyChanging(() => ErrorText);
                    errorText = value;
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

        #endregion

    }
}
