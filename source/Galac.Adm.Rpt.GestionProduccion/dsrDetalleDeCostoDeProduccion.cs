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
    /// Summary description for DetalleDeCostoDeProduccion.
    /// </summary>
    public partial class dsrDetalleDeCostoDeProduccion : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;

        #endregion //Variables
        #region Propiedades
        public decimal SumaTotalConsumo { get; set; }
        #endregion
        #region Constructor
        public dsrDetalleDeCostoDeProduccion()
            : this(false, string.Empty) {
        }

        public dsrDetalleDeCostoDeProduccion(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion
        #region Metodos Generados
        public string ReportTitle() {
            return "Detalle de Costo de Producción";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            SumaTotalConsumo = LibConvert.ToDec(valDataSource.Compute("SUM(MontoTotalConsumo)", ""), 4);
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                if (valParameters["MostrarFechaInicialFinal"] == true.ToString()) {
                    LibReport.ChangeControlVisibility(this, "lblFechaInicialYFinal", true);
                } else {
                    LibReport.ChangeControlVisibility(this, "lblFechaInicialYFinal", false);
                }
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                //Cuerpo del Informe
                LibReport.ConfigFieldStr(this, "txtOrden",                      string.Empty, "OrdenCodigoDescripcion");
                LibReport.ConfigFieldStr(this, "txtEstatus",                    string.Empty, "Estatus");
                LibReport.ConfigFieldStr(this, "txtCodigoOrden",                string.Empty, "CodigoOrden");
                LibReport.ConfigFieldStr(this, "txtOrdenDeAgrupamiento",        string.Empty, "OrdenAgrupamiento");
                LibReport.ConfigFieldDate(this, "txtFechaInicio",               string.Empty, "FechaInicio", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldDate(this, "txtFechaFinalizacion",         string.Empty, "FechaFinalizacion", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldStr(this, "txtInventarioProducido",        string.Empty, "InventarioProducido");
                LibReport.ConfigFieldDec(this, "txtCantidadAProducir",          string.Empty, "CantidadAProducir", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCantidadProducida",          string.Empty, "CantidadProducida", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoTotalDeOrden",          string.Empty, "MontoTotalOrden", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtAlmacenProductoTerminado",   string.Empty, "AlmacenProductoTerminado");
                LibReport.ConfigFieldStr(this, "txtAlmacenMateriales",          string.Empty, "AlmacenMateriales");
                LibReport.ConfigFieldDec(this, "txtCostoUnitarioInvProducido",  string.Empty, "CostoUnitarioProductoTerminado", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtArticuloServicio",           string.Empty, "ArtServUtilizado");
                LibReport.ConfigFieldDec(this, "txtCantidadEstimada",           string.Empty, "CantidadEstimada", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCantidadConsumida",          string.Empty, "CantidadConsumida", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoUnitarioMatServ",       string.Empty, "CostoUnitarioMatServ", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoTotalConsumido",        string.Empty, "MontoTotalConsumo", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txt_TCostoTotalConsumido",      string.Empty, "MontoTotalConsumo", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtObservaciones",              string.Empty, "Observaciones");

                LibReport.ConfigGroupHeader(this, "GHSecOrdenDeProduccion", "CodigoOrden", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
               
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }
        #endregion

        private void PageFooter_Format(object sender, EventArgs e) {
            this.txt_TCostoTotalConsumido.Value = LibConvert.ToStr(SumaTotalConsumo, 4);
        }
    }
}
