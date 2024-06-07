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

namespace Galac.Adm.Uil.GestionProduccion.Reportes {
    public class clsOrdenDeProduccionInformesViewModel : LibReportsViewModel {
        #region Constructores
        public clsOrdenDeProduccionInformesViewModel()
            : this(null, null) {
        }

        public clsOrdenDeProduccionInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsCostoProduccionInventarioEntreFechasViewModel());
            AvailableReports.Add(new clsDetalleDeCostoDeProduccionViewModel());
            AvailableReports.Add(new clsPrecierreOrdenDeProduccionViewModel());
            AvailableReports.Add(new clsProduccionPorEstatusEntreFechaViewModel());
            AvailableReports.Add(new clsRequisicionDeMaterialesViewModel());
            //AvailableReports.Add(new clsCostoMatServUtilizadosEnProduccionInvViewModel());
            Title = "Informes de Orden de Producción";
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override ILibRpt ConfigReport() {
            MoveFocusIfNecessary();
            ILibRpt vResult = null;
            if (SelectedReport is clsPrecierreOrdenDeProduccionViewModel) {
                vResult = ConfigReportPrecierreOrdenDeProduccionRpt(SelectedReport as clsPrecierreOrdenDeProduccionViewModel);
            } else if (SelectedReport is clsRequisicionDeMaterialesViewModel) {
                vResult = ConfigReportRequisicionDeMateriales(SelectedReport as clsRequisicionDeMaterialesViewModel);
            } else if (SelectedReport is clsCostoProduccionInventarioEntreFechasViewModel) {
                vResult = ConfigReportCostoProduccionInventarioEntreFechas(SelectedReport as clsCostoProduccionInventarioEntreFechasViewModel);
            } else if (SelectedReport is clsCostoMatServUtilizadosEnProduccionInvViewModel) {
                vResult = ConfigReportCostoMatServUtilizadosEnProduccionInv(SelectedReport as clsCostoMatServUtilizadosEnProduccionInvViewModel);
            } else if (SelectedReport is clsProduccionPorEstatusEntreFechaViewModel) {
                vResult = ConfigReportProduccionPorEstatusEntreFecha(SelectedReport as clsProduccionPorEstatusEntreFechaViewModel);
            } else if (SelectedReport is clsDetalleDeCostoDeProduccionViewModel) {
                vResult = ConfigReportDetalleDeCostoDeProduccion(SelectedReport as clsDetalleDeCostoDeProduccionViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportPrecierreOrdenDeProduccionRpt(clsPrecierreOrdenDeProduccionViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionProduccion.clsPrecierreOrdenDeProduccionRpt(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.CodigoDeOrden, valViewModel.GeneradoPor, valViewModel.FechaDesde, valViewModel.FechaHasta, valViewModel.SeleccionarOrdenPor) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportRequisicionDeMateriales(clsRequisicionDeMaterialesViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionProduccion.clsRequisicionDeMateriales(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.CodigoDeOrden, valViewModel.GeneradoPor, valViewModel.MostrarSoloExistenciaInsuficiente) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportCostoProduccionInventarioEntreFechas(clsCostoProduccionInventarioEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.GestionProduccion.clsCostoProduccionInventarioEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.CantidadAImprimir, valViewModel.CodigoArticuloInventario, valViewModel.GeneradoPor, valViewModel.CodigoDeOrden, valViewModel.MonedaDelInforme, valViewModel.TasaDeCambio, valViewModel.Moneda) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportCostoMatServUtilizadosEnProduccionInv(clsCostoMatServUtilizadosEnProduccionInvViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Rpt.GestionProduccion.clsCostoMatServUtilizadosEnProduccionInv(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.CodigoDeOrden, valViewModel.GeneradoPor, valViewModel.MonedaDelInforme, valViewModel.TasaDeCambio, valViewModel.Moneda) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportProduccionPorEstatusEntreFecha(clsProduccionPorEstatusEntreFechaViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionProduccion.clsProduccionPorEstatusEntreFecha(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.Estatus, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.GeneradoPor, valViewModel.CodigoDeOrden) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportDetalleDeCostoDeProduccion(clsDetalleDeCostoDeProduccionViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionProduccion.clsDetalleDeCostoDeProduccion(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.ConsecutivoOrden, valViewModel.CodigoDeOrden, valViewModel.SeleccionarOrdenPor, valViewModel.MonedaDelInforme, valViewModel.TasaDeCambio, valViewModel.Moneda) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsOrdenDeProduccionInformesViewModel
} //End of namespace Galac.Adm.Uil. GestionProduccion

