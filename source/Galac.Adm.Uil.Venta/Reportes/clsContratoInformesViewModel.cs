using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Rpt.Venta;
namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsContratoInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsContratoInformesViewModel()
            : this(null, null) {
        }

        public clsContratoInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsContratoPorNumeroViewModel());
            AvailableReports.Add(new clsContratoEntreFechasViewModel());
            Title = "Informes de Contratos";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsContratoPorNumeroViewModel) {
                vResult = ConfigReportContratoPorNumero(SelectedReport as clsContratoPorNumeroViewModel);
            }
            if (SelectedReport is clsContratoEntreFechasViewModel) {
                vResult = ConfigReportContratoEntreFechas(SelectedReport as clsContratoEntreFechasViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportContratoPorNumero(clsContratoPorNumeroViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new clsContratoPorNumero(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.NumeroContrato) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        private ILibRpt ConfigReportContratoEntreFechas(clsContratoEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new clsContratoEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FiltrarPorStatus, valViewModel.FiltrarPorFechaFinal, valViewModel.FechaDeInicio, valViewModel.FechaFinal, valViewModel.StatusContrato) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsContratoInformesViewModel
} //End of namespace Galac.Adm.Uil.Venta

