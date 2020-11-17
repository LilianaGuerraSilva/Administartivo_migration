using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Galac.Adm.Uil.GestionProduccion.Views {
    /// <summary>
    /// Interaction logic for GSListaDeMaterialesDetalleArticuloView.xaml
    /// </summary>
    public partial class GSListaDeMaterialesDetalleArticuloView : UserControl {

        #region Constructores

        public GSListaDeMaterialesDetalleArticuloView() {
            InitializeComponent();
            this.Loaded += GSListaDeMaterialesDetalleArticuloView_Loaded;
        }

        #endregion //Constructores

        #region Metodos

        private void GSListaDeMaterialesDetalleArticuloView_Loaded(object sender, RoutedEventArgs e) {
            FocusManager.SetFocusedElement(this, txtCantidad);
            FocusManager.SetFocusedElement(this, txtCodigoArticuloInventario);
            txtCantidad.GotFocus += TxtCantidad_GotFocus;
            txtCantidad.MouseDoubleClick += TxtCantidad_GotFocus;
        }

        private void TxtCantidad_GotFocus(object sender, RoutedEventArgs e) {
            FocusManager.SetFocusedElement(this, txtCantidad);
            txtCantidad.SelectAll();
        }

        #endregion

    } //End of class GSListaDeMaterialesDetalleArticuloView.xaml

} //End of namespace Galac.Saw.Uil.Inventario

