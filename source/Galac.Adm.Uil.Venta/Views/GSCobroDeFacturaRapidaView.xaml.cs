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
    /// Interaction logic for GSCobroDeFacturaRapidaView.xaml
    /// </summary>
    public partial class GSCobroDeFacturaRapidaView: UserControl {
        #region Constructores

        public GSCobroDeFacturaRapidaView() {
            InitializeComponent();
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(GSCobroDeFacturaRapidaView_DataContextChanged);
        }

        void GSCobroDeFacturaRapidaView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            try {
                Galac.Adm.Uil.Venta.ViewModel.CobroDeFacturaRapidaViewModel vViewModel = DataContext as Galac.Adm.Uil.Venta.ViewModel.CobroDeFacturaRapidaViewModel;
                if (vViewModel != null) {
                    vViewModel.IrACobroFactura += new EventHandler(vViewModel_IrACobroFactura);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        void vViewModel_IrACobroFactura(object sender, EventArgs e) {
            try {
                lblMontoTotalACobrar.Focus();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        #endregion //Constructores


    } //End of class GSCobroDeFacturaRapidaView.xaml

} //End of namespace Galac.Adm.Uil.Venta

