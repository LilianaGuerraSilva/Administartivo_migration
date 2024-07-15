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
    /// Summary description for dsrProduccionPorEstatusEntreFecha.
    /// </summary>
    public partial class dsrProduccionPorEstatusEntreFecha : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructor
        public dsrProduccionPorEstatusEntreFecha()
            : this(false, string.Empty) {
        }

        public dsrProduccionPorEstatusEntreFecha(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion
        #region Metodos Generados
        public string ReportTitle() {
            return "Producción por Estatus";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters, eTipoStatusOrdenProduccion valEstatus) {
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
                LibReport.ConfigFieldStr(this, "txtEstatus", string.Empty, "EstatusStr");
                LibReport.ConfigFieldStr(this, "txtCodigoOrden", string.Empty, "Orden");
                LibReport.ConfigFieldStr(this, "txtInventarioProducido", string.Empty, "InventarioAProducir");
                LibReport.ConfigFieldDate(this, "txtFechaCreacion", string.Empty, "FechaCreacion", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldDate(this, "txtFechaInicio", string.Empty, "FechaInicio", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldDate(this, "txtFechaFinalizacion", string.Empty, "FechaFinalizacion", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldStr(this, "txtUnidad", string.Empty, "Unidad");
                LibReport.ConfigFieldDec(this, "txtCantidadEstimada", string.Empty, "CantidadSolicitada", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCantidadProducida", string.Empty, "CantidadProducida", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtDiferencia", string.Empty, "Diferencia", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtMotivoAnulacion", string.Empty, "MotivoDeAnulacion");

                LibReport.ChangeControlVisibility(this, "lblMotivoAnulacion", (valEstatus == eTipoStatusOrdenProduccion.Anulada));
                LibReport.ChangeControlVisibility(this, "txtMotivoAnulacion", (valEstatus == eTipoStatusOrdenProduccion.Anulada));

                switch (valEstatus) {
                    case eTipoStatusOrdenProduccion.Ingresada:
                    case eTipoStatusOrdenProduccion.Anulada:
                        LibReport.ConfigLabel(this, "lblFechaInicio", "");
                        LibReport.ConfigLabel(this, "lblFechaFinalizacion", "");
                        LibReport.ConfigLabel(this, "lblCantidadProducida", "");
                        LibReport.ConfigLabel(this, "lblDiferencia", "");

                        LibReport.ChangeControlVisibility(this, "txtFechaInicio", false);
                        LibReport.ChangeControlVisibility(this, "txtFechaFinalizacion", false);
                        LibReport.ChangeControlVisibility(this, "txtCantidadProducida", false);
                        LibReport.ChangeControlVisibility(this, "txtDiferencia", false);
                        break;
                    case eTipoStatusOrdenProduccion.Iniciada:
                        LibReport.ConfigLabel(this, "lblFechaFinalizacion", "");
                        LibReport.ConfigLabel(this, "lblCantidadProducida", "");
                        LibReport.ConfigLabel(this, "lblDiferencia", "");

                        LibReport.ChangeControlVisibility(this, "txtFechaFinalizacion", false);
                        LibReport.ChangeControlVisibility(this, "txtCantidadProducida", false);
                        LibReport.ChangeControlVisibility(this, "txtDiferencia", false);
                        break;
                    default:
                        break;
                }

                LibReport.ConfigGroupHeader(this, "GHSecEstatus", "EstatusStr", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecOrden", "Orden", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }
        #endregion
    }
}
