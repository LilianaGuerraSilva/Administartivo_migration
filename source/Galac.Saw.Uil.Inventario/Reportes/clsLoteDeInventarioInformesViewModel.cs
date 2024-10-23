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

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsLoteDeInventarioInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsLoteDeInventarioInformesViewModel()
            : this(null, null) {
        }

        public clsLoteDeInventarioInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsArticulosPorVencerViewModel());
            AvailableReports.Add(new clsLoteDeInventarioVencidosViewModel());
		    AvailableReports.Add(new clsMovimientoDeLoteInventarioViewModel());
            Title = "Informes de Lote de Inventario";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsArticulosPorVencerViewModel) {
                vResult = ConfigReportArticulosPorVencer(SelectedReport as clsArticulosPorVencerViewModel);
                
            } else if(SelectedReport is clsLoteDeInventarioVencidosViewModel) {
                vResult = ConfigReportArticulosVencidos(SelectedReport as clsLoteDeInventarioVencidosViewModel);
            } else if (SelectedReport is clsMovimientoDeLoteInventarioViewModel) {
                vResult = ConfigReportMovimientoDeLoteInventario(SelectedReport as clsMovimientoDeLoteInventarioViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportArticulosPorVencer(clsArticulosPorVencerViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Inventario.clsArticulosPorVencer(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.LineaDeProducto, valViewModel.CodigoArticulo, valViewModel.DiasParaVencerse, valViewModel.CantidadAImprimir, valViewModel.OrdenarFecha) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportArticulosVencidos(clsLoteDeInventarioVencidosViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Inventario.clsLoteDeInventarioVencidos(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.LineaDeProducto, valViewModel.CodigoArticulo, valViewModel.CantidadAImprimir, valViewModel.OrdenarFecha) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportMovimientoDeLoteInventario(clsMovimientoDeLoteInventarioViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Saw.Rpt.Inventario.clsMovimientoDeLoteInventario(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.CodigoArticulo, valViewModel.CodigoLote, valViewModel.FechaInicial, valViewModel.FechaFinal) {
                    Worker = Manager
                };
            }
            return vResult;
        }				
        #endregion //Metodos Generados


    } //End of class clsLoteDeInventarioInformesViewModel

} //End of namespace Galac.Saw.Uil.Inventario

