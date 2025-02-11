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
using System.Runtime.CompilerServices;
namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrCuadreCajaCobroMultimonedaDetallado.
    /// </summary>
    public partial class dsrCuadreCajaCobroMultimonedaDetallado: DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        private decimal subTotalPorTipoDeDocumento = 0;
        private decimal subTotalDocumentoMonedaLocal = 0;
        private decimal subTotalDocumentoMonedaExt = 0;
        private decimal TotalCajaMonedaLocal = 0;
        private decimal TotalCajaMonedaExt = 0;
        private decimal totalEfectivoMonedaLocal = 0;
        private decimal totalDepositoMonedaLocal = 0;
        private decimal totalOtrosMonedaLocal = 0;
        private decimal totalTarjetaMonedaLocal = 0;
        private decimal totalEfectivoMonedaExt = 0;
        private decimal totalTransferenciaMonedaExt = 0;
        private string vCodigoMonedaDeFormaCobro = string.Empty;
        #endregion //Variables
        #region Propiedades
        private Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
        public string CodigoMonedaExtranjera { get; set; }
        public string SimboloMonedaExtranjera { get; set; }
        #endregion
        #region Constructores
        public dsrCuadreCajaCobroMultimonedaDetallado()
            : this(false, string.Empty) {
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
        }
        public dsrCuadreCajaCobroMultimonedaDetallado(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados
        public string ReportTitle() {
            return "Informe de Cuadre de Caja con Cobro Multimoneda (Cierre del Día)";
        }
        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            bool incluirTotalesPorFormaDeCobro = LibConvert.SNToBool(valParameters["TotalesTipoDeCobro"]);
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
                LibReport.ConfigFieldStr(this, "txtUserName", string.Empty, "NombreDelOperador");
                LibReport.ConfigFieldInt(this, "txtConsecutivoCaja", string.Empty, "ConsecutivoCaja");
                LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
                LibReport.ConfigFieldStr(this, "txtNombreMoneda", string.Empty, "NombreMoneda");
                LibReport.ConfigFieldStr(this, "txtTipoDeDocumento", string.Empty, "TipoDeDocumento");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
                LibReport.ConfigFieldDate(this, "txtHora", string.Empty, "HoraModificacion", "hh:mm");
                LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtNumeroComprobanteFiscal", string.Empty, "NumeroComprobanteFiscal");
                LibReport.ConfigFieldStr(this, "txtSimboloMonedaDoc", string.Empty, "SimboloMonedaDoc");
                LibReport.ConfigFieldDec(this, "txtTotalFactura", string.Empty, "TotalFactura");
                LibReport.ConfigFieldStr(this, "txtNombreMonedaFormaDelCobro", string.Empty, "NombreMonedaFormaDelCobro");
                LibReport.ConfigFieldStr(this, "txtCodMonedaCobro", string.Empty, "CodMonedaFormaDelCobro");
                LibReport.ConfigFieldStr(this, "txtNombreTipoDeCobro", string.Empty, "TipoDeCobro");
                LibReport.ConfigFieldDec(this, "txtMonto", string.Empty, "Monto");
                if (enMonedaOriginal) {
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtCambioDeOperacion", string.Empty, "TasaOperacion", 4);                    
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtCambioABolivares", LibConvert.ToStr(1), "", 4);
                    LibReport.ConfigFieldStr(this, "txtCambioABolivaresSimbolo", string.Empty, "");
                    LibReport.ChangeControlVisibility(this, "txtCambioABolivares", false);
                    LibReport.ConfigFieldStr(this, "txtLblCambioABolivares", string.Empty, " ");
                    LibReport.ChangeControlVisibility(this, "txtCambioABolivaresSimbolo", false);
                } else {
                    LibReport.ChangeControlVisibility(this, "txtCambioDeOperacion", false);
                    LibReport.ConfigLabel(this, "lblCambioDeOperacion","");
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtCambioABolivares", LibConvert.ToStr(1), "CambioABolivares", 4);
                    LibReport.ConfigFieldStr(this, "txtCambioABolivaresSimbolo", string.Empty, "SimboloFormaDeCobro");
                }
                LibReport.ConfigSummaryField(this, "txtSubTotalCobro", "Monto", SummaryFunc.Sum, "GHSecCobro", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtSubTotalDocumentosCobrado", string.Empty, "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalCaja", string.Empty, "", 2);
                if (incluirTotalesPorFormaDeCobro) {
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoMonedaLocal", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalTarjetaMonedaLocal", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalDepositoMonedaLocal", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalEfectivoMonedaExt", string.Empty, "", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalTransferenciaMonedaExt", string.Empty, "", 2);
                    LibARReport.ChangeControlVisibility(this, "lblTotalPorMonedaCobro", true);
                    LibARReport.ChangeControlVisibility(this, "lblEfectivoMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "lblTarjetaMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "lblDepositoMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "lblDepositoSimboloMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "lblEfectivoMonedaExt", true);
                    LibARReport.ChangeControlVisibility(this, "lblTransferenciaMonedaExt", true);
                    LibARReport.ChangeControlVisibility(this, "lblEfectivoSimboloMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "lblTarjetaSimboloMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "lblEfectivoSimboloMonedaExt", true);
                    LibARReport.ChangeControlVisibility(this, "lblTransferenciaSimboloMonedaExt", true);
                    LibARReport.ChangeControlVisibility(this, "txtTotalEfectivoMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "txtTotalTarjetaMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "txtTotalDepositoMonedaLocal", true);
                    LibARReport.ChangeControlVisibility(this, "txtTotalEfectivoMonedaExt", true);
                    LibARReport.ChangeControlVisibility(this, "txtTotalTransferenciaMonedaExt", true);
                } else {
                    LibARReport.ChangeControlVisibility(this, "lblTotalPorMonedaCobro", false);
                    LibARReport.ChangeControlVisibility(this, "lblEfectivoMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "lblTarjetaMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "lblDepositoMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "lblDepositoSimboloMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "lblEfectivoMonedaExt", false);
                    LibARReport.ChangeControlVisibility(this, "lblTransferenciaMonedaExt", false);
                    LibARReport.ChangeControlVisibility(this, "lblEfectivoSimboloMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "lblTarjetaSimboloMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "lblEfectivoSimboloMonedaExt", false);
                    LibARReport.ChangeControlVisibility(this, "lblTransferenciaSimboloMonedaExt", false);
                    LibARReport.ChangeControlVisibility(this, "txtTotalEfectivoMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "txtTotalTarjetaMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "txtTotalDepositoMonedaLocal", false);
                    LibARReport.ChangeControlVisibility(this, "txtTotalEfectivoMonedaExt", false);
                    LibARReport.ChangeControlVisibility(this, "txtTotalTransferenciaMonedaExt", false);
                }
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalCajaMonedaLocal", string.Empty, "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalCajaMonedaExt", string.Empty, "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtSubTotalMonedaLocal", string.Empty, "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtSubTotalMonedaExt", string.Empty, "", 2);
                #region Simbolos
                LibReport.ConfigFieldStr(this, "txtLblSubTotalSimboloMonedaLocal", vMonedaLocal.GetHoySimboloMoneda(), string.Empty);
                LibReport.ConfigFieldStr(this, "txtLblSubTotalSimboloMonedaExt", SimboloMonedaExtranjera, string.Empty);
                LibReport.ConfigFieldStr(this, "txtLblTotalCajaSimboloMonedaLocal", vMonedaLocal.GetHoySimboloMoneda(), string.Empty);
                LibReport.ConfigFieldStr(this, "txtLblTotalCajaSimboloMonedaExt", SimboloMonedaExtranjera, string.Empty);
                LibARReport.ConfigLabel(this, "lblEfectivoSimboloMonedaLocal", vMonedaLocal.GetHoySimboloMoneda());
                LibARReport.ConfigLabel(this, "lblTarjetaSimboloMonedaLocal", vMonedaLocal.GetHoySimboloMoneda());
                LibARReport.ConfigLabel(this, "lblDepositoSimboloMonedaLocal", vMonedaLocal.GetHoySimboloMoneda());
                if (enMonedaOriginal) {
                    LibARReport.ConfigLabel(this, "lblEfectivoSimboloMonedaExt", SimboloMonedaExtranjera);
                    LibARReport.ConfigLabel(this, "lblTransferenciaSimboloMonedaExt", SimboloMonedaExtranjera);
                } else {
                    LibARReport.ConfigLabel(this, "lblEfectivoSimboloMonedaExt", SimboloMonedaExtranjera + "/" + vMonedaLocal.GetHoySimboloMoneda());
                    LibARReport.ConfigLabel(this, "lblTransferenciaSimboloMonedaExt", SimboloMonedaExtranjera + "/" + vMonedaLocal.GetHoySimboloMoneda());
                }
                #endregion
                LibReport.ConfigGroupHeader(this, "GHSecOperador", "NombreDelOperador", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecCaja", "ConsecutivoCaja", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecMoneda", "NombreMoneda", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecDocumento", "TipoDeDocumento", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecCobranzaDelDocumento", "Numero", GroupKeepTogether.FirstDetail, RepeatStyle.None, false, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecCobro", "NombreMonedaFormaDelCobro", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void GHSecDocumento_Format(object sender, EventArgs e) {
            subTotalPorTipoDeDocumento = 0;
        }

        private void GHSecCobranzaDelDocumento_Format(object sender, EventArgs e) {
            subTotalPorTipoDeDocumento += LibConvert.ToDec(txtTotalFactura.Text);
            txtSubTotalDocumentosCobrado.Text = LibConvert.ToStr(subTotalPorTipoDeDocumento);
        }

        private void GHSecCaja_Format(object sender, EventArgs e) {
            TotalCajaMonedaLocal = 0;
            TotalCajaMonedaExt = 0;
            subTotalDocumentoMonedaLocal = 0;
            subTotalDocumentoMonedaExt = 0;
        }

        private void GHSecOperador_Format(object sender, EventArgs e) {
            totalEfectivoMonedaLocal = 0;
            totalTarjetaMonedaLocal = 0;
            totalDepositoMonedaLocal = 0;
            totalOtrosMonedaLocal = 0;
            totalEfectivoMonedaExt = 0;
            totalTransferenciaMonedaExt = 0;
            txtTotalEfectivoMonedaLocal.Text = LibConvert.ToStr(totalEfectivoMonedaLocal);
            txtTotalTarjetaMonedaLocal.Text = LibConvert.ToStr(totalTarjetaMonedaLocal);
            txtTotalDepositoMonedaLocal.Text = LibConvert.ToStr(totalDepositoMonedaLocal);
            txtTotalEfectivoMonedaExt.Text = LibConvert.ToStr(totalEfectivoMonedaExt);
            txtTotalTransferenciaMonedaExt.Text = LibConvert.ToStr(totalTransferenciaMonedaExt);
        }

        private void Detail_Format(object sender, EventArgs e) {
            eFormaDeCobro codFormaCobro = (eFormaDeCobro)LibConvert.DbValueToEnum(txtNombreTipoDeCobro.Text);
            txtNombreTipoDeCobro.Text = codFormaCobro.GetDescription(0).ToUpper();
            string vCodigoMonedaLocal = vMonedaLocal.GetHoyCodigoMoneda();
            if (LibString.S1IsEqualToS2(vCodigoMonedaDeFormaCobro, vCodigoMonedaLocal)) {
                if (codFormaCobro == eFormaDeCobro.Efectivo) {
                    totalEfectivoMonedaLocal += LibConvert.ToDec(txtMonto.Text);
                    txtTotalEfectivoMonedaLocal.Text = LibConvert.ToStr(totalEfectivoMonedaLocal);
                    subTotalDocumentoMonedaLocal += LibConvert.ToDec(txtMonto.Text);
                } else if (codFormaCobro == eFormaDeCobro.Tarjeta) {
                    totalTarjetaMonedaLocal += LibConvert.ToDec(txtMonto.Text);
                    txtTotalTarjetaMonedaLocal.Text = LibConvert.ToStr(totalTarjetaMonedaLocal);
                    subTotalDocumentoMonedaLocal += LibConvert.ToDec(txtMonto.Text);
                } else if (codFormaCobro == eFormaDeCobro.Deposito) {
                    totalDepositoMonedaLocal += LibConvert.ToDec(txtMonto.Text);
                    txtTotalDepositoMonedaLocal.Text = LibConvert.ToStr(totalDepositoMonedaLocal);
                    subTotalDocumentoMonedaLocal += LibConvert.ToDec(txtMonto.Text);
                } else {
                    totalOtrosMonedaLocal += LibConvert.ToDec(txtMonto.Text);
                    subTotalDocumentoMonedaLocal += LibConvert.ToDec(txtMonto.Text);
                }
            } else if (!LibString.IsNullOrEmpty(CodigoMonedaExtranjera) && LibString.S1IsEqualToS2(vCodigoMonedaDeFormaCobro, CodigoMonedaExtranjera)) {
                if (codFormaCobro == eFormaDeCobro.Efectivo) {
                    totalEfectivoMonedaExt += LibConvert.ToDec(txtMonto.Text);
                    txtTotalEfectivoMonedaExt.Text = LibConvert.ToStr(totalEfectivoMonedaExt);
                    subTotalDocumentoMonedaExt += LibConvert.ToDec(txtMonto.Text);
                } else if (codFormaCobro == eFormaDeCobro.Deposito) {
                    totalTransferenciaMonedaExt += LibConvert.ToDec(txtMonto.Text);
                    txtTotalTransferenciaMonedaExt.Text = LibConvert.ToStr(totalTransferenciaMonedaExt);
                    subTotalDocumentoMonedaExt += LibConvert.ToDec(txtMonto.Text);
                }
            }
            txtSubTotalMonedaLocal.Text = LibConvert.ToStr(subTotalDocumentoMonedaLocal);
            txtSubTotalMonedaExt.Text = LibConvert.ToStr(subTotalDocumentoMonedaExt);
            TotalCajaMonedaLocal = subTotalDocumentoMonedaLocal;
            txtTotalCajaMonedaLocal.Text = LibConvert.ToStr(TotalCajaMonedaLocal);
            TotalCajaMonedaExt = subTotalDocumentoMonedaExt;
            txtTotalCajaMonedaExt.Text = LibConvert.ToStr(TotalCajaMonedaExt);
        }

        private void GHSecCobro_Format(object sender, EventArgs e) {
            vCodigoMonedaDeFormaCobro = string.Empty;
            vCodigoMonedaDeFormaCobro = txtCodMonedaCobro.Text;
            if (vCodigoMonedaDeFormaCobro == string.Empty) {
                LibARReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSecCobro", false, 0);
                LibARReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecCobro", false, 0);
                LibARReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", false, 0);
            } else {
                LibARReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSecCobro", true, (float)0.229);
                LibARReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecCobro", true, (float)0.208);
                LibARReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", true, (float)0.188);
            }
        }
    } //End of class dsrCuadreCajaCobroMultimonedaDetallado
} //End of namespace Galac.Adm.Rpt.Venta