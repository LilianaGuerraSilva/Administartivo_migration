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
    public partial class dsrImprimirHistoricoDeCompras : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrImprimirHistoricoDeCompras()
            : this(false, string.Empty) {
        }
            //
        public dsrImprimirHistoricoDeCompras(bool initUseExternalRpx, string initRpxFileName) {
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

                LibReport.ConfigFieldStr(this, "txtLineaDeProducto", string.Empty, "LineaDeProducto");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
                LibReport.ConfigFieldInt(this, "txtNumero", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtCodigoDelArticulo", string.Empty, "CodigoDelArticulo");
                LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
                LibReport.ConfigFieldStr(this, "txtSerial", string.Empty, "Serial");
                LibReport.ConfigFieldStr(this, "txtRollo", string.Empty, "Rollo");
                LibReport.ConfigFieldStr(this, "txtCodigoAlmacen", string.Empty, "CodigoAlmacen");
                LibReport.ConfigFieldStr(this, "txtNombreProveedor", string.Empty, "NombreProveedor");
                LibReport.ConfigFieldStr(this, "txtStatusCompra", string.Empty, "StatusCompra");
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
                LibReport.ConfigFieldDec(this, "txtcantidadrecibida", string.Empty, "cantidadrecibida", "#,##0.00", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoUnitario", string.Empty, "CostoUnitario", "#,##0.00", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtcambio", string.Empty, "cambio", "0.0000", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtcostoAlcambio", string.Empty, "costoAlcambio", "#,##0.00", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtT_cantidadrecibida_SecLineaProducto", string.Empty, "cantidadrecibida", "#,##0.00", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtT_CostoUnitario_SecLineaProducto", string.Empty, "CostoUnitario", "#,##0.00", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtT_cambio_SecLineaProducto", string.Empty, "cambio", "0.0000", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtT_costoAlcambio_SecLineaProducto", string.Empty, "costoAlcambio", "#,##0.00", false, TextAlignment.Right);

                LibReport.ConfigGroupHeader(this, "GHSecLineaProducto", "LineaDeProducto", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHStatusCompra", "StatusCompra", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.AddNoDataEvent(this);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);                
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrImprimirCostoDeCompraEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado
