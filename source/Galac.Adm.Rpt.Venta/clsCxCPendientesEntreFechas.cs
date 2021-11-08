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

    public class clsCxCPendientesEntreFechas: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        #region Codigo Ejemplo
        public string RpxName { get; set; }
        private DateTime FechaDesde { get; set; }
        private DateTime FechaHasta { get; set; }
        private Saw.Lib.eMonedaParaImpresion MonedaDelReporte { get; set; }
        private bool UsaContacto { get; set; }

        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsCxCPendientesEntreFechas(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaDesde, DateTime valFechaHasta, bool valUsaContacto)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            UsaContacto = valUsaContacto;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCxCPendientesEntreFechas().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("UsaContacto", LibConvert.BoolToSN(UsaContacto));
            vParams.Add("UsaContabilidad", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaModuloDeContabiliad"));
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("MonedaParaElReporte", LibConvert.EnumToDbValue((int) MonedaDelReporte));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICxCInformes vRpt = new Brl.Venta.Reportes.clsCxCRpt() as ICxCInformes;
            Data = vRpt.BuildCxCPendientesEntreFechas(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), MonedaDelReporte, FechaDesde, FechaHasta, UsaContacto);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCxCPendientesEntreFechas vRpt = new dsrCxCPendientesEntreFechas();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados

    } //End of class clsCxCPendientesEntreFechas

} //End of namespace Galac.Adm.Rpt.Venta

