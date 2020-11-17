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

namespace Galac.Adm.Uil.Venta.Views {
    /// <summary>
    /// Interaction logic for GSCobroDeFacturaRapidaDepositoTransfView.xaml
    /// </summary>
    public partial class GSCobroDeFacturaRapidaDepositoTransfView: UserControl {
        #region Constructores

        public GSCobroDeFacturaRapidaDepositoTransfView() {
            InitializeComponent();
            Loaded += new RoutedEventHandler(LibFKRetrivalView_Loaded);
        }
        #endregion //Constructores

        private void LibFKRetrivalView_Loaded(object sender, RoutedEventArgs e) {
            try {
                FocusManager.SetFocusedElement(this, dgDetailDepositoTransf);
                dgDetailDepositoTransf.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void GSDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        } 
    } //End of class GSCobroDeFacturaRapidaDepositoTransfView.xaml

} //End of namespace Galac.Adm.Uil.Venta

