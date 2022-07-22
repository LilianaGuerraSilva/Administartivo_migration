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

namespace Galac.Adm.Rpt.GestionProduccion {
    public class clsDetalleDeCostoDeProduccion : LibRptBaseMfc {
        #region Propiedades
        public int ConsecutivoCompania { get; set; }

        public int ConsecutivoOrden { get; set; }

        public string CodigoOrden { get; set; }

        public eSeleccionarPor SeleccionarPor { get; set; }

        protected DataTable Data { get; set; }

        public DateTime FechaInicial { get; set; }

        public DateTime FechaFinal { get; set; }

        #endregion //Propiedades
        #region Constructores
        public clsDetalleDeCostoDeProduccion(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime iniFechaInicial, DateTime iniFechaFinal, int iniConsecutivoOrden, string iniCodigoOrden, eSeleccionarPor iniSeleccionarPor)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            ConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            FechaInicial = iniFechaInicial;
            FechaFinal = iniFechaFinal;
            ConsecutivoOrden = iniConsecutivoOrden;
            CodigoOrden = iniCodigoOrden;
            SeleccionarPor = iniSeleccionarPor;
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
            if (SeleccionarPor == eSeleccionarPor.FechaDeFinalizacion || SeleccionarPor == eSeleccionarPor.FechaDeInicio) {
                vMostrarFechaInicioFinal = true;
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
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
            Data = vRpt.BuildDetalleDeCostoDeProduccion(ConsecutivoCompania, FechaInicial, FechaFinal,SeleccionarPor, ConsecutivoOrden);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrDetalleDeCostoDeProduccion vRpt = new dsrDetalleDeCostoDeProduccion();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsDetalleDeCostoDeProduccion.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados

    }
}
