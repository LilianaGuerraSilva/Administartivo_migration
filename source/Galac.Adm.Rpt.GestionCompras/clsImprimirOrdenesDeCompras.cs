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
using Galac.Adm.Rpt.GestionCompras;
namespace Galac.Adm.Rpt.GestionCompras {

    public class clsImprimirOrdenesDeCompras: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data {
            get;
            set;
        }

        public int ConsecutivoCompania {
            get;
            set;
        }

        public DateTime FechaInicial {
            get;
            set;
        }

        public DateTime FechaFinal {
            get;
            set;
        }

        public bool ImprimirRenglones {
            get;
            set;
        }

        public eStatusDeOrdenDeCompra StatusDeOrdenDeCompra {
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
        public clsImprimirOrdenesDeCompras(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
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
                return new dsrImprimirOrdenesDeCompras().ReportTitle();
            }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsImprimirOrdenesDeCompras.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF") + " - " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("ImprimirRenglones", LibConvert.BoolToSN(ImprimirRenglones));
            

        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            vParams.Add("FechaInicialYFinal", string.Format("{0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
        */
        #endregion //Codigo Ejemplo
            return vParams;
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrImprimirOrdenesDeCompras vRpt = new dsrImprimirOrdenesDeCompras();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsImprimirOrdenesDeCompras.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IOrdenDeCompraInformes vRpt = new Galac.Adm.Brl.GestionCompras.Reportes.clsOrdenDeCompraRpt() as IOrdenDeCompraInformes;
            Data = vRpt.BuildOrdenesDeCompras(Mfc.GetInt("Compania"), FechaInicial, FechaFinal, StatusDeOrdenDeCompra);
        }
        #endregion //Metodos Generados


    } //End of class clsImprimirCompra

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

