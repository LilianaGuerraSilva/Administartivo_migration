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
using Galac.Saw.Lib;

namespace Galac.Saw.Rpt.Inventario {

    public class clsValoracionDeInventario:LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        private string CodigoDesde { get; set; }
        private string CodigoHasta { get; set; }
        private string LineaDeProducto { get; set; }
        private string MonedaDelReporte { get; set; }
        private decimal CambioMoneda { get; set; }
        private Saw.Lib.eMonedaPresentacionDeReporte TipoDeMonedaDelReporte { get; set; }
        private bool UsaPrecioConIva { get; set; }
        public string RpxName { get; set; }
        #region Codigo Ejemplo        
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsValoracionDeInventario(ePrintingDevice initPrintingDevice,eExportFileFormat initExportFileFormat,LibXmlMemInfo initAppMemInfo,LibXmlMFC initMfc,string valCodigoDesde,string valCodigoHasta,string valLineaDeProducto,decimal valCambioMoneda,Saw.Lib.eMonedaPresentacionDeReporte valTipoDeMonedaDelReporte,bool valUsaPrecioConIva,string valMonedaDelReporte,string valRpxName)
            : base(initPrintingDevice,initExportFileFormat,initAppMemInfo,initMfc) {
            CodigoDesde = valCodigoDesde;
            CodigoHasta = valCodigoHasta;
            LineaDeProducto = valLineaDeProducto;
            CambioMoneda = valCambioMoneda;
            TipoDeMonedaDelReporte = valTipoDeMonedaDelReporte;
            MonedaDelReporte = valMonedaDelReporte;
            UsaPrecioConIva = valUsaPrecioConIva;
            RpxName = valRpxName;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrValoracionDeInventario().ReportTitle(); }
        }

        public override Dictionary<string,string> GetConfigReportParameters() {
            string vTitulo = clsValoracionDeInventario.ReportName;
            if(UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string,string> vParams = new Dictionary<string,string>();
            string vDecimalesparaReporte = (TipoDeMonedaDelReporte != Lib.eMonedaPresentacionDeReporte.EnAmbasMonedas ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","CantidadDeDecimales") : "2");
            vParams.Add("NombreCompania",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania","Nombre") + " - RIF" + LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania","NumeroDeRif"));
            vParams.Add("TituloInforme",vTitulo);
            vParams.Add("TipoDeMonedaDelReporte",LibConvert.EnumToDbValue((int)TipoDeMonedaDelReporte));
            vParams.Add("DecimalesParaReporte",vDecimalesparaReporte);
            vParams.Add("CodigoDesdeHasta",String.Format("DEL {0} AL {1}",CodigoDesde,CodigoHasta));
            vParams.Add("UsaPrecioConIva",LibConvert.BoolToSN(UsaPrecioConIva));
            vParams.Add("MonedaDelReporte",MonedaDelReporte);
            vParams.Add("MonedaLocal",new clsNoComunSaw().InstanceMonedaLocalActual.NombreMoneda(LibDate.Today()));
            return vParams;
        }

        public override void RunReport() {
            if(WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30,"Obteniendo datos...");
            IArticuloInventarioInformes vRpt = new Galac.Saw.Brl.Inventario.Reportes.clsArticuloInventarioRpt() as IArticuloInventarioInformes;
            Data = vRpt.BuildValoracionDeInventario(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"),CodigoDesde,CodigoHasta,LineaDeProducto,CambioMoneda,UsaPrecioConIva);
        }

        public override void SendReportToDevice() {
            try {
                WorkerReportProgress(90,"Configurando Informe...");
                Dictionary<string,string> vParams = GetConfigReportParameters();
                dsrValoracionDeInventario vRpt = new dsrValoracionDeInventario(true,RpxName);
                if(Data.Rows.Count >= 1) {
                    if(vRpt.ConfigReport(Data,vParams)) {
                        LibReport.SendReportToDevice(vRpt,1,PrintingDevice,clsValoracionDeInventario.ReportName,true,ExportFileFormat,"",false);
                    }
                    WorkerReportProgress(100,"Finalizando...");
                } else {
                    throw new GalacException("No Existen datos para mostrar",eExceptionManagementType.Alert);
                }
            } catch(Exception vex) {
                if(LibString.S1IsInS2("El proceso no puede obtener acceso al archivo",vex.Message) || LibString.S1IsInS2("Error creating File",vex.Message)) {
                    throw new LibGalac.Aos.Catching.GalacException("El proceso no puede obtener acceso al archivo por que esta siendo usado por otro proceso, por favor cierre el archivo e intente generar el reporte nuevamente",eExceptionManagementType.Alert);
                } else {
                    throw vex;
                }
            }
        }
        #endregion //Metodos Generados
    } //End of class clsValoracionDeInventario
} //End of namespace Galac.Saw.Rpt.Inventario

