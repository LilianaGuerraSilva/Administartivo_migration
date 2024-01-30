using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using LibGalac.Aos.ARRpt;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Lib;
using System.Text;

namespace Galac.Adm.Rpt.Venta {
    public partial class dsrCxCEntreFechas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrCxCEntreFechas()
            : this(false, string.Empty) {
        }

        public dsrCxCEntreFechas(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Cuentas por Cobrar entre fechas";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters, bool valMostrarInfoAdicional, bool valMostrarContacto, bool valMostrarNroComprobanteContable, eInformeAgruparPor valAgruparPor, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                LibReport.ConfigFieldStr(this, "txtSectorDeNegocio", string.Empty, "SectorDeNegocio");
                LibReport.ConfigFieldStr(this, "txtZonaDeCobranza", string.Empty, "ZonaDeCobranza");
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "MonedaTotales");
                LibReport.ConfigFieldStr(this, "txtStatusCxC", string.Empty, "StatusStr");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtNroDocumento", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "CodigoCliente");
                LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio", "#,###.0000", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtMontoOriginal", string.Empty, "MontoOriginal");
                LibReport.ConfigFieldDec(this, "txtMontoRestante", string.Empty, "MontoRestante");

                if (valMostrarNroComprobanteContable && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva")) {
                    LibReport.ConfigFieldStr(this, "txtNroComprobanteContable", string.Empty, "NroComprobanteContable");
                } else {
                    LibReport.ChangeControlVisibility(this, "lblNroComprobanteContable", false);
                    LibReport.ChangeControlVisibility(this, "txtNroComprobanteContable", false);
                    float vWidth = lblNroComprobanteContable.Width;
                    lblNombreCliente.Width += vWidth;
                    txtNombreCliente.Width += vWidth;
                    lblCambio.Left += vWidth;
                    txtCambio.Left += vWidth;
                    lblMontoOriginal.Left += vWidth;
                    txtMontoOriginal.Left += vWidth;
                    lblMontoRestante.Left += vWidth;
                    txtMontoRestante.Left += vWidth;

                    txtTotalMontoOriginalPorStatus.Left = lblMontoOriginal.Left;
                    txtTotalMontoOriginalPorMoneda.Left = lblMontoOriginal.Left;
                    txtTotalMontoOriginalPorZonaDeCobranza.Left = lblMontoOriginal.Left;
                    txtTotalMontoOriginalPorSectorDeNegocio.Left = lblMontoOriginal.Left;

                    txtTotalMontoRestantePorStatus.Left = lblMontoRestante.Left;
                    txtTotalMontoRestantePorMoneda.Left = lblMontoRestante.Left;
                    txtTotalMontoRestantePorZonaDeCobranza.Left = lblMontoRestante.Left;
                    txtTotalMontoRestantePorSectorDeNegocio.Left = lblMontoRestante.Left;

                    txtGFStatusCxC.Width += vWidth;
                    txtGFMoneda.Width += vWidth;
                    txtGFZonaDeCobranza.Width += vWidth;
                    txtGFSectorDeNegocio.Width += vWidth;
                    //lblTotalPorMoneda.Width = lblMontoOriginal.Left;
                    //lblTotalPorStatus.Width = lblMontoOriginal.Left;
                    //lblTotalPorZonaCobranza.Width = lblMontoOriginal.Left;
                    //lblTotalPorSectorDeNegocio.Width = lblMontoOriginal.Left;
                }
                if (valMostrarInfoAdicional) {
                    LibReport.ConfigFieldStr(this, "txtInformacionAdicional", string.Empty, "Descripcion");
                } else {
                    LibReport.ChangeControlVisibility(this, "txtInformacionAdicional", false);
                    LibReport.ChangeControlVisibility(this, "lblInformacionAdicional", false);
                    txtInformacionAdicional.Height = 0;
                    lblInformacionAdicional.Height = 0;
                }
                if (valMostrarContacto) {
                    LibReport.ConfigFieldStr(this, "txtContacto", string.Empty, "Contacto");
                } else {
                    LibReport.ChangeControlVisibility(this, "txtContacto", false);
                    LibReport.ChangeControlVisibility(this, "lblContacto", false);
                    txtContacto.Height = 0;
                    lblContacto.Height = 0;
                }

                if (valAgruparPor == eInformeAgruparPor.SectorDeNegocio) {
                    LibReport.ConfigFieldStr(this, "txtGFSectorDeNegocio", string.Empty, "SectorDeNegocio");
                    LibReport.ConfigGroupHeader(this, "GHSectorDeNegocio", "SectorDeNegocio", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoOriginalPorSectorDeNegocio", "MontoOriginal", SummaryFunc.Sum, "GHSectorDeNegocio", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoRestantePorSectorDeNegocio", "MontoRestante", SummaryFunc.Sum, "GHSectorDeNegocio", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHZonaDeCobranza", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFZonaDeCobranza", false, 0);
                } else if (valAgruparPor == eInformeAgruparPor.ZonaDeCobranza) {
                    LibReport.ConfigFieldStr(this, "txtGFZonaDeCobranza", string.Empty, "ZonaDeCobranza");
                    LibReport.ConfigGroupHeader(this, "GHZonaDeCobranza", "ZonaDeCobranza", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoOriginalPorZonaDeCobranza", "MontoOriginal", SummaryFunc.Sum, "GHZonaDeCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoRestantePorZonaDeCobranza", "MontoRestante", SummaryFunc.Sum, "GHZonaDeCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSectorDeNegocio", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSectorDeNegocio", false, 0);
                } else {
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSectorDeNegocio", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSectorDeNegocio", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHZonaDeCobranza", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFZonaDeCobranza", false, 0);
                }


                LibReport.ConfigFieldStr(this, "txtGFStatusCxC", string.Empty, "StatusStr");
                LibReport.ConfigGroupHeader(this, "GHStatus", "Status", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoOriginalPorStatus", "MontoOriginal", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoRestantePorStatus", "MontoRestante", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigFieldStr(this, "txtGFMoneda", string.Empty, "MonedaTotales");
                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoOriginalPorMoneda", "MontoOriginal", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoRestantePorMoneda", "MontoRestante", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);

                string vNotaMonedaCambio = new clsLibSaw().NotaMonedaCambioParaInformes(valMonedaDelInforme, valTasaDeCambio, valMoneda, "cuenta por cobrar");
                LibReport.ConfigFieldStr(this, "txtNotaMonedaCambio", vNotaMonedaCambio, "");

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void Detail_Format(object sender, EventArgs e) {
            try {
                if (LibString.IsNullOrEmpty(txtNroComprobanteContable.Text, true)) {
                    txtNroComprobanteContable.Text = "No aplica";
                }
            } catch (Exception) {
                throw;
            }
        }
    } //End of class dsrCxCEntreFechas
} //End of namespace Galac.Adm.Rpt.Venta