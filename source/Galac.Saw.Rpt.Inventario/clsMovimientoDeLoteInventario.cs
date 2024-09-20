using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Catching;

namespace Galac.Saw.Rpt.Inventario {

    public class clsMovimientoDeLoteInventario : LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        private string CodigoArticulo { get; set; }
        private string CodigoLote { get; set; }
        private DateTime FechaInicial { get; set; }
        private DateTime FechaFinal { get; set; }

        #endregion //Propiedades
        #region Constructores
        public clsMovimientoDeLoteInventario(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, string initCodigoArticulo, string initCodigoLote, DateTime initFechaInicial, DateTime initFechaFinal)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            CodigoArticulo = initCodigoArticulo;
            CodigoLote = initCodigoLote;
            FechaInicial = initFechaInicial;
            FechaFinal = initFechaFinal;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrMovimientoDeLoteInventario().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsMovimientoDeLoteInventario.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }            
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);            
            vParams.Add("FechaInicialYFinal", string.Format("{0} al {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ILoteDeInventarioInformes vRpt = new Galac.Saw.Brl.Inventario.Reportes.clsLoteDeInventarioRpt() as ILoteDeInventarioInformes;
            Data = vRpt.BuildMovimientoDeLoteInventario(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), CodigoLote, CodigoArticulo, FechaInicial, FechaFinal);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrMovimientoDeLoteInventario vRpt = new dsrMovimientoDeLoteInventario();
            if (Data.Rows.Count >= 1) {
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsMovimientoDeLoteInventario.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No Existen datos para mostrar", eExceptionManagementType.Alert);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados

    } //End of class clsMovimientoDeLoteInventario

} //End of namespace Galac.Saw.Rpt.Inventario

