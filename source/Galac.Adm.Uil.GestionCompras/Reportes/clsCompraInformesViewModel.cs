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
using Galac.Adm.Uil.GestionCompras.ViewModel;
using Galac.Adm.Ccl.GestionCompras;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Uil.GestionCompras.Reportes {

    public class clsCompraInformesViewModel : LibReportsViewModel {
        #region Constructores

        public clsCompraInformesViewModel()
            : this(null, null) {
        }

        public clsCompraInformesViewModel(LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc) {
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            AvailableReports.Add(new clsImprimirComprasEntreFechasViewModel());
            AvailableReports.Add(new clsImprimirCostoDeCompraEntreFechasViewModel() {
                Mfc = initMfc
            });
            AvailableReports.Add(new clsImprimirMargenSobreCostoPromedioDeCompraViewModel() {
                Mfc = initMfc
            });
            AvailableReports.Add(new clsImprimirHistoricoDeComprasViewModel() {
                Mfc = initMfc
            });
            AvailableReports.Add(new clsImprimirOrdenesDeComprasViewModel());
            AvailableReports.Add(new clsImpresionDeEtiquetasPorComprasViewModel() {
                Mfc = initMfc
            });
            if(!LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                AvailableReports.Add(new clsImprimirCotizacionOrdenDeCompraViewModel() {
                    Mfc = initMfc
                });
            }
            Title = "Informes de Compra";

        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibRpt ConfigReport() {
            ILibRpt vResult = null;
            if (SelectedReport is clsImprimirComprasEntreFechasViewModel) {
                vResult = ConfigReportImprimirComprasEntreFechas(SelectedReport as clsImprimirComprasEntreFechasViewModel);
            } else if (SelectedReport is clsImprimirCostoDeCompraEntreFechasViewModel) {
                vResult = ConfigReportImprimirCostoDeCompraEntreFechas(SelectedReport as clsImprimirCostoDeCompraEntreFechasViewModel);
            } else if (SelectedReport is clsImprimirMargenSobreCostoPromedioDeCompraViewModel) {
                vResult = ConfigReportImprimirMargenSobreCostoPromedioDeCompra(SelectedReport as clsImprimirMargenSobreCostoPromedioDeCompraViewModel);
            } else if (SelectedReport is clsImprimirHistoricoDeComprasViewModel) {
                vResult = ConfigReportImprimirHistoricoDeCompras(SelectedReport as clsImprimirHistoricoDeComprasViewModel);
            } else if (SelectedReport is clsImprimirOrdenesDeComprasViewModel) {
                vResult = ConfigReportImprimirOrdenesDeCompras(SelectedReport as clsImprimirOrdenesDeComprasViewModel);
            } else if (SelectedReport is clsImprimirCotizacionOrdenDeCompraViewModel) {
                vResult = ConfigReportImpresionCotizacionOrdenDeCompra(SelectedReport as clsImprimirCotizacionOrdenDeCompraViewModel);
            } else if (SelectedReport is clsImpresionDeEtiquetasPorComprasViewModel) {
                vResult = ConfigReportImpresionDeEtiquetasPorCompras(SelectedReport as clsImpresionDeEtiquetasPorComprasViewModel);
            }
            return vResult;

        }

        internal ILibRpt ConfigReportCompra(int valConsecutivo, string valNumeroOrdeDeCompra) {
            ILibRpt vResult = null;
            vResult = ConfigReportImprimirCompra(valConsecutivo,valNumeroOrdeDeCompra);
            vResult.RunReport();
            vResult.SendReportToDevice();
            return vResult;
        }

        private ILibRpt ConfigReportImprimirCompra(int valConsecutivo,string valNumeroOrdeDeCompra) {
            ILibRpt vResult = null;
            vResult = new Galac.Adm.Rpt.GestionCompras.clsCompra(ePrintingDevice.Screen, ExportFileFormat, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
                Worker = Manager,
                ConsecutivoCompra = valConsecutivo,
                NumeroDeOrdenDeCompra =valNumeroOrdeDeCompra,
                UseExternalRpx=true,
            };
            return vResult;
        }

        internal ILibRpt ConfigReportImprimirComprasEntreFechas(clsImprimirComprasEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
		     if (valViewModel != null) {
                 vResult = new Galac.Adm.Rpt.GestionCompras.clsImprimirComprasEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                        Worker = Manager,
                        ConsecutivoCompra = valViewModel.ConsecutivoCompania,
                        FechaInicial = valViewModel.FechaInicial,
                        FechaFinal = valViewModel.FechaFinal,
                        ImprimirRenglones = valViewModel.ImprimirRenglones,
                        ImprimirTotales = valViewModel.ImprimirTotales,
                        MostrarComprasAnuladas = valViewModel.MostrarComprasAnuladas,
                        MonedaParaImpresion = valViewModel.MonedaParaImpresion,
                        SolOMonedaOriginal = valViewModel.Original
                    };
             }
             return vResult;
        }

        internal ILibRpt ConfigReportImprimirCostoDeCompraEntreFechas(clsImprimirCostoDeCompraEntreFechasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionCompras.clsImprimirCostoDeCompraEntreFechas(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                    Worker = Manager,
                    ConsecutivoCompra = Mfc.GetInt("Compania"),
                    FechaInicial = valViewModel.FechaInicial,
                    FechaFinal = valViewModel.FechaFinal,
                    LineasDeProductoCantidadAImprimir = valViewModel.CantidadAImprimir,
                    CodigoProducto = valViewModel.CodigoProducto
                };
            }
            return vResult;
        }

        internal ILibRpt ConfigReportImprimirMargenSobreCostoPromedioDeCompra(clsImprimirMargenSobreCostoPromedioDeCompraViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionCompras.clsImprimirMargenSobreCostoPromedioDeCompra(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                    Worker = Manager,
                    ConsecutivoCompra = Mfc.GetInt("Compania"),
                    NivelDePrecio = valViewModel.NivelDePrecio,
                    LineasDeProductoCantidadAImprimir = valViewModel.CantidadAImprimir,
                    CodigoProducto = valViewModel.CodigoProducto
                };
            }
            return vResult;
        }

        internal ILibRpt ConfigReportImprimirHistoricoDeCompras(clsImprimirHistoricoDeComprasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionCompras.clsImprimirHistoricoDeCompras(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                    Worker = Manager,
                    ConsecutivoCompra = Mfc.GetInt("Compania"),
                    FechaInicial = valViewModel.FechaInicial,
                    FechaFinal = valViewModel.FechaFinal,
                    LineasDeProductoCantidadAImprimir = valViewModel.CantidadAImprimir,
                    CodigoProducto = valViewModel.CodigoProducto
                };
            }
            return vResult;
        }

        internal ILibRpt ConfigReportImprimirOrdenesDeCompras(clsImprimirOrdenesDeComprasViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionCompras.clsImprimirOrdenesDeCompras(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                    Worker = Manager,
                    ConsecutivoCompania = Mfc.GetInt("Compania"),
                    FechaInicial = valViewModel.FechaInicial,
                    FechaFinal = valViewModel.FechaFinal,
                    StatusDeOrdenDeCompra = valViewModel.StatusDeOrdenDeCompra,
                    ImprimirRenglones = valViewModel.ImprimirRenglones
                };
            }
            return vResult;
        }

        internal ILibRpt ConfigReportImpresionCotizacionOrdenDeCompra(clsImprimirCotizacionOrdenDeCompraViewModel valViewModel) {
            ILibRpt vResult = null;
            if (valViewModel != null) {
                vResult = new Galac.Adm.Rpt.GestionCompras.clsImprimirCotizacionOrdenDeCompra(PrintingDevice, ExportFileFormat, AppMemoryInfo, Mfc) {
                    Worker = Manager,
                    NumeroCotizacion = valViewModel.NumeroCotizacion
                };
            }
            return vResult;
        }

        internal ILibRpt ConfigReportImpresionDeEtiquetasPorCompras(clsImpresionDeEtiquetasPorComprasViewModel valViewModel) {
            ILibRpt vResult = null;
            string vRpx = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombrePlantillaImpresionCodigoBarrasCompras");
            Galac.Saw.Lib.clsUtilRpt vUtil = new Galac.Saw.Lib.clsUtilRpt();
            if (vUtil.EsFormatoRpxValidoParaAOS(vRpx)) {
                vResult = ConfigReportImprimirEtiquetasPorCompras(valViewModel);
            }
            else {
                StringBuilder vMensaje = new StringBuilder();
                vMensaje.AppendLine("No se puede imprimir la orden de compra, hay un problema con el formato seleccionado,");
                vMensaje.AppendLine("verifique que el archivo .RPX seleccionado en parámetros tenga un formato válido");
                vMensaje.AppendLine("y se encuentre en el directorio de reportes.");
                LibMessages.MessageBox.Alert(this, vMensaje.ToString(), "Imprimir");
            }
            return vResult;
        }

        internal ILibRpt ConfigReportImprimirEtiquetasPorCompras(clsImpresionDeEtiquetasPorComprasViewModel valViewModel) {
            ILibRpt vResult = null;
            vResult = new Galac.Adm.Rpt.GestionCompras.clsImpresionDeEtiquetasPorCompras(ePrintingDevice.Screen, ExportFileFormat, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
                Worker = Manager,
                NivelDePrecio = valViewModel.NivelDePrecio,
                NumeroCompra = valViewModel.NumeroCompra,
                PrecioSinIva = valViewModel.PrecioSinIva,
                MostrarProveedor = valViewModel.MostrarProveedor
            };
            return vResult;
        }

        internal ILibRpt ConfigReportOrdenCompra(int valConsecutivo, eTipoCompra valTipoCompra) {
            ILibRpt vResult = null;
            vResult = ConfigReportImprimirOrdenDeCompra(valConsecutivo, valTipoCompra);
            vResult.RunReport();
            vResult.SendReportToDevice();
            return vResult;
        }
		
        private ILibRpt ConfigReportImprimirOrdenDeCompra(int valConsecutivo, eTipoCompra valTipoCompra) {
            ILibRpt vResult = null;
            vResult = new Galac.Adm.Rpt.GestionCompras.clsOrdenDeCompra(ePrintingDevice.Screen, ExportFileFormat, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
                Worker = Manager,
                ConsecutivoCompra = valConsecutivo,
                TipoCompra = valTipoCompra
            };
            return vResult;
        }


        #endregion //Metodos Generados


    } //End of class clsGuiaDeRemisionInformesViewModel

} //End of namespace Galac.Adm.Uil.Venta

