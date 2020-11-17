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

    public class clsImprimirCotizacionOrdenDeCompra: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public string NumeroCotizacion {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores
        public clsImprimirCotizacionOrdenDeCompra(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
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
            get { return new dsrImprimirCotizacionOrdenDeCompra().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsImprimirCotizacionOrdenDeCompra.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("NumeroCotizacion", NumeroCotizacion);
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
            Data = vRpt.BuildImprimirCotizacionOrdenDeCompra(Mfc.GetInt("Compania"), NumeroCotizacion);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrImprimirCotizacionOrdenDeCompra vRpt = new dsrImprimirCotizacionOrdenDeCompra();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsImprimirCotizacionOrdenDeCompra.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsImprimirCotizacionOrdenDeCompra

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

