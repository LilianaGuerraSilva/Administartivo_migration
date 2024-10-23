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

    public class clsNotaDeEntradaSalidaInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsNotaDeEntradaSalidaInformesViewModel()
            : this(null, null) {
        }

        public clsNotaDeEntradaSalidaInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            //AvailableReports.Add(new clsNotaDeEntradaSalidaDeInventarioViewModel());
            Title = "Informes de Nota de Entrada/Salida";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            //if (SelectedReport is clsNotaDeEntradaSalidaDeInventarioViewModel) {
            //    vResult = ConfigReportNotaDeEntradaSalidaDeInventario(SelectedReport as clsNotaDeEntradaSalidaDeInventarioViewModel);
            //}
            return vResult;
        }

        internal ILibRpt ConfigReportNotaEntradaSalida(string valNumeroDocumento) {
            ILibRpt vResult = ConfigReportNotaDeEntradaSalidaDeInventario(valNumeroDocumento);
            vResult.RunReport();
            vResult.SendReportToDevice();
            return vResult;
        }

        private ILibRpt ConfigReportNotaDeEntradaSalidaDeInventario(string valNumeroDocumento) {            
            ILibRpt vResult = new Galac.Saw.Rpt.Inventario.clsNotaDeEntradaSalidaDeInventario(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                Worker = Manager, 
                NumeroDocumento = valNumeroDocumento
            };
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntradaSalidaInformesViewModel

} //End of namespace Galac.Saw.Uil.Inventario

