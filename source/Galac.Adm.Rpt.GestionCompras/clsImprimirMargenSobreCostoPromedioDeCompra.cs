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
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Rpt.GestionCompras {

    public class clsImprimirMargenSobreCostoPromedioDeCompra: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }

        public int ConsecutivoCompra {
            get;
            set;
        }

        public eNivelDePrecio NivelDePrecio {
            get;
            set;
        }

        public eReporteCostoDeCompras LineasDeProductoCantidadAImprimir {
            get;
            set;
        }

        public string CodigoProducto {
            get;
            set;
        }

        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsImprimirMargenSobreCostoPromedioDeCompra(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
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
            get {
                return new dsrImprimirMargenSobreCostoPromedioDeCompra().ReportTitle();
            }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsImprimirMargenSobreCostoPromedioDeCompra.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            //vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
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
            ICompraInformes vRpt = new Galac.Adm.Brl.GestionCompras.Reportes.clsCompraRpt() as ICompraInformes;
            Data = vRpt.BuildImprimirMargenSobreCostoPromedioDeCompra(Mfc.GetInt("Compania"), NivelDePrecio, LineasDeProductoCantidadAImprimir, CodigoProducto);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrImprimirMargenSobreCostoPromedioDeCompra vRpt = new dsrImprimirMargenSobreCostoPromedioDeCompra();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsImprimirMargenSobreCostoPromedioDeCompra.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsImprimirCostoDeCompraEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

