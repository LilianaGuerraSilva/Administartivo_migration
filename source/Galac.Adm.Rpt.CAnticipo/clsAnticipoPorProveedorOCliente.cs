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
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.CAnticipo {

    public class clsAnticipoPorProveedorOCliente : LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data {
            get; set;
        }
        private eStatusAnticipo StatusAnticipo {
            get; set;
        }
        private eCantidadAImprimir CantidadAImprimir {
            get; set;
        }
        private string CodigoClienteProveedor {
            get; set;
        }
        private bool OrdenamientoPorStatus {
            get; set;
        }
        eTasaDeCambioParaImpresion TasaCambio {
            get; set;
        }
        private eMonedaDelInformeMM MonedaDelInformeMM {
            get; set;
        }
        private bool EsCliente {
            get; set;
        }
        string Moneda {
            get; set;
        }

        #endregion //Propiedades
        #region Constructores
        public clsAnticipoPorProveedorOCliente(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, eStatusAnticipo initStatusAnticipo, eCantidadAImprimir initCantidadAImprimir, string initCodigoClienteProveedor, bool initOrdenamientoPorStatus, eMonedaDelInformeMM initMonedaDelInformeMM, eTasaDeCambioParaImpresion initTasaDeCambio, string initMoneda, bool initEsCliente)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            StatusAnticipo = initStatusAnticipo;
            CantidadAImprimir = initCantidadAImprimir;
            CodigoClienteProveedor = initCodigoClienteProveedor;
            OrdenamientoPorStatus = initOrdenamientoPorStatus;
            TasaCambio = initTasaDeCambio;
            MonedaDelInformeMM = initMonedaDelInformeMM;
            EsCliente = initEsCliente;
            Moneda = initMoneda;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get {
                return new dsrAnticipoPorProveedorOCliente().ReportTitle();
            }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsAnticipoPorProveedorOCliente.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("Nombre", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre") + " - RIF " + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF"));
            vParams.Add("TituloInforme", vTitulo);
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IAnticipoInformes vRpt = new Galac.Adm.Brl.CAnticipo.Reportes.clsAnticipoRpt() as IAnticipoInformes;
            string vCodigoMoneda = LibString.Trim(LibString.Mid(Moneda, 1, LibString.InStr(Moneda, ")") - 1));
            vCodigoMoneda = LibString.IsNullOrEmpty(vCodigoMoneda, true) ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera") : vCodigoMoneda;
            string vNombreMoneda = LibString.Trim(LibString.Mid(Moneda, 1 + LibString.InStr(Moneda, ")")));
            Data = vRpt.BuildAnticipoPorProveedorOCliente(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), StatusAnticipo, CantidadAImprimir, CodigoClienteProveedor, OrdenamientoPorStatus, MonedaDelInformeMM, EsCliente, TasaCambio, vCodigoMoneda, vNombreMoneda);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrAnticipoPorProveedorOCliente vRpt = new dsrAnticipoPorProveedorOCliente();
            if (Data.Rows.Count < 1) {
                throw new GalacException("No existen datos para mostrar", eExceptionManagementType.Alert);
            } else {
                if (vRpt.ConfigReport(Data, vParams, MonedaDelInformeMM, Moneda, TasaCambio, EsCliente)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsAnticipoPorProveedorOCliente.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            }
        }
        #endregion //Metodos Generados
    } //End of class clsAnticipoPorProveedorOCliente
} //End of namespace Galac.Dbo.Rpt.CAnticipo

