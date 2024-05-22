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
            //this.Loaded += GSListaDeMaterialesDetalleArticuloView_Loaded;
        }

        private void GSListaDeMaterialesDetalleArticuloView_Loaded(object sender, RoutedEventArgs e) {           
            FocusManager.SetFocusedElement(this, lneCodigoArticulo);
            //txtPorcentajedeCosto.GotFocus += txtPorcentajedeCosto_GotFocus;
            //txtPorcentajedeCosto.MouseDoubleClick += txtPorcentajedeCosto_GotFocus;
        }

        private void txtPorcentajedeCosto_GotFocus(object sender, RoutedEventArgs e) {
            //FocusManager.SetFocusedElement(this, txtPorcentajedeCosto);
            //txtPorcentajedeCosto.SelectAll();
        }
        #endregion //Constructores


    } //End of class GSOrdenDeProduccionDetalleSalidaView.xaml

} //End of namespace Galac.Adm.Uil. GestionProduccion

