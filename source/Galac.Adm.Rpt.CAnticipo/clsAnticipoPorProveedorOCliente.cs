using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.CAnticipo;
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.CAnticipo {

    public class clsAnticipoPorProveedorOCliente: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }

        private eStatusAnticipo StatusAnticipo { get; set; } 
        private eCantidadAImprimir CantidadAImprimir{ get; set; }
        private string CodigoClienteProveedor{ get; set; }
        private bool OrdenamientoClienteStatus{ get; set; }
        private eMonedaDelInformeMM MonedaDelInformeMM{ get; set; } 
        private bool ProveedorCliente{ get; set; }

        #endregion //Propiedades
        #region Constructores
        public clsAnticipoPorProveedorOCliente(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, eStatusAnticipo initStatusAnticipo, eCantidadAImprimir initCantidadAImprimir, string initCodigoClienteProveedor, bool initOrdenamientoClienteStatus, eMonedaDelInformeMM initMonedaDelInformeMM, bool initProveedorCliente)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            StatusAnticipo = initStatusAnticipo;
            CantidadAImprimir = initCantidadAImprimir;
            CodigoClienteProveedor = initCodigoClienteProveedor;
            OrdenamientoClienteStatus = initOrdenamientoClienteStatus;
            MonedaDelInformeMM = initMonedaDelInformeMM;
            ProveedorCliente = initProveedorCliente;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get {
                return null;
            }
           // get { return new dsrAnticipoPorProveedorOCliente().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsAnticipoPorProveedorOCliente.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania",  LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            vParams.Add("FechaInicialYFinal", string.Format("{0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
        */
        #endregion //Codigo Ejemplo
            return vParams;
        }
        //eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoClienteStatus, eMonedaDelInformeMM valMonedaDelInformeMM, bool valProveedorCliente
        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IAnticipoInformes vRpt = new Galac.Adm.Brl.CAnticipo.Reportes.clsAnticipoRpt() as IAnticipoInformes;
            Data = vRpt.BuildAnticipoPorProveedorOCliente(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), StatusAnticipo, CantidadAImprimir, CodigoClienteProveedor, OrdenamientoClienteStatus, MonedaDelInformeMM, ProveedorCliente);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            //dsrAnticipoPorProveedorOCliente vRpt = new dsrAnticipoPorProveedorOCliente();
            //if (vRpt.ConfigReport(Data, vParams)) {
            //  //  LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsAnticipoPorProveedorOCliente.ReportName, true, ExportFileFormat, "", false);
            //}
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsAnticipoPorProveedorOCliente

} //End of namespace Galac.Dbo.Rpt.CAnticipo

