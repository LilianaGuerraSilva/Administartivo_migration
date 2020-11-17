using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Brl.CajaChica;
namespace Galac.Adm.Rpt.CajaChica {

    public class clsReposicionesEntreFechas: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public string CodigoCtaBancariaCajaChica { get; set; }
        public bool ImprimeUna { get; set; }
        public bool UnaPaginaPorCajaChica { get; set; }
        public LibXmlMemInfo AppMemoryInfo { get; set; }
        public LibXmlMFC Mfc { get; set; }
        
        #endregion //Propiedades
        #region Constructores
        public clsReposicionesEntreFechas(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime initFechaInicial, DateTime initFechaFinal, bool initImprimeUna, string initCodigoCtaBancariaCajaChica, bool initUnaPaginaPorCajaChica)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {        
            FechaInicial = initFechaInicial;
            FechaFinal = initFechaFinal;
            ImprimeUna = initImprimeUna;
            AppMemoryInfo = initAppMemInfo;
            Mfc = initMfc;
            if (ImprimeUna) {
                CodigoCtaBancariaCajaChica = initCodigoCtaBancariaCajaChica;
            } else {
                CodigoCtaBancariaCajaChica = "";
            }
            UnaPaginaPorCajaChica = initUnaPaginaPorCajaChica;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrReposicionesEntreFechas().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsReposicionesEntreFechas.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - " +LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroRif"));
            vParams.Add("TituloInforme", vTitulo);        
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
            vParams.Add("UnaPaginaPorCajaChica", LibConvert.BoolToSN(UnaPaginaPorCajaChica));
            
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IRendicionInformes vRpt = new Galac.Adm.Brl.CajaChica.Reportes.clsRendicionRpt() as IRendicionInformes;
            Data = vRpt.BuildReposicionesEntreFechas(Mfc.GetInt("Compania"),FechaInicial, FechaFinal, ImprimeUna, CodigoCtaBancariaCajaChica);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrReposicionesEntreFechas vRpt = new dsrReposicionesEntreFechas();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsReposicionesEntreFechas.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsReposicionesEntreFechas

} //End of namespace Galac.Adm.Rpt.CajaChica

