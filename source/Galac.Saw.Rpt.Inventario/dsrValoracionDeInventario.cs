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
using Galac.Saw.Lib;

namespace Galac.Saw.Rpt.Inventario {

    public partial class dsrValoracionDeInventario:DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrValoracionDeInventario()
            : this(false,string.Empty) {
        }
        public dsrValoracionDeInventario(bool initUseExternalRpx,string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if(_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        public string ReportTitle() {
            return "Valoración De Inventario";
        }

        public bool ConfigReport(DataTable valDataSource,Dictionary<string,string> valParameters) {
            eMonedaPresentacionDeReporte TipoDeMonedaDelReporte = (eMonedaPresentacionDeReporte)LibConvert.DbValueToEnum(valParameters["TipoDeMonedaDelReporte"]);
            byte vDecimalDigits = LibConvert.ToByte(valParameters["DecimalesParaReporte"]);
            bool UsaPrecioConIVA = LibConvert.SNToBool(valParameters["UsaPrecioConIva"]);
            string StrUsaPrecioConIVA = UsaPrecioConIVA ? "Precio con IVA" : "Precio sin IVA";            
            if(_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName,ReportTitle(),false,LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if(!LibString.IsNullOrEmpty(vRpxPath,true)) {
                    LibReport.LoadLayout(this,vRpxPath);
                }
            }
            if(LibReport.ConfigDataSource(this,valDataSource)) {
                LibReport.ConfigFieldStr(this,"txtNombreCompania",valParameters["NombreCompania"],string.Empty);
                LibReport.ConfigLabel(this,"lblTituloInforme",ReportTitle());
                LibReport.ConfigLabel(this,"lblCodigoDesdeHasta",valParameters["CodigoDesdeHasta"]);
                LibReport.ConfigLabel(this,"lblFechaYHoraDeEmision",LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigFieldStr(this,"txtNroDePaginaH",string.Empty,LibConvert.ToStr(PageNumber));
                LibReport.ConfigFieldStr(this,"txtNroDePagina",string.Empty, LibConvert.ToStr(PageNumber));
                LibReport.ConfigHeader(this,"txtNombreCompania","lblFechaYHoraDeEmision","lblTituloInforme","txtNroDePagina","lblCodigoDesdeHasta",LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber,LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this,"txtPrecioConIva",StrUsaPrecioConIVA,string.Empty);
                LibReport.ConfigFieldStr(this,"txtMoneda",valParameters["MonedaDelReporte"],string.Empty);                
                LibReport.ConfigFieldStr(this,"txtCodigo",string.Empty,"Codigo");
                LibReport.ConfigFieldStr(this,"txtDescripcion",string.Empty,"Descripcion");
                LibReport.ConfigFieldDecWithNDecimal(this,"txtExistencia",string.Empty,"Existencia",vDecimalDigits);
                LibReport.ConfigFieldDate(this,"txtFechaUltimaCompra",string.Empty,"FechaUltimaCompra","dd/MM/yyyy");
                LibReport.ConfigFieldDecWithNDecimal(this,"txtCostoUnitario",string.Empty,"CostoUnitario",vDecimalDigits);
                LibReport.ConfigFieldDecWithNDecimal(this,"txtValoracion",string.Empty,"Valoracion",vDecimalDigits);
                LibReport.ConfigFieldDecWithNDecimal(this,"txtPrecioDeVenta",string.Empty,"PrecioDeVenta",vDecimalDigits);
                LibReport.ConfigFieldDecWithNDecimal(this,"txtValorDeLaVenta",string.Empty,"ValorDeVenta",vDecimalDigits);
                LibReport.ConfigFieldDecWithNDecimal(this,"txtCostoUnitarioME",string.Empty,"CostoUnitarioME",vDecimalDigits);
                LibReport.ConfigFieldDecWithNDecimal(this,"txtValoracionME",string.Empty,"ValoracionME",vDecimalDigits);
                LibReport.ConfigFieldDecWithNDecimal(this,"txtPrecioDeVentaME",string.Empty,"PrecioDeVentaME",vDecimalDigits);
                LibReport.ConfigFieldDecWithNDecimal(this,"txtValorDeLaVentaME",string.Empty,"ValorDeVentaME",vDecimalDigits);
                LibReport.ConfigFieldDecWithNDecimal(this,"txtCambio",string.Empty,"Cambio",2);
                LibReport.ConfigFieldStr(this,"txtLineaDeProducto",string.Empty,"LineaDeProducto");
                LibReport.ConfigGroupHeader(this,"GHSeccionLineaDeProducto","LineaDeProducto",GroupKeepTogether.FirstDetail,RepeatStyle.OnPage,true,NewPage.After);
                LibReport.ConfigGroupHeader(this,"GHTotales","",GroupKeepTogether.FirstDetail,RepeatStyle.OnPage,true,NewPage.After);                
                switch(TipoDeMonedaDelReporte) {
                case eMonedaPresentacionDeReporte.EnMonedaLocal:
                    LibReport.ConfigSummaryField(this,"txtTotalValoracionLP","Valoracion",SummaryFunc.Sum,"GHSeccionLineaDeProducto",SummaryRunning.All,SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalValorDeLaVentaLP","ValorDeVenta",SummaryFunc.Sum,"GHSeccionLineaDeProducto",SummaryRunning.All,SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalesValoracion","Valoracion",SummaryFunc.Sum,"GFTotales",SummaryRunning.All,SummaryType.GrandTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalesValorDeLaVenta","ValorDeVenta",SummaryFunc.Sum,"GFTotales",SummaryRunning.All,SummaryType.GrandTotal);
                    LibGraphPrnMargins.SetGeneralMargins(this,DataDynamics.ActiveReports.Document.PageOrientation.Portrait);                   
                    break;
                case eMonedaPresentacionDeReporte.EnDivisa:
                    LibReport.ConfigSummaryField(this,"txtTotalValoracionLP","ValoracionME",SummaryFunc.Sum,"GHSeccionLineaDeProducto",SummaryRunning.All,SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalValorDeLaVentaLP","ValorDeVentaME",SummaryFunc.Sum,"GHSeccionLineaDeProducto",SummaryRunning.All,SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalesValoracion","ValoracionME",SummaryFunc.Sum,"GFTotales",SummaryRunning.All,SummaryType.GrandTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalesValorDeLaVenta","ValorDeVentaME",SummaryFunc.Sum,"GFTotales",SummaryRunning.All,SummaryType.GrandTotal);                    
                    LibGraphPrnMargins.SetGeneralMargins(this,DataDynamics.ActiveReports.Document.PageOrientation.Portrait);                    
                    break;
                case eMonedaPresentacionDeReporte.EnAmbasMonedas:                    
                    LibReport.ConfigSummaryField(this,"txtTotalValoracionLP","Valoracion",SummaryFunc.Sum,"GHSeccionLineaDeProducto",SummaryRunning.All,SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalValorDeLaVentaLP","ValorDeVenta",SummaryFunc.Sum,"GHSeccionLineaDeProducto",SummaryRunning.All,SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalesValoracion","Valoracion",SummaryFunc.Sum,"GFTotales",SummaryRunning.All,SummaryType.GrandTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalesValorDeLaVenta","ValorDeVenta",SummaryFunc.Sum,"GFTotales",SummaryRunning.All,SummaryType.GrandTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalValoracionLPME","ValoracionME",SummaryFunc.Sum,"GHSeccionLineaDeProducto",SummaryRunning.All,SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalValorDeLaVentaLPME","ValorDeVentaME",SummaryFunc.Sum,"GHSeccionLineaDeProducto",SummaryRunning.All,SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalesValoracionME","ValoracionME",SummaryFunc.Sum,"GFTotales",SummaryRunning.All,SummaryType.GrandTotal);
                    LibReport.ConfigSummaryField(this,"txtTotalesValorDeLaVentaME","ValorDeVentaME",SummaryFunc.Sum,"GFTotales",SummaryRunning.All,SummaryType.GrandTotal);
                    LibReport.ConfigFieldStr(this,"txtMonedaLocal",valParameters["MonedaLocal"],string.Empty);                    
                    LibGraphPrnMargins.SetGeneralMargins(this,DataDynamics.ActiveReports.Document.PageOrientation.Landscape);                                      
                    break;
                }
            }
            return true;
        }
        #endregion //Metodos Generados
    } //End of class dsrValoracionDeInventario
} //End of namespace Galac.Saw.Rpt.Inventario
