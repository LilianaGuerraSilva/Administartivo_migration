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
using Galac.Adm.Rpt.GestionProduccion;

namespace Galac.Adm.Uil.GestionProduccion.Reportes {

    public class clsListaDeMaterialesInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsListaDeMaterialesInformesViewModel()
            : this(null, null) {
        }

        public clsListaDeMaterialesInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsListaDeMaterialesViewModel());
            Galac.Adm.Ccl.GestionProduccion.IListaDeMaterialesPdn insNombreListaMateriales = new Galac.Adm.Brl.GestionProduccion.clsListaDeMaterialesNav();
            string vNombreAMostrarListaMateriales = insNombreListaMateriales.NombreParaMostrarListaDeMateriales();
            if (LibString.IsNullOrEmpty(vNombreAMostrarListaMateriales)) {
                Title = "Informes de " + vNombreAMostrarListaMateriales;
            } else {
                Title = "Informes de Lista de Materiales";
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsListaDeMaterialesViewModel) {
                vResult = ConfigReportListaDeMateriales(SelectedReport as clsListaDeMaterialesViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportListaDeMateriales(clsListaDeMaterialesViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new clsListaDeMaterialesDeSalida(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.CantidadAImprimir, valViewModel.CantidadAProducir, valViewModel.CodigoListaMateriales, valViewModel.TasaDeCambio, valViewModel.MonedaDelInforme, valViewModel.ListaMonedaDelInforme.ToArray()) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsListaDeMaterialesInformesViewModel

} //End of namespace Galac.Adm.Uil.GestionProduccion

