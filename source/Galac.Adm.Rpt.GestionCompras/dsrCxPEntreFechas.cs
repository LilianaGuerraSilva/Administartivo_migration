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
using System.Text;

namespace Galac.Adm.Rpt.GestionCompras {
    public partial class dsrCxPEntreFechas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrCxPEntreFechas()
            : this(false, string.Empty) {
        }

        public dsrCxPEntreFechas(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados
        public string ReportTitle() {
            return "Cuentas por Pagar entre Fechas";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters, bool valMostrarInfoAdicional, bool valMostrarNroComprobanteContable, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio) {
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

                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
                LibReport.ConfigFieldStr(this, "txtStatusCxP", string.Empty, "StatusStr");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtNroDocumento", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtCodigoProveedor", string.Empty, "CodigoProveedor");
                LibReport.ConfigFieldStr(this, "txtNombreProveedor", string.Empty, "NombreProveedor");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio", "#,###.0000", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtMontoOriginal", string.Empty, "MontoOriginal");
                LibReport.ConfigFieldDec(this, "txtMontoRestante", string.Empty, "MontoRestante");

                if (valMostrarNroComprobanteContable && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva")) {
                    LibReport.ConfigFieldStr(this, "txtNroComprobanteContable", string.Empty, "NroComprobanteContable");
                } else {
                    LibReport.ChangeControlVisibility(this, "lblNroComprobanteContable", false);
                    LibReport.ChangeControlVisibility(this, "txtNroComprobanteContable", false);
                    float vWidth = lblNroComprobanteContable.Width;
                    lblNombreProveedor.Width += vWidth;
                    txtNombreProveedor.Width += vWidth;
                    lblCambio.Left += vWidth;
                    txtCambio.Left += vWidth;
                    lblMontoOriginal.Left += vWidth;
                    txtMontoOriginal.Left += vWidth;
                    lblMontoRestante.Left += vWidth;
                    txtMontoRestante.Left += vWidth;

                    txtTotalMontoOriginalPorMoneda.Left = lblMontoOriginal.Left;
                    txtTotalMontoOriginalPorStatus.Left = lblMontoOriginal.Left;

                    txtTotalMontoRestantePorMoneda.Left = lblMontoRestante.Left;
                    txtTotalMontoRestantePorStatus.Left = lblMontoRestante.Left;

                    txtGFStatusCxP.Width += vWidth;
                    txtGFMoneda.Width += vWidth;

                    //lblTotalPorMoneda.Width = lblMontoOriginal.Left;
                    //lblTotalPorStatus.Width = lblMontoOriginal.Left;
                }
                if (valMostrarInfoAdicional) {
                    LibReport.ConfigFieldStr(this, "txtInformacionAdicional", string.Empty, "Descripcion");
                } else {
                    LibReport.ChangeControlVisibility(this, "txtInformacionAdicional", false);
                    LibReport.ChangeControlVisibility(this, "lblInformacionAdicional", false);
                }


                LibReport.ConfigFieldStr(this, "txtGFStatusCxP", string.Empty, "StatusStr");
                LibReport.ConfigGroupHeader(this, "GHStatus", "Status", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoOriginalPorStatus", "MontoOriginal", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoRestantePorStatus", "MontoRestante", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigFieldStr(this, "txtGFMoneda", string.Empty, "Moneda");
                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoOriginalPorMoneda", "MontoOriginal", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoRestantePorMoneda", "MontoRestante", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);

                string vNotaMonedaCambio = new clsLibSaw().NotaMonedaCambioParaInformes(valMonedaDelInforme, valTasaDeCambio, valMoneda, "cuenta por pagar");
                LibReport.ConfigFieldStr(this, "txtNotaMonedaCambio", vNotaMonedaCambio, "");

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrCuentasPorPagarEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado