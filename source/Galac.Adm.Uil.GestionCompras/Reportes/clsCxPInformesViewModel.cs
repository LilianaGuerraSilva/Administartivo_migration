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
            AvailableReports.Add(new clsCuentasPorPagarEntreFechasViewModel());
            Title = "Informes de Cx P";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsCuentasPorPagarEntreFechasViewModel) {
                vResult = ConfigReportCuentasPorPagarEntreFechas(SelectedReport as clsCuentasPorPagarEntreFechasViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportCuentasPorPagarEntreFechas(clsCuentasPorPagarEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionCompras.clsCuentasPorPagarEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCxPInformesViewModel

} //End of namespace Galac..Uil.ComponenteNoEspecificado

