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
    public partial class dsrImprimirMargenSobreCostoPromedioDeCompra : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrImprimirMargenSobreCostoPromedioDeCompra()
            : this(false, string.Empty) {
        }

        public dsrImprimirMargenSobreCostoPromedioDeCompra(bool initUseExternalRpx, string initRpxFileName) {
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Margen sobre Costo Promedio de Compra";
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
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", "");
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                LibReport.ConfigFieldStr(this, "txtCodigoArticulo", "", "Codigo");
                LibReport.ConfigFieldStr(this, "txtDescripcionDelArticulo", "", "Descripcion");
                LibReport.ConfigFieldStr(this, "txtLineaDeProducto", "", "LineaDeProducto");
                LibReport.ConfigFieldDec(this, "txtPromedioPonderadoCompEntreFeha", "", "PromDeComp", "#,#00.00", false, TextAlignment.Right);

                LibReport.ConfigFieldDec(this, "txtPrecioDeVenta", "", "PV", "#,#00.00", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtMargen", "", "Margen", "#,#00.00", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtPorcentajeDelMargen", "", "PorcentajeMargen", "#,#00.00", false, TextAlignment.Right);
                LibReport.ConfigLabel(this, "lblPrecioDeVenta", "Precio de Venta ");
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", false, true);
                LibReport.AddNoDataEvent(this);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);                
                LibReport.ConfigGroupHeader(this, "GHLineaDeProducto", "LineaDeProducto", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrImprimirCostoDeCompraEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

