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
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.Venta {

    public class clsCxCPendientesEntreFechas: LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        public string RpxName { get; set; }
        private DateTime FechaDesde { get; set; }
        private DateTime FechaHasta { get; set; }
        private bool MostrarContacto { get; set; }
        private eMonedaDelInformeMM MonedaDelReporte { get; set; }
        string Moneda { get; set; }
        private eTasaDeCambioParaImpresion TipoTasaDeCambio { get; set; }
        bool MostrarNroComprobanteContable { get; set; }
        eMonedaDelInformeMM MonedaDelInforme { get; set; }
        #endregion //Propiedades

        #region Constructores
        public clsCxCPendientesEntreFechas(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaDesde, DateTime valFechaHasta, bool valMostrarContacto, eMonedaDelInformeMM valMonedaDelReporte, string initMoneda, eTasaDeCambioParaImpresion valTipoTasaDeCambio)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            MostrarContacto = valMostrarContacto;
            MonedaDelReporte = valMonedaDelReporte;
            Moneda = MonedaDelReporte == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa ? initMoneda : string.Empty;
            TipoTasaDeCambio = valTipoTasaDeCambio;
        }
        #endregion //Constructores

        #region Metodos Generados
        public static string ReportName {
            get { return new dsrCxCPendientesEntreFechas().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("UsaContabilidad", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaModuloDeContabilidad"));
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("MostrarContacto", LibConvert.BoolToSN(MostrarContacto));
            vParams.Add("MonedaDelReporte", LibConvert.EnumToDbValue(( int ) MonedaDelReporte));
            vParams.Add("TipoTasaDeCambio", LibConvert.EnumToDbValue(( int ) TipoTasaDeCambio));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICxCInformes vRpt = new Brl.Venta.Reportes.clsCxCRpt();
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            string vCodigoMoneda = LibString.Trim(LibString.Mid(Moneda, 1, LibString.InStr(Moneda, ")") - 1));
            vCodigoMoneda = LibString.IsNullOrEmpty(vCodigoMoneda, true) ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera") : vCodigoMoneda;
            string vNombreMoneda = LibString.Trim(LibString.Mid(Moneda, LibString.InStr(Moneda, ")")));

            Data = vRpt.BuildCxCPendientesEntreFechas(vConsecutivoCompania, FechaDesde, FechaHasta, MonedaDelReporte, TipoTasaDeCambio, vCodigoMoneda, vNombreMoneda);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCxCPendientesEntreFechas vRpt = new dsrCxCPendientesEntreFechas();
            if (LibDataTable.DataTableHasRows(Data)) {
                if (vRpt.ConfigReport(Data, vParams, MostrarContacto, MostrarNroComprobanteContable, MonedaDelInforme, Moneda, TipoTasaDeCambio)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No se encontró información para imprimir", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados

    } //End of class clsCxCPendientesEntreFechas

} //End of namespace Galac.Adm.Rpt.Venta

