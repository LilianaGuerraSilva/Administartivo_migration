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
using Galac.Adm.Uil.Venta.ViewModel;

namespace Galac.Adm.Uil.Venta.Views {
    /// <summary>
    /// Interaction logic for GSCobroDeFacturaRapidaEfectivoView.xaml
    /// </summary>
    public partial class GSCobroDeFacturaRapidaEfectivoView: UserControl {
        #region Constructores

        public GSCobroDeFacturaRapidaEfectivoView() {
            InitializeComponent();
            txtMontoEfectivo.PreviewLostKeyboardFocus += new KeyboardFocusChangedEventHandler (txtMontoEfectivo_PreviewLostKeyboardFocus );
            this.Loaded += new RoutedEventHandler(GSCobroDeFacturaRapidaEfectivoView_Loaded);
        }

        

        private void txtMontoEfectivo_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs  e) {
            try {
                CobroDeFacturaRapidaEfectivoViewModel vViewModel = DataContext as CobroDeFacturaRapidaEfectivoViewModel;
                vViewModel.ActualizarTotales();
                e.Handled = true;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        void GSCobroDeFacturaRapidaEfectivoView_Loaded(object sender, RoutedEventArgs e) {
            try {
                txtMontoEfectivo.Focus();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        #endregion //Constructores
    } //End of class GSCobroDeFacturaRapidaEfectivoView.xaml

} //End of namespace Galac.Adm.Uil.Venta

