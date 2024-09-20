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
namespace Galac.Saw.Rpt.Inventario {

    public partial class dsrMovimientoDeLoteInventario : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores 
        public dsrMovimientoDeLoteInventario()
            : this(false, string.Empty) {
        }

        public dsrMovimientoDeLoteInventario(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Lote de Inventario";
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
                //
				LibReport.ConfigFieldStr(this, "txtArticulo", string.Empty, "Articulo");
				LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
				LibReport.ConfigFieldStr(this, "txtLote", string.Empty, "Lote");
				LibReport.ConfigFieldDate(this, "txtFechaVencimiento", string.Empty, "FechaDeVencimiento", "dd/MM/yyyy");
				LibReport.ConfigFieldDate(this, "txtFechaElaboracion", string.Empty, "FechaDeElaboracion", "dd/MM/yyyy");
				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "FechaMovimiento", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtTipoMovimiento", string.Empty, "TipoMovimiento");
				LibReport.ConfigFieldStr(this, "txtNroDocumento", string.Empty, "NroDocumento");
				LibReport.ConfigFieldDec(this, "txtExistenciaInicial", string.Empty, "ExistenciaInicial");
				LibReport.ConfigFieldDec(this, "txtEntrada", string.Empty, "Entrada");
				LibReport.ConfigFieldDec(this, "txtSalida", string.Empty, "Salida");               
                LibReport.ConfigGroupHeader(this, "GHsecArticulo", "CodigoArticulo", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHsecLote", "Lote", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalEntrada", "Entrada", SummaryFunc.DSum, "GHsecLote", SummaryRunning.All, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalSalida", "Salida", SummaryFunc.DSum, "GHsecLote", SummaryRunning.All, SummaryType.SubTotal);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrMovimientoDeLoteInventario

} //End of namespace Galac.Saw.Rpt.Inventario

