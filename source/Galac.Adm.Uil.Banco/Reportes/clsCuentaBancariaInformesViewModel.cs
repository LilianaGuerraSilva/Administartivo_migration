using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.ARRpt.Reports;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
namespace Galac.Adm.Uil.Banco.Reportes {

    public class clsCuentaBancariaInformesViewModel : LibReportsViewModel {
        #region Constructores
        public clsCuentaBancariaInformesViewModel()
            : this(null, null) {
        }

        public clsCuentaBancariaInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsSaldosBancariosViewModel());
            Title = "Informes de Cuenta Bancaria";
        }
        #endregion //Constructores

        #region Metodos Generados
        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsSaldosBancariosViewModel) {
                vResult = ConfigReportSaldosBancarios(SelectedReport as clsSaldosBancariosViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportSaldosBancarios(clsSaldosBancariosViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Banco.clsSaldosBancarios(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.SoloCuentasActivas) {
                        Worker = Manager
                    };
            }
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class clsCuentaBancariaInformesViewModel

} //End of namespace Galac.Adm.Uil.Banco

