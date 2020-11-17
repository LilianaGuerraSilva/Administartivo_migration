using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.Venta {

    public class clsCobranzasEntreFechas:LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        #region Codigo Ejemplo

        public string RpxName { get; set; }
        private DateTime FechaDesde { get; set; }
        private DateTime FechaHasta { get; set; }
        private Saw.Lib.eMonedaParaImpresion MonedaDelReporte { get; set; }
        private Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio { get; set; }
        private string NombreCobrador { get; set; }
        private string NombreCliente { get; set; }
        private string NombreCuentaBancaria { get; set; }
        private eFiltrarCobranzasPor FiltrarCobranzasPor { get; set; }
        private bool AgruparPor { get; set; }
        private decimal TasaDeCambio { get; set; }
        private bool UsaVentasConIvaDiferidos { get; set; }
                
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsCobranzasEntreFechas(ePrintingDevice initPrintingDevice,eExportFileFormat initExportFileFormat,LibXmlMemInfo initAppMemInfo,LibXmlMFC initMfc,Saw.Lib.eMonedaParaImpresion valMonedaReporte,Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio,
            decimal valTasaDeCambio,DateTime valFechaDesde,DateTime valFechaHasta,string valNombreCobrador,string valNombreCliente,string valNombreCuentaBancaria,eFiltrarCobranzasPor valFiltrarCobranzasPor,bool valAgruparPor,string valRpxName, bool valUsaVentasConIvaDiferidos)
            : base(initPrintingDevice,initExportFileFormat,initAppMemInfo,initMfc) {
            #region Codigo Ejemplo
            FechaDesde = valFechaDesde;
            FechaHasta = valFechaHasta;
            MonedaDelReporte = valMonedaReporte;
            TipoTasaDeCambio = valTipoTasaDeCambio;
            NombreCliente = valNombreCliente;
            NombreCobrador = valNombreCobrador;
            NombreCuentaBancaria = valNombreCuentaBancaria;
            FiltrarCobranzasPor = valFiltrarCobranzasPor;
            AgruparPor = valAgruparPor;
            TasaDeCambio = valTasaDeCambio;
            RpxName = valRpxName;
            UsaVentasConIvaDiferidos = valUsaVentasConIvaDiferidos;
            #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCobranzasEntreFechas().ReportTitle(); }
        }

        public override Dictionary<string,string> GetConfigReportParameters() {
            string vTitulo = clsCobranzasEntreFechas.ReportName;
            if(UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string,string> vParams = new Dictionary<string,string>();
            vParams.Add("NombreCompania",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania","Nombre")+" - "+ LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania","NumeroDeRif"));
            vParams.Add("TituloInforme",vTitulo);
            vParams.Add("UsaContabilidad",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaModuloDeContabiliad"));
            vParams.Add("FechaInicialYFinal",string.Format("del {0} al {1}",LibConvert.ToStr(FechaDesde,"dd/MM/yyyy"),LibConvert.ToStr(FechaHasta,"dd/MM/yyyy")));
            vParams.Add("FiltrarPor",LibConvert.EnumToDbValue((int)FiltrarCobranzasPor));
            vParams.Add("AgruparPor",LibConvert.BoolToSN(AgruparPor));
            vParams.Add("CambioMoneda",LibConvert.EnumToDbValue((int)TipoTasaDeCambio));
            vParams.Add("MonedaParaElReporte",LibConvert.EnumToDbValue((int)MonedaDelReporte));
            return vParams;
        }

        public override void RunReport() {
            if(WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30,"Obteniendo datos...");
            ICobranzaInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsCobranzaRpt() as ICobranzaInformes;
            Data = vRpt.BuildCobranzasEntreFechas(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"),MonedaDelReporte,TipoTasaDeCambio,TasaDeCambio,FechaDesde,FechaHasta,NombreCobrador,NombreCliente,NombreCuentaBancaria,FiltrarCobranzasPor,AgruparPor,UsaVentasConIvaDiferidos);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90,"Configurando Informe...");
            Dictionary<string,string> vParams = GetConfigReportParameters();
            dsrCobranzasEntreFechas vRpt = new dsrCobranzasEntreFechas(true,RpxName);
            if(Data.Rows.Count >= 1) {
                if(vRpt.ConfigReport(Data,vParams)) {
                    LibReport.SendReportToDevice(vRpt,1,PrintingDevice,clsCobranzasEntreFechas.ReportName,true,ExportFileFormat,"",false);
                }
                WorkerReportProgress(100,"Finalizando...");
            } else {
                throw new GalacException("No se encontró información para imprimir",eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados
    } //End of class clsCobranzasEntreFechas
} //End of namespace Galac.Dbo.Rpt.Venta

