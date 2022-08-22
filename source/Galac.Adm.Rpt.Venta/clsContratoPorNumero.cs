using System.Collections.Generic;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.Venta
{

    public class clsContratoPorNumero : LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        public string NumeroContrato { get; set; }

        #endregion //Propiedades
        #region Constructores
        public clsContratoPorNumero(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, string initNumeroContrato)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            NumeroContrato = initNumeroContrato;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrContratoPorNumero().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsContratoPorNumero.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("RifCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IContratoInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsContratoRpt() as IContratoInformes;
            Data = vRpt.BuildContratoPorNumero(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), NumeroContrato);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrContratoPorNumero vRpt = new dsrContratoPorNumero();
            if (LibDataTable.DataTableHasRows(Data)) {
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsContratoPorNumero.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No se encontró información para mostrar", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados
    } //End of class clsContratoPorNumero
} //End of namespace Galac.Adm.Rpt.Venta

