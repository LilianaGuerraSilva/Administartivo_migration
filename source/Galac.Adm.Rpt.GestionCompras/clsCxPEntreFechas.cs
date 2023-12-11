using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.GestionCompras {
    public class clsCxPEntreFechas: LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        DateTime FechaDesde { get; set; }
        DateTime FechaHasta { get; set; }
        eInformeStatusCXC_CXP StatusCxP { get; set; }
        eMonedaDelInformeMM MonedaDelInforme { get; set; }
        string Moneda { get; set; }
        bool MostrarInfoAdicional { get; set; }
        bool MostrarNroComprobanteContable { get; set; }
        eTasaDeCambioParaImpresion TasaDeCambio { get; set; }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsCxPEntreFechas(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime initFechaDesde, DateTime initFechaHasta, eInformeStatusCXC_CXP initStatusCxP, eMonedaDelInformeMM initMonedaDelInforme, eTasaDeCambioParaImpresion initTasaDeCambio, string initMoneda, bool initMostrarInfoAdicional, bool initMostrarNroComprobanteContable)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = initFechaDesde;
            FechaHasta = initFechaHasta;
            StatusCxP = initStatusCxP;
            MonedaDelInforme = initMonedaDelInforme;
            Moneda = MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa ? initMoneda : string.Empty;
            MostrarInfoAdicional = initMostrarInfoAdicional;
            MostrarNroComprobanteContable = initMostrarNroComprobanteContable;
            TasaDeCambio = initTasaDeCambio;
        }
        #endregion //Constructores
        #region Metodos Generados
        public static string ReportName {
            get { return new dsrCxPEntreFechas().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCxPEntreFechas.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("{0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICxPInformes vRpt = new Galac.Adm.Brl.GestionCompras.Reportes.clsCxPRpt() as ICxPInformes;
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            string vCodigoMoneda = LibString.Trim(LibString.Mid(Moneda, 1, LibString.InStr(Moneda, ")") - 1));
            vCodigoMoneda = LibString.IsNullOrEmpty(vCodigoMoneda, true) ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera") : vCodigoMoneda;
            Data = vRpt.BuildCxPEntreFechas(vConsecutivoCompania, FechaDesde, FechaHasta, StatusCxP, MonedaDelInforme, vCodigoMoneda, TasaDeCambio, MostrarNroComprobanteContable);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCxPEntreFechas vRpt = new dsrCxPEntreFechas();
            if (vRpt.ConfigReport(Data, vParams, MostrarInfoAdicional, MostrarNroComprobanteContable, MonedaDelInforme, Moneda, TasaDeCambio)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCxPEntreFechas.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados

    } //End of class clsCuentasPorPagarEntreFechas
} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

