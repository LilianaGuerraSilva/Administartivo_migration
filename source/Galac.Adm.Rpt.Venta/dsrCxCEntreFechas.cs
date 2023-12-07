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
                LibReport.ConfigFieldStr(this, "txtStatusCxC", string.Empty, "StatusStr");
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtNroDocumento", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "CodigoCliente");
                LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
                LibReport.ConfigFieldDec(this, "txtMontoTotal", string.Empty, "Monto");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio");
                if (valMostrarNroComprobanteContable && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva")) {
                    LibReport.ConfigFieldStr(this, "txtNroComprobanteContable", string.Empty, "NroComprobanteContable");
                } else {
                    LibReport.ChangeControlVisibility(this, "lblNroComprobanteContable", false);
                    LibReport.ChangeControlVisibility(this, "txtNroComprobanteContable", false);
                    float vWidth = lblMontoTotal.Width + lblNroComprobanteContable.Width; ;
                    lblMontoTotal.Width = vWidth;
                    txtMontoTotal.Width = vWidth;
                    txtTotalMontoTotalPorMoneda.Width = vWidth;
                    txtTotalMontoTotalPorStatus.Width = vWidth;
                    txtTotalMontoTotalPorZonaDeCobranza.Width = vWidth;
                    txtTotalMontoTotalSectorDeNegocio.Width = vWidth;
                }
                if (valMostrarInfoAdicional) {
                    LibReport.ConfigFieldStr(this, "txtInformacionAdicional", string.Empty, "Descripcion");
                } else {
                    LibReport.ChangeControlVisibility(this, "txtInformacionAdicional", false);
                    LibReport.ChangeControlVisibility(this, "lblInformacionAdicional", false);
                }
                if (valMostrarContacto) {
                    LibReport.ConfigFieldStr(this, "txtContacto", string.Empty, "Contacto");
                } else {
                    LibReport.ChangeControlVisibility(this, "txtContacto", false);
                    LibReport.ChangeControlVisibility(this, "lblContacto", false);
                }

                if (valAgruparPor == eInformeAgruparPor.SectorDeNegocio) {
                    LibReport.ConfigGroupHeader(this, "GHSectorDeNegocio", "SectorDeNegocio", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoTotalSectorDeNegocio", "Monto", SummaryFunc.Sum, "GHSectorDeNegocio", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHZonaDeCobranza", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFZonaDeCobranza", false, 0);
                } else if (valAgruparPor == eInformeAgruparPor.ZonaDeCobranza) {
                    LibReport.ConfigGroupHeader(this, "GHZonaDeCobranza", "ZonaDeCobranza", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoTotalPorZonaDeCobranza", "Monto", SummaryFunc.Sum, "GHZonaDeCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSectorDeNegocio", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSectorDeNegocio", false, 0);
                } else {
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSectorDeNegocio", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSectorDeNegocio", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHZonaDeCobranza", false, 0);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFZonaDeCobranza", false, 0);
                }
                LibReport.ConfigGroupHeader(this, "GHStatus", "Status", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoTotalPorStatus", "Monto", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoTotalPorMoneda", "Monto", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);

                string vNotaMonedaCambio = new clsLibSaw().NotaMonedaCambioParaInformes(valMonedaDelInforme, valTasaDeCambio, valMoneda);
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