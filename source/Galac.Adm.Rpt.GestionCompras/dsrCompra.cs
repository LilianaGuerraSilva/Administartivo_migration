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
    public partial class dsrCompra : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores

        public dsrCompra() 
            : this(false, string.Empty) {
        }

        public dsrCompra(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Compra";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            string vNumeroDeOrdenDeCompra ="";
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                vNumeroDeOrdenDeCompra = valParameters["NumeroDeOrdenDeCompra"];
                LibReport.ConfigHeader(this, "txtNombreCompania", "", "lblTituloInforme", "", "", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    LibReport.ConfigLabel(this, "lblRif", "R.U.C.");
                    LibReport.ChangeControlVisibility(this, "lblNit", false);
                    LibReport.ChangeControlVisibility(this, "txtNit", false);
                }

                if(LibString.IsNullOrEmpty(vNumeroDeOrdenDeCompra)) {
                    LibReport.ChangeControlVisibility(this,"txtNumeroOrdenDeCompra",false);
                    LibReport.ChangeControlVisibility(this,"lblNumeroOrdenDeCompra",false);
                } else {                   
                    LibReport.ConfigFieldStr(this,"txtNumeroOrdenDeCompra",vNumeroDeOrdenDeCompra,string.Empty);
                }
                //LibReport.ConfigLabel(this, "lblDireccionCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","DireccionCompania"));

                LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtStatusCompra", string.Empty, "StatusCompraStr");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong); //esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato direct
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
                LibReport.ConfigFieldStr(this, "txtCodigoProveedor", string.Empty, "CodigoProveedor");
                LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "NombreProveedor");
                LibReport.ConfigFieldStr(this, "txtDireccion", string.Empty, "Direccion");
                LibReport.ConfigFieldStr(this, "txtRif", string.Empty, "NumeroRIF");
                LibReport.ConfigFieldStr(this, "txtNit", string.Empty, "NumeroNIT");
                LibReport.ConfigFieldStr(this, "txtTelefonos", string.Empty, "Telefonos");

                LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
                LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
                LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Cantidad");
                LibReport.ConfigFieldStr(this, "txtUnidad", string.Empty, "UnidadDeVenta");
                LibReport.ConfigFieldDec(this, "txtPrecio", string.Empty, "PrecioUnitario");
                LibReport.ConfigFieldDec(this, "txtSubTotal", string.Empty, "SubTotal");

                LibReport.ConfigFieldStr(this, "txtCodigoLote", string.Empty, "CodigoLote");                
                LibReport.ConfigFieldDate(this, "txtFechaDeElaboracion", string.Empty, "FechaDeElaboracion", "dd/MM/yy");
                LibReport.ConfigFieldDate(this, "txtFechaDeVencimiento", string.Empty, "FechaDeVencimiento", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtTipoArticuloInv", string.Empty, "TipoArticuloInv");
                

                LibReport.ConfigFieldDec(this, "txtOtrosGastos", string.Empty, "TotalOtrosGastos");
                LibReport.ConfigFieldDec(this, "txtTotal", string.Empty, "TotalCompra");
                LibReport.ConfigFieldStr(this, "txtObservaciones", string.Empty, "Comentarios");
                LibReport.ConfigLabel(this, "lbEnvia", ((CustomIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Login);

                LibReport.ConfigGroupHeader(this, "GHSecCompra", "Numero", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.After);
               
                LibReport.AddNoDataEvent(this);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);                
                return true;
            }
            return false;
        }


        #endregion //Metodos Generados

        private void Detail_Format(object sender, EventArgs e) {
            lblLote.Visible = false;
            txtCodigoLote.Visible = false;
            lblFechaElab.Visible = false;
            txtFechaDeElaboracion.Visible = false;
            lblFechaVenc.Visible = false;
            txtFechaDeVencimiento.Visible = false;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaLoteFechaDeVencimiento") && !LibString.IsNullOrEmpty(txtTipoArticuloInv.Text)) {
                if (LibString.S1IsEqualToS2(txtTipoArticuloInv.Text, "6")) {
                    lblLote.Visible = true;
                    txtCodigoLote.Visible = true;
                    lblFechaElab.Visible = true;
                    txtFechaDeElaboracion.Visible = true;
                    lblFechaVenc.Visible = true;
                    txtFechaDeVencimiento.Visible = true;
                } else if (LibString.S1IsEqualToS2(txtTipoArticuloInv.Text, "5")) {
                    lblLote.Visible = true;
                    txtCodigoLote.Visible = true;                  
                }
            }
        }
    }
}
