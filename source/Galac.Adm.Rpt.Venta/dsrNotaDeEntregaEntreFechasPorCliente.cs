using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

using LibGalac.Aos.ARRpt;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using System.Data;

namespace Galac.Adm.Rpt.Venta
{
    /// <summary>
    /// Summary description for dsrNotaDeEntregaEntreFechasPorCliente.
    /// </summary>
    public partial class dsrNotaDeEntregaEntreFechasPorCliente : DataDynamics.ActiveReports.ActiveReport { 
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrNotaDeEntregaEntreFechasPorCliente()
            : this(false, string.Empty) {
        }
            //
        public dsrNotaDeEntregaEntreFechasPorCliente(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Notas de Entrega por Cliente";
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

				LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "CodigoCliente");
				LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "Cliente");
				LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
//				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort); esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato directo
				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtNroDocumento", string.Empty, "Numero");
				LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDoc");
				LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio");
				LibReport.ConfigFieldDec(this, "txtAnulada", string.Empty, "EsAnulada");
				LibReport.ConfigFieldDec(this, "txtMontoTotal", string.Empty, "TotalFactura");
                LibReport.ConfigSummaryField(this, "txtSumMontoTotal", "TotalFactura", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.Group, SummaryType.SubTotal);

                //LibReport.ConfigSummaryField(this, "txtT_SumMontoTotal_SecMoneda", "SumMontoTotal", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigGroupHeader(this, "GHSecCliente", "Cliente", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHSecMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void dsrNotaDeEntregaEntreFechasPorCliente_ReportStart(object sender, EventArgs e)
        {

        }
    } //End of class dsrNotaDeEntregaEntreFechasPorCliente

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado
