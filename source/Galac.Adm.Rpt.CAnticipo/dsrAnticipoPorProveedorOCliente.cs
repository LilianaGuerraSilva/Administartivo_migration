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
namespace Galac.Adm.Rpt.CAnticipo {
    /// <summary>
    /// Summary description for dsrAnticipoPorProveedorOCliente.
    /// </summary>
    public partial class dsrAnticipoPorProveedorOCliente : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrAnticipoPorProveedorOCliente()
            : this(false, string.Empty) {
        }

        public dsrAnticipoPorProveedorOCliente(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Anticipos";
        }


        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters, bool valCliente, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valOrdenarPorStatus) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }            
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtCompania", valParameters["Nombre"], string.Empty);
                //LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "txtNumeroDePagina", "", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "MonedaReporte");

                if (valOrdenarPorStatus){//Cuando es Ordenar x Status, se inverte el orden de los grupos GHClienteProveedor y GHStatus, con sus respectivos GF. Como no se puede hacer eso en tiempo de ejecución, se hace a través de los campos.
                    LibReport.ConfigLabel(this, "lblClienteProveedor", "Status");
                    LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "StatusStr");
                    LibReport.ConfigFieldStr(this, "txtStatus", string.Empty, "CodigoClienteProveedor");
                    LibReport.ConfigFieldStr(this, "txtNombre2", string.Empty, "NombreClienteProveedor");
                    txtCodigo.Width += txtNombre.Width;
                    txtNombre.Visible = false;
                    if (valCliente){
                        LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle() + " Cobrados por Cliente");
                        LibReport.ConfigLabel(this, "lblStatus", "Cliente");
                        LibReport.ConfigLabel(this, "lblTotalStatus", "Total por Cliente");
                    }else{
                        LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle() + " Pagados a Proveedor");
                        LibReport.ConfigLabel(this, "lblStatus", "Proveedor");
                        LibReport.ConfigLabel(this, "lblTotalStatus", "Total por Proveedor");                        
                    }
                    LibReport.ConfigLabel(this, "lblTotalClienteProveedor", "Total por Status");
                    LibReport.ConfigGroupHeader(this, "GHClienteProveedor", "StatusStr", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                    LibReport.ConfigFieldStr(this, "txtCodigoGF", string.Empty, "StatusStr");
                    LibReport.ConfigGroupHeader(this, "GHStatus", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                    LibReport.ConfigFieldStr(this, "txtStatusGF", string.Empty, "CodigoClienteProveedor");
                
                }else{
                    LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "CodigoClienteProveedor");
                    LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "NombreClienteProveedor");
                    LibReport.ConfigFieldStr(this, "txtStatus", string.Empty, "StatusStr");
                    LibReport.ConfigLabel(this, "lblStatus", "Status");
                    txtStatus.Width += txtNombre2.Width;
                    txtNombre2.Visible = false;
                    if (valCliente){
                        LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle() + " Cobrados por Cliente");
                        LibReport.ConfigLabel(this, "lblClienteProveedor", "Cliente");
                        LibReport.ConfigLabel(this, "lblTotalClienteProveedor", "Total por Cliente");
                    }else{
                        LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle() + " Pagados a Proveedor");
                        LibReport.ConfigLabel(this, "lblClienteProveedor", "Proveedor");
                        LibReport.ConfigLabel(this, "lblTotalClienteProveedor", "Total por Proveedor");
                    }
                    LibReport.ConfigLabel(this, "lblTotalStatus", "Total por Status");
                    LibReport.ConfigGroupHeader(this, "GHStatus", "StatusStr", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                    LibReport.ConfigFieldStr(this, "txtStatusGF", string.Empty, "StatusStr");
                    LibReport.ConfigGroupHeader(this, "GHClienteProveedor", "CodigoClienteProveedor", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                    LibReport.ConfigFieldStr(this, "txtCodigoGF", string.Empty, "CodigoClienteProveedor");
                }

                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtMonedaDocumento", string.Empty, "Moneda");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio", "#,###.0000", false, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtNumeroCheque", string.Empty, "NumeroCheque");
                LibReport.ConfigFieldDec(this, "txtMontoAnulado", string.Empty, "MontoAnulado");
                LibReport.ConfigFieldDec(this, "txtMontoTotal", string.Empty, "MontoTotal");
                LibReport.ConfigFieldDec(this, "txtMontoUsado", string.Empty, "MontoUsado");
                LibReport.ConfigFieldDec(this, "txtMontoDevuelto", string.Empty, "MontoDevuelto");
                LibReport.ConfigFieldDec(this, "txtDiferenciaDevolucion", string.Empty, "MontoDiferenciaEnDevolucion");

                LibReport.ConfigSummaryField(this, "txtTotalMontoAnulado", "MontoAnulado", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMonto", "MontoTotal", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoUsado", "MontoUsado", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoDev", "MontoDevuelto", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalDiferenciaDevol", "MontoDiferenciaEnDevolucion", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigSummaryField(this, "txtMontoAnuladoClienteProveedor", "MontoAnulado", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoTotalClienteProveedor", "MontoTotal", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoUsadoClienteProveedor", "MontoUsado", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoDevClienteProveedor", "MontoDevuelto", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoDifClienteProveedor", "MontoDiferenciaEnDevolucion", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                LibReport.ConfigFieldStr(this, "txtMonedaGF", string.Empty, "MonedaReporte");
                LibReport.ConfigSummaryField(this, "txtMontoAnuladoXMoneda", "MontoAnulado", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoTotalXMoneda", "MontoTotal", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoUsadoXMoneda", "MontoUsado", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMtoDevtoXMoneda", "MontoDevuelto", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalDifDevolucionXMoneda", "MontoDiferenciaEnDevolucion", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);

                string vNotaMonedaCambio = new clsLibSaw().NotaMonedaCambioParaInformes(valMonedaDelInforme, valTasaDeCambio, valMoneda, "Anticipo");
                LibReport.ConfigFieldStr(this, "txtNotaMonedaCambio", vNotaMonedaCambio, "");

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

    } //End of class dsrAnticipoPorProveedorOCliente

} //End of namespace Galac.Dbo.Rpt.CAnticipo

