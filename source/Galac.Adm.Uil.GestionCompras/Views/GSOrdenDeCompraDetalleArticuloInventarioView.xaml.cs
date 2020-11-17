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
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.WpfControls;

namespace Galac.Adm.Uil.GestionCompras.Views {
    /// <summary>
    /// Interaction logic for GSOrdenDeCompraDetalleArticuloInventarioView.xaml
    /// </summary>
    public partial class GSOrdenDeCompraDetalleArticuloInventarioView: UserControl {
        #region Constructores

        public GSOrdenDeCompraDetalleArticuloInventarioView() {
            InitializeComponent();
        }

        #endregion //Constructores

        private void txtDescripcionArticulo_KeyUp(object sender, KeyEventArgs e) {
            Galac.Adm.Uil.GestionCompras.ViewModel.OrdenDeCompraDetalleArticuloInventarioViewModel vViewModel = DataContext as Galac.Adm.Uil.GestionCompras.ViewModel.OrdenDeCompraDetalleArticuloInventarioViewModel;            
            vViewModel.SeModificoDescripcion(this.txtDescripcionArticulo.Text);
        }

        private void txtCodigoArticulo_KeyUp(object sender, KeyEventArgs e) {
            Galac.Adm.Uil.GestionCompras.ViewModel.OrdenDeCompraDetalleArticuloInventarioViewModel vViewModel = DataContext as Galac.Adm.Uil.GestionCompras.ViewModel.OrdenDeCompraDetalleArticuloInventarioViewModel;
            var vTxtCriteria = LibXamlHelper.FindVisualChild<GSTextBoxWpf>(txtCodigoArticulo, "PART_SearchCriteria");
            vViewModel.SeModificoCodigoArticulo(vTxtCriteria.Text);
        }
    } //End of class GSOrdenDeCompraDetalleArticuloInventarioView.xaml

} //End of namespace Galac.Adm.Uil.GestionCompras

