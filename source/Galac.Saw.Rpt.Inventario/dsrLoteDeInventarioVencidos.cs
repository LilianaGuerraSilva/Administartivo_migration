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
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Rpt.Inventario {
    
    public partial class dsrLoteDeInventarioVencidos : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrLoteDeInventarioVencidos()
            : this(false, string.Empty) {
        }

        public dsrLoteDeInventarioVencidos(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Articulo Vencidos";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            eCantidadAImprimirArticulo vCantidadAImprimir = (eCantidadAImprimirArticulo)LibConvert.DbValueToEnum(valParameters["CantidadAImprimir"]);
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                //LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ChangeControlVisibility(this, "lblFechaInicialYFinal", false);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

				LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
				LibReport.ConfigFieldStr(this, "txtDescripcionArticulo", string.Empty, "DescripcionArticulo");
                LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Existencia");
                LibReport.ConfigFieldStr(this, "txtLoteDeInventario", string.Empty, "LoteDeInventario");
				LibReport.ConfigFieldDate(this, "txtFechaDeVencimiento", string.Empty, "FechaDeVencimiento", "dd/MM/yyyy");
                LibReport.ConfigFieldStr(this, "txtDiasVencido", string.Empty, "DiasVencido");

				if (vCantidadAImprimir == eCantidadAImprimirArticulo.Articulo) {
                    LibReport.ConfigGroupHeader(this, "GHSecNota", "CodigoArticulo", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                    LibReport.ConfigLabel(this, "lblArticuloLinea", "Código");
                    LibReport.ConfigFieldStr(this, "txtArticuloLinea", string.Empty, "CodigoArticulo");
                    LibReport.ChangeControlVisibility(this, "lblArticuloLinea", true);
                    LibReport.ChangeControlVisibility(this, "txtArticuloLinea", true);
                } else if (vCantidadAImprimir == eCantidadAImprimirArticulo.LineaDeProducto) {
                    LibReport.ConfigGroupHeader(this, "GHSecNota", "LineaDeProducto", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                    LibReport.ConfigLabel(this, "lblArticuloLinea", "Línea De Producto");
                    LibReport.ConfigFieldStr(this, "txtArticuloLinea", string.Empty, "LineaDeProducto");
                    LibReport.ChangeControlVisibility(this, "lblArticuloLinea", true);
                    LibReport.ChangeControlVisibility(this, "txtArticuloLinea", true);
                } else {
                    LibReport.ChangeControlVisibility(this, "lblArticuloLinea", false);
                    LibReport.ChangeControlVisibility(this, "txtArticuloLinea", false);
                }
				
                LibReport.ConfigGroupHeader(this, "GHSecDetalle", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrLoteDeInventarioVencidos

} //End of namespace Galac.Saw.Rpt.Inventario

