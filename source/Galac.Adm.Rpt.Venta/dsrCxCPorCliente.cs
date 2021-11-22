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
	/// <summary>
	/// Summary description for dsrCxCPorCliente.
	/// </summary>
	public partial class dsrCxCPorCliente : ActiveReport {
		#region Variables
		private bool _UseExternalRpx;
		private static string _RpxFileName;
		#endregion //Variables

		#region Constructores
		public dsrCxCPorCliente()
			: this(false, string.Empty)
		{
		}

		public dsrCxCPorCliente(bool initUseExternalRpx, string initRpxFileName)
		{
			InitializeComponent();
			_UseExternalRpx = initUseExternalRpx;
			if (_UseExternalRpx) {
				_RpxFileName = initRpxFileName;
			}
		}
		#endregion //Constructores

		#region Metodos Generados
		public string ReportTitle()
		{
			return "Cuentas por Cobrar Por Cliente";
		}

		public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters)
		{
			Saw.Lib.clsUtilRpt UtiltRpt = new Saw.Lib.clsUtilRpt();
			bool vMostrarContacto = LibConvert.SNToBool(valParameters["MostrarContacto"]);
			eClientesOrdenadosPor vClientesOrdenadosPor = (eClientesOrdenadosPor)LibConvert.DbValueToEnum(valParameters["ClientesOrdenadosPor"]);
			Saw.Lib.eMonedaParaImpresion MonedaDelReporte = (Saw.Lib.eMonedaParaImpresion)LibConvert.DbValueToEnum(valParameters["MonedaDelReporte"]);
			Saw.Lib.eMonedaParaImpresion MonedasAgrupadasPor = (Saw.Lib.eMonedaParaImpresion)LibConvert.DbValueToEnum(valParameters["MonedasAgrupadasPor"]);

			Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
			vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
			string vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
			bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, MonedaDelReporte.GetDescription());
			bool vIsGroupedByMonedaLocal = LibString.S1IsInS2(vMonedaLocal, MonedasAgrupadasPor.GetDescription());

			if (_UseExternalRpx) {
				string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
				if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
					LibReport.LoadLayout(this, vRpxPath);
				}
			}
			if (LibReport.ConfigDataSource(this, valDataSource)) {
				LibReport.ConfigFieldStr(this, "txtCompania", valParameters["NombreCompania"], string.Empty);
				LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle());
				LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
				LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
				LibReport.ConfigHeader(this, "txtCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "txtNumeroDePagina", "lblFechaInicialYFinal", LibGraphPrnSettings.PrintPageNumber, LibGraphPrnSettings.PrintEmitDate);

				if (vIsInMonedaLocal) {
					LibReport.ConfigLabel(this, "lblMensajeDelCambioDeLaMoneda", "Nota: Los Montos en Monedas extranjeras son cálculadas a Bolívares tomando en cuenta la última tasa de cambio");
				}
				else {
					LibReport.ChangeControlVisibility(this, "lblMensajeDelCambioDeLaMoneda", true, false);
				}

				if (vIsGroupedByMonedaLocal) {
					LibReport.ConfigFieldStr(this, "txtMonedaPorGrupo", vMonedaLocal, string.Empty);
				}
				else {
					LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
					LibReport.ConfigFieldStr(this, "txtMonedaPorGrupo", string.Empty, "Moneda");
				}

				LibReport.ConfigGroupHeader(this, "GHZonaCobranza", "ZonaCobranza", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigFieldStr(this, "txtZona", string.Empty, "ZonaCobranza");

				LibReport.ConfigGroupHeader(this, "GHCodigoCliente", "Codigo", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "Codigo");
				LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
				LibReport.ConfigFieldStr(this, "txtTelefonos", string.Empty, "Telefono");
				LibReport.ConfigFieldStr(this, "txtDireccion", string.Empty, "Direccion");

				if (vMostrarContacto) {
					LibReport.ConfigFieldStr(this, "txtContacto", string.Empty, "Contacto");
				}
				else {
					LibReport.ChangeControlVisibility(this, "txtContacto", true, false);
					LibReport.ChangeControlVisibility(this, "lblContacto", true, false);
				}

				LibReport.ConfigFieldStr(this, "txtNumeroDocumento", string.Empty, "Numero");
				LibReport.ConfigFieldStr(this, "txtEstatus", string.Empty, "Estatus");
				LibReport.ConfigFieldStr(this, "txtNumFiscal", string.Empty, "NumFiscal");
				//				LibReport.ConfigFieldDate(this, "txtFechaVencimiento", string.Empty, "FechaVencimiento", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort); esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato directo
				LibReport.ConfigFieldDate(this, "txtFechaVencimiento", string.Empty, "FechaVencimiento", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
				LibReport.ConfigFieldDec(this, "txtTotalLinea", string.Empty, "Monto");

				LibReport.ConfigSummaryField(this, "txtTotalGeneralCliente", "Monto", SummaryFunc.Sum, "GHCodigoCliente", SummaryRunning.All, SummaryType.SubTotal);

				LibReport.ConfigFieldStr(this, "txtZonaCobranza", string.Empty, "ZonaCobranza");
				LibReport.ConfigSummaryField(this, "txtTotalGeneralZona", "Monto", SummaryFunc.Sum, "GHZonaCobranza", SummaryRunning.All, SummaryType.SubTotal);

				LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
				LibReport.ConfigSummaryField(this, "txtTotalGeneralMoneda", "Monto", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);

				LibReport.ConfigFieldStr(this, "txtSiglasE1", eStatusCXC.PORCANCELAR.GetDescription(1), string.Empty);
				LibReport.ConfigFieldStr(this, "txtNombreE1", eStatusCXC.PORCANCELAR.GetDescription(0), string.Empty);
				LibReport.ConfigFieldStr(this, "txtSiglasE2", eStatusCXC.ABONADO.GetDescription(1), string.Empty);
				LibReport.ConfigFieldStr(this, "txtNombreE2", eStatusCXC.ABONADO.GetDescription(0), string.Empty);
				LibReport.ConfigFieldStr(this, "txtSiglasE3", eStatusCXC.CANCELADO.GetDescription(1), string.Empty);
				LibReport.ConfigFieldStr(this, "txtNombreE3", eStatusCXC.CANCELADO.GetDescription(0), string.Empty);
				LibReport.ConfigFieldStr(this, "txtSiglasE4", eStatusCXC.ANULADO.GetDescription(1), string.Empty);
				LibReport.ConfigFieldStr(this, "txtNombreE4", eStatusCXC.ANULADO.GetDescription(0), string.Empty);

				LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Portrait);
				return true;
			}
			return false;
		}
		#endregion //Metodos Generados

	} //End of class dsrCxCPorCliente

} //End of namespace Galac.Adm.Rpt.Venta
