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

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCxCInformesViewModel : LibReportsViewModel {
        #region Constructores
        public clsCxCInformesViewModel()
            : this(null, null) {
        }

        public clsCxCInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsCxCPendientesEntreFechasViewModel());
            AvailableReports.Add(new clsCxCPorClienteViewModel());
            Title = "Informes de CxC";
        }
        #endregion //Constructores

        #region Metodos Generados
        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsCxCPendientesEntreFechasViewModel) {
                vResult = ConfigReportCxCPendientesEntreFechas(SelectedReport as clsCxCPendientesEntreFechasViewModel);
            } else if (SelectedReport is clsCxCPorClienteViewModel) {
                vResult = ConfigReportCxCPorCliente(SelectedReport as clsCxCPorClienteViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportCxCPendientesEntreFechas(clsCxCPendientesEntreFechasViewModel valViewModel) {
            Saw.Lib.clsUtilRpt vRpxUtil = new Saw.Lib.clsUtilRpt();
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Venta.clsCxCPendientesEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.MostrarContacto, valViewModel.MonedaDelInforme, valViewModel.TipoTasaDeCambio) {
                        Worker = Manager
                    };
            }
            return vResult;
        }

        private ILibRpt ConfigReportCxCPorCliente(clsCxCPorClienteViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Venta.clsCxCPorCliente(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.CodigoDelCliente, valViewModel.ZonaCobranza, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.MostrarContacto, valViewModel.ClientesOrdenadosPor, valViewModel.MonedaDelInforme, valViewModel.TipoTasaDeCambio) {
                        Worker = Manager
                    };
            }
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class clsCxCInformesViewModel

} //End of namespace Galac.Adm.Uil.Venta

