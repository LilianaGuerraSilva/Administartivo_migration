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
    /// Interaction logic for GSCobroDeFacturaRapidaAnticipoView.xaml
    /// </summary>
    public partial class GSCobroDeFacturaRapidaAnticipoView: UserControl {
        #region Constructores

        public GSCobroDeFacturaRapidaAnticipoView() {
            InitializeComponent();
            Loaded += new RoutedEventHandler(LibFKRetrivalView_Loaded);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(GSCobroDeFacturaRapidaAnticipoView_DataContextChanged);
        }
        #endregion //Constructores
        private void LibFKRetrivalView_Loaded(object sender, RoutedEventArgs e) {
            try {
                FocusManager.SetFocusedElement(this, dgDetailAnticipo);
                dgDetailAnticipo.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private void GSDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        void GSCobroDeFacturaRapidaAnticipoView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            try {
                Galac.Adm.Uil.Venta.ViewModel.CobroDeFacturaRapidaAnticipoViewModel vViewModel = DataContext as Galac.Adm.Uil.Venta.ViewModel.CobroDeFacturaRapidaAnticipoViewModel;
                if (vViewModel != null) {
                    vViewModel.IrACobroAnticipo += new EventHandler(vViewModel_IrACobroAnticipo);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        void vViewModel_IrACobroAnticipo(object sender, EventArgs e) {
            try {
                lblMontoTotalACobrar.Focus();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

    } //End of class GSCobroDeFacturaRapidaAnticipoView.xaml

} //End of namespace Galac.Adm.Uil.Venta

