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
    /// Interaction logic for GSOrdenDeProduccionDetalleArticuloView.xaml
    /// </summary>
    public partial class GSOrdenDeProduccionDetalleArticuloView: UserControl {
        #region Constructores

        public GSOrdenDeProduccionDetalleArticuloView() {
            InitializeComponent();
            this.Loaded += GSListaDeMaterialesDetalleArticuloView_Loaded;
        }

        private void GSListaDeMaterialesDetalleArticuloView_Loaded(object sender, RoutedEventArgs e) {           
            FocusManager.SetFocusedElement(this, lneCodigoArticulo);
            txtCantidadSolicitada.GotFocus += TxtCantidad_GotFocus;
            txtCantidadSolicitada.MouseDoubleClick += TxtCantidad_GotFocus;
        }

        private void TxtCantidad_GotFocus(object sender, RoutedEventArgs e) {
            FocusManager.SetFocusedElement(this, txtCantidadSolicitada);
            txtCantidadSolicitada.SelectAll();
        }
        #endregion //Constructores


    } //End of class GSOrdenDeProduccionDetalleSalidaView.xaml

} //End of namespace Galac.Adm.Uil. GestionProduccion

