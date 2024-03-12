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
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;

namespace Galac.Adm.Uil.GestionCompras.Reportes {

    public class clsCxPInformesViewModel : LibReportsViewModel {
        #region Constructores
        public clsCxPInformesViewModel()
            : this(null, null) {
        }

        public clsCxPInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsCxPEntreFechasViewModel());
            AvailableReports.Add(new clsHistoricoDeProveedorViewModel());
            Title = "Informes de Cuentas por Pagar";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsCxPEntreFechasViewModel) {
                vResult = ConfigReportCuentasPorPagarEntreFechas(SelectedReport as clsCxPEntreFechasViewModel);
            } else if (SelectedReport is clsHistoricoDeProveedorViewModel) {
                vResult = ConfigReportHistoricoDeProveedor(SelectedReport as clsHistoricoDeProveedorViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportCuentasPorPagarEntreFechas(clsCxPEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.GestionCompras.clsCxPEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.StatusCxP, valViewModel.MonedaDelInforme, valViewModel.TasaDeCambio, valViewModel.Moneda, valViewModel.IncluirInfoAdicional, valViewModel.IncluirNroComprobanteContable) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportHistoricoDeProveedor(clsHistoricoDeProveedorViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.GestionCompras.clsHistoricoDeProveedor(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.CodigoProveedor, valViewModel.MonedaDelInforme, valViewModel.Moneda, valViewModel.TasaDeCambio, valViewModel.SaltoDePaginaPorProveedor, valViewModel.OrdenarPor) {
                    Worker = Manager,
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCxPInformesViewModel

} //End of namespace Galac..Uil.ComponenteNoEspecificado

