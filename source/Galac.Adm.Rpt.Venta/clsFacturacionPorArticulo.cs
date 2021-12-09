using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.Venta {

    public class clsFacturacionPorArticulo: LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        public string RpxName { get; set; }
        private DateTime FechaDesde { get; set; }
        private DateTime FechaHasta { get; set; }
        private string CodigoDelArticulo { get; set; }
        private Saw.Lib.eMonedaParaImpresion MonedaDelReporte { get; set; }
        private Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio { get; set; }
        private bool IsInformeDetallado { get; set; }
        #endregion //Propiedades

        #region Constructores
        public clsFacturacionPorArticulo(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoDelArticulo, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            CodigoDelArticulo = valCodigoDelArticulo;
            MonedaDelReporte = valMonedaDelReporte;
            TipoTasaDeCambio = valTipoTasaDeCambio;
            IsInformeDetallado = valIsInformeDetallado;
        }
        #endregion //Constructores

        #region Metodos Generados
        public static string ReportName {
            get { return "Facturación por Artículo"; }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsFacturacionPorArticulo.ReportName + (IsInformeDetallado? " - Detallado" : " - Resumido");
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("MonedaDelReporte", LibConvert.EnumToDbValue(( int ) MonedaDelReporte));
            vParams.Add("TipoTasaDeCambio", LibConvert.EnumToDbValue(( int ) TipoTasaDeCambio));
            vParams.Add("UsaPrecioSinIva", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaPrecioSinIva"));
            vParams.Add("CantidadDeDecimalesInt", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CantidadDeDecimalesInt"));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IFacturaInformes vRpt = new Brl.Venta.Reportes.clsFacturaRpt();
            Data = vRpt.BuildFacturacionPorArticulo(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaDesde, FechaHasta, CodigoDelArticulo, MonedaDelReporte, TipoTasaDeCambio, IsInformeDetallado);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();

            if (Data.Rows.Count == 0) {
                throw new GalacException("No se encontró información para imprimir", eExceptionManagementType.Alert);
			}

            if (IsInformeDetallado) {
                dsrFacturacionPorArticuloDetallado vRpt = new dsrFacturacionPorArticuloDetallado();
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsFacturacionPorArticulo.ReportName, true, ExportFileFormat, "", false);
                }
            } else {
                dsrFacturacionPorArticuloResumido vRpt = new dsrFacturacionPorArticuloResumido();
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsFacturacionPorArticulo.ReportName, true, ExportFileFormat, "", false);
                }
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados

    } //End of class clsFacturacionPorArticulo

} //End of namespace Galac.Adm.Rpt.Venta

