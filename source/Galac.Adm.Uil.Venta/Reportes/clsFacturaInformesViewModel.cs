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

    public class clsFacturaInformesViewModel : LibReportsViewModel {
        #region Constructores
        public clsFacturaInformesViewModel()
            : this(null, null) {
        }

        public clsFacturaInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsFacturacionPorArticuloViewModel());
            Title = "Informes de Factura";
        }
        #endregion //Constructores

        #region Metodos Generados
        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsFacturacionPorArticuloViewModel) {
                vResult = ConfigReportFacturacionPorArticulo(SelectedReport as clsFacturacionPorArticuloViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportFacturacionPorArticulo(clsFacturacionPorArticuloViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Venta.clsFacturacionPorArticulo(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.CodigoDelArticulo, valViewModel.MonedaDelInforme, valViewModel.TipoTasaDeCambio, valViewModel.IsInformeDetallado) {
                        Worker = Manager
                    };
            }
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class clsFacturaInformesViewModel

} //End of namespace Galac.Adm.Uil.Venta

