using Galac.Adm.Uil.GestionProduccion.ViewModel;
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

namespace Galac.Adm.Uil. GestionProduccion.Views {
    /// <summary>
    /// Interaction logic for OrdenDeProduccionMasterView.xaml
    /// </summary>
    public partial class GSOrdenDeProduccionMasterView: UserControl {
        #region Constructores

        public GSOrdenDeProduccionMasterView() {
            InitializeComponent();
            this.Loaded += GSOrdenDeProduccionDetalleArticuloView_Loaded;
        }

        private void GSOrdenDeProduccionDetalleArticuloView_Loaded(object sender, RoutedEventArgs e) {
            dgDetailDetailOrdenDeProduccionDetalleSalida.SelectedIndex = 0;
            dgDetailDetailOrdenDeProduccionDetalleMateriales.SelectedIndex = 0;
        }
        #endregion //Constructores


    } //End of class OrdenDeProduccionMasterView.xaml

} //End of namespace Galac.Adm.Uil. GestionProduccion

