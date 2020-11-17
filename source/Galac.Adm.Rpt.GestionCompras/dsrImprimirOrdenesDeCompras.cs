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
namespace Galac.Adm.Rpt.GestionCompras {
    /// <summary>
    /// Summary description for NewActiveReport1.
    /// </summary>
    public partial class dsrImprimirOrdenesDeCompras : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrImprimirOrdenesDeCompras()
            : this(false, string.Empty) {
        }

        public dsrImprimirOrdenesDeCompras(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Orden de Compra Entre Fechas";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                bool MuestraDetalle = LibConvert.SNToBool(valParameters["ImprimirRenglones"]);
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtMoneda", "", "Moneda");
                LibReport.ConfigFieldDate(this, "txtFecha", "", "Fecha", "dd/MM/yyyy" );
                LibReport.ConfigFieldStr(this, "txtNumero", "", "Numero");
                LibReport.ConfigFieldStr(this, "txtNombreProveedor", "", "NombreProveedor");
                LibReport.ConfigFieldDec(this, "txtTotalCompra", "", "TotalCompra","0.00",false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCompraTotalAlcambio", "", "Total al Cambio","0.00",false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtcambio", "", "cambioABolivares","0.00",false, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtCodigoDelArticulo", "", "CodigoArticulo");
                LibReport.ConfigFieldDec(this, "txtcantidad", "", "cantidad","0.00",false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtcantidadrecibida", "", "cantidadrecibida","0.00",false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoUnitario", "", "CostoUnitario","0.00",false, TextAlignment.Right);
                LibReport.ConfigGroupHeader(this, "GHSecMoneda", "Moneda", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecCompra", "Numero", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecCompra", MuestraDetalle, 0.25f);
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", MuestraDetalle, 0.104f);
                LibReport.ChangeControlVisibility(this, "lblCodigoDelArticulo", MuestraDetalle);
                LibReport.ChangeControlVisibility(this, "lblDescripcion", MuestraDetalle);
                LibReport.ChangeControlVisibility(this, "lblCantidadRecibida", MuestraDetalle);
                LibReport.ChangeControlVisibility(this, "lblCostoUnitarioTitulo", MuestraDetalle);
                LibReport.ChangeControlVisibility(this, "lblAuxiliar", MuestraDetalle);
                LibReport.ChangeControlVisibility(this, "lblCantidad", MuestraDetalle);
                LibReport.ConfigFieldStr(this, "txtDescripcion", "", "Descripcion");
                if (!MuestraDetalle) {
                    ConfigBorder(this, "lblNumero");
                    ConfigBorder(this, "lblFecha");
                    ConfigBorder(this, "lblNombreProveedor");
                    ConfigBorder(this, "lblTotalCompra");
                }                
                DataDynamics.ActiveReports.BorderLine border = new BorderLine();
                LibReport.AddNoDataEvent(this);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }

        void ConfigBorder(DataDynamics.ActiveReports.ActiveReport refRpt, string valObjectName) {
            DataDynamics.ActiveReports.Label vArObject;
            int vSectionIndex;
            int vControlIndex;
            if (LibReport.ControlExists(refRpt, valObjectName, out vSectionIndex, out vControlIndex)) {
                vArObject = ((DataDynamics.ActiveReports.Label)refRpt.Sections[vSectionIndex].Controls[vControlIndex]);
                vArObject.Border.BottomColor = System.Drawing.Color.Black;
                vArObject.Border.BottomStyle = BorderLineStyle.Solid;
            }
        }
        #endregion //Metodos Generados


    } //End of class dsrImprimirCostoDeCompraEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

