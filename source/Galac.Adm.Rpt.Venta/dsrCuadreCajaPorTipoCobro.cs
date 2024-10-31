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
namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrCuadreCajaPorTipoCobro.
    /// </summary>
    public partial class dsrCuadreCajaPorTipoCobro : DataDynamics.ActiveReports.ActiveReport {
		#region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrCuadreCajaPorTipoCobro()
            : this(false, string.Empty) {
        }

        public dsrCuadreCajaPorTipoCobro(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
		#region Metodos Generados

        public string ReportTitle() {
            return "Cuadre Caja por Tipo de Cobro Multimoneda";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            bool esInformeDetallado = LibConvert.SNToBool(valParameters["EsInformeDetallado"]);
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"] + " - " +  valParameters["RifCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", valParameters["TituloInforme"]);
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
				LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDoc");
				LibReport.ConfigFieldStr(this, "txtMonedaCobro", string.Empty, "MonedaCobro");
                LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
                LibReport.ConfigFieldInt(this, "txtConsecutivoCaja", string.Empty, "ConsecutivoCaja");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
				LibReport.ConfigFieldStr(this, "txtNumeroFactura", string.Empty, "NumeroDoc");
				LibReport.ConfigFieldStr(this, "txtNumeroCompFiscal", string.Empty, "NumFiscal");
				LibReport.ConfigFieldStr(this, "txtRifCliente", string.Empty, "RifCliente");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoDoc", string.Empty, "MontoDoc", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtEfectivo", string.Empty, "Efectivo", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTarjeta", string.Empty, "Tarjeta", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCheque", string.Empty, "Cheque", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtDepositoTransf", string.Empty, "Deposito", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCambioOperacion", string.Empty, "CambioDeOperacion", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtPago", string.Empty, "MontoPagado", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtVuelto", string.Empty, "Vuelto", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtNotaDeCredito", string.Empty, "NotaDeCredito", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtVentaCredito", string.Empty, "VentaCredito", 2);
                LibReport.ConfigSummaryField(this, "txtTotalMontoDoc", "MontoDoc", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalEfectivo", "Efectivo", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalTarjeta", "Tarjeta", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalCheque", "Cheque", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalDepositoTransf", "Deposito", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalPago", "MontoPagado", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalVuelto", "Vuelto", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalNotaDeCredito", "NotaDeCredito", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalVentaCredito", "VentaCredito", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ChangeControlVisibility(this, "lblTotalesCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalMontoDocCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalEfectivoCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalTarjetaCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalChequeCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalDepositoTransfCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalPagoCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalVueltoCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalNotaDeCreditoCaja", false);
                LibReport.ChangeControlVisibility(this, "txtTotalVentaCreditoCaja", false);
                if (esInformeDetallado) {
                    LibReport.ConfigGroupHeader(this, "GHSecCaja", "ConsecutivoCaja", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                    LibReport.ConfigGroupHeader(this, "GHSecDocumento", "MonedaCobro", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                } else {
                    LibARReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", false, 0);
                    LibARReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSecDocumento", false, 0);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoDocCaja", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalEfectivoCaja", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalTarjetaCaja", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalChequeCaja", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalDepositoTransfCaja", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalPagoCaja", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalVueltoCaja", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalNotaDeCreditoCaja", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalVentaCreditoCaja", true);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoDocCaja", "MontoDoc", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalEfectivoCaja", "Efectivo", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalTarjetaCaja", "Tarjeta", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalChequeCaja", "Cheque", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalDepositoTransfCaja", "Deposito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalPagoCaja", "MontoPagado", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalVueltoCaja", "Vuelto", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalNotaDeCreditoCaja", "NotaDeCredito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalVentaCreditoCaja", "VentaCredito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigGroupHeader(this, "GHSecCaja", "MonedaCobro", GroupKeepTogether.All, RepeatStyle.None, true, NewPage.None);
                    LibReport.ConfigGroupHeader(this, "GHSecDocumento", "ConsecutivoCaja", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                }
				LibReport.ConfigGroupHeader(this, "GHSecMonedaDoc", "MonedaDoc", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
    } //End of class dsrCuadreCajaPorTipoCobro
} //End of namespace Galac.Adm.Rpt.Venta
