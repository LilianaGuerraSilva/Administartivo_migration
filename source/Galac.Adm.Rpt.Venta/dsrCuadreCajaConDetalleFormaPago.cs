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
namespace Galac.Adm.Rpt.Venta
{
    /// <summary>
    /// Summary description for dsrCuadreCajaConDetalleFormaPago.
    /// </summary>
    public partial class dsrCuadreCajaConDetalleFormaPago : DataDynamics.ActiveReports.ActiveReport
    {
		#region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrCuadreCajaConDetalleFormaPago()
            : this(false, string.Empty) {
        }

        public dsrCuadreCajaConDetalleFormaPago(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
		
		#region Metodos Generados

        public string ReportTitle() {
            return "Informe de Cuadre de Caja con Detalle de Pago Detallado";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            bool incluirTotalesPorFormaDeCobro = LibConvert.SNToBool(valParameters["TotalesTipoPago"]);
            bool enMonedaOriginal = LibConvert.SNToBool(valParameters["EnMonedaOriginal"]);
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

				LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDoc");
                LibReport.ConfigFieldInt(this, "txtConsecutivoCaja", string.Empty, "ConsecutivoCaja");
				LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
				LibReport.ConfigFieldStr(this, "txtNumeroDoc", string.Empty, "NumeroDoc");
				LibReport.ConfigFieldStr(this, "txtNumeroCompFiscal", string.Empty, "NumFiscal");
				LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
				LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoDoc", string.Empty, "MontoDoc", 2);

                LibReport.ConfigFieldDecWithNDecimal(this, "txtCambioDeOperacion", string.Empty, "TasaOperacion", 2);
				
				LibReport.ConfigFieldStr(this, "txtNombreMonedaFormaDelCobro", string.Empty, "NombreMonedaFormaDelCobro");
                if (!enMonedaOriginal) {
                    LibReport.ChangeControlVisibility(this, "txtCambioABs", true);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtCambioABs", string.Empty, "CambioABolivares", 2);
                }
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoCobro", string.Empty, "MontoCobro", 2);
                LibReport.ConfigFieldStr(this, "txtNombreFormaDelCobro", string.Empty, "TipoDeCobro");
                LibReport.ConfigSummaryField(this, "txtTotalMontoPorMoneda", "MontoCobro", SummaryFunc.Sum, "GHSecCobranzaDelDocumento", SummaryRunning.Group, SummaryType.SubTotal);
                
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDocumento", string.Empty, "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalVuelto", string.Empty, "", 2);

                if (incluirTotalesPorFormaDeCobro) {
                    //LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecMonedaDoc", true, 0);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoBs", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalTarjetaBs", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepTransfBs", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoUSD", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepTransfUSD", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtCodMonedaCobro", string.Empty, "CodMonedaCobro", 2);
                } else {
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecMonedaDoc", false, 0);
                }
				LibReport.ConfigGroupHeader(this, "GHSecMonedaDoc", "MonedaDoc", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
				LibReport.ConfigGroupHeader(this, "GHSecCaja", "ConsecutivoCaja", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
				LibReport.ConfigGroupHeader(this, "GHSecDocumento", "NumeroDoc", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHSecCobranzaDelDocumento", "NombreMonedaFormaDelCobro", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
        private string codMonedaCobro;
        private decimal totalMontoDoc = 0;
        private decimal totalEfectivoBs = 0;
        private decimal totalTarjetaBs = 0;
        private decimal totalDepTransfBs = 0;
        private decimal totalEfectivoUSD = 0;
        private decimal totalDepTransfUSD = 0;

        private void GHSecCobranzaDelDocumento_Format(object sender, EventArgs e) {
            string nombreMonedaFormaDeCobro = string.Empty;
            Saw.Lib.clsLibSaw insUtil = new Saw.Lib.clsLibSaw();
            codMonedaCobro = txtCodMonedaCobro.Text;
            if (LibString.IsNullOrEmpty(txtNombreMonedaFormaDelCobro.Text)) {
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSecCobranzaDelDocumento", false, 0);
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", false, 0);
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecCobranzaDelDocumento", false, 0);
            } else {
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSecCobranzaDelDocumento", true, 0);
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", true, 0);
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecCobranzaDelDocumento", true, 0);
                nombreMonedaFormaDeCobro = txtNombreMonedaFormaDelCobro.Text;
                txtNombreMonedaFormaDelCobro.Text = insUtil.Plural(nombreMonedaFormaDeCobro);
            }
        }

        private void Detail_Format(object sender, EventArgs e) {
            eFormaDeCobro codFormaDeCobro = (eFormaDeCobro)LibConvert.DbValueToEnum(txtNombreFormaDelCobro.Text);
            txtNombreFormaDelCobro.Text = codFormaDeCobro.GetDescription(0).ToUpper();

            switch (codMonedaCobro) {
                case "VES":
                case "VED":
                    if (codFormaDeCobro == eFormaDeCobro.Efectivo) {
                        totalEfectivoBs += LibConvert.ToDec(txtMontoCobro.Text);
                    } else if (codFormaDeCobro == eFormaDeCobro.Tarjeta) {
                        totalTarjetaBs += LibConvert.ToDec(txtMontoCobro.Text);
                    } else if (codFormaDeCobro == eFormaDeCobro.Deposito) {
                        totalDepTransfBs += LibConvert.ToDec(txtMontoCobro.Text);
                    }
                    break;
                case "USD":
                    if (codFormaDeCobro == eFormaDeCobro.Efectivo) {
                        totalEfectivoUSD += LibConvert.ToDec(txtMontoCobro.Text);
                    } else if (codFormaDeCobro == eFormaDeCobro.Deposito) {
                        totalDepTransfUSD += LibConvert.ToDec(txtMontoCobro.Text);
                    }
                    break;
            }
        }

        private void GFSecCobranzaDelDocumento_Format(object sender, EventArgs e) {
            lblTotalMontoPorMoneda.Text = "Total pagado en " + txtNombreMonedaFormaDelCobro.Text + " : ";
        }

        private void GFSecMonedaDoc_Format(object sender, EventArgs e) {
            txtTotalEfectivoBs.Text = LibConvert.ToStr(totalEfectivoBs);
            txtTotalTarjetaBs.Text = LibConvert.ToStr(totalTarjetaBs);
            txtTotalDepTransfBs.Text = LibConvert.ToStr(totalDepTransfBs);
            txtTotalEfectivoUSD.Text = LibConvert.ToStr(totalEfectivoUSD);
            txtTotalDepTransfUSD.Text = LibConvert.ToStr(totalDepTransfUSD);
            totalEfectivoBs = 0;
            totalTarjetaBs = 0;
            totalDepTransfBs = 0;
            totalEfectivoUSD = 0;
            totalDepTransfUSD = 0;
        }

        private void GHSecDocumento_Format(object sender, EventArgs e) {
            
            totalMontoDoc = LibConvert.ToDec(txtMontoDoc.Text);
        }

        private void GFSecDocumento_Format(object sender, EventArgs e) {
            decimal vuelto = 0;
            decimal totalMontoDocTransaccion = 0;
            decimal totalMontoCobrado = 0;
            Saw.Lib.clsLibSaw insUtil = new Saw.Lib.clsLibSaw();
            txtTotalDocumento.Text = LibConvert.ToStr(totalMontoDoc);
            if (LibString.S1IsEqualToS2(insUtil.Plural(txtMonedaDoc.Text), txtNombreMonedaFormaDelCobro.Text)) {
                totalMontoDocTransaccion = LibConvert.ToDec(txtTotalDocumento.Text);
                totalMontoCobrado = LibConvert.ToDec(txtTotalMontoPorMoneda.Text);
                if (totalMontoCobrado > totalMontoDocTransaccion) {
                    vuelto = LibConvert.ToDec(txtTotalMontoPorMoneda.Text) - LibConvert.ToDec(txtTotalDocumento.Text);
                    txtTotalVuelto.Text = LibConvert.ToStr(vuelto,2);
                } else {
                    txtTotalVuelto.Text = LibConvert.ToStr(0, 2);
                }
            }
            totalMontoDoc = 0;
        }
    } //End of class dsrCuadreCajaConDetalleFormaPago
} //End of namespace Galac.Adm.Rpt.Venta
