using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Rpt.Venta {

    public partial class dsrCobranzasEntreFechas:DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores

        public dsrCobranzasEntreFechas()
            : this(false,string.Empty) {
        }

        public dsrCobranzasEntreFechas(bool initUseExternalRpx,string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if(_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Cobranzas Entre Fechas";
        }
        public bool ConfigReport(DataTable valDataSource,Dictionary<string,string> valParameters) {
            Saw.Lib.clsUtilRpt UtiltRpt = new Saw.Lib.clsUtilRpt();
            bool vUsaContabilidad = LibConvert.SNToBool(valParameters["UsaContabilidad"]);
            eFiltrarCobranzasPor vFiltrarCobranzasPor = (eFiltrarCobranzasPor)LibConvert.DbValueToEnum(valParameters["FiltrarPor"]);
            Saw.Lib.eTasaDeCambioParaImpresion vTasaDeCambio = (Saw.Lib.eTasaDeCambioParaImpresion)LibConvert.DbValueToEnum(valParameters["CambioMoneda"]);
            Saw.Lib.eMonedaParaImpresion vMonedaParaImpresion = ((Saw.Lib.eMonedaParaImpresion)LibConvert.DbValueToEnum(valParameters["MonedaParaElReporte"]));
            bool vEsAgrupado = LibConvert.SNToBool(valParameters["AgruparPor"]);
            string vNombreCompania = valParameters["NombreCompania"];
            string vMensajeCambio = "";
            if(_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName,ReportTitle(),false,LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if(!LibString.IsNullOrEmpty(vRpxPath,true)) {
                    LibReport.LoadLayout(this,vRpxPath);
                }
            }
            if(LibReport.ConfigDataSource(this,valDataSource)) {
                LibReport.ConfigFieldStr(this,"txtCompania",vNombreCompania,string.Empty);
                LibReport.ConfigLabel(this,"lblTituloDelReporte",ReportTitle() + " - " + vFiltrarCobranzasPor.GetDescription());
                LibReport.ConfigLabel(this,"lblFechaInicialYFinal",valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this,"lblFechaYHoraDeEmision",LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigLabel(this,"lblFechaYHoraDeEmision",LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigFieldStr(this,"txtNumeroDePagina",LibConvert.ToStr(PageNumber),string.Empty);
                LibReport.ConfigHeader(this,"txtCompania","lblFechaYHoraDeEmision","lblTituloDelReporte","txtNumeroDePagina","lblFechaInicialYFinal",LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber,LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this,"txtMonedaDeCobro",string.Empty,"MonedaCobro");
                LibReport.ConfigFieldDate(this,"txtFecha",string.Empty,"Fecha","dd/MM/yyyy");
                LibReport.ConfigFieldStr(this,"txtNumeroCobranza",string.Empty,"Numero");
                LibReport.ConfigFieldStr(this,"txtNombreCliente",string.Empty,"NombreCliente");
                LibReport.ConfigFieldDec(this,"txtCambio",string.Empty,"Cambio");
                LibReport.ConfigFieldDec(this,"txtTotalCobradoRenglon",string.Empty,"TotalCobrado");
                LibReport.ConfigFieldStr(this,"txtCuentaBancaria",string.Empty,"NombreCuenta");
                LibReport.ConfigFieldStr(this,"txtStatusAnulada",string.Empty,"Status");

                switch(vFiltrarCobranzasPor) {
                case eFiltrarCobranzasPor.Cobrador:
                    LibReport.ConfigFieldStr(this,"txtVar",string.Empty,"NombreVendedor");
                    LibReport.ConfigLabel(this,"lblVar","Cobrador");
                    break;
                case eFiltrarCobranzasPor.Cliente:
                    LibReport.ConfigFieldStr(this,"txtVar",string.Empty,"CodigoCliente");
                    LibReport.ConfigLabel(this,"lblVar","Código Cliente");
                    break;
                case eFiltrarCobranzasPor.CuentaBancaria:
                    LibReport.ConfigFieldStr(this,"txtVar",string.Empty,"NombreCuenta");
                    LibReport.ConfigLabel(this,"lblVar","Cuenta Bancaria");
                    break;
                }
                if(vUsaContabilidad) {
                    LibReport.ConfigFieldStr(this,"txtNComprobante",string.Empty,"NumeroComprobante");
                    LibReport.ConfigLabel(this,"lbNComprobante","Comprobante");
                } else {
                    LibReport.ChangeControlVisibility(this,"txtNComprobante",true,false);
                    LibReport.ChangeControlVisibility(this,"lbNComprobante",true,false);
                }

                if(vMonedaParaImpresion.GetDescription() != UtiltRpt.MensajesDeMonedaParaInformes(vTasaDeCambio)) {
                    vMensajeCambio = new Saw.Lib.clsUtilRpt().MensajesDeMonedaParaInformes(vTasaDeCambio);
                    LibReport.ConfigLabel(this,"lblNotaDeCambio",vMensajeCambio);
                } else {
                    LibReport.ChangeControlVisibility(this,"lblNotaDeCambio",false);
                }
                LibReport.ConfigLabel(this,"lblNotaDeCambio",vMensajeCambio);
                if(vEsAgrupado) {
                    LibReport.ConfigGroupHeader(this,"GHMoneda","MonedaCobro",GroupKeepTogether.All,RepeatStyle.All,true,NewPage.None);
                    switch(vFiltrarCobranzasPor) {
                    case eFiltrarCobranzasPor.Cobrador:
                        LibReport.ConfigLabel(this,"lblNombreCliente","Cliente");
                        LibReport.ConfigFieldStr(this,"txtNombreCliente",string.Empty,"NombreCliente");
                        LibReport.ConfigFieldStr(this,"txtVar",string.Empty,"NombreCuenta");
                        LibReport.ConfigLabel(this,"lblVar","Cuenta Bancaria");
                        LibReport.ConfigLabel(this,"lblCampo","Cobrador:");
                        LibReport.ConfigFieldStr(this,"txtCampoFiltro",string.Empty,"NombreVendedor");
                        LibReport.ConfigGroupHeader(this,"GHCampoFiltro","NombreVendedor",GroupKeepTogether.FirstDetail,RepeatStyle.OnPage,true,NewPage.After);
                        break;
                    case eFiltrarCobranzasPor.Cliente:
                        LibReport.ConfigLabel(this,"lblNombreCliente","Cuenta Bancaria");
                        LibReport.ConfigFieldStr(this,"txtNombreCliente",string.Empty,"NombreCuenta");
                        LibReport.ConfigFieldStr(this,"txtVar",string.Empty,"NombreVendedor");
                        LibReport.ConfigLabel(this,"lblVar","Cobrador");
                        LibReport.ConfigLabel(this,"lblCampo","Cliente:");
                        LibReport.ConfigFieldStr(this,"txtCampoFiltro",string.Empty,"NombreCliente");
                        LibReport.ConfigGroupHeader(this,"GHCampoFiltro","NombreCliente",GroupKeepTogether.FirstDetail,RepeatStyle.OnPage,true,NewPage.After);
                        break;
                    case eFiltrarCobranzasPor.CuentaBancaria:
                        LibReport.ConfigLabel(this,"lblNombreCliente","Cliente");
                        LibReport.ConfigFieldStr(this,"txtNombreCliente",string.Empty,"NombreCliente");
                        LibReport.ConfigFieldStr(this,"txtVar",string.Empty,"NombreVendedor");
                        LibReport.ConfigLabel(this,"lblVar","Cobrador");
                        LibReport.ConfigLabel(this,"lblCampo","Cuenta Bancaria:");
                        LibReport.ConfigFieldStr(this,"txtCampoFiltro",string.Empty,"NombreCuenta");
                        LibReport.ConfigGroupHeader(this,"GHCampoFiltro","NombreCuenta",GroupKeepTogether.FirstDetail,RepeatStyle.OnPage,true,NewPage.After);
                        break;
                    }
                    LibReport.ChangeControlVisibility(this,"lblNotaDeCambio",false);
                    LibReport.ConfigSummaryField(this,"txtTotalTotalCobradoCampoFiltro","TotalCobrado",SummaryFunc.Sum,"GHCampoFiltro",SummaryRunning.All,SummaryType.SubTotal);
                } else {
                    LibReport.ConfigGroupHeader(this,"GHMoneda","MonedaCobro",GroupKeepTogether.FirstDetail,RepeatStyle.OnPage,true,NewPage.None);
                    LibReport.ChangeControlVisibility(this,"txtTotalTotalCobradoCampoFiltro",false);
                    LibReport.ChangeControlVisibility(this,"lblTotalCampoFiltro",false);               
                }
                LibReport.ChangeControlVisibility(this,"lblNotaDeCambio",false);
                LibReport.ConfigSummaryField(this,"txtTotalCobrado","TotalCobrado",SummaryFunc.Sum,"GHMoneda",SummaryRunning.Group,SummaryType.SubTotal);
                LibGraphPrnMargins.SetGeneralMargins(this,DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
            }
            return true;
        }
        #endregion //Metodos Generados
    } //End of class dsrCobranzasEntreFechas
} //End of namespace Galac.Dbo.Rpt.Venta
