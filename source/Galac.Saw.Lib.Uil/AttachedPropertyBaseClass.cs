using System.Windows;
using System.Windows.Controls;

namespace Galac.Saw.Lib.Uil {
    /// <summary>
    /// Clase abstracta para facilitar la implementación de Attached Properties
    /// </summary>
    /// <typeparam name="Property">El tipo de la nueva propiedad que se quiere agregar.</typeparam>
    /// <typeparam name="Owner">La clase hija que se está creando.</typeparam>
    public abstract class AttachedPropertyBaseClass<Property,Owner> 
        where Owner : AttachedPropertyBaseClass<Property, Owner>, new() {

        /// <summary>
        /// Instancia de la clase hija.
        /// </summary>
        public static Owner Instance = new Owner();

        /// <summary>
        /// Instancia de la nueva propiedad que se está agregando
        /// </summary>
        public static DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), 
                                                            typeof(AttachedPropertyBaseClass<Property, Owner>), 
                                                            new PropertyMetadata(ValuePropertyChanged));

        #region Setter and Getters
        /// <summary>
        /// Permite obtener el valor de la propiedad para el objeto <paramref name="target"/>
        /// </summary>
        /// <param name="target">El Control al que se le está agregando la nueva propiedad</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject target){
            return (Property)target.GetValue(ValueProperty);
        }

        /// <summary>
        /// Permite Asignarle el valor <paramref name="value"/> de la propiedad para el objeto <paramref name="target"/>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetValue(DependencyObject target, Property value){
            target.SetValue(ValueProperty, value);
        }
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Método a sobreescribir, este metodo se va a llamar cuando haya un cambio en la propiedad.
        /// Es aquí donde se debe implementar la lógica.
        /// </summary>
        /// <param name="d"> El control al cual se le está agregando la nueva propiedad.</param>
        /// <param name="e"> Argumentos de cambio de propiedad.</param>
        public virtual void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
        #endregion

        /// <summary>
        /// Evento que se dispara cada vez que hay un cambio en el valor en la propiedad
        /// </summary>
        /// <param name="target">El control al cual se le está agregando la nueva propiedad.</param>
        /// <param name="e"> Argumentos de cambio de propiedad.</param>
        private static void ValuePropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) {
            Instance.ValueChanged(target, e);
        }
    }
}
