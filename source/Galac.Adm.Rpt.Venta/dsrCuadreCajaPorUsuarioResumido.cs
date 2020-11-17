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

namespace Galac.Adm.Rpt.Venta
{
    /// <summary>
    /// Summary description for dsrCuadreCajaPorUsuarioResumido.
    /// </summary>
    public partial class dsrCuadreCajaPorUsuarioResumido : DataDynamics.ActiveReports.ActiveReport
    {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrCuadreCajaPorUsuarioResumido()
            : this(false, string.Empty) {
        }
        public dsrCuadreCajaPorUsuarioResumido(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
		#endregion //Constructores
		#region Metodos Generados

        public string ReportTitle() {
            return "Cuadre de Caja Por Usuario Resumido";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            string vNombreCompania = valParameters["NombreCompania"] + " - " + valParameters["RifCompania"];
            bool esInformeEnMonedaOriginal = LibConvert.SNToBool(valParameters["EnMonedaOriginal"]);
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
                LibReport.ConfigFieldStr(this, "txtNombreUsuario", string.Empty, "NombreUsuario");
                #region Detail Para Totalizar
                //LibReport.ConfigFieldInt(this, "txtConsecutivoCaja", string.Empty, "ConsecutivoCaja");
				//LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
				LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDoc");
				LibReport.ConfigFieldStr(this, "txtMonedaCobro", string.Empty, "MonedaCobro");
				LibReport.ConfigFieldStr(this, "txtCodMonedaCobro", string.Empty, "CodMonedaCobro"); // No sera necesario
				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtNumeroDoc", string.Empty, "NumeroDoc");
				LibReport.ConfigFieldStr(this, "txtNumeroCompFiscal", string.Empty, "NumFiscal");
				LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
				LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoDoc", string.Empty, "MontoDoc", 2);
				LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoPagado", string.Empty, "MontoPagado", 2);
				LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoVuelto", string.Empty, "Vuelto", 2);
                #endregion
                #region Totales
                // Por Operador (Usuario)
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoDoc", LibConvert.ToStr((decimal)0), "", 2);
				LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoPagado", LibConvert.ToStr((decimal)0), "", 2);
				LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoVuelto", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigSummaryField(this, "txtTotalMontoDoc", "MontoDoc", SummaryFunc.Sum, "GHSecMonedaCobro", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoPagado", "MontoPagado", SummaryFunc.Sum, "GHSecMonedaCobro", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoVuelto", "Vuelto", SummaryFunc.Sum, "GHSecMonedaCobro", SummaryRunning.Group, SummaryType.SubTotal);
                // Total Operadores x Moneda Cobro
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoDocOperador", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoPagadoOperador", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoVueltoOperador", LibConvert.ToStr((decimal)0), "", 2);
                if (esInformeEnMonedaOriginal){
                    LibReport.ChangeControlVisibility(this, "lblTotalPorMonedaCobro", false);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoDocOperador", false);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoPagadoOperador", false);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoVueltoOperador", false);
                } else {
                    LibReport.ConfigSummaryField(this, "txtTotalMontoDocOperador", "MontoDoc", SummaryFunc.Sum, "GHSecOperador", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoPagadoOperador", "MontoPagado", SummaryFunc.Sum, "GHSecOperador", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoVueltoOperador", "Vuelto", SummaryFunc.Sum, "GHSecOperador", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoDocOperador", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoPagadoOperador", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoVueltoOperador", true);
                }
                #endregion
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", false, 0);
                LibReport.ConfigGroupHeader(this, "GHSecOperador", "NombreUsuario", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHSecMonedaDoc", "MonedaDoc", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHSecMonedaCobro", "MonedaCobro", GroupKeepTogether.All, RepeatStyle.OnPage, false, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
    }
}
