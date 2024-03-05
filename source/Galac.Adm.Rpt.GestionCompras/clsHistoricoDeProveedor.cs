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
using LibGalac.Aos.Dal;
using LibGalac.Aos.Catching;
using Galac.Adm.Rpt.GestiosCompras;

namespace Galac.Adm.Rpt.GestionCompras {

    public class clsHistoricoDeProveedor: LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        private DateTime FechaDesde;
        private DateTime FechaHasta;
        private eMonedaDelInformeMM MonedaDelInforme;
        private string Moneda;
        private eTasaDeCambioParaImpresion TasaDeCambio;
        private string CodigoProveedor;
        private bool SaltoDePaginaPorProveedor;
        private eProveedorOrdenadosPor OrdenarProveedorPor;
        #region Codigo Ejemplo      

        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsHistoricoDeProveedor(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoProveedor, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valSaltoDePaginaPorProveedor, eProveedorOrdenadosPor valOrdenarProveedorPor)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            CodigoProveedor = valCodigoProveedor;
            MonedaDelInforme = valMonedaDelInforme;
            Moneda = LibString.Trim(LibString.Mid(valMoneda, LibString.InStr(valMoneda, ")") + 1));           
            TasaDeCambio = valTasaDeCambio;
            SaltoDePaginaPorProveedor = valSaltoDePaginaPorProveedor;
            OrdenarProveedorPor = valOrdenarProveedorPor;
        }
        #endregion //Constructores
        #region Metodos Generados
        public static string ReportName { get { return new dsrHistoricoDeProveedor().ReportTitle(); } }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsHistoricoDeProveedor.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("FechaInicialYFinal", string.Format("desde {0} hasta {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("SaltoDePaginaPorProveedor", LibConvert.BoolToSN(SaltoDePaginaPorProveedor));
            vParams.Add("MonedaDelInforme", LibConvert.EnumToDbValue((int)MonedaDelInforme));
            vParams.Add("TasaDeCambioParaElReporte", LibConvert.EnumToDbValue((int)TasaDeCambio));
            vParams.Add("MonedaExpresadaEn",Moneda);
            #region Codigo Ejemplo          
            #endregion //Codigo Ejemplo
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IProveedorInformes vRpt = new Brl.GestionCompras.Reportes.clsProveedorRpt() as IProveedorInformes;
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            string vCodigoMoneda = LibString.Trim(LibString.Mid(Moneda, 1, LibString.InStr(Moneda, ")") - 1));
            vCodigoMoneda = LibString.IsNullOrEmpty(vCodigoMoneda, true) ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera") : vCodigoMoneda;            
            Data = vRpt.BuildHistoricoDeProveedor(vConsecutivoCompania, FechaDesde, FechaHasta, CodigoProveedor, MonedaDelInforme, vCodigoMoneda, Moneda, TasaDeCambio, OrdenarProveedorPor);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrHistoricoDeProveedor vRpt = new dsrHistoricoDeProveedor();
            if (LibDataTable.DataTableHasRows(Data)) {
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {

                throw new GalacException("No se encontró información para mostrar", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados

    } //End of class clsHistoricoDeProveedor

} //End of namespace Galac.Adm.Rpt.Proveedor

