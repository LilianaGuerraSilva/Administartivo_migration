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
using Galac.Saw.Reconv;
namespace Galac.Saw.Rpt.Inventario {
    /// <summary>
    /// Summary description for dsrListadoDePrecios.
    /// </summary>
    public partial class dsrListadoDePrecios : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores

        public dsrListadoDePrecios()
            : this(false, string.Empty) {
        }

        public dsrListadoDePrecios(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            string vMensaje = "";
            if (LibDate.Today() >= clsUtilReconv.GetFechaReconversion()) {
                vMensaje = "Listado de Precios Bolívar Soberano";
            } else if (LibDate.Today() >= clsUtilReconv.GetFechaDisposicionesTransitorias()) {
                vMensaje = "Listado de Precios Bolívar Digital";
            }
            return vMensaje;
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
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", string.Empty);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                if (LibDate.Today() >= clsUtilReconv.GetFechaReconversion()) {
                    LibReport.ConfigLabel(this, "lblEnBsS", "En BsS");
                    LibReport.ConfigLabel(this, "lblEnBsS2", "En BsS");
                    LibReport.ConfigLabel(this, "lblEnBsS3", "En BsS");
                    LibReport.ConfigLabel(this, "lblEnBsS4", "En BsS");
                } else if (LibDate.Today() >= clsUtilReconv.GetFechaDisposicionesTransitorias()) {
                    LibReport.ConfigLabel(this, "lblEnBsS", "En BsD");
                    LibReport.ConfigLabel(this, "lblEnBsS2", "En BsD");
                    LibReport.ConfigLabel(this, "lblEnBsS3", "En BsD");
                    LibReport.ConfigLabel(this, "lblEnBsS4", "En BsD");
                }
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtLineaDeProducto", string.Empty, "LineaDeProducto");
                LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "Codigo");
                LibReport.ConfigFieldStr(this, "txtDescripcionArticuloInventario", string.Empty, "Descripcion");
                LibReport.ConfigFieldDec(this, "txtPrecio", string.Empty, "PrecioConIva");
                LibReport.ConfigFieldDec(this, "txtEnBsS", string.Empty, "EnBsS");
                LibReport.ConfigFieldDec(this, "txtPrecio2", string.Empty, "PrecioConIva2");
                LibReport.ConfigFieldDec(this, "txtEnBsS2", string.Empty, "EnBsS2");
                LibReport.ConfigFieldDec(this, "txtPrecio3", string.Empty, "PrecioConIva3");
                LibReport.ConfigFieldDec(this, "txtEnBsS3", string.Empty, "EnBsS3");
                LibReport.ConfigFieldDec(this, "txtPrecio4", string.Empty, "PrecioConIva4");
                LibReport.ConfigFieldDec(this, "txtEnBsS4", string.Empty, "EnBsS4");
                LibReport.ConfigGroupHeader(this, "GHSecLineaProducto", "LineaDeProducto", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void dsrListadoDePrecios_ReportStart(object sender, EventArgs e) {

        }
    }
}