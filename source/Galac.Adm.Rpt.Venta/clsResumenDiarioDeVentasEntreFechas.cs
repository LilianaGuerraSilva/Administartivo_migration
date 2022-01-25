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
using LibGalac.Aos.Dal;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.Venta {

    public class clsResumenDiarioDeVentasEntreFechas: LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        public string RpxName { get; set; }
        private DateTime FechaDesde { get; set; }
        private DateTime FechaHasta { get; set; }
        private bool AgruparPorMaquinaFiscal { get; set; }
        private string ConsecutivoMaquinaFiscal { get; set; }
        #endregion //Propiedades

        #region Constructores
        public clsResumenDiarioDeVentasEntreFechas(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaDesde, DateTime valFechaHasta, bool valAgruparPorMaquinaFiscal, string valConsecutivoMaquinaFiscal)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            AgruparPorMaquinaFiscal = valAgruparPorMaquinaFiscal;
            ConsecutivoMaquinaFiscal = valConsecutivoMaquinaFiscal;
        }
        #endregion //Constructores

        #region Metodos Generados
        public static string ReportName {
            get { return new dsrResumenDiarioDeVentasEntreFechas().ReportTitle(); }
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
            vParams.Add("AgruparPorMaquinaFiscal", LibConvert.BoolToSN(AgruparPorMaquinaFiscal));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IResumenDiarioDeVentasInformes vRpt = new Brl.Venta.Reportes.clsResumenDiarioDeVentasRpt();
            Data = vRpt.BuildResumenDiarioDeVentasEntreFechas(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaDesde, FechaHasta, AgruparPorMaquinaFiscal, ConsecutivoMaquinaFiscal);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();

            if (!LibDataTable.DataTableHasRows(Data)) {
                throw new GalacException("No se encontró información para imprimir", eExceptionManagementType.Alert);
            }

            if (AgruparPorMaquinaFiscal) {
                dsrResumenDiarioDeVentasEntreFechasPorMaquinaFiscal vRpt = new dsrResumenDiarioDeVentasEntreFechasPorMaquinaFiscal();
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
                }
            } else {
                dsrResumenDiarioDeVentasEntreFechas vRpt = new dsrResumenDiarioDeVentasEntreFechas();
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
                }
            }
            
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados

    } //End of class clsResumenDiarioDeVentasEntreFechas

} //End of namespace Galac.Adm.Rpt.Venta

