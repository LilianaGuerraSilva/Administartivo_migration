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
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base.Dal;

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrCajaCerrada.
    /// </summary>
    public partial class dsrCajaCerrada: DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        DateTime _FechaDesde;
        DateTime _FechaHasta;

        #endregion //Variables
        #region Constructores
        public dsrCajaCerrada()
            : this(false, string.Empty) {
        }

        public dsrCajaCerrada(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Caja Cerrada";
        }

        public bool ConfigReport(DataTable valDataSource, DateTime valFechaDesde, DateTime valFechaHasta) {
            _FechaDesde = valFechaDesde;
            _FechaHasta = valFechaHasta;
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"), string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", string.Format("Desde {0} hasta {1}", LibConvert.ToStr(valFechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(valFechaHasta, "dd/MM/yyyy")));
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
                LibReport.ConfigFieldStr(this, "txtOperador", string.Empty, "Operador");
                LibReport.ConfigFieldDate(this, "txtFechaApertura", string.Empty, "Fecha", "dd/MM/yyyy");
                LibReport.ConfigFieldStr(this, "txtHoraApertura", string.Empty, "HoraApertura");
                LibReport.ConfigFieldStr(this, "txtMontoCodigoMoneda", string.Empty, "EtiquetaMontoME");
                LibReport.ConfigFieldDec(this, "txtMontoAperturaML", string.Empty, "MontoApertura");
                LibReport.ConfigFieldDec(this, "txtMontoAperturaME", string.Empty, "MontoAperturaME");
                LibReport.ConfigFieldStr(this, "txtMovimiento", string.Empty, "Movimiento");
                LibReport.ConfigFieldDec(this, "txtMontoML", string.Empty, "MontoML");
                LibReport.ConfigFieldDec(this, "txtMontoME", string.Empty, "MontoME");
                LibReport.ConfigFieldDec(this, "txtMontoCierreML", string.Empty, "MontoCierre");
                LibReport.ConfigFieldDec(this, "txtMontoCierreME", string.Empty, "MontoCierreME");
                LibReport.ConfigFieldStr(this, "txtHoraCierre", string.Empty, "HoraCierre");
                LibReport.ConfigFieldStr(this, "txtConsecutivoCaja", string.Empty, "ConsecutivoCaja");
                LibReport.ConfigGroupHeader(this, "GHCajaApertura", "ConsecutivoConsecutivoCaja", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void GFCajaApertura_Format(object sender, EventArgs e) {
            decimal vEfectivoEnCaja = 0;
            decimal vEfectivoEnCajaME = 0;
            string vSql = SqlEfectivoEnCajaCerrada();
            XElement vData = LibGalac.Aos.Brl.LibBusiness.ExecuteSelect(vSql, null, "", 0);
            if (vData != null && vData.HasElements) {
                vEfectivoEnCaja = LibConvert.ToDec(LibXml.GetPropertyString(vData, "EfectivoEnCaja"));
                vEfectivoEnCajaME = LibConvert.ToDec(LibXml.GetPropertyString(vData, "EfectivoEnCajaME"));
            }
            LibReport.ConfigFieldDec(this, "txtEfectivoEnCaja", LibConvert.ToStr(vEfectivoEnCaja), "");
            LibReport.ConfigFieldDec(this, "txtEfectivoEnCajaME", LibConvert.ToStr(vEfectivoEnCajaME), "");
        }

        private string SqlEfectivoEnCajaCerrada() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql insSql = new QAdvSql("");
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            int vConsecutivoCaja = LibConvert.ToInt(txtConsecutivoCaja.Value);
            vSql.AppendLine("SELECT");
            vSql.AppendLine("   SUM(MontoApertura) + SUM(MontoEfectivo) + (SUM(MontoVuelto) + SUM(MontoVueltoPM)) AS EfectivoEnCaja,");
            vSql.AppendLine("   SUM(MontoAperturaME) + SUM(MontoEfectivoME) + SUM(MontoVueltoME)  AS EfectivoEnCajaME");
            vSql.AppendLine("   FROM adm.cajaapertura");
            vSql.AppendLine("   WHERE ");
            vSql.AppendLine("   ConsecutivoCompania = " + insSql.ToSqlValue(vConsecutivoCompania));
            vSql.AppendLine("   AND ConsecutivoCaja = " + insSql.ToSqlValue(vConsecutivoCaja));
            vSql.AppendLine("   AND " + insSql.SqlDateValueBetween("", "Fecha", _FechaDesde, _FechaHasta));
            vSql.AppendLine("   AND CajaCerrada = " + insSql.ToSqlValue(true));
            return vSql.ToString();
        }

    } //End of class dsrCajaCerrada
} //End of namespace Galac.Adm.Rpt.Venta

