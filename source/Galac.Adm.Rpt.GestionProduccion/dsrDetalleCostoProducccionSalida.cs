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

namespace Galac.Adm.Rpt.GestionProduccion {
    /// <summary>
    /// ESTE ARCHIVO NO ES PARA SER AGREGADO DIRECTO AL PROYECTO
    /// </summary>

    public partial class dsrDetalleCostoProducccionSalida : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores

        public dsrDetalleCostoProducccionSalida()
            : this(false, string.Empty) {
        }

        public dsrDetalleCostoProducccionSalida(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Detalle de Costo de Produccción Salida";
        }

        public bool ConfigReport(DataTable valDataSource) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
				LibReport.ConfigFieldStr(this, "txtDescripcionArticuloSalida",  string.Empty, "DescripcionArticuloSalida");
				LibReport.ConfigFieldStr(this, "txtUnidades",                   string.Empty, "Unidad");
				LibReport.ConfigFieldDec(this, "txtCantidadAProducir",          string.Empty, "CantidadAProducir", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCantidadProducida",          string.Empty, "CantidadProducida", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtPorcentajeCosto",      string.Empty, "PorcentajeCosto", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoUnitario",              string.Empty, "CostoUnitario", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoTotal",                 string.Empty, "CostoTotal", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigSummaryField(this, "txtTotalCostoTotal", "CostoTotal", SummaryFunc.Sum, "GHCodigoOrdenProduccion", SummaryRunning.Group, SummaryType.SubTotal, "n" + 8, "");
                LibReport.ConfigGroupHeader(this, "GHCodigoOrdenProduccion", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                return true;
            }
            return false;
        }

        private void PageFooter_Format(object sender, EventArgs e) {
        }
        #endregion //Metodos Generados


    } //End of class dsrDetalleCostoProducccionSalida

} //End of namespace Galac.Adm.Rpt.GestionProduccion
