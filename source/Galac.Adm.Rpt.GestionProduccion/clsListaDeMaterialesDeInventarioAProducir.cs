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
namespace Galac.Adm.Rpt.GestionProduccion {

    public class clsListaDeMaterialesDeInventarioAProducir: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }

        public int ConsecutivoCompania { get; set; }

        public string CodigoInventarioAProducir { get; set; }

        public eCantidadAImprimir CantidadAImprimir { get; set; }

        public decimal CantidadAProducir { get; set; }

        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsListaDeMaterialesDeInventarioAProducir(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc,
                string iniCodigoInventarioAProducir, eCantidadAImprimir iniCantidadAImprimir, decimal iniCantidadAProducir)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            ConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");                                
            CodigoInventarioAProducir = iniCodigoInventarioAProducir;
            CantidadAImprimir = iniCantidadAImprimir;
            CantidadAProducir = iniCantidadAProducir;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrListaDeMaterialesDeInventarioAProducir().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsListaDeMaterialesDeInventarioAProducir.ReportName;
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
            IListaDeMaterialesInformes vRpt = new Galac.Adm.Brl.GestionProduccion.Reportes.clsListaDeMaterialesRpt() as IListaDeMaterialesInformes;
            Data = vRpt.BuildListaDeMaterialesDeInventarioAProducir(ConsecutivoCompania, CodigoInventarioAProducir, CantidadAImprimir, CantidadAProducir);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrListaDeMaterialesDeInventarioAProducir vRpt = new dsrListaDeMaterialesDeInventarioAProducir();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsListaDeMaterialesDeInventarioAProducir.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsListaDeMaterialesDeInventarioAProducir

} //End of namespace Galac.Adm.Rpt.GestionProduccion

