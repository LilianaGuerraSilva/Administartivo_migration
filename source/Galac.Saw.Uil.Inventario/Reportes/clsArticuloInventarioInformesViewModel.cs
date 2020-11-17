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
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsArticuloInventarioInformesViewModel:LibReportsViewModel {
        #region Constructores

        public clsArticuloInventarioInformesViewModel()
            : this(null,null) {
        }

        public clsArticuloInventarioInformesViewModel(LibXmlMemInfo initAppMemInfo,LibXmlMFC initMfc) {
            int vIdInforme = 0;
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            vIdInforme = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("RecordName","IdReporte");
            if(vIdInforme == 1) {
                // reservado
                AvailableReports.Add(new clsListadoDePreciosViewModel());
            } else {
                // el Resto de los reportes va acá
                if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","UsaBalanza")) {
                    AvailableReports.Add(new clsListadoDeArticulosBalanzaViewModel());
                }
                AvailableReports.Add(new clsValoracionDeInventarioViewModel());
            }
            Title = "Informes de Artículo Inventario";
        }

        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if(SelectedReport is clsListadoDePreciosViewModel) {
                vResult = ConfigReportListadoDePrecios(SelectedReport as clsListadoDePreciosViewModel);
            } else if(SelectedReport is clsListadoDeArticulosBalanzaViewModel) {
                vResult = ConfigReportListarArticulosBalanza(SelectedReport as clsListadoDeArticulosBalanzaViewModel);
            } else if(SelectedReport is clsValoracionDeInventarioViewModel) {
                vResult = ConfigReportValoracionDeInventario(SelectedReport as clsValoracionDeInventarioViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportListadoDePrecios(clsListadoDePreciosViewModel valViewModel) {
            ILibRpt vResult = null;
            if(valViewModel != null) {
                vResult = new Galac.Saw.Rpt.Inventario.clsListadoDePrecios(PrintingDevice,ExportFileFormat,AppMemoryInfo,Mfc,valViewModel.NombreLineaDeProducto) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportListarArticulosBalanza(clsListadoDeArticulosBalanzaViewModel valViewModel) {
            ILibRpt vResult = null;
            if(valViewModel != null) {
                vResult = new Galac.Saw.Rpt.Inventario.clsListdoDeArticulosBalanza(PrintingDevice,ExportFileFormat,AppMemoryInfo,Mfc,valViewModel.NombreLineaDeProducto,valViewModel.FiltrarPorLineaDeProducto) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportValoracionDeInventario(clsValoracionDeInventarioViewModel valViewModel) {
            Galac.Saw.Lib.clsUtilRpt vRpxUtil = new Galac.Saw.Lib.clsUtilRpt();
            ILibRpt vResult = null;           
            string vRpxName = valViewModel.TipoDeMonedaDelReporte == Lib.eMonedaPresentacionDeReporte.EnMonedaLocal ? "rpxValoracionDeInventario" : valViewModel.TipoDeMonedaDelReporte == Lib.eMonedaPresentacionDeReporte.EnDivisa ? "rpxValoracionDeInventarioME" : "rpxValoracionDeInventarioMultimoneda";
            if(vRpxUtil.EsFormatoRpxValidoParaAOS(vRpxName)) {
                if(valViewModel != null) {
                    vResult = new Galac.Saw.Rpt.Inventario.clsValoracionDeInventario(PrintingDevice,ExportFileFormat,AppMemoryInfo,Mfc,valViewModel.CodigoDesde,valViewModel.CodigoHasta,valViewModel.LineaDeProducto,valViewModel.TasaDeCambio,valViewModel.TipoDeMonedaDelReporte,valViewModel.UsaPrecioConIvaAsBool,valViewModel.Moneda,vRpxName) {
                        Worker = Manager
                    };
                }
            } else {
                LibMessages.MessageBox.Alert(this,"El formato del rpx no es compatible, Por favor verifique e intente de nuevo","");
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsArticuloInventarioInformesViewModel
} //End of namespace Galac.Saw.Uil.Inventario

