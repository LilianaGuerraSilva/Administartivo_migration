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


        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio, bool valCliente) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }            
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtCompania", string.Empty, valParameters["Nombre"]);
                if (valCliente) {
                    LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle() + " Cobrados por Cliente");
                    LibReport.ConfigLabel(this, "lblClienteProveedor", "Cliente");
                    LibReport.ConfigLabel(this, "lblTotalClienteProveedor", "Total por Cliente");
                } else {
                    LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle() + " Pagados por Proveedor");
                    LibReport.ConfigLabel(this, "lblClienteProveedor", "Proveedor");
                    LibReport.ConfigLabel(this, "lblTotalClienteProveedor", "Total por Proveedor");
                }
                //LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "txtNumeroDePagina", "", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "MonedaReporte");
                LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "CodigoClienteProveedor");
                LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "NombreClienteProveedor");
                LibReport.ConfigFieldStr(this, "txtEstatus", string.Empty, "Estatus");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtMonedaDocumento", string.Empty, "MonedaDocumento");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio", "#,###.0000", false, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtNumeroCheque", string.Empty, "NumeroCheque");
                LibReport.ConfigFieldDec(this, "txtMontoAnulado", string.Empty, "MontoAnulado");
                LibReport.ConfigFieldDec(this, "txtMontoTotal", string.Empty, "MontoTotal");
                LibReport.ConfigFieldDec(this, "txtMontoUsado", string.Empty, "MontoUsado");
                LibReport.ConfigFieldDec(this, "txtMontoDevuelto", string.Empty, "MontoDevuelto");
                LibReport.ConfigFieldDec(this, "txtDifDevolucion", string.Empty, "DifDevolucion");

                LibReport.ConfigGroupHeader(this, "GHStatus", "Status", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoAnulado", "MontoAnulado", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMonto", "MontoTotal", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoUsado", "MontoUsado", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoDev", "MontoDevuelto", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalDiferenciaDevol", "DifDevolucion", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigGroupHeader(this, "GHClienteProveedor", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtMontoAnuladoClienteProveedor", "MontoAnulado", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoTotalClienteProveedor", "MontoTotal", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoUsadoClienteProveedor", "MontoUsado", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoDevClienteProveedor", "MontoDevuelto", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoDifClienteProveedor", "DifDevolucion", SummaryFunc.Sum, "GHClienteProveedor", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtMontoAnuladoXMoneda", "MontoAnulado", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoTotalXMoneda", "MontoTotal", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoUsadoXMoneda", "MontoUsado", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMtoDevtoXMoneda", "MontoDevuelto", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalDifDevolucionXMoneda", "DifDevolucion", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);

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

