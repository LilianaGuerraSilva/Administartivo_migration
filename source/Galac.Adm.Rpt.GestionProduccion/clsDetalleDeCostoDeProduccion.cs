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

namespace Galac.Adm.Rpt.GestionProduccion {
    public class clsDetalleDeCostoDeProduccion : LibRptBaseMfc {
        #region Propiedades
        public int ConsecutivoCompania { get; set; }

        public int ConsecutivoOrden { get; set; }

        public string CodigoOrden { get; set; }

        public eSeleccionarOrdenPor SeleccionarOrdenPor { get; set; }

        protected DataTable Data { get; set; }

        protected DataTable DataSalidas { get; set; }

        public DateTime FechaInicial { get; set; }

        public DateTime FechaFinal { get; set; }

        private decimal TasaDeCambio { get; set; }
        private string MonedaDelInforme { get; set; }
        private string[] ListaMonedas { get; set; }

        public int CostoTerminadoCalculadoAPartirDe { get; set; }

        #endregion //Propiedades
        #region Constructores
        public clsDetalleDeCostoDeProduccion(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime iniFechaInicial, DateTime iniFechaFinal, int iniConsecutivoOrden, string iniCodigoOrden, eSeleccionarOrdenPor iniSeleccionarOdenPor) //, decimal initTasaDeCambio, string initMonedaDelInforme, string[] initListaMonedas)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            ConsecutivoCompania = 16; //LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", LibConvert.ToInt("16"));
            FechaInicial = iniFechaInicial;
            FechaFinal = iniFechaFinal;
            ConsecutivoOrden = iniConsecutivoOrden;
            CodigoOrden = iniCodigoOrden;
            SeleccionarOrdenPor = iniSeleccionarOdenPor;
            TasaDeCambio = 1;//initTasaDeCambio;
            MonedaDelInforme = "";//initMonedaDelInforme;
            //ListaMonedas = ;//initListaMonedas;
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
            Data = vRpt.BuildDetalleDeCostoDeProduccion(ConsecutivoCompania, FechaInicial, FechaFinal,SeleccionarOrdenPor, ConsecutivoOrden, MonedaDelInforme, TasaDeCambio, ListaMonedas);
            DataSalidas = vRpt.BuildDetalleDeCostoDeProduccionSalida(ConsecutivoCompania, FechaInicial, FechaFinal, SeleccionarOrdenPor, ConsecutivoOrden, MonedaDelInforme, TasaDeCambio, ListaMonedas);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrDetalleDeCostoDeProduccion vRpt = new dsrDetalleDeCostoDeProduccion();
            if (DataSalidas.Rows.Count > 0) {
                if (vRpt.ConfigReport(DataSalidas, Data, ListaMonedas, MonedaDelInforme, TasaDeCambio, vParams)) {
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
