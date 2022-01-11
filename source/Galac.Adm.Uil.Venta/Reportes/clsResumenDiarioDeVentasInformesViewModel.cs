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

    public class clsResumenDiarioDeVentasViewModel : LibReportsViewModel {
        #region Constructores
        public clsResumenDiarioDeVentasViewModel()
            : this(null, null) {
        }

        public clsResumenDiarioDeVentasViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsResumenDiarioDeVentasEntreFechasViewModel());
            Title = "Informes de Resumen Diario de Ventas";
        }
        #endregion //Constructores

        #region Metodos Generados
        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsResumenDiarioDeVentasEntreFechasViewModel) {
                vResult = ConfigReportResumenDiarioDeVentasEntreFechas(SelectedReport as clsResumenDiarioDeVentasEntreFechasViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportResumenDiarioDeVentasEntreFechas(clsResumenDiarioDeVentasEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Venta.clsResumenDiarioDeVentasEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.AgruparPorMaquinaFiscal, valViewModel.ConsecutivoMaquinaFiscal) {
                        Worker = Manager
                    };
            }
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class clsFacturaInformesViewModel

} //End of namespace Galac.Adm.Uil.Venta

