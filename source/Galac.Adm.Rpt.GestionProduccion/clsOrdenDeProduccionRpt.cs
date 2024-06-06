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

    public class clsOrdenDeProduccionRpt : LibRptBaseMfc {
        #region Propiedades
        public int ConsecutivoCompania { get; set; }

        public string CodigoOrden { get; set; }

        public eGeneradoPor GeneradoPor { get; set; }
        public eSeleccionarOrdenPor SeleccionarPor { get; set; }

        public DateTime FechaDesde { get; set;}

        public DateTime FechaHasta { get; set; }

        protected DataTable DataSalidas { get; set; }
        protected DataTable DataInsumos { get; set; }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsOrdenDeProduccionRpt(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, string iniCodigoOrden, eGeneradoPor iniGeneradoPor, DateTime iniFechaDesde, DateTime iniFechaHasta, eSeleccionarOrdenPor iniSeleccionarOrdenPor)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            CodigoOrden = iniCodigoOrden;
            GeneradoPor = iniGeneradoPor;
            SeleccionarPor = iniSeleccionarOrdenPor;
            FechaDesde = iniFechaDesde;
            FechaHasta = iniFechaHasta;
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
            FechaDesde = initFechaDesde;
            FechaHasta = initFechaHasta;
        */
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrOrdenDeProduccion().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsOrdenDeProduccionRpt.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
           // vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("TituloInforme", vTitulo);
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
            vParams.Add("FechaInicialYFinal", string.Format("{0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
        */
            #endregion //Codigo Ejemplo
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IOrdenDeProduccionInformes vRpt = new Galac.Adm.Brl.GestionProduccion.Reportes.clsOrdenDeProduccionRpt() as IOrdenDeProduccionInformes;
            DataSalidas = vRpt.BuildPrecierreOrdendeProduccionSalidas(ConsecutivoCompania, CodigoOrden, FechaDesde, FechaHasta, GeneradoPor);
            DataInsumos = vRpt.BuildPrecierreOrdendeProduccionInsumos(ConsecutivoCompania, CodigoOrden, FechaDesde, FechaHasta, GeneradoPor);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrOrdenDeProduccion vRpt = new dsrOrdenDeProduccion();
            if (vRpt.ConfigReport(DataSalidas, DataInsumos, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsOrdenDeProduccionRpt.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionRpt

} //End of namespace Galac.Adm.Rpt. GestionProduccion

