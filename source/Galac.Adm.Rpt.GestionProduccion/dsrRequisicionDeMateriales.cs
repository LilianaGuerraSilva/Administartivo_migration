using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using LibGalac.Aos.Base;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Ccl.GestionProduccion;
using LibGalac.Aos.ARRpt;
using System.Data;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Rpt.GestionProduccion {
    /// <summary>
    /// Summary description for dsrRequisicionDeMateriales.
    /// </summary>
    public partial class dsrRequisicionDeMateriales : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructor
        public dsrRequisicionDeMateriales()
            : this(false, string.Empty) {
        }

        public dsrRequisicionDeMateriales(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion
        #region Metodos Generados
        public string ReportTitle() {
            return "Requisición de Materiales";
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
                LibReport.ConfigFieldStr(this, "txtArticuloServicioAUtilizar", string.Empty, "MaterialesServicioUtilizado");
                LibReport.ConfigFieldStr(this, "txtUnidad", string.Empty, "Unidad");
                LibReport.ConfigFieldDate(this, "txtFechaCreacion", string.Empty, "FechaCreacion", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldStr(this, "txtCodigoDeOrden", string.Empty, "Codigo");
                LibReport.ConfigFieldStr(this, "txtAlmacen", string.Empty, "AlmacenMaterialesServicioUtilizado");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidadReservadaDeInventario", string.Empty, "CantidadReservadaInventario", 8);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtExistencia", string.Empty, "ExistenciaPorAlmacen", 4);

                LibReport.ConfigGroupHeader(this, "GHSecArticuloServicioAUtilizar", "MaterialesServicioUtilizado", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txt_TCantidadReservadaDeInventario", "CantidadReservadaInventario", SummaryFunc.Sum, "GHSecArticuloServicioAUtilizar", SummaryRunning.Group, SummaryType.SubTotal, "n" + 8, "");

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }
        #endregion
    }
}
