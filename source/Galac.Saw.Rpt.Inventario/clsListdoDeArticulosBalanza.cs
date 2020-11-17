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
using LibGalac.Aos.Catching;

namespace Galac.Saw.Rpt.Inventario {

    public class clsListdoDeArticulosBalanza: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public string _LineaDeProducto { get; set; }
        public bool _FiltrarPorLineaDeProducto { get; set; }
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsListdoDeArticulosBalanza(ePrintingDevice initPrintingDevice,eExportFileFormat initExportFileFormat,LibXmlMemInfo initAppMemInfo,LibXmlMFC initMfc,string valLineaDeProducto,bool valFiltrarPorLineaDeProducto)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
                _LineaDeProducto = valLineaDeProducto;
                _FiltrarPorLineaDeProducto= valFiltrarPorLineaDeProducto; 
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
            get { return new dsrListadoDeArticulosBalanza().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsListdoDeArticulosBalanza.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania","Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FiltrarPorLineaDeProducto",LibConvert.BoolToSN(_FiltrarPorLineaDeProducto));
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
            Data = vRpt.BuildListdoDeArticulosBalanza(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"),_LineaDeProducto,_FiltrarPorLineaDeProducto);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(30, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrListadoDeArticulosBalanza vRpt = new dsrListadoDeArticulosBalanza();
            if(Data.Rows.Count >= 1) {
                if(vRpt.ConfigReport(Data,vParams)) {                    
                    LibReport.SendReportToDevice(vRpt,1,PrintingDevice,clsListdoDeArticulosBalanza.ReportName,true,ExportFileFormat,"",false);
                }
                WorkerReportProgress(100,"Finalizando...");
            } else {
                throw new GalacException("No Existen datos para mostrar",eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados


    } //End of class clsListarArticulosBalanza

} //End of namespace Galac.Saw.Rpt.Inventario

