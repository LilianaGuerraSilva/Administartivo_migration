using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.GestionProduccion;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.GestionProduccion {
    public class clsDetalleDeCostoDeProduccion : LibRptBaseMfc {
        #region Propiedades
        public int ConsecutivoCompania { get; set; }

        public int ConsecutivoOrden { get; set; }

        public string CodigoOrden { get; set; }

        public eSeleccionarOrdenPor SeleccionarOrdenPor { get; set; }

        protected DataTable DataInsumos { get; set; }

        protected DataTable DataSalidas { get; set; }

        public DateTime FechaInicial { get; set; }

        public DateTime FechaFinal { get; set; }

        eTasaDeCambioParaImpresion TasaDeCambio { get; set; }
        eMonedaDelInformeMM MonedaDelInforme { get; set; }
        string Moneda { get; set; }

        public int CostoTerminadoCalculadoAPartirDe { get; set; }

        #endregion //Propiedades
        #region Constructores
        public clsDetalleDeCostoDeProduccion(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime iniFechaInicial, DateTime iniFechaFinal, int iniConsecutivoOrden, string iniCodigoOrden, eSeleccionarOrdenPor iniSeleccionarOdenPor, eMonedaDelInformeMM initMonedaDelInforme, eTasaDeCambioParaImpresion initTasaDeCambio, string initMoneda)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            ConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            FechaInicial = iniFechaInicial;
            FechaFinal = iniFechaFinal;
            ConsecutivoOrden = iniConsecutivoOrden;
            CodigoOrden = iniCodigoOrden;
            SeleccionarOrdenPor = iniSeleccionarOdenPor;
            TasaDeCambio = initTasaDeCambio;
            MonedaDelInforme = initMonedaDelInforme;
            Moneda = MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa ? initMoneda : string.Empty;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrDetalleDeCostoDeProduccion().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsDetalleDeCostoDeProduccion.ReportName;
            bool vMostrarFechaInicioFinal = false;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            if (SeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeFinalizacion || SeleccionarOrdenPor == eSeleccionarOrdenPor.FechaDeInicio) {
                vMostrarFechaInicioFinal = true;
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("RifCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "NumeroDeRIF"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("MostrarFechaInicialFinal", vMostrarFechaInicioFinal.ToString());
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IOrdenDeProduccionInformes vRpt = new Galac.Adm.Brl.GestionProduccion.Reportes.clsOrdenDeProduccionRpt() as IOrdenDeProduccionInformes;
            string vCodigoMoneda = LibString.Trim(LibString.Mid(Moneda, 1, LibString.InStr(Moneda, ")") - 1));
            vCodigoMoneda = LibString.IsNullOrEmpty(vCodigoMoneda, true) ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera") : vCodigoMoneda;
            string vNombreMoneda = LibString.Trim(LibString.Mid(Moneda, 1 + LibString.InStr(Moneda, ")")));
            DataInsumos = vRpt.BuildDetalleDeCostoDeProduccion(ConsecutivoCompania, FechaInicial, FechaFinal,SeleccionarOrdenPor, ConsecutivoOrden, MonedaDelInforme, TasaDeCambio, vCodigoMoneda, vNombreMoneda);
            DataSalidas = vRpt.BuildDetalleDeCostoDeProduccionSalida(ConsecutivoCompania, FechaInicial, FechaFinal, SeleccionarOrdenPor, ConsecutivoOrden, MonedaDelInforme, TasaDeCambio, vCodigoMoneda, vNombreMoneda);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrDetalleDeCostoDeProduccion vRpt = new dsrDetalleDeCostoDeProduccion();
            if (DataSalidas.Rows.Count > 0) {
                if (vRpt.ConfigReport(DataSalidas, DataInsumos, MonedaDelInforme, TasaDeCambio, Moneda, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsDetalleDeCostoDeProduccion.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No existen registros para mostrar", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados

    }
}
