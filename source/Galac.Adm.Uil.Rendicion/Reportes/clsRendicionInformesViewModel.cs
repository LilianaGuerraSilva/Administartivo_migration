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
using Galac.Adm.Rpt.CajaChica;

namespace Galac.Adm.Uil.CajaChica.Reportes {

    public class clsRendicionInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsRendicionInformesViewModel()
            : this(null, null) {
        }

        public clsRendicionInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsReposicionesEntreFechasViewModel());
            Title = "Informes de Reposición de Caja Chica";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsReposicionesEntreFechasViewModel) {
                vResult = ConfigReportReposicionesEntreFechas(SelectedReport as clsReposicionesEntreFechasViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportReposicionesEntreFechas(clsReposicionesEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {                
                vResult = new Galac.Adm.Rpt.CajaChica.clsReposicionesEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.ImprimeUna, valViewModel.CodigoCtaBancariaCajaChica, valViewModel.UnaPaginaPorCajaChica) {
                        Worker = Manager
                    };
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRendicionInformesViewModel

} //End of namespace Galac.Adm.Uil.CajaChica

