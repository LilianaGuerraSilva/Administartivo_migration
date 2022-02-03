using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.ARRpt;

namespace Galac.Adm.Rpt.Banco {
	/// <summary>
	/// Summary description for dsrSaldosBancarios.
	/// </summary>
	public partial class dsrSaldosBancarios : ActiveReport {
		#region Variables
		private bool _UseExternalRpx;
		private static string _RpxFileName;
		private decimal totalSaldoFinal = 0;
		#endregion //Variables

		#region Constructores
		public dsrSaldosBancarios()
			: this(false, string.Empty) {
		}

		public dsrSaldosBancarios(bool initUseExternalRpx, string initRpxFileName) {
			InitializeComponent();
			_UseExternalRpx = initUseExternalRpx;
			if (_UseExternalRpx) {
				_RpxFileName = initRpxFileName;
			}
		}
		#endregion //Constructores

		#region Metodos Generados
		public string ReportTitle() {
			return "Saldos Bancarios";
		}

		public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
			if (_UseExternalRpx) {
				string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
				if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
					LibReport.LoadLayout(this, vRpxPath);
				}
			}
			if (LibReport.ConfigDataSource(this, valDataSource)) {
				LibReport.ConfigFieldStr(this, "txtCompania", valParameters["NombreCompania"], string.Empty);
				LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
				LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
				LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "txtNumeroDePagina", "lblFechaInicialYFinal", LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

				LibReport.ConfigFieldStr(this, "txtNombreDeLaMoneda", string.Empty, "NombreDeLaMoneda");
				LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "Codigo");
				LibReport.ConfigFieldStr(this, "txtNombreCuenta", string.Empty, "NombreCuenta");
				LibReport.ConfigFieldStr(this, "txtNumeroCuenta", string.Empty, "NumeroCuenta");
				LibReport.ConfigFieldDec(this, "txtSaldoInicial", string.Empty, "SaldoInicial");
				LibReport.ConfigFieldDec(this, "txtIngresos", string.Empty, "Ingresos");
				LibReport.ConfigFieldDec(this, "txtEgresos", string.Empty, "Egresos");
				LibReport.ConfigFieldDec(this, "txtSaldoFinal", string.Empty, "");

				LibReport.ConfigSummaryField(this, "txtTotalSaldoInicial", "SaldoInicial", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
				LibReport.ConfigSummaryField(this, "txtTotalIngresos", "Ingresos", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
				LibReport.ConfigSummaryField(this, "txtTotalEgresos", "Egresos", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);

				LibReport.ConfigGroupHeader(this, "GHMoneda", "NombreDeLaMoneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);

				LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Portrait);
				return true;
			}
			return false;
		}
		#endregion //Metodos Generados

		private void GHMoneda_Format(object sender, EventArgs e) {
			totalSaldoFinal = 0;
		}

		private void Detail_Format(object sender, EventArgs e) {
			decimal resultadoParse, saldoFinalFila = 0;
			saldoFinalFila += (decimal.TryParse(txtSaldoInicial.Text, out resultadoParse) ? resultadoParse : 0);
			saldoFinalFila += (decimal.TryParse(txtIngresos.Text, out resultadoParse) ? resultadoParse : 0);
			saldoFinalFila -= (decimal.TryParse(txtEgresos.Text, out resultadoParse) ? resultadoParse : 0);
			totalSaldoFinal += saldoFinalFila;
			txtSaldoFinal.Text = LibConvert.ToStr(saldoFinalFila);
		}

		private void GFMoneda_Format(object sender, EventArgs e) {
			txtTotalSaldoFinal.Text = LibConvert.ToStr(totalSaldoFinal);
		}
	}
}