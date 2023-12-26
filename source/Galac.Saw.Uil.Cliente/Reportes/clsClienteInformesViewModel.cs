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

namespace Galac.Saw.Uil.Cliente.Reportes {

    public class clsClienteInformesViewModel : LibReportsViewModel {
        #region Constructores
        public clsClienteInformesViewModel()
            : this(null, null) {
        }

        public clsClienteInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsHistoricoDeClienteViewModel());
            Title = "Informes de Cliente";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsHistoricoDeClienteViewModel) {
                vResult = ConfigReportHistoricoDeCliente(SelectedReport as clsHistoricoDeClienteViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportHistoricoDeCliente(clsHistoricoDeClienteViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Saw.Rpt.Cliente.clsHistoricoDeCliente(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.CodigoCliente, valViewModel.MonedaDelInforme, valViewModel.Moneda, valViewModel.TasaDeCambio,valViewModel.SaltoDePaginaPorCliente) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsClienteInformesViewModel

} //End of namespace Galac.Saw.Uil.Cliente

