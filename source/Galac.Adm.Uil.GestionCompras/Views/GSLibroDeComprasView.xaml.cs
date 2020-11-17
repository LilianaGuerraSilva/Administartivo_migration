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

namespace Galac.Adm.Uil.GestionCompras.Views {
    /// <summary>
    /// Interaction logic for GSLibroDeComprasView.xaml
    /// </summary>
    public partial class GSLibroDeComprasView: UserControl {
        #region Constructores

        public GSLibroDeComprasView() {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(GSMesPeriodoView_Loaded);
        }

        void GSMesPeriodoView_Loaded(object sender, RoutedEventArgs e) {
            try {
                txtMes.Focus();
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }
        #endregion //Constructores


    } //End of class GSLibroDeComprasView.xaml

} //End of namespace Galac.Adm.Uil.Compras

