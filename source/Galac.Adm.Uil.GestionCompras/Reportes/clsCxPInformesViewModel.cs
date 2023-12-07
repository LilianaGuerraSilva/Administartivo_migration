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
            Title = "Informes de Cx P";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsCxPEntreFechasViewModel) {
                vResult = ConfigReportCuentasPorPagarEntreFechas(SelectedReport as clsCxPEntreFechasViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportCuentasPorPagarEntreFechas(clsCxPEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionCompras.clsCxPEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.StatusCxP, valViewModel.MonedaDelInforme, valViewModel.TasaDeCambio, valViewModel.Moneda, valViewModel.IncluirInfoAdicional, valViewModel.IncluirNroComprobanteContable) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCxPInformesViewModel

} //End of namespace Galac..Uil.ComponenteNoEspecificado

