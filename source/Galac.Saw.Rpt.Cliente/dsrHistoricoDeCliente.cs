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

namespace Galac.Saw.Rpt.Cliente {
    /// <summary>
    /// Summary description for dsrHistoricoDeCliente.
    /// </summary>
    public partial class dsrHistoricoDeCliente : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrHistoricoDeCliente()
            : this(false, string.Empty) {
        }

        public dsrHistoricoDeCliente(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Histórico de Cliente";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
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

				LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "Codigo");
				LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "Nombre");
				LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
				LibReport.ConfigFieldStr(this, "txtTipoReporte", string.Empty, "TipoReporte");
                LibReport.ConfigFieldStr(this, "txtTituloTipoReporte", string.Empty, "TituloTipoReporte");
                LibReport.ConfigFieldDec(this, "txtSaldoInicial", string.Empty, "SaldoInicial");
                LibReport.ConfigFieldStr(this, "txtNoDocumentoParaAgrupar", string.Empty, "NoDocumentoParaAgrupar");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "FechaDocumento", "dd/MM/yy");
				LibReport.ConfigFieldStr(this, "txtTipoDocumento", string.Empty, "TipoDeDocumento");
				LibReport.ConfigFieldStr(this, "txtNoDocumento", string.Empty, "NumeroDocumento");
				LibReport.ConfigFieldDate(this, "txtFechaVencimiento", string.Empty, "FechaVencimiento", "dd/MM/yy");
                LibReport.ConfigFieldDec(this, "txtMontoOriginal", string.Empty, "MontoOriginal");
                LibReport.ConfigFieldDec(this, "txtSaldoActual", string.Empty, "SaldoActual");
                LibReport.ConfigFieldStr(this, "txtStatusCobranza", string.Empty, "StatusCobranza");
                LibReport.ConfigFieldStr(this, "txtTipoDocumentoDetail", string.Empty, "TipoDocumentoDetalle");
                LibReport.ConfigFieldStr(this, "txtNoCobranza", string.Empty, "NumeroCobranza");
				LibReport.ConfigFieldDate(this, "txtFechaCobranza", string.Empty, "FechaCobranza", "dd/MM/yy");
				LibReport.ConfigFieldDec(this, "txtMontoCobrado", string.Empty, "MontoCobrado");
				LibReport.ConfigFieldDec(this, "txtTotalMontoOriginal", string.Empty, "TotalMontoOriginal");
				LibReport.ConfigFieldDec(this, "txtTotalMontoCobrado", string.Empty, "TotalMontoCobrado");
				LibReport.ConfigFieldDec(this, "txtTotalSaldoActual", string.Empty, "TotalSaldoActual");
				LibReport.ConfigFieldDec(this, "txtTotalMasSaldoInicial", string.Empty, "TotalMasSaldoInicial");


				LibReport.ConfigFieldStr(this, "txtNotaMonedaCambio", string.Empty, "NotaMonedaCambio");



                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void Detail_Format(object sender, EventArgs e) {
            if (LibConvert.ToStr(this.txtStatusCobranza.Value) == "'0'") {
                this.Detail.Visible = true;
            } else {
                this.Detail.Visible = false;
            }
        }
      
    }
}