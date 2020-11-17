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

    public class clsComisionDeVendedoresPorCobranzaMonto:LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        private DateTime FechaInicial { get; set; }
        private DateTime FechaFinal { get; set; }
        private Saw.Lib.eTipoDeInforme TipoDeInforme { get; set; }
        private Saw.Lib.eMonedaParaImpresion MonedaDeReporte { get; set; }
        private Saw.Lib.eTasaDeCambioParaImpresion TasaDeCambioImpresion { get; set; }
        private bool IncluirComisionEnMonedaExt { get; set; }
        private decimal TasaDeCambioComisionMonedaExt { get; set; }
        private Saw.Lib.eCantidadAImprimir CantidadAImprimir { get; set; }
        private string CodigoVendedor { get; set; }
        private string SimboloMonedaExtranjera { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsComisionDeVendedoresPorCobranzaMonto(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme,Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion, bool valIncluirComisionEnMonedaExt, decimal valTasaDeCambioComisionMonedaExt, Saw.Lib.eCantidadAImprimir valCantidadAImprimir, string valCodigoVendedor, string valSimboloMonedaExtranjera)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaInicial = valFechaInicial;
            FechaFinal = valFechaFinal;
            TipoDeInforme = valTipoDeInforme;
            MonedaDeReporte = valMonedaDeReporte;
            TasaDeCambioImpresion = valTasaDeCambioImpresion;
            IncluirComisionEnMonedaExt = valIncluirComisionEnMonedaExt;
            TasaDeCambioComisionMonedaExt = valTasaDeCambioComisionMonedaExt;
            CantidadAImprimir = valCantidadAImprimir;
            CodigoVendedor = valCodigoVendedor;
            SimboloMonedaExtranjera = valSimboloMonedaExtranjera;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrComisionDeVendedoresPorCobranzaMonto().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsComisionDeVendedoresPorCobranzaMonto.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("desde {0} hasta {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
            vParams.Add("EnMonedaOriginal", LibConvert.BoolToSN(LibEnum.Equals(MonedaDeReporte, Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal)));
            vParams.Add("TasaDeCambioOriginalAMonedaLocal", LibConvert.BoolToSN(LibEnum.Equals(TasaDeCambioImpresion, Saw.Lib.eTasaDeCambioParaImpresion.Original)));
            vParams.Add("IncluirComisionEnMonedaExt", LibConvert.BoolToSN(IncluirComisionEnMonedaExt));
            vParams.Add("UsaAsignacionDeComisionEnCobranza", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "AsignarComisionDeVendedorEnCobranza"));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICobranzaInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsCobranzaRpt() as ICobranzaInformes;
            Data = vRpt.BuildComisionDeVendedoresPorCobranzaMonto(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaInicial, FechaFinal, TipoDeInforme,MonedaDeReporte, TasaDeCambioImpresion, CantidadAImprimir, CodigoVendedor);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            if (Data.Rows.Count < 1 ) {
                throw new GalacException("No existen datos para mostrar", eExceptionManagementType.Alert);
            } else {
                dsrComisionDeVendedoresPorCobranzaMonto vRpt = new dsrComisionDeVendedoresPorCobranzaMonto();
                vRpt.TasaDeCambioComisionMonedaExt = TasaDeCambioComisionMonedaExt;
                vRpt.SimboloMonedaExtranjera = SimboloMonedaExtranjera;
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsComisionDeVendedoresPorCobranzaMonto.ReportName, true, ExportFileFormat, "", false);
                }
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsComisionDeVendedoresPorCobranza

} //End of namespace Galac.Adm.Rpt.Venta

