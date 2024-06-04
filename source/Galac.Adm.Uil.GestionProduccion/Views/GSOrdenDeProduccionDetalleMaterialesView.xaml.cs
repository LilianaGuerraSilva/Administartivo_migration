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
    /// Interaction logic for GSOrdenDeProduccionDetalleMaterialesView.xaml
    /// </summary>
    public partial class GSOrdenDeProduccionDetalleMaterialesView: UserControl {
        #region Constructores

        public GSOrdenDeProduccionDetalleMaterialesView() {
            InitializeComponent();
            this.Loaded += GSOrdenDeProduccionDetalleMaterialesView_Loaded;
        }

        private void GSOrdenDeProduccionDetalleMaterialesView_Loaded(object sender, RoutedEventArgs e) {            
            FocusManager.SetFocusedElement(this, lneCodigoArticulo);            
            txtCantidadReservadaInventario.GotFocus += TxtCantidadConsumida_GotFocus;
            txtCantidadReservadaInventario.MouseDoubleClick += TxtCantidadConsumida_GotFocus;

        }        

        private void TxtCantidadConsumida_GotFocus(object sender, RoutedEventArgs e) {
            FocusManager.SetFocusedElement(this, txtCantidadReservadaInventario);
            txtCantidadReservadaInventario.SelectAll();
        }


        #endregion //Constructores


    } //End of class GSOrdenDeProduccionDetalleMaterialesView.xaml

} //End of namespace Galac.Adm.Uil. GestionProduccion

