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
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.Venta {
    public class clsCxCEntreFechas : LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        DateTime FechaDesde { get; set; }
        DateTime FechaHasta { get; set; }
        eInformeStatusCXC_CXP StatusCxC { get; set; }
        eInformeAgruparPor AgruparPor { get; set; }
        string ZonaDeCobranza { get; set; }
        string SectorDeNegocio { get; set; }
        eMonedaDelInformeMM MonedaDelInforme { get; set; }
        string Moneda { get; set; }
        bool MostrarInfoAdicional { get; set; }
        bool MostrarContacto { get; set; }
        bool MostrarNroComprobanteContable { get; set; }
        eTasaDeCambioParaImpresion TasaDeCambio { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsCxCEntreFechas(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime initFechaDesde, DateTime initFechaHasta, eInformeStatusCXC_CXP initStatusCxC, eInformeAgruparPor initAgruparPor, string initZonaDeCobranza, string initSectorDeNegocio, eMonedaDelInformeMM initMonedaDelInforme, eTasaDeCambioParaImpresion initTasaDeCambio, string initMoneda, bool initMostrarInfoAdicional, bool initMostrarContacto, bool initMostrarNroComprobanteContable)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = initFechaDesde;
            FechaHasta = initFechaHasta;
            StatusCxC = initStatusCxC;
            AgruparPor = initAgruparPor;
            ZonaDeCobranza = AgruparPor == eInformeAgruparPor.ZonaDeCobranza ? initZonaDeCobranza : string.Empty;
            SectorDeNegocio = AgruparPor == eInformeAgruparPor.SectorDeNegocio ? initSectorDeNegocio : string.Empty;
            MonedaDelInforme = initMonedaDelInforme;
            Moneda = MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa ? initMoneda : string.Empty;
            MostrarInfoAdicional = initMostrarInfoAdicional;
            MostrarContacto = initMostrarContacto;
            MostrarNroComprobanteContable = initMostrarNroComprobanteContable;
            TasaDeCambio = initTasaDeCambio;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCxCEntreFechas().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCxCEntreFechas.ReportName;
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
            ICxCInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsCxCRpt() as ICxCInformes;
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            string vCodigoMoneda = LibString.Trim(LibString.Mid(Moneda, 1, LibString.InStr(Moneda, ")") - 1));
            vCodigoMoneda = LibString.IsNullOrEmpty(vCodigoMoneda, true) ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera") : vCodigoMoneda;
            string vNombreMoneda = LibString.Trim(LibString.Mid(Moneda, LibString.InStr(Moneda, ")")));
            Data = vRpt.BuildCxCEntreFechas(vConsecutivoCompania, FechaDesde, FechaHasta, StatusCxC, AgruparPor, ZonaDeCobranza, SectorDeNegocio, MonedaDelInforme, vCodigoMoneda, vNombreMoneda, TasaDeCambio, MostrarNroComprobanteContable);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCxCEntreFechas vRpt = new dsrCxCEntreFechas();
            if (vRpt.ConfigReport(Data, vParams, MostrarInfoAdicional, MostrarContacto, MostrarNroComprobanteContable, AgruparPor, MonedaDelInforme, Moneda, TasaDeCambio)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCxCEntreFechas.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsCxCEntreFechas
} //End of namespace Galac.Adm.Rpt.Venta, 