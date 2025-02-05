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
    /// Summary description for dsrCuadreCajaPorTipoCobroYUsuario.
    /// </summary>
    public partial class dsrCuadreCajaPorTipoCobroYUsuario : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrCuadreCajaPorTipoCobroYUsuario()
            : this(false, string.Empty) {
        }
        public dsrCuadreCajaPorTipoCobroYUsuario(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados
        public string ReportTitle() {
            return "Informe de Cuadre Caja por Tipo de Cobro Detallado y Usuario";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            string vNombreCompania = valParameters["NombreCompania"] + " - " + valParameters["RifCompania"];
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", vNombreCompania, string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtNombreOperador", string.Empty, "NombreDelOperador");
                LibReport.ConfigFieldInt(this, "txtConsecutivoCaja", string.Empty, "ConsecutivoCaja");
                LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDoc");
                LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
                LibReport.ConfigFieldStr(this, "txtMonedaCobro", string.Empty, "MonedaCobro");

                LibReport.ConfigFieldStr(this, "txtCodMonedaCobro", string.Empty, "CodMoneda");

                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtNumeroDoc", string.Empty, "NumeroDoc");
                LibReport.ConfigFieldStr(this, "txtNumeroCompFiscal", string.Empty, "NumFiscal");
                LibReport.ConfigFieldStr(this, "txtRifCliente", string.Empty, "RifCliente");
                LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoDoc", string.Empty, "MontoDoc", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtEfectivo", string.Empty, "Efectivo", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTarjeta", string.Empty, "Tarjeta", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCheque", string.Empty, "Cheque", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtDepositoTransf", string.Empty, "Deposito", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtAntUsado", string.Empty, "AnticipoUsado", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtAntResta", string.Empty, "AnticipoRestante", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtPago", string.Empty, "MontoPagado", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtVuelto", string.Empty, "Vuelto", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtNotaDeCredito", string.Empty, "NotaDeCredito", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtVentaCredito", string.Empty, "VentaCredito", 2);
                #region Totales por moneda de cobro
                LibReport.ConfigSummaryField(this, "txtTotalMontoDoc", "MontoDoc", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalEfectivo", "Efectivo", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalTarjeta", "Tarjeta", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalCheque", "Cheque", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalDepositoTransf", "Deposito", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalAnticipoUsado", "AnticipoUsado", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalAnticipoResta", "AnticipoRestante", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalPago", "MontoPagado", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalVuelto", "Vuelto", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalNotaDeCredito", "NotaDeCredito", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalVentaCredito", "VentaCredito", SummaryFunc.Sum, "GHSecDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                #endregion Totales por moneda de cobro

                #region Totales por Caja en Bs
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalTarjetaCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalChequeCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepositoTransfCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalAnticipoUsadoCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalAnticipoRestaCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalPagoCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVueltoCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalNotaDeCreditoCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVentaCreditoCajaBs", LibConvert.ToStr((decimal)0), "", 2);
                //LibReport.ConfigSummaryField(this, "txtTotalMontoDocCajaBs", "MontoDoc", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalEfectivoCajaBs", "Efectivo", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalTarjetaCajaBs", "Tarjeta", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalChequeCajaBs", "Cheque", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalDepositoTransfCajaBs", "Deposito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalAnticipoUsadoCajaBs", "AnticipoUsado", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalAnticipoRestaCajaBs", "AnticipoRestante", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalPagoCajaBs", "MontoPagado", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalVueltoCajaBs", "Vuelto", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalNotaDeCreditoCajaBs", "NotaDeCredito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalVentaCreditoCajaBs", "VentaCredito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                #endregion Totales por Caja Bs

                #region Totales por Caja en USD
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalTarjetaCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalChequeCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepositoTransfCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalAnticipoUsadoCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalAnticipoRestaCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalPagoCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVueltoCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalNotaDeCreditoCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVentaCreditoCajaUSD", LibConvert.ToStr((decimal)0), "", 2);
                //LibReport.ConfigSummaryField(this, "txtTotalMontoDocCajaUSD", "MontoDoc", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalEfectivoCajaUSD", "Efectivo", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalTarjetaCajaUSD", "Tarjeta", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalChequeCajaUSD", "Cheque", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalDepositoTransfCajaUSD", "Deposito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalAnticipoUsadoCajaUSD", "AnticipoUsado", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalAnticipoRestaCajaUSD", "AnticipoRestante", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalPagoCajaUSD", "MontoPagado", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalVueltoCajaUSD", "Vuelto", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalNotaDeCreditoCajaUSD", "NotaDeCredito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                //LibReport.ConfigSummaryField(this, "txtTotalVentaCreditoCajaUSD", "VentaCredito", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                #endregion Totales por Caja USD

                #region Totales por Operador Bs
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalTarjetaOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalChequeOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepositoTransfOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalAnticipoUsadoOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalAnticipoRestaOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalPagoOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVueltoOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalNotaDeCreditoOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVentaCreditoOperadorBs", LibConvert.ToStr((decimal)0), "", 2);
                #endregion TotalesPorOperador Bs

                #region Totales por Operador USD
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalTarjetaOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalChequeOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepositoTransfOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalAnticipoUsadoOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalAnticipoRestaOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalPagoOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVueltoOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalNotaDeCreditoOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVentaCreditoOperadorUSD", LibConvert.ToStr((decimal)0), "", 2);
                #endregion TotalesPorOperador USD

                LibReport.ConfigGroupHeader(this, "GHSecOperador", "NombreDelOperador", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
                LibReport.ConfigGroupHeader(this, "GHSecCaja", "ConsecutivoCaja", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecMonedaDoc", "MonedaDoc", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecDocumento", "MonedaCobro", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
        // Caja
        private decimal totalEfectivoBsCaja;
        private decimal totalTarjetaBsCaja;
        private decimal totalChequeBsCaja;
        private decimal totalDepositoTransfBsCaja;
        private decimal totalAnticipoUsadoBsCaja;
        private decimal totalAnticipoRestaBsCaja;
        private decimal totalPagoBsCaja;
        private decimal totalVueltoBsCaja;
        private decimal totalNotaDeCreditoBsCaja;
        private decimal totalVentaCreditoBsCaja;

        private decimal totalEfectivoUSDCaja;
        private decimal totalTarjetaUSDCaja;
        private decimal totalChequeUSDCaja;
        private decimal totalDepositoTransfUSDCaja;
        private decimal totalAnticipoUsadoUSDCaja;
        private decimal totalAnticipoRestaUSDCaja;
        private decimal totalPagoUSDCaja;
        private decimal totalVueltoUSDCaja;
        private decimal totalNotaDeCreditoUSDCaja;

        // Operador
        private decimal totalEfectivoBsOperador;
        private decimal totalTarjetaBsOperador;
        private decimal totalChequeBsOperador;
        private decimal totalDepositoTransfBsOperador;
        private decimal totalAnticipoUsadoBsOperador;
        private decimal totalAnticipoRestaBsOperador;
        private decimal totalPagoBsOperador;
        private decimal totalVueltoBsOperador;
        private decimal totalNotaDeCreditoBsOperador;
        private decimal totalVentaCreditoBsOperador;

        private decimal totalEfectivoUSDOperador;
        private decimal totalTarjetaUSDOperador;
        private decimal totalChequeUSDOperador;
        private decimal totalDepositoTransfUSDOperador;
        private decimal totalAnticipoUsadoUSDOperador;
        private decimal totalAnticipoRestaUSDOperador;
        private decimal totalPagoUSDOperador;
        private decimal totalVueltoUSDOperador;
        private decimal totalNotaDeCreditoUSDOperador;



        private void Detail_Format(object sender, EventArgs e) {
            string codMonedaCobroRenglon = string.Empty;
            codMonedaCobroRenglon = txtCodMonedacobro.Text;
            switch (codMonedaCobroRenglon) {
                case "VES":
                case "VED":
                    //Caja
                    totalEfectivoBsCaja += LibConvert.ToDec(txtEfectivo.Text);
                    totalTarjetaBsCaja += LibConvert.ToDec(txtTarjeta.Text);
                    totalChequeBsCaja += LibConvert.ToDec(txtCheque.Text);
                    totalDepositoTransfBsCaja += LibConvert.ToDec(txtDepositoTransf.Text);
                    totalAnticipoUsadoBsCaja += LibConvert.ToDec(txtTotalAnticipoUsado.Text);
                    totalAnticipoRestaBsCaja += LibConvert.ToDec(txtAntResta.Text);
                    totalPagoBsCaja += LibConvert.ToDec(txtPago.Text);
                    totalVueltoBsCaja += LibConvert.ToDec(txtVuelto.Text);
                    totalNotaDeCreditoBsCaja += LibConvert.ToDec(txtNotaDeCredito.Text);
                    txtTotalEfectivoCajaBs.Text = LibConvert.ToStr(totalEfectivoBsCaja);
                    txtTotalTarjetaCajaBs.Text = LibConvert.ToStr(totalTarjetaBsCaja);
                    txtTotalChequeCajaBs.Text = LibConvert.ToStr(totalChequeBsCaja);
                    txtTotalDepositoTransfCajaBs.Text = LibConvert.ToStr(totalDepositoTransfBsCaja);
                    txtTotalAnticipoUsadoCajaBs.Text = LibConvert.ToStr(totalAnticipoUsadoBsCaja);
                    txtTotalAnticipoRestaCajaBs.Text = LibConvert.ToStr(totalAnticipoRestaBsCaja);
                    txtTotalPagoCajaBs.Text = LibConvert.ToStr(totalPagoBsCaja);
                    txtTotalVueltoCajaBs.Text = LibConvert.ToStr(totalVueltoBsCaja);
                    txtTotalNotaDeCreditoCajaBs.Text = LibConvert.ToStr(totalNotaDeCreditoBsCaja);
                    // Operador
                    totalEfectivoBsOperador += LibConvert.ToDec(txtEfectivo.Text);
                    totalTarjetaBsOperador += LibConvert.ToDec(txtTarjeta.Text);
                    totalChequeBsOperador += LibConvert.ToDec(txtCheque.Text);
                    totalDepositoTransfBsOperador += LibConvert.ToDec(txtDepositoTransf.Text);
                    totalAnticipoUsadoBsOperador += LibConvert.ToDec(txtTotalAnticipoUsado.Text);
                    totalAnticipoRestaBsOperador += LibConvert.ToDec(txtAntResta.Text);
                    totalPagoBsOperador += LibConvert.ToDec(txtPago.Text);
                    totalVueltoBsOperador += LibConvert.ToDec(txtVuelto.Text);
                    totalNotaDeCreditoBsOperador += LibConvert.ToDec(txtNotaDeCredito.Text);

                    txtTotalEfectivoOperadorBs.Text = LibConvert.ToStr(totalEfectivoBsOperador);
                    txtTotalTarjetaOperadorBs.Text = LibConvert.ToStr(totalTarjetaBsOperador);
                    txtTotalChequeOperadorBs.Text = LibConvert.ToStr(totalChequeBsOperador);
                    txtTotalDepositoTransfOperadorBs.Text = LibConvert.ToStr(totalDepositoTransfBsOperador);
                    txtTotalAnticipoUsadoOperadorBs.Text = LibConvert.ToStr(totalAnticipoUsadoBsOperador);
                    txtTotalAnticipoRestaOperadorBs.Text = LibConvert.ToStr(totalAnticipoRestaBsOperador);
                    txtTotalPagoOperadorBs.Text = LibConvert.ToStr(totalPagoBsOperador);
                    txtTotalVueltoOperadorBs.Text = LibConvert.ToStr(totalVueltoBsOperador);
                    txtTotalNotaDeCreditoOperadorBs.Text = LibConvert.ToStr(totalNotaDeCreditoBsOperador);
                    break;

                case "USD":
                    //Caja
                    totalEfectivoUSDCaja += LibConvert.ToDec(txtEfectivo.Text);
                    totalTarjetaUSDCaja += LibConvert.ToDec(txtTarjeta.Text);
                    totalChequeUSDCaja += LibConvert.ToDec(txtCheque.Text);
                    totalDepositoTransfUSDCaja += LibConvert.ToDec(txtDepositoTransf.Text);
                    totalAnticipoUsadoUSDCaja += LibConvert.ToDec(txtTotalAnticipoUsado.Text);
                    totalAnticipoRestaUSDCaja += LibConvert.ToDec(txtAntResta.Text);
                    totalPagoUSDCaja += LibConvert.ToDec(txtPago.Text);
                    totalVueltoUSDCaja += LibConvert.ToDec(txtVuelto.Text);
                    totalNotaDeCreditoUSDCaja += LibConvert.ToDec(txtNotaDeCredito.Text);
                    txtTotalEfectivoCajaUSD.Text = LibConvert.ToStr(totalEfectivoUSDCaja);
                    txtTotalTarjetaCajaUSD.Text = LibConvert.ToStr(totalTarjetaUSDCaja);
                    txtTotalChequeCajaUSD.Text = LibConvert.ToStr(totalChequeUSDCaja);
                    txtTotalDepositoTransfCajaUSD.Text = LibConvert.ToStr(totalDepositoTransfUSDCaja);
                    txtTotalAnticipoUsadoCajaUSD.Text = LibConvert.ToStr(totalAnticipoUsadoUSDCaja);
                    txtTotalAnticipoRestaCajaUSD.Text = LibConvert.ToStr(totalAnticipoRestaUSDCaja);
                    txtTotalPagoCajaUSD.Text = LibConvert.ToStr(totalPagoUSDCaja);
                    txtTotalVueltoCajaUSD.Text = LibConvert.ToStr(totalVueltoUSDCaja);
                    txtTotalNotaDeCreditoCajaUSD.Text = LibConvert.ToStr(totalNotaDeCreditoUSDCaja);
                    // Operador
                    totalEfectivoUSDOperador += LibConvert.ToDec(txtEfectivo.Text);
                    totalTarjetaUSDOperador += LibConvert.ToDec(txtTarjeta.Text);
                    totalChequeUSDOperador += LibConvert.ToDec(txtCheque.Text);
                    totalDepositoTransfUSDOperador += LibConvert.ToDec(txtDepositoTransf.Text);
                    totalAnticipoUsadoUSDOperador += LibConvert.ToDec(txtTotalAnticipoUsado.Text);
                    totalAnticipoRestaUSDOperador += LibConvert.ToDec(txtAntResta.Text);
                    totalPagoUSDOperador += LibConvert.ToDec(txtPago.Text);
                    totalVueltoUSDOperador += LibConvert.ToDec(txtVuelto.Text);
                    totalNotaDeCreditoUSDOperador += LibConvert.ToDec(txtNotaDeCredito.Text);
                    txtTotalEfectivoOperadorUSD.Text = LibConvert.ToStr(totalEfectivoUSDOperador);
                    txtTotalTarjetaOperadorUSD.Text = LibConvert.ToStr(totalTarjetaUSDOperador);
                    txtTotalChequeOperadorUSD.Text = LibConvert.ToStr(totalChequeUSDOperador);
                    txtTotalDepositoTransfOperadorUSD.Text = LibConvert.ToStr(totalDepositoTransfUSDOperador);
                    txtTotalAnticipoUsadoOperadorUSD.Text = LibConvert.ToStr(totalAnticipoUsadoUSDOperador);
                    txtTotalAnticipoRestaOperadorUSD.Text = LibConvert.ToStr(totalAnticipoRestaUSDOperador);
                    txtTotalPagoOperadorUSD.Text = LibConvert.ToStr(totalPagoUSDOperador);
                    txtTotalVueltoOperadorUSD.Text = LibConvert.ToStr(totalVueltoUSDOperador);
                    txtTotalNotaDeCreditoOperadorUSD.Text = LibConvert.ToStr(totalNotaDeCreditoUSDOperador);
                    break;

                default:
                    totalVentaCreditoBsOperador += LibConvert.ToDec(txtVentaCredito.Text);
                    txtTotalVentaCreditoOperadorBs.Text = LibConvert.ToStr(totalVentaCreditoBsOperador);
                    totalVentaCreditoBsCaja += LibConvert.ToDec(txtVentaCredito.Text);
                    txtTotalVentaCreditoCajaBs.Text = LibConvert.ToStr(totalVentaCreditoBsCaja);
                    break;
            }
        }

        private void GFSecOperador_Format(object sender, EventArgs e) {
            totalEfectivoBsOperador = 0;
            totalTarjetaBsOperador = 0;
            totalChequeBsOperador = 0;
            totalDepositoTransfBsOperador = 0;
            totalAnticipoUsadoBsOperador = 0;
            totalAnticipoRestaBsOperador = 0;
            totalPagoBsOperador = 0;
            totalVueltoBsOperador = 0;
            totalNotaDeCreditoBsOperador = 0;
            totalVentaCreditoBsOperador = 0;
            totalEfectivoUSDOperador = 0;
            totalTarjetaUSDOperador = 0;
            totalChequeUSDOperador = 0;
            totalDepositoTransfUSDOperador = 0;
            totalAnticipoUsadoUSDOperador = 0;
            totalAnticipoRestaUSDOperador = 0;
            totalPagoUSDOperador = 0;
            totalVueltoUSDOperador = 0;
            totalNotaDeCreditoUSDOperador = 0;
        }

        private void GFSecCaja_Format(object sender, EventArgs e) {
            totalEfectivoBsCaja = 0;
            totalTarjetaBsCaja = 0;
            totalChequeBsCaja = 0;
            totalDepositoTransfBsCaja = 0;
            totalAnticipoUsadoBsCaja = 0;
            totalAnticipoRestaBsCaja = 0;
            totalPagoBsCaja = 0;
            totalVueltoBsCaja = 0;
            totalNotaDeCreditoBsCaja = 0;
            totalVentaCreditoBsCaja = 0;
            totalEfectivoUSDCaja = 0;
            totalTarjetaUSDCaja = 0;
            totalChequeUSDCaja = 0;
            totalDepositoTransfUSDCaja = 0;
            totalAnticipoUsadoUSDCaja = 0;
            totalAnticipoRestaUSDCaja = 0;
            totalPagoUSDCaja = 0;
            totalVueltoUSDCaja = 0;
            totalNotaDeCreditoUSDCaja = 0;        }

    } //End of class dsrCuadreCajaPorTipoCobroYUsuario
} //End of namespace Galac.Adm.Rpt.Venta
