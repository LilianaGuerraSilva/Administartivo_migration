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
using Galac.Adm.Uil.GestionCompras.ViewModel;

namespace Galac.Adm.Uil.GestionCompras.Views {
    /// <summary>
    /// Interaction logic for GSCompraDetalleArticuloInventarioView.xaml
    /// </summary>
    public partial class GSCompraDetalleArticuloInventarioView : UserControl {
        #region Constructores

        public GSCompraDetalleArticuloInventarioView() {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(GSCompraDetalleArticuloInventarioView_DataContextChanged);
            txtCantidad.GotKeyboardFocus += txtCantidad_GotKeyboardFocus;
            txtPrecioUnitario.GotKeyboardFocus += txtPrecioUnitario_GotKeyboardFocus;
        }


        void GSCompraDetalleArticuloInventarioView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            try {
                Galac.Adm.Uil.GestionCompras.ViewModel.CompraDetalleArticuloInventarioViewModel vViewModel = DataContext as Galac.Adm.Uil.GestionCompras.ViewModel.CompraDetalleArticuloInventarioViewModel;
                if (vViewModel != null) {
                    vViewModel.Master.MoveFocusArticuloInventarioEvent += new EventHandler(vViewModel_MoveFocusArticuloInventario);                    
                    if (!LibGalac.Aos.Base.LibString.IsNullOrEmpty(vViewModel.CodigoArticulo)) {
                        txtCantidad.IsEnabled = vViewModel.IsEnabledCantidad;
                    }
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        void vViewModel_MoveFocusArticuloInventario(object sender, EventArgs e) {
            try {
                txtCodigoArticulo.Focus();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        #endregion //Constructores      
        private void txtCantidad_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
            CompraDetalleArticuloInventarioViewModel vViewModel = DataContext as CompraDetalleArticuloInventarioViewModel;
            if (vViewModel.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerial ||
                vViewModel.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaSerialRollo ||
                vViewModel.TipoArticuloInv == Saw.Ccl.Inventario.eTipoArticuloInv.UsaTallaColorySerial) {
                FocusManager.SetFocusedElement(this, txtPrecioUnitario);
            }
        }

        private void txtPrecioUnitario_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) {
            CompraDetalleArticuloInventarioViewModel vViewModel = DataContext as CompraDetalleArticuloInventarioViewModel;
            txtCantidad.IsEnabled = vViewModel.IsEnabledCantidad;
        }

    } //End of class GSCompraDetalleArticuloInventarioView.xaml

} //End of namespace Galac.Adm.Uil.GestionCompras

