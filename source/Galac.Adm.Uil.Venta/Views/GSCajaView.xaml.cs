using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.Base;
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
    /// Interaction logic for GSCajaView.xaml
    /// </summary>
    public partial class GSCajaView:UserControl {
        #region Constructores

        public GSCajaView() {
            InitializeComponent();
            tabMaquinaFiscal.GotFocus += new RoutedEventHandler(tabMaquinaFiscal_RoutedEventHandler);
            tabCajaRegistradora.GotFocus += new RoutedEventHandler(tabCajaRegistradora_RoutedEventHandler);
            cmbFamiliaDeMaquinaFiscal.SelectionChanged += new SelectionChangedEventHandler(cmbFamiliaDeMaquinaFiscal_SelectionChanged);
            cmbModeloDeMaquinaFiscal.SelectionChanged += new SelectionChangedEventHandler(cmbModeloDeMaquinaFiscal_SelectionChangedEventHandler);                        
        }

        private void cmbFamiliaDeMaquinaFiscal_SelectionChanged(object sender,SelectionChangedEventArgs e) {
            var ViewModel = ((CajaViewModel)DataContext);            
            Galac.Adm.Uil.Venta.ViewModel.CajaViewModel vViewModel = DataContext as Galac.Adm.Uil.Venta.ViewModel.CajaViewModel;
            ViewModel.LlenarEnumerativosImpresoraFiscal();
            ViewModel.MoverFocoSiCambiaTab();
            if(LibString.IsNullOrEmpty(cmbModeloDeMaquinaFiscal.Text) && (vViewModel.Action == eAccionSR.Insertar || vViewModel.Action == eAccionSR.Modificar)) {
                vViewModel.ModeloDeMaquinaFiscal = vViewModel.ListarMaquinaFiscal[0];
            }
        }

        private void cmbModeloDeMaquinaFiscal_SelectionChangedEventHandler(object sender,SelectionChangedEventArgs e) {
            var vViewModel = ((CajaViewModel)DataContext);
            if(LibString.IsNullOrEmpty(cmbModeloDeMaquinaFiscal.Text) && (vViewModel.Action == eAccionSR.Insertar|| vViewModel.Action == eAccionSR.Modificar)) {
                vViewModel.ModeloDeMaquinaFiscal = vViewModel.ListarMaquinaFiscal[0];
            }
        }

        private void tabMaquinaFiscal_RoutedEventHandler(object sender,RoutedEventArgs e) {
            var vViewModel = ((CajaViewModel)DataContext);
            if(vViewModel.UsaMaquinaFiscal) {
                vViewModel.EnableParaMaquinaFiscal = true;
                vViewModel.DiagnosticarCommand.RaiseCanExecuteChanged();
                vViewModel.CancelarDocumentoCommand.RaiseCanExecuteChanged();
                vViewModel.ObtenerSerialCommand.RaiseCanExecuteChanged();
                vViewModel.MoverFocoSiCambiaTab();
            }                       
        }

        private void tabCajaRegistradora_RoutedEventHandler(object sender,RoutedEventArgs e) {
            var vViewModel = ((CajaViewModel)DataContext);           
            if(vViewModel.UsaMaquinaFiscal) {
                vViewModel.EnableParaMaquinaFiscal = false;
                vViewModel.DiagnosticarCommand.RaiseCanExecuteChanged();
                vViewModel.CancelarDocumentoCommand.RaiseCanExecuteChanged();
                vViewModel.ObtenerSerialCommand.RaiseCanExecuteChanged();
                vViewModel.MoverFocoSiCambiaTab();
            }
        }
        #endregion //Constructores
    } //End of class GSCajaView.xaml

} //End of namespace Galac.Adm.Uil.Venta

