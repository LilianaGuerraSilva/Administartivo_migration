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
            Title = "Informes de Lote de Inventario";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsArticulosPorVencerViewModel) {
                vResult = ConfigReportArticulosPorVencer(SelectedReport as clsArticulosPorVencerViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportArticulosPorVencer(clsArticulosPorVencerViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Saw.Rpt.Inventario.clsArticulosPorVencer(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.LineaDeProducto, valViewModel.CodigoArticulo, valViewModel.DiasParaVencerse) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsLoteDeInventarioInformesViewModel

} //End of namespace Galac.Saw.Uil.Inventario

