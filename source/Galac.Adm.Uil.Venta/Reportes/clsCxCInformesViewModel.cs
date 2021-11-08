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
            Title = "Informes de CxC";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsCxCPendientesEntreFechasViewModel) {
                vResult = ConfigReportCxCPendientesEntreFechas(SelectedReport as clsCxCPendientesEntreFechasViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportCxCPendientesEntreFechas(clsCxCPendientesEntreFechasViewModel valViewModel) {
            Saw.Lib.clsUtilRpt vRpxUtil = new Saw.Lib.clsUtilRpt();
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Venta.clsCxCPendientesEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.UsaContacto) {
                        Worker = Manager
                    };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCxCInformesViewModel

} //End of namespace Galac.Adm.Uil.Venta

