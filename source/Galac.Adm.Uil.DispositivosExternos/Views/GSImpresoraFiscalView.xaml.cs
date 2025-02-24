using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
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

namespace Galac.Adm.Uil.DispositivosExternos.Views {
    /// <summary>
    /// Interaction logic for GSEmitirFacturaFiscalView.xaml
    /// </summary>
    public partial class GSImpresoraFiscalView:UserControl {
        #region Constructores

        public GSImpresoraFiscalView() {
            InitializeComponent();
            Loaded +=new RoutedEventHandler(GSImpresoraFiscalView_Loaded);
        }

        private void GSImpresoraFiscalView_Loaded(object sender,RoutedEventArgs e) {
            eTipoDocumentoFiscal vTipoDocumentoFisca = ((ImpresoraFiscalViewModel)DataContext).TipoDocumentoFiscal;
            switch(vTipoDocumentoFisca) {
                case eTipoDocumentoFiscal.FacturaFiscal:              
                case eTipoDocumentoFiscal.NotadeCredito:
                case eTipoDocumentoFiscal.NotadeDebito:
                ((ImpresoraFiscalViewModel)DataContext).ImprimirDocumentoTask();
                break;
                case eTipoDocumentoFiscal.ReporteZ:
                ((ImpresoraFiscalViewModel)DataContext).RealizarReporteZ();
                break;
                case eTipoDocumentoFiscal.ReporteX:                
                ((ImpresoraFiscalViewModel)DataContext).RealizarReporteX();
                break;                
            }            
        }
        #endregion //Constructores
    } //End of class GSEmitirFacturaFiscalView.xaml

} //End of namespace Galac.Adm.Uil.Venta

