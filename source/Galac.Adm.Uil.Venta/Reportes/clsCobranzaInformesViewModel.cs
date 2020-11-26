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
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Uil;
using Galac.Comun.Brl.TablasGen;
using Galac.Comun.Ccl.TablasGen;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCobranzaInformesViewModel:LibReportsViewModel {
        #region Constructores

        public clsCobranzaInformesViewModel()
            : this(null,null) {
        }

        public clsCobranzaInformesViewModel(LibXmlMemInfo initAppMemInfo,LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsCobranzasEntreFechasViewModel());
            AvailableReports.Add(new clsComisionDeVendedoresViewModel());
            Title = "Informes de Cobranza";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if(SelectedReport is clsCobranzasEntreFechasViewModel) {
                vResult = ConfigReportCobranzasEntreFechas(SelectedReport as clsCobranzasEntreFechasViewModel);
            } else if (SelectedReport is clsComisionDeVendedoresViewModel) {
                vResult = ConfigReportComisionDeVendedores(SelectedReport as clsComisionDeVendedoresViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportCobranzasEntreFechas(clsCobranzasEntreFechasViewModel valViewModel) {
            Galac.Saw.Lib.clsUtilRpt vRpxUtil = new Galac.Saw.Lib.clsUtilRpt();
            ILibRpt vResult = null;
            string vRpxName = valViewModel.AgruparCampos ? "rpxCobranzasEntreFechasAgrupado" : "rpxCobranzasEntreFechas";
            if(vRpxUtil.EsFormatoRpxValidoParaAOS(vRpxName)) {
                if(valViewModel != null) {
                    vResult = new Galac.Adm.Rpt.Venta.clsCobranzasEntreFechas(PrintingDevice,ExportFileFormat,AppMemoryInfo,Mfc,valViewModel.MonedaDelInforme,valViewModel.TipoTasaDeCambio,valViewModel.TasaDeCambio,
                        valViewModel.FechaDesde,valViewModel.FechaHasta,valViewModel.NombreCobrador,valViewModel.NombreCliente,valViewModel.NombreCuentaBancaria,valViewModel.FiltrarPor,valViewModel.AgruparCampos,vRpxName,valViewModel.UsaVentasConIvaDiferidos) {
                        Worker = Manager
                    };
                }
            } else {                
                LibMessages.MessageBox.Alert(this,"El formato del rpx no es compatible, Por favor verifique e intente de nuevo","");
            }
            return vResult;
        }

        private ILibRpt ConfigReportComisionDeVendedores(clsComisionDeVendedoresViewModel valViewModel) {
            ILibRpt vResult = null;
            bool vParametroUsaMonedaExtranjera = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"));
            string vParametroCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            string vCodigoMonedaExtranjera = string.Empty;
            string vSimboloMonedaExtranjera = string.Empty;
            if (vParametroUsaMonedaExtranjera) {
                vCodigoMonedaExtranjera = vParametroCodigoMonedaExtranjera;
                Moneda MonedaExtranjera = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<Moneda>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vCodigoMonedaExtranjera), new clsMonedaNav());
                vSimboloMonedaExtranjera = LibString.IsNullOrEmpty(vCodigoMonedaExtranjera) ? string.Empty : MonedaExtranjera.Simbolo;
            }
            if (valViewModel != null) {
                if (valViewModel.TipoCalculoComision == eTipoDeCalculoComision.PorCobranzas) {
                    switch (valViewModel.TipoDeComision) {
                        case eCalculoParaComisionesSobreCobranzaEnBaseA.Monto: {
                                if (valViewModel.TipoDeInforme == Saw.Lib.eTipoDeInforme.Detallado) {
                                    vResult = new Galac.Adm.Rpt.Venta.clsComisionDeVendedoresPorCobranzaMonto(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.TipoDeInforme,valViewModel.Moneda, valViewModel.TasaDeCambio, valViewModel.IncluirComisionEnMonedaExt, valViewModel.TasaDeCambioComisionEnMonedaExt, valViewModel.CantidadAImprimir, valViewModel.CodigoVendedor, vSimboloMonedaExtranjera) {
                                        Worker = Manager,
                                    };
                                } else {
                                    vResult = new Galac.Adm.Rpt.Venta.clsComisionDeVendedoresPorCobranzaMontoResumido(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.TipoDeInforme,valViewModel.Moneda, valViewModel.TasaDeCambio, valViewModel.IncluirComisionEnMonedaExt, valViewModel.TasaDeCambioComisionEnMonedaExt, valViewModel.CantidadAImprimir, valViewModel.CodigoVendedor, vSimboloMonedaExtranjera) {
                                        Worker = Manager,
                                    };
                                }
                            }
                            break;
                        //Aqui se deben agregar los enumerativos para las variantes de este reportes
                    }
                } else {
                    //Aqui se debe hacer un switch como el de arriba para los que son calculado en base a ventas.
                    //Es decir, que el enumerativo sea eTipoDeCalculoComision.PorVentas
                    vResult = null;
                }

            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsCobranzaInformesViewModels
} //End of namespace Galac.Adm.Uil.Venta

