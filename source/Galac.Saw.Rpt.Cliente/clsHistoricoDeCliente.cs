using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Lib;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Rpt.Cliente {

    public class clsHistoricoDeCliente: LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        private DateTime FechaDesde;
        private DateTime FechaHasta;
        private eMonedaDelInformeMM MonedaDelInforme;
        private string Moneda;
        private eTasaDeCambioParaImpresion TasaDeCambio;
        private string CodigoCliente;
        private bool SaltoDePaginaPorCliente;
        private eClientesOrdenadosPor OrdenarClientePor;
        #region Codigo Ejemplo      

        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsHistoricoDeCliente(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoCliente, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valSaltoDePaginaPorCliente, eClientesOrdenadosPor valOrdenarClientePor)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            CodigoCliente = valCodigoCliente;
            MonedaDelInforme = valMonedaDelInforme;
            Moneda = valMoneda;
            TasaDeCambio = valTasaDeCambio;
            SaltoDePaginaPorCliente = valSaltoDePaginaPorCliente;
            OrdenarClientePor = valOrdenarClientePor;
        }
        #endregion //Constructores
        #region Metodos Generados
        public static string ReportName { get { return new dsrHistoricoDeCliente().ReportTitle(); } }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsHistoricoDeCliente.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("FechaInicialYFinal", string.Format("desde {0} hasta {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("SaltoDePaginaPorCliente", LibConvert.BoolToSN(SaltoDePaginaPorCliente));
            #region Codigo Ejemplo          
            #endregion //Codigo Ejemplo
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IClienteInformes vRpt = new Galac.Saw.Brl.Cliente.Reportes.clsClienteRpt() as IClienteInformes;
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            string vCodigoMoneda = LibString.Trim(LibString.Mid(Moneda, 1, LibString.InStr(Moneda, ")") - 1));
            vCodigoMoneda = LibString.IsNullOrEmpty(vCodigoMoneda, true) ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera") : vCodigoMoneda;
            string vNombreMoneda = LibString.Trim(LibString.Mid(Moneda, LibString.InStr(Moneda, ")") + 1));
            Data = vRpt.BuildHistoricoDeCliente(vConsecutivoCompania, FechaDesde, FechaHasta, CodigoCliente, MonedaDelInforme, vCodigoMoneda, vNombreMoneda, TasaDeCambio, OrdenarClientePor);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrHistoricoDeCliente vRpt = new dsrHistoricoDeCliente();
            if (LibDataTable.DataTableHasRows(Data)) {
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsHistoricoDeCliente.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {

                throw new GalacException("No se encontró información para mostrar", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados

    } //End of class clsHistoricoDeCliente

} //End of namespace Galac.Saw.Rpt.Cliente

