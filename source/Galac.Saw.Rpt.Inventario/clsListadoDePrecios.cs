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
namespace Galac.Saw.Rpt.Inventario {

    public class clsListadoDePrecios: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public string LineaDeProducto { get; set; }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsListadoDePrecios(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, string valLineaDeProducto)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            LineaDeProducto = valLineaDeProducto;
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
            get { return new dsrListadoDePrecios().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsListadoDePrecios.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
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
            IArticuloInventarioInformes vRpt = new Galac.Saw.Brl.Inventario.Reportes.clsArticuloInventarioRpt() as IArticuloInventarioInformes;
            Data = vRpt.BuildListadoDePrecios(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LineaDeProducto);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrListadoDePrecios vRpt = new dsrListadoDePrecios();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsListadoDePrecios.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsListadoDePrecios

} //End of namespace Galac.Saw.Rpt.Inventario

