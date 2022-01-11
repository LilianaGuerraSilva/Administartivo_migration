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
using LibGalac.Aos.Dal;

namespace Galac.Adm.Rpt.Venta {

    public class clsFacturacionPorUsuario : LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        public string RpxName { get; set; }
        private DateTime FechaDesde { get; set; }
        private DateTime FechaHasta { get; set; }
        private string NombreOperador { get; set; }
        private Saw.Lib.eMonedaParaImpresion MonedaDelReporte { get; set; }
        private Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio { get; set; }
        private bool IsInformeDetallado { get; set; }
        #endregion //Propiedades

        #region Constructores
        public clsFacturacionPorUsuario(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaDesde, DateTime valFechaHasta, string valNombreOperador, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc)
        {
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            NombreOperador = valNombreOperador;
            MonedaDelReporte = valMonedaDelReporte;
            TipoTasaDeCambio = valTipoTasaDeCambio;
            IsInformeDetallado = valIsInformeDetallado;
        }
        #endregion //Constructores

        #region Metodos Generados
        public static string ReportName
        {
            get { return "Facturación por Usuario"; }
        }

        public override Dictionary<string, string> GetConfigReportParameters()
        {
            string vTitulo = ReportName + (IsInformeDetallado? " - Detallado" : " - Resumido");
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("MonedaDelReporte", LibConvert.EnumToDbValue(( int ) MonedaDelReporte));
            vParams.Add("TipoTasaDeCambio", LibConvert.EnumToDbValue(( int ) TipoTasaDeCambio));
            vParams.Add("UsaContabilidad", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaModuloDeContabilidad"));
            vParams.Add("UsaPrecioSinIva", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaPrecioSinIva"));
            vParams.Add("CantidadDeDecimalesInt", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CantidadDeDecimalesInt"));
            return vParams;
        }

        public override void RunReport()
        {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IFacturaInformes vRpt = new Brl.Venta.Reportes.clsFacturaRpt();
            Data = vRpt.BuildFacturacionPorUsuario(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaDesde, FechaHasta, NombreOperador, MonedaDelReporte, TipoTasaDeCambio, IsInformeDetallado);
        }

        public override void SendReportToDevice()
        {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();

            if (!LibDataTable.DataTableHasRows(Data)) {
                throw new GalacException("No se encontró información para imprimir", eExceptionManagementType.Alert);
            }

            if (IsInformeDetallado) {
                dsrFacturacionPorUsuarioDetallado vRpt = new dsrFacturacionPorUsuarioDetallado();
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
                }
            } else {
                dsrFacturacionPorUsuarioResumido vRpt = new dsrFacturacionPorUsuarioResumido();
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
                }
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados

    } //End of class clsFacturacionPorArticulo

} //End of namespace Galac.Adm.Rpt.Venta
