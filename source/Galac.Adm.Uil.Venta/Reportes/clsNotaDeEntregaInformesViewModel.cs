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

    public class clsNotaDeEntregaInformesViewModel : LibReportsViewModel {

        #region Constructores

        public clsNotaDeEntregaInformesViewModel()
            : this(null, null) {
        }

        public clsNotaDeEntregaInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsNotaDeEntregaEntreFechasPorClienteViewModel());
            Title = "Informes de Nota De Entrega";
        }

        #endregion //Constructores

        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsNotaDeEntregaEntreFechasPorClienteViewModel) {
                vResult = ConfigReportNotaDeEntregaEntreFechasPorCliente(SelectedReport as clsNotaDeEntregaEntreFechasPorClienteViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportNotaDeEntregaEntreFechasPorCliente(clsNotaDeEntregaEntreFechasPorClienteViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                if (valViewModel.IncluirDetalleNotasDeEntregas) {
                    vResult = new Rpt.Venta.clsNotaDeEntregaEntreFechasPorClienteDetallado(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.IncluirNotasDeEntregasAnuladas, valViewModel.CantidadAImprimir, valViewModel.CodigoCliente, valViewModel.IncluirDetalleNotasDeEntregas) {
                        Worker = Manager,
                    };
                } else {
                    vResult = new Rpt.Venta.clsNotaDeEntregaEntreFechasPorCliente(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.IncluirNotasDeEntregasAnuladas, valViewModel.CantidadAImprimir, valViewModel.CodigoCliente) {
                        Worker = Manager,
                    };
                }
            }
            return vResult;
        }

        #endregion //Metodos Generados


    } //End of class clsNotaDeEntregaInformesViewModel

} //End of namespace Galac..Uil.ComponenteNoEspecificado

