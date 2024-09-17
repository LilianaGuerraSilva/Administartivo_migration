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

    public class clsLoteDeInventarioVencidos : LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        private string LineaDeProducto { get; set; }
        private string CodigoArticulo { get; set; }
        private eCantidadAImprimirArticulo CantidadAImprimirArticulo { get; set; }
        private eOrdenarFecha OrdenarFecha { get; set; }

        #endregion //Propiedades
        #region Constructores
        public clsLoteDeInventarioVencidos(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, string initLineaDeProducto, string initCodigoArticulo, eCantidadAImprimirArticulo initCantidadAImprimirArticulo, eOrdenarFecha initOrdenarFecha)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            LineaDeProducto = initLineaDeProducto;
            CodigoArticulo = initCodigoArticulo;
            OrdenarFecha = initOrdenarFecha;
            CantidadAImprimirArticulo = initCantidadAImprimirArticulo;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrLoteDeInventarioVencidos().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsLoteDeInventarioVencidos.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("CantidadAImprimir", LibConvert.EnumToDbValue((int)CantidadAImprimirArticulo));            
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ILoteDeInventarioInformes vRpt = new Galac.Saw.Brl.Inventario.Reportes.clsLoteDeInventarioRpt() as ILoteDeInventarioInformes;
            Data = vRpt.BuildLoteDeInventarioVencidos(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LineaDeProducto, CodigoArticulo, OrdenarFecha);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrLoteDeInventarioVencidos vRpt = new dsrLoteDeInventarioVencidos();
            if (Data.Rows.Count >= 1) {
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsLoteDeInventarioVencidos.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No Existen datos para mostrar", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados

    } //End of class clsLoteDeInventarioVencidos

} //End of namespace Galac.Saw.Rpt.Inventario

