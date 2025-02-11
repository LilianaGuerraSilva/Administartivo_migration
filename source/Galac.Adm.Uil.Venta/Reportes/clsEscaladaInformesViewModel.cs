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

    public class clsEscaladaInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsEscaladaInformesViewModel()
            : this(null, null) {
        }

        public clsEscaladaInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            //AvailableReports.Add(new clsFacturacionEntreFechasVerificacionViewModel());
            AvailableReports.Add(new clsAuditoriaConfiguracionDeCajaViewModel());
            Title = "Informes de Auditoría";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            //if (SelectedReport is clsFacturacionEntreFechasVerificacionViewModel) {
            //    vResult = ConfigReportFacturacionEntreFechasVerificacion(SelectedReport as clsFacturacionEntreFechasVerificacionViewModel);
            //}
            if (SelectedReport is clsAuditoriaConfiguracionDeCajaViewModel) {
                vResult = ConfigReportclsAuditoriaConfiguracionDeCaja(SelectedReport as clsAuditoriaConfiguracionDeCajaViewModel);
            }
            return vResult;
        }

        //private ILibRpt ConfigReportFacturacionEntreFechasVerificacion(clsFacturacionEntreFechasVerificacionViewModel valViewModel) {
        //    ILibRpt vResult = null;
        //    if (valViewModel != null) {
        //        vResult = new Rpt.Venta.clsFacturacionEntreFechasVerificado(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDeInicio, valViewModel.FechaFinal) {
        //            Worker = Manager
        //            };
        //    }
        //    return vResult;
        //}

        private ILibRpt ConfigReportclsAuditoriaConfiguracionDeCaja(clsAuditoriaConfiguracionDeCajaViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.Venta.clsAuditoriaConfiguracionDeCaja(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaDesde, valViewModel.FechaHasta) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsEscaladaInformesViewModel

} //End of namespace Galac.Dbo.Uil.Venta

