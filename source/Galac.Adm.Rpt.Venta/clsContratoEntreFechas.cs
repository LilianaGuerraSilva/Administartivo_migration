using System.Collections.Generic;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using System;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.Venta {

    public class clsContratoEntreFechas : LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        public bool FiltrarPorStatus { get; }
        public bool FiltrarPorFechaFinal { get; }
        public DateTime FechaInicio { get; }
        public DateTime FechaFinal { get; }
        public eStatusContrato StatusContrato { get; set; }

        #endregion //Propiedades
        #region Constructores
        public clsContratoEntreFechas(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, bool initFiltrarPorStatus, bool initFiltrarPorFechaFinal, DateTime initFechaDeInicio, DateTime initFechaFinal, eStatusContrato initStatusContrato)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FiltrarPorStatus = initFiltrarPorStatus;
            FiltrarPorFechaFinal = initFiltrarPorFechaFinal;
            FechaInicio = initFechaDeInicio;
            FechaFinal = initFechaFinal;
            StatusContrato = initStatusContrato;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrContratoEntreFechas().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsContratoEntreFechas.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("RifCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("FechaInicialYFinal", "Del " + FechaInicio.ToShortDateString() + (FiltrarPorFechaFinal ? " al " + FechaFinal.ToShortDateString() : string.Empty));
            vParams.Add("TituloInforme", vTitulo);
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IContratoInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsContratoRpt() as IContratoInformes;
            Data = vRpt.BuildContratoEntreFechas(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FiltrarPorStatus, FiltrarPorFechaFinal, FechaInicio, FechaFinal, StatusContrato);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrContratoEntreFechas vRpt = new dsrContratoEntreFechas();
            if (LibDataTable.DataTableHasRows(Data)) {
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsContratoEntreFechas.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No se encontró información para mostrar", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados
    } //End of class clsContratoEntreFechas

} //End of namespace Galac.Adm.Rpt.Venta

