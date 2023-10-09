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
using Galac.Adm.Uil.Venta.ViewModel;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using LibGalac.Aos.Uil;

namespace Galac.Adm.Uil.Venta.Reportes {

    public class clsCajaInformesViewModel: LibReportsViewModel {
        #region Constructores

        public clsCajaInformesViewModel()
            : this(null, null) {
        }

        public clsCajaInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsCuadreCajaCobroMultimonedaDetalladoViewModel());
            AvailableReports.Add(new clsCuadreCajaPorUsuarioViewModel());
            AvailableReports.Add(new clsCuadreCajaPorTipoCobroViewModel());
            AvailableReports.Add(new clsCuadreCajaConDetalleFormaPagoViewModel());
            AvailableReports.Add(new clsCuadreCajaPorTipoCobroYUsuarioViewModel());
            AvailableReports.Add(new clsCajasAperturadasViewModel());
            AvailableReports.Add(new clsCajaCerradaViewModel());
            Mfc = initMfc;
            Title = "Informes de Caja";
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsCuadreCajaCobroMultimonedaDetalladoViewModel) {
                vResult = ConfigReportCuadreCajaCobroMultimonedaDetallado(SelectedReport as clsCuadreCajaCobroMultimonedaDetalladoViewModel);
            } else if (SelectedReport is clsCuadreCajaPorUsuarioViewModel) {
                vResult = ConfigReportCuadreCajaPorUsuario(SelectedReport as clsCuadreCajaPorUsuarioViewModel);
            } else if (SelectedReport is clsCuadreCajaPorTipoCobroViewModel) {
                vResult = ConfigReportCuadreCajaPorTipoCobro(SelectedReport as clsCuadreCajaPorTipoCobroViewModel);
            } else if (SelectedReport is clsCuadreCajaConDetalleFormaPagoViewModel) {
                vResult = ConfigReportCuadreCajaConDetalleFormaPago(SelectedReport as clsCuadreCajaConDetalleFormaPagoViewModel);
            } else if (SelectedReport is clsCuadreCajaPorTipoCobroYUsuarioViewModel) {
                vResult = ConfigReportCuadreCajaPorTipoCobroYUsuario(SelectedReport as clsCuadreCajaPorTipoCobroYUsuarioViewModel);
            } else if (SelectedReport is clsCajasAperturadasViewModel) {
                vResult = ConfigReportCajasAperturadas(SelectedReport as clsCajasAperturadasViewModel);
            } else if (SelectedReport is clsCajaCerradaViewModel) {
                vResult = ConfigReportCajaCerrada(SelectedReport as clsCajaCerradaViewModel);
            }
            return vResult;
        }

        private ILibRpt ConfigReportCuadreCajaCobroMultimonedaDetallado(clsCuadreCajaCobroMultimonedaDetalladoViewModel valViewModel) {
            ILibRpt vResult = null;
            bool vParametroUsaMonedaExtranjera = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"));
            string vParametroCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            string vCodigoMonedaExtranjera = string.Empty;
            string vSimboloMonedaExtranjera = "$";
            if (vParametroUsaMonedaExtranjera) {
                vCodigoMonedaExtranjera = LibString.IsNullOrEmpty(vParametroCodigoMonedaExtranjera) ? string.Empty : vParametroCodigoMonedaExtranjera;
                Moneda MonedaExtranjera = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<Moneda>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vCodigoMonedaExtranjera), new clsMonedaNav());
                vSimboloMonedaExtranjera = LibString.IsNullOrEmpty(vCodigoMonedaExtranjera) ? string.Empty : MonedaExtranjera.Simbolo;
            }
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.Venta.clsCuadreCajaCobroMultimonedaDetallado(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.CantidadAImprimir, valViewModel.NombreDelUsuario, valViewModel.Moneda, valViewModel.TotalesTipoCobro, vCodigoMonedaExtranjera, vSimboloMonedaExtranjera) {
                    Worker = Manager
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportCuadreCajaPorTipoCobro(clsCuadreCajaPorTipoCobroViewModel valViewModel) {
            Galac.Saw.Lib.clsUtilRpt vRpxUtil = new Saw.Lib.clsUtilRpt();
            ILibRpt vResult = null;
            string vRpxName = string.Empty;
            if (valViewModel.TipoDeInforme == Saw.Lib.eTipoDeInforme.Detallado) {
                vRpxName = "rpxCuadreCajaPorTipoCobroMultimonedaDetallado";
            } else {
                vRpxName = "rpxCuadreCajaPorTipoCobroMultimonedaResumido";
            }
            if (vRpxUtil.EsFormatoRpxValidoParaAOS(vRpxName)) {
                if (valViewModel != null) {
                    vResult = new Galac.Adm.Rpt.Venta.clsCuadreCajaPorTipoCobro(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                        Worker = Manager,
                        FechaInicial = valViewModel.FechaInicial,
                        FechaFinal = valViewModel.FechaFinal,
                        MonedaDeReporte = valViewModel.Moneda,
                        TipoDeInforme = valViewModel.TipoDeInforme,
                        RpxName = vRpxName
                    };
                }
            } else {
                LibMessages.MessageBox.Alert(this, "El formato del rpx no es compatible. Por favor, verifique e intente de nuevo", "Formato no valido");
            }
            return vResult;
        }

        private ILibRpt ConfigReportCuadreCajaConDetalleFormaPago(clsCuadreCajaConDetalleFormaPagoViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                if (valViewModel.TipoDeInforme == Saw.Lib.eTipoDeInforme.Detallado) {
                    vResult = new Galac.Adm.Rpt.Venta.clsCuadreCajaConDetalleFormaPago(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                        Worker = Manager,
                        FechaInicial = valViewModel.FechaInicial,
                        FechaFinal = valViewModel.FechaFinal,
                        MonedaDeReporte = valViewModel.Moneda,
                        TipoDeInforme = valViewModel.TipoDeInforme,
                        TotalesTipoPago = valViewModel.TotalesPorFormaDePago,
                    };
                } else {
                    vResult = new Galac.Adm.Rpt.Venta.clsCuadreCajaConDetalleFormaPagoResumido(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                        Worker = Manager,
                        FechaInicial = valViewModel.FechaInicial,
                        FechaFinal = valViewModel.FechaFinal,
                        MonedaDeReporte = valViewModel.Moneda,
                        TipoDeInforme = valViewModel.TipoDeInforme,
                        TotalesTipoPago = valViewModel.TotalesPorFormaDePago,
                    };
                }
            }
            return vResult;
        }

        private ILibRpt ConfigReportCuadreCajaPorTipoCobroYUsuario(clsCuadreCajaPorTipoCobroYUsuarioViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.Venta.clsCuadreCajaPorTipoCobroYUsuario(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                    Worker = Manager,
                    FechaInicial = valViewModel.FechaInicial,
                    FechaFinal = valViewModel.FechaFinal,
                    CantidadOperadorDeReporte = valViewModel.CantidadAImprimir,
                    NombreDelOperador = valViewModel.NombreDelUsuario,
                    MonedaDeReporte = valViewModel.Moneda
                };
            }
            return vResult;
        }

        private ILibRpt ConfigReportCajasAperturadas(clsCajasAperturadasViewModel valViewModel) {
            Galac.Saw.Lib.clsUtilRpt vRpxUtil = new Galac.Saw.Lib.clsUtilRpt();
            ILibRpt vResult = null;
            string vRpxName = "rpxCajasAperturadas";
            if (vRpxUtil.EsFormatoRpxValidoParaAOS(vRpxName)) {
                if (valViewModel != null) {
                    vResult = new Galac.Adm.Rpt.Venta.clsCajasAperturadas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                        Worker = Manager
                    };
                }
            } else {
                LibMessages.MessageBox.Alert(this, "El formato del rpx no es compatible. Por favor, verifique e intente de nuevo", "Formato no valido");
            }
            return vResult;
        }

        private ILibRpt ConfigReportCuadreCajaPorUsuario(clsCuadreCajaPorUsuarioViewModel valViewModel) {
            ILibRpt vResult = null;
            bool vParametroUsaMonedaExtranjera = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"));
            string vParametroCodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            string vCodigoMonedaExtranjera = string.Empty;
            string vSimboloMonedaExtranjera = string.Empty;
            if (vParametroUsaMonedaExtranjera) {
                vCodigoMonedaExtranjera = LibString.IsNullOrEmpty(vParametroCodigoMonedaExtranjera) ? string.Empty : vParametroCodigoMonedaExtranjera;
                Moneda MonedaExtranjera = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<Moneda>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", vCodigoMonedaExtranjera), new clsMonedaNav());
                vSimboloMonedaExtranjera = LibString.IsNullOrEmpty(vCodigoMonedaExtranjera) ? string.Empty : MonedaExtranjera.Simbolo;
            }
            if (valViewModel != null) {
                if (valViewModel.TipoDeInforme == Saw.Lib.eTipoDeInforme.Detallado) {
                    vResult = new Galac.Adm.Rpt.Venta.clsCuadreCajaPorUsuario(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.TipoDeInforme, valViewModel.Moneda, valViewModel.CantidadAImprimir, valViewModel.NombreDelOperador, vCodigoMonedaExtranjera, vSimboloMonedaExtranjera) {
                        Worker = Manager
                    };
                } else {
                    vResult = new Galac.Adm.Rpt.Venta.clsCuadreCajaPorUsuarioResumido(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.FechaInicial, valViewModel.FechaFinal, valViewModel.TipoDeInforme, valViewModel.Moneda, valViewModel.CantidadAImprimir, valViewModel.NombreDelOperador) {
                        Worker = Manager
                    };
                }
            }
            return vResult;
        }

        private ILibRpt ConfigReportCajaCerrada(clsCajaCerradaViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.Venta.clsCajaCerrada(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc, valViewModel.ConsecutivoCaja, valViewModel.FechaDesde, valViewModel.FechaHasta) {
                    Worker = Manager
                };
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsCajaInformesViewModel
} //End of namespace Galac.Adm.Uil.Venta

