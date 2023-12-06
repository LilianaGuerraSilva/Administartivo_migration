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

namespace Galac.Adm.Rpt.GestionCompras {

    public class clsCuentasPorPagarEntreFechas: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsCuentasPorPagarEntreFechas(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
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
            get { return new dsrCuentasPorPagarEntreFechas().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCuentasPorPagarEntreFechas.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            vParams.Add("FechaInicialYFinal", string.Format("{0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
        */
        #endregion //Codigo Ejemplo
            return vParams;
        }

        public override void RunReport() {
            //if (WorkerCancellPending()) {
            //    return;
            //}
            //WorkerReportProgress(30, "Obteniendo datos...");
            //ICxPInformes vRpt = new Galac.Dbo.Brl.ComponenteNoEspecificado.Reportes.clsCxPRpt() as ICxPInformes;
            //Data = vRpt.BuildCuentasPorPagarEntreFechas();
        }

        public override void SendReportToDevice() {
            //WorkerReportProgress(90, "Configurando Informe...");
            //Dictionary<string, string> vParams = GetConfigReportParameters();
            //dsrCuentasPorPagarEntreFechas vRpt = new dsrCuentasPorPagarEntreFechas();
            //if (vRpt.ConfigReport(Data, vParams)) {
            //    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCuentasPorPagarEntreFechas.ReportName, true, ExportFileFormat, "", false);
            //}
            //WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsCuentasPorPagarEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

