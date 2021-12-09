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

    public class clsCxCPorCliente : LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        public string RpxName { get; set; }
        private string CodigoDelCliente { get; set; }
        private string ZonaCobranza { get; set; }
        private DateTime FechaDesde { get; set; }
        private DateTime FechaHasta { get; set; }
        private bool MostrarContacto { get; set; }
        private eClientesOrdenadosPor ClientesOrdenadosPor { get; set; }
        private Saw.Lib.eMonedaParaImpresion MonedaDelReporte { get; set; }
        private Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio { get; set; }
        #endregion //Propiedades

        #region Constructores
        public clsCxCPorCliente(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, string valCodigoDelCliente, string valZonaCobranza, DateTime valFechaDesde, DateTime valFechaHasta, bool valMostrarContacto, eClientesOrdenadosPor valClientesOrdenadosPor, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            CodigoDelCliente = valCodigoDelCliente;
            ZonaCobranza = valZonaCobranza;
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            MostrarContacto = valMostrarContacto;
            ClientesOrdenadosPor = valClientesOrdenadosPor;
            MonedaDelReporte = valMonedaDelReporte;
            TipoTasaDeCambio = valTipoTasaDeCambio;
        }
        #endregion //Constructores

        #region Metodos Generados
        public static string ReportName {
            get { return new dsrCxCPorCliente().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("MostrarContacto", LibConvert.BoolToSN(MostrarContacto));
            vParams.Add("ClientesOrdenadosPor", LibConvert.EnumToDbValue((int) ClientesOrdenadosPor));
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
            Data = vRpt.BuildCxCPorCliente(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), CodigoDelCliente, ZonaCobranza, FechaDesde, FechaHasta, ClientesOrdenadosPor, MonedaDelReporte, TipoTasaDeCambio);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCxCPorCliente vRpt = new dsrCxCPorCliente();
            if (Data.Rows.Count >= 1) {
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No se encontró información para imprimir", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados

    } //End of class clsCxCPorCliente

} //End of namespace Galac.Adm.Rpt.Venta

