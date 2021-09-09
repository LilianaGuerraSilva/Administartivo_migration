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
    public partial class dsrCuadreCajaConDetalleFormaPagoResumido : DataDynamics.ActiveReports.ActiveReport
    {
		#region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrCuadreCajaConDetalleFormaPagoResumido()
            : this(false, string.Empty) {
        }

        public dsrCuadreCajaConDetalleFormaPagoResumido(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
		
		#region Metodos Generados

        public string ReportTitle() {
            return "Informe de Cuadre de Caja con Detalle de Pago Resumido";
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
				LibReport.ConfigFieldStr(this, "txtMonedaPago", string.Empty, "MonedaPago");
                LibReport.ConfigFieldInt(this, "txtConsecutivoCaja", string.Empty, "ConsecutivoCaja");
				LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoFacturas", string.Empty, "MontoFacturas", 2);
				LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoCobro", string.Empty, "MontoCobro", 2);
				LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoVuelto", string.Empty, "", 2);                
                LibReport.ConfigFieldStr(this, "txtNombreFormaDelCobro", string.Empty, "TipoDeCobro");
				LibReport.ConfigFieldStr(this, "txtCodMonedaDeCobro", string.Empty, "CodMonedaDeCobro");
				LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoFacturas", string.Empty, "", 2);
                LibReport.ConfigSummaryField(this, "txtTotalMontoCobro", "MontoCobro", SummaryFunc.Sum, "GHSecCaja", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoVuelto", string.Empty, "",2);
                if (incluirTotalesPorFormaDeCobro) {
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoBs", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalTarjetaBs", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepTransfBs", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoUSD", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepTransfUSD", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtCodMonedaCobro", string.Empty, "CodMonedaCobro", 2);
                } else {
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecMonedaDoc", false, 0);
                }
				LibReport.ConfigGroupHeader(this, "GHSecMonedaDoc", "MonedaDoc", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHSecMonedaCobro", "MonedaPago", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHSecCaja", "ConsecutivoCaja", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
        private string codMonedaCobro;
        private decimal totalEfectivoBs = 0;
        private decimal totalTarjetaBs = 0;
        private decimal totalDepTransfBs = 0;
        private decimal totalEfectivoUSD = 0;
        private decimal totalDepTransfUSD = 0;
        private void Detail_Format(object sender, EventArgs e) {
            eFormaDeCobro codFormaCobro = (eFormaDeCobro)LibConvert.DbValueToEnum(txtNombreFormaDelCobro.Text);
            txtNombreFormaDelCobro.Text = codFormaCobro.GetDescription(0).ToUpper();
            codMonedaCobro = txtCodMonedaDeCobro.Text;
            switch (codMonedaCobro) {
                case "VES":
                case "VED":
                    if (codFormaCobro == eFormaDeCobro.Efectivo) {
                        totalEfectivoBs += LibConvert.ToDec(txtMontoCobro.Text);
                    } else if (codFormaCobro == eFormaDeCobro.Tarjeta) {
                        totalTarjetaBs += LibConvert.ToDec(txtMontoCobro.Text);
                    } else if (codFormaCobro == eFormaDeCobro.Deposito) {
                        totalDepTransfBs += LibConvert.ToDec(txtMontoCobro.Text);
                    }
                    break;
                case "USD":
                    if (codFormaCobro == eFormaDeCobro.Efectivo) {
                        totalEfectivoUSD += LibConvert.ToDec(txtMontoCobro.Text);
                    } else if (codFormaCobro == eFormaDeCobro.Deposito) {
                        totalDepTransfUSD += LibConvert.ToDec(txtMontoCobro.Text);
                    }
                    break;
            }
        }

        private void GFSecCaja_Format(object sender, EventArgs e) {
            decimal vuelto = 0;
            decimal totalMontoFacturas = 0;
            decimal totalMontoCobro = 0;
            txtTotalMontoFacturas.Text = LibConvert.ToStr(txtMontoFacturas.Text);
            if (LibString.S1IsEqualToS2(txtMonedaDoc.Text, txtMonedaPago.Text)) {
                totalMontoFacturas = LibConvert.ToDec(txtTotalMontoFacturas.Text);
                totalMontoCobro = LibConvert.ToDec(txtTotalMontoCobro.Text);
                if (totalMontoCobro > totalMontoFacturas) {
                    vuelto = LibConvert.ToDec(txtTotalMontoCobro.Text) - LibConvert.ToDec(txtTotalMontoFacturas.Text);
                    txtTotalMontoVuelto.Text = LibConvert.ToStr(vuelto,2);
                } else {
                    txtTotalMontoVuelto.Text = LibConvert.ToStr(0, 2);
                }
            }
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
    } //End of class dsrCuadreCajaConDetalleFormaPagoResumido
} //End of namespace Galac.Adm.Rpt.Venta
