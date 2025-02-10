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
namespace Galac.Adm.Rpt.Venta {

    public class clsAuditoriaConfiguracionDeCaja: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsAuditoriaConfiguracionDeCaja(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime initFechaDesde, DateTime initFechaHasta)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = initFechaDesde;
            FechaHasta = initFechaHasta;

        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrAuditoriaConfiguracionDeCaja().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsAuditoriaConfiguracionDeCaja.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("{0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IAuditoriaConfiguracionInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsAuditoriaConfiguracionRpt() as IAuditoriaConfiguracionInformes;
            Data = vRpt.BuildAuditoriaConfiguracionDeCaja(Mfc.GetInt("Compania"), FechaDesde, FechaHasta);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrAuditoriaConfiguracionDeCaja vRpt = new dsrAuditoriaConfiguracionDeCaja();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsAuditoriaConfiguracionDeCaja.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsAuditoriaConfiguracionDeCaja

} //End of namespace Galac.Adm.Rpt.Venta

