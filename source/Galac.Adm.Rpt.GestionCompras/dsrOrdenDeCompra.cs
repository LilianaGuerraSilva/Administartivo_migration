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
namespace Galac.Adm.Rpt.GestionCompras{
    /// <summary>
    /// Summary description for dsrCompra.
    /// </summary>
    public partial class dsrOrdenDeCompra : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        private bool _EsImportacion;
        #endregion //Variables
        #region Constructores

        public dsrOrdenDeCompra() 
            : this(false, string.Empty, false) {
        }

        public dsrOrdenDeCompra(bool initUseExternalRpx, string initRpxFileName, bool valEsImportacion) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
            _EsImportacion = valEsImportacion;
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Orden De Compra";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if(LibReport.ConfigDataSource(this,valDataSource)) {
                LibReport.ConfigFieldStr(this,"txtNombreCompania",valParameters["NombreCompania"],string.Empty);
                LibReport.ConfigLabel(this,"lblTituloInforme",ReportTitle());
                LibReport.ConfigHeader(this,"txtNombreCompania","","lblTituloInforme","","",LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber,LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigLabel(this,"lblDireccionCompania",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","DireccionCompania"));
                if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                    LibReport.ConfigLabel(this,"lblRif","R.U.C.");
                    if(_EsImportacion) {
                        LibReport.ChangeControlVisibility(this,"lblTotalOC",false);
                        LibReport.ChangeControlVisibility(this,"txtTotalOrden",false);
                        LibReport.ConfigLabel(this,"lblTotal","Total Orden ");
                    } else {
                        LibReport.ConfigLabel(this,"lblTotal","Total Afecto ");
                    }
                    LibReport.ConfigLabel(this,"lblIVA","Total I.G.V. ");
                    LibReport.ConfigLabel(this,"lblTotalOC","Total Orden ");
                    LibReport.ChangeControlVisibility(this,"lblNit",false);
                    LibReport.ChangeControlVisibility(this,"txtNit",false);
                } else {
                    if(_EsImportacion) {
                        LibReport.ChangeControlVisibility(this,"lblTotalOC",false);
                        LibReport.ChangeControlVisibility(this,"txtTotalOrden",false);
                        LibReport.ConfigLabel(this,"lblTotal","Total Orden ");
                    }
                }
                LibReport.ConfigFieldStr(this, "txtMoneda",string.Empty,"Moneda");                
                LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");                
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha",LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong); //esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato direct                
                LibReport.ConfigFieldStr(this, "txtCodigoProveedor", string.Empty, "CodigoProveedor");
                LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "NombreProveedor");
                LibReport.ConfigFieldStr(this, "txtDireccion", string.Empty, "Direccion");
                LibReport.ConfigFieldStr(this, "txtRif", string.Empty, "NumeroRIF");
                LibReport.ConfigFieldStr(this, "txtNit", string.Empty, "NumeroNIT");
                LibReport.ConfigFieldStr(this, "txtTelefonos", string.Empty, "Telefonos");
                LibReport.ConfigFieldStr(this, "txtCondicionesDePago", string.Empty, "DescripcionCondicionesDePago");
                LibReport.ConfigFieldStr(this, "txtCondicionesDeEntrega", string.Empty, "CondicionesDeEntrega");
                LibReport.ConfigFieldStr(this, "txtCondicionesDeImportacion", string.Empty, "CondicionesDeImportacion");
                LibReport.ChangeControlVisibility(this, "txtCondicionesDeImportacion", _EsImportacion);
                LibReport.ChangeControlVisibility(this, "lblCondicionesDeImportacion", _EsImportacion);
                LibReport.ChangeControlVisibility(this, "lblIVA", !_EsImportacion);
                LibReport.ChangeControlVisibility(this, "txtTotalIVA", !_EsImportacion);

                LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
                LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
                LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Cantidad");
                LibReport.ConfigFieldStr(this, "txtUnidad", string.Empty, "UnidadDeVenta");
                LibReport.ConfigFieldDec(this, "txtPrecio", string.Empty, "CostoUnitario");
                LibReport.ConfigFieldDec(this, "txtSubTotal", string.Empty, "SubTotal");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible1", string.Empty, "CampoDefinible1");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible2", string.Empty, "CampoDefinible2");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible3", string.Empty, "CampoDefinible3");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible4", string.Empty, "CampoDefinible4");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible5", string.Empty, "CampoDefinible5");
                LibReport.ConfigFieldDec(this, "txtUnidadDeVentaSecundaria", string.Empty, "UnidadDeVentaSecundaria");
                LibReport.ConfigFieldDec(this, "txtTotal", string.Empty, "TotalCompra");
                LibReport.ConfigFieldStr(this, "txtObservaciones", string.Empty, "Comentarios");
                LibReport.ConfigLabel(this, "lbEnvia", ((CustomIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Login);

                LibReport.ConfigGroupHeader(this, "GHSecCompra", "Numero", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.After);
                LibReport.ConfigSummaryField(this, "txtTotalIVA", "IVA", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.GrandTotal);
                LibReport.ConfigSummaryField(this, "txtTotalOrden", "SubTotalOC", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.GrandTotal);

                LibReport.AddNoDataEvent(this);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);                
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
               

        private void PageHeader_Format(object sender, EventArgs e) {
            
        }

        private void GHSecCompra_Format(object sender, EventArgs e) {

        }
    }
}
