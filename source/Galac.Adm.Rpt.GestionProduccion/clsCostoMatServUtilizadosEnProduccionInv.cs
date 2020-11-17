using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.GestionProduccion;
namespace Galac.Adm.Rpt. GestionProduccion {

    public class clsCostoMatServUtilizadosEnProduccionInv: LibRptBaseMfc {
        #region Propiedades
        public int ConsecutivoCompania { get; set; }
        public string CodigoOrden { get; set; }

        public eGeneradoPor GeneradoPor { get; set; }

        protected DataTable Data { get; set; }
        #region Codigo Ejemplo
        public DateTime FechaInicial { get; set; }

        public DateTime FechaFinal { get; set; }

        
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsCostoMatServUtilizadosEnProduccionInv(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime iniFechaInicial, DateTime iniFechaFinal, string iniCodigoOrden, eGeneradoPor iniGeneradoPor)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            ConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            FechaInicial = iniFechaInicial;
            FechaFinal = iniFechaFinal;
            CodigoOrden = iniCodigoOrden;
            GeneradoPor = iniGeneradoPor;
           
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
            get { return new dsrCostoMatServUtilizadosEnProduccionInv().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCostoMatServUtilizadosEnProduccionInv.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("del {0} al {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));

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
            IOrdenDeProduccionInformes vRpt = new Galac.Adm.Brl. GestionProduccion.Reportes.clsOrdenDeProduccionRpt() as IOrdenDeProduccionInformes;
            Data = vRpt.BuildCostoMatServUtilizadosEnProduccionInv(ConsecutivoCompania, FechaInicial, FechaFinal, CodigoOrden, GeneradoPor);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCostoMatServUtilizadosEnProduccionInv vRpt = new dsrCostoMatServUtilizadosEnProduccionInv();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCostoMatServUtilizadosEnProduccionInv.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados
    } //End of class clsCostoMatServUtilizadosEnProduccionInv

} //End of namespace Galac.Adm.Rpt. GestionProduccion

