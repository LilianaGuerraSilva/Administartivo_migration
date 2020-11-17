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
using System.Xml.Linq;
namespace Galac.Adm.Rpt.GestionCompras {
    /// <summary>
    /// Summary description for NewActiveReport1.
    /// </summary>
    public partial class dsrImpresionDeEtiquetasPorCompras : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrImpresionDeEtiquetasPorCompras()
            : this(false, string.Empty) {
        }
            //
        public dsrImpresionDeEtiquetasPorCompras(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Historico de Compras";
        }
        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            bool PrecioSinIva = LibConvert.SNToBool(valParameters["PrecioSinIva"]);
            bool MostrarProveedor = LibConvert.SNToBool(valParameters["MostrarProveedor"]);
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), true, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigLabel(this,"lblPrecio", "Precio Sin IVA:");
                LibReport.ConfigFieldDec(this, "txtColor","", "Color");
                ConfigBarCode(this, "bcCodigoArticulo", "", "CodigoDelArticulo");
                LibReport.ConfigFieldDec( this, "txtTalla", "", "Talla");
                LibReport.ConfigFieldDec( this, "txtPrecioSinIVA", "", "PrecioSinIva");
                LibReport.ConfigFieldDec( this, "txtPrecioConIVA", "", "PrecioConIva");
                LibReport.ConfigFieldDec( this, "txtMontoIVA", "", "MontoIVA");
                LibReport.ConfigFieldStr( this, "txtCodigo", "", "CodigoDelArticulo");
                LibReport.ConfigFieldStr( this, "txtDescripcion", "", "Descripcion");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
                LibReport.ConfigFieldStr( this, "txtSerial", "", "Serial");
                LibReport.ConfigFieldStr( this, "txtRollo", "", "Rollo");
                LibReport.ConfigFieldStr(this, "txtNumeroCompra", "", "Numero");
                if (MostrarProveedor) {
                    LibReport.ConfigFieldStr(this, "txtProveedor", "", "NombreProveedor");
                }
                if (PrecioSinIva) {
                    LibReport.ConfigLabel(this, "lblPrecio", Galac.Adm.Rpt.GestionCompras.Properties.Resources.lblPrecioSinIva);
                    LibReport.ConfigFieldDec(this, "txtPrecio", "", "PrecioSinIva");
                } else {
                    LibReport.ConfigLabel(this, "lblPrecio", Galac.Adm.Rpt.GestionCompras.Properties.Resources.lblPrecioConIva);
                    LibReport.ConfigFieldDec(this, "txtPrecio", "", "PrecioConIva");
                }
                LibReport.AddNoDataEvent(this);
                LibReport.ConfigGroupHeader(this, "GHArticulo", "CodigoDelArticulo", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.BeforeAfter);                
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
        void ConfigBarCode(DataDynamics.ActiveReports.ActiveReport refRpt, string valObjectName, string valText, string valDataField) {
            DataDynamics.ActiveReports.Barcode vArObject;
            int vSectionIndex;
            int vControlIndex;
            if (LibReport.ControlExists(refRpt, valObjectName, out vSectionIndex, out vControlIndex)) {
                vArObject = ((DataDynamics.ActiveReports.Barcode)refRpt.Sections[vSectionIndex].Controls[vControlIndex]);
                vArObject.Text = valText;
                if (LibString.Len(valDataField) > 0) {
                    vArObject.DataField = valDataField;
                }
            }
        }

        public void AsignarMargenesImpresionDeEtiquetasPorCompras(DataDynamics.ActiveReports.ActiveReport refRpt) {
            XElement vElement = LibGalac.Aos.Brl.PrnStt.LibPrnSttMargins.GetReportMargins(_RpxFileName, true, 0.5M, true, 0.5M, true, 0.5M, true, 0.50M, true, LibGalac.Aos.Ccl.PrnStt.ePageOrientation.Portrait, true, false, 8.5M, false, 11M);
            LibGraphPrnMargins.SetMargins(refRpt, vElement);
        }

    } //End of class dsrImprimirCostoDeCompraEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado
