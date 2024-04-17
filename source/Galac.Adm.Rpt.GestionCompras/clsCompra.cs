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
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.GestionCompras {

    public class clsCompra : LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }

        public int ConsecutivoCompra { get; set; }

        public string NumeroDeOrdenDeCompra { get; set; }
        #region Codigo Ejemplo
        
        #endregion //Propiedades
        #region Constructores
        public clsCompra(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            #region Codigo Ejemplo                   
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCompra().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCompra.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("NumeroDeOrdenDeCompra", NumeroDeOrdenDeCompra);



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
            Data = vRpt.BuildCompra(Mfc.GetInt("Compania"), ConsecutivoCompra);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            Galac.Saw.Lib.clsUtilRpt vUtil = new Galac.Saw.Lib.clsUtilRpt();
            string vRpxCompra = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "NombrePlantillaCompra");
            if (vUtil.EsFormatoRpxValidoParaAOS(vRpxCompra)) {
                dsrCompra vRpt = new dsrCompra(UseExternalRpx, vRpxCompra);
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCompra.ReportName, true, ExportFileFormat, "", false);
                    WorkerReportProgress(100, "Finalizando...");
                }
            } else {
                StringBuilder vMensaje = new StringBuilder();
                vMensaje.AppendLine("No se puede imprimir la compra, hay un problema con el formato del reporte,");
                vMensaje.AppendLine("verifique que el archivo .RPX se encuentre en el directorio de reportes y tenga un formato válido");
                throw new GalacException(vMensaje.ToString(), eExceptionManagementType.Validation);
            }
        }
        #endregion //Metodos Generados


    } //End of class clsDiarioCompra

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

