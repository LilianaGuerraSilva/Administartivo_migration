using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Rpt.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Lib;
namespace Galac.Adm.Rpt.GestionCompras
{

    public class clsOrdenDeCompra: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }

        public int ConsecutivoCompra{get; set;}
        public eTipoCompra TipoCompra { get; set; }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsOrdenDeCompra(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
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
            get { return new dsrOrdenDeCompra().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsOrdenDeCompra.ReportName;
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
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IOrdenDeCompraInformes vRpt = new Galac.Adm.Brl.GestionCompras.Reportes.clsOrdenDeCompraRpt() as IOrdenDeCompraInformes;
            Data = vRpt.BuildCompra(Mfc.GetInt("Compania"), ConsecutivoCompra);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            string vRpx = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","NombrePlantillaOrdenDeCompra");
            dsrOrdenDeCompra vRpt;
            if (LibText.IsNullOrEmpty(vRpx)) {
                vRpt = new dsrOrdenDeCompra(false, "", (TipoCompra == eTipoCompra.Importacion));
            } else {
                vRpt = new dsrOrdenDeCompra(true, vRpx, (TipoCompra == eTipoCompra.Importacion));
            }
            
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCompra.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeCompra

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

