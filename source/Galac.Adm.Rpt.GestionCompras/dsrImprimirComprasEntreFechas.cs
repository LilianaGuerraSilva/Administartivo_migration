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
    /// Summary description for dsrImprimirCompra.
    /// </summary>
    public partial class dsrImprimirComprasEntreFechas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrImprimirComprasEntreFechas()
            : this(false, string.Empty) {
        }

        public dsrImprimirComprasEntreFechas(bool initUseExternalRpx, string initRpxFileName) {
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

        public string ReportTiTleSoloMonedaOriginal() {
            return "Compra Entre Fechas Solo Moneda Original";
        }
        public string ReportTiTleSolesMonedaOriginal() {
            return "Compra entre Fechas en Bolívares con Cambio Original";
        }
        public string ReportTiTleSolesMonedaDelDia() {
            return "Compra Entre Fechas en Bolívares con Ultimo Cambio";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {

            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//ac� se indicar�a si se busca en ULS, por defecto buscar�a en app.path... Tip: Una funci�n con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                if (LibConvert.SNToBool(valParameters["EnMonedaOriginal"])) {
                    sConfigurarEntreFechasSoloMonedaOriginal(valDataSource, valParameters);
                } else {
                   if ( LibConvert.SNToBool(valParameters["SolOMonedaOriginal"]) )
                       sConfigurarEntreFechasSolesMonedaOriginal(valDataSource, valParameters);
                   else {
                       sConfigurarEntreFechasSolesMonedaDelDia(valDataSource, valParameters);
                   }
                }
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }

        private void sConfigurarEntreFechasSoloMonedaOriginal(DataTable valDataSource, Dictionary<string, string> valParameters) {
            bool ImprimirTotales = LibConvert.SNToBool(valParameters["ImprimirTotales"]);
            bool ImprimirRenglones = LibConvert.SNToBool(valParameters["ImprimirRenglones"]);


            LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
            LibReport.ConfigLabel(this, "lblTituloInforme", ReportTiTleSoloMonedaOriginal());
            LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
            LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
            LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
            LibReport.ConfigFieldStr(this, "txtEstatus", string.Empty, "Estatus");
            LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
            LibReport.ConfigFieldStr(this, "txtTotalMoneda", string.Empty, "TotalMoneda");
            LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
            LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "Codigo");
            //				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort); esta sobrecarga no est� en versi�n 5.0.2.0 de lib, temporalmente pasar formato directo
            LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
            LibReport.ConfigFieldStr(this, "txtSerial", string.Empty, "Serial");
            LibReport.ConfigFieldStr(this, "txtNombreProveedor", string.Empty, "NombreProveedor");
            LibReport.ConfigFieldStr(this, "txtRollo", string.Empty, "Rollo");
            LibReport.ConfigFieldStr(this, "txtCantidad", string.Empty, "Cantidad");
            LibReport.ConfigFieldDec(this, "txtTotalCompra", string.Empty, "TotalCompra");
            LibReport.ConfigFieldDec(this, "txtCostoUnitario", string.Empty, "CostoUnitario");
            LibReport.ConfigFieldStr(this, "txtCambio", string.Empty, "Cambio");
            LibReport.ConfigFieldDec(this, "txtCompraAlCambio", string.Empty, "CompraTotalAlcambio");
            LibReport.ConfigFieldStr(this, "txtStatusCompra", string.Empty, "StatusCompra");
            LibReport.ConfigFieldStr(this, "txtCodigoDelArticulo", string.Empty, "CodigoDelArticulo");
            LibReport.ConfigFieldDec(this, "txtCantidadRecibida", string.Empty, "cantidadrecibida");
            LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraAlCambio", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.SubTotal);

            LibReport.ConfigGroupHeader(this, "GHSecEstatusCompra", "StatusCompra", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
            LibReport.ConfigGroupHeader(this, "GHSecMoneda", "Moneda", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);
            LibReport.ConfigGroupHeader(this, "GHSecCompra", "Numero", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);

            LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
            //LibReport.ChangeControlVisibility(this, "lblCambioMaster", false);
            LibReport.ChangeControlVisibility(this, "txtCambio", false);
            //LibReport.ChangeControlVisibility(this, "lblCompraAlCambio", false);
            LibReport.ChangeControlVisibility(this, "txtCompraTotalAlcambio", false);
            LibReport.ConfigLabel(this, "lblCambioMaster", "");
            LibReport.ConfigLabel(this, "lblCompraAlCambio", "");

            LibReport.ChangeControlVisibility(this, "txtTotalMoneda", ImprimirTotales);
            //LibReport.ChangeControlVisibility(this, "lblTotalMoneda", ImprimirTotales);
            LibReport.ChangeControlVisibility(this, "txtCambio", ImprimirRenglones);
            LibReport.ChangeControlVisibility(this, "txtCompraAlCambio", ImprimirRenglones);
            LibReport.ChangeControlVisibility(this, "txtCostoUnitario", ImprimirRenglones);
            LibReport.ChangeControlVisibility(this, "txtCantidadRecibida", ImprimirRenglones);
            LibReport.ChangeControlVisibility(this, "txtRollo", ImprimirRenglones);
            LibReport.ChangeControlVisibility(this, "txtSerial", ImprimirRenglones);
            LibReport.ChangeControlVisibility(this, "txtCodigoDelArticulo", ImprimirRenglones);

            //LibReport.ChangeControlVisibility(this, "lblCostoUnitarioTitulo", ImprimirRenglones);
            //LibReport.ChangeControlVisibility(this, "lblCodigoDelArticulo", ImprimirRenglones);
            //LibReport.ChangeControlVisibility(this, "lblSerial", ImprimirRenglones);
            //LibReport.ChangeControlVisibility(this, "lblRollo", ImprimirRenglones);
            //LibReport.ChangeControlVisibility(this, "lblCantidad", ImprimirRenglones);


            if (ImprimirTotales) {
                LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraTotalAlcambio", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMoneda", "TotalCompra", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.All, SummaryType.SubTotal);
            } else {
                LibReport.ConfigLabel(this, "lblTotalMoneda", "");
            }
            if (ImprimirRenglones) {
                LibReport.ChangeControlVisibility(this, "lblTotalCompra", ImprimirRenglones);
                LibReport.ChangeControlVisibility(this, "txtTotalCompra", !ImprimirRenglones);
                //LibReport.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 225, 5490, 0
                LibReport.ChangeControlLocation(this, "lblTotalCompra", 5.59f, 0.16f);
                LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraTotalAlcambio", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.SubTotal);
                if (ImprimirTotales) {
                    LibReport.ConfigSummaryField(this, "txtTotalMoneda", "TotalCompra", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.All, SummaryType.SubTotal);
                }
            } else {
                //LibReport.sAsignaAlturaDeSeccion refRpt, "GHSecMoneda", 450
                //LibReport.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 225, 5490, 225
                //LibReport.sChangePositionAndSize refRpt, "lblNombreProveedor", 2250, 225, 3240, 225
                //LibReport.sChangePositionAndSize refRpt, "lblFecha", 1125, 225, 2115, 225
                //LibReport.sChangePositionAndSize refRpt, "lblNumero", 2115, 225, 0, 225
                LibReport.ChangeControlLocation(this, "lblTotalCompra", 5.59f, 0.16f);
                //LibReport.ChangeControlLocation(this, "lblNombreProveedor", 225, 3240);
                //LibReport.ChangeControlLocation(this, "lblFecha", 225, 2115);
                //LibReport.ChangeControlLocation(this, "lblNumero", 225, 0);
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecCompra", false, 0);



                LibReport.ConfigLabel(this, "lblCostoUnitarioTitulo", "");

                LibReport.ConfigLabel(this, "lblCodigoDelArticulo", "");
                LibReport.ConfigLabel(this, "lblSerial", "");
                LibReport.ConfigLabel(this, "lblRollo", "");
                LibReport.ConfigLabel(this, "lblCantidad", "");
            }
            //LibReport.sChangePositionAndSize refRpt, "txtTotalCompra", 4252, 0, 5490, 225            
            LibReport.ChangeControlLocation(this, "txtTotalCompra", 5.59f, 0f);
        }

        private void sConfigurarEntreFechasSolesMonedaOriginal(DataTable valDataSource, Dictionary<string, string> valParameters) {

            bool ImprimirTotales = LibConvert.SNToBool(valParameters["ImprimirTotales"]);
            bool ImprimirRenglones = LibConvert.SNToBool(valParameters["ImprimirRenglones"]);

            LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
            LibReport.ConfigLabel(this, "lblTituloInforme", ReportTiTleSolesMonedaOriginal());
            LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
            LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
            LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
            LibReport.ConfigFieldStr(this, "txtEstatus", string.Empty, "Estatus");
            LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
            LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
            LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
            LibReport.ConfigFieldStr(this, "txtSerial", string.Empty, "Serial");
            LibReport.ConfigFieldStr(this, "txtNombreProveedor", string.Empty, "NombreProveedor");
            LibReport.ConfigFieldStr(this, "txtRollo", string.Empty, "Rollo");
            LibReport.ConfigFieldStr(this, "txtCantidad", string.Empty, "Cantidad");
            LibReport.ConfigFieldDec(this, "txtTotalCompra", string.Empty, "TotalCompra");
            LibReport.ConfigLabel(this, "lblTotalCompra", "Compra Bolívar");
            LibReport.ConfigFieldDec(this, "txtCostoUnitario", string.Empty, "CostoUnitario");
            LibReport.ConfigFieldStr(this, "txtCambio", string.Empty, "cambio");
            LibReport.ConfigFieldDec(this, "txtCompraAlCambio", string.Empty, "CompraTotalAlcambio");
            LibReport.ConfigFieldDec(this, "txtCompraTotalAlCambio", string.Empty, "CompraTotalAlcambio");
            LibReport.ConfigFieldStr(this, "txtStatusCompra", string.Empty, "StatusCompra");
            LibReport.ConfigFieldStr(this, "txtCodigoDelArticulo", string.Empty, "CodigoDelArticulo");
            LibReport.ConfigFieldDec(this, "txtCantidadRecibida", string.Empty, "cantidadrecibida");
            LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraAlCambio", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.SubTotal);

            LibReport.ConfigGroupHeader(this, "GHSecEstatusCompra", "StatusCompra", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
            LibReport.ConfigGroupHeader(this, "GHSecMoneda", "Moneda", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);
            LibReport.ConfigGroupHeader(this, "GHSecCompra", "Numero", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);

            LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
            if (ImprimirTotales) {
                LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraTotalAlcambio", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigSummaryField(this, "txtTotalMoneda", "CompraTotalAlcambio", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.All, SummaryType.SubTotal);
            } else {
                LibReport.ChangeControlVisibility(this, "txtCompraTotalAlCambio", false);
                LibReport.ConfigLabel(this, "lblTotalMoneda", "");
            }
            LibReport.ChangeControlVisibility(this, "txtTotalMoneda", ImprimirTotales);

            if (!ImprimirRenglones) {
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecCompra", false, 0);
                LibReport.ChangeControlVisibility(this, "txtTotalCompra", false);
                LibReport.ChangeControlVisibility(this, "txtCompraTotalAlCambio", true);
                LibReport.ChangeControlVisibility(this, "txtCompraAlCambio", false);
                LibReport.ChangeControlVisibility(this, "txtCostoUnitario", false);
                LibReport.ChangeControlVisibility(this, "txtCantidadRecibida", false);
                LibReport.ChangeControlVisibility(this, "txtRollo", false);
                LibReport.ChangeControlVisibility(this, "txtSerial", false);
                LibReport.ChangeControlVisibility(this, "txtCodigoDelArticulo", false);
                //Reposicionar
                LibReport.ChangeControlLocation(this, "lblTotalCompra", 5.59f, 0.16f);

                LibReport.ConfigLabel(this, "lblCodigoDelArticulo", "");
                LibReport.ConfigLabel(this, "lblSerial", "");
                LibReport.ConfigLabel(this, "lblRollo", "");
                LibReport.ConfigLabel(this, "lblCantidad", "");
                LibReport.ConfigLabel(this, "lblCostoUnitarioTitulo", "");
            } else {
                LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraTotalAlcambio", SummaryFunc.Sum,"GHSecCompra", SummaryRunning.All, SummaryType.SubTotal);
                if (!ImprimirTotales) {
                     LibReport.ConfigSummaryField(this, "txtTotalMoneda", "TotalCompra", SummaryFunc.Sum,"GHSecMoneda", SummaryRunning.All, SummaryType.SubTotal);
                }
                LibReport.ChangeControlVisibility(this, "txtTotalCompra", false);
                LibReport.ChangeControlVisibility(this, "txtCompraTotalAlCambio", false);
                //Reposicionar
                LibReport.ChangeControlLocation(this, "lblTotalCompra", 5.59f, 0.16f);
            }
            LibReport.ChangeControlVisibility(this, "txtCambio", true);
            LibReport.ChangeControlVisibility(this, "txtCambioMaster", true);
            LibReport.ConfigLabel(this, "lblCompraAlCambio", "");            

        }
        private void sConfigurarEntreFechasSolesMonedaDelDia(DataTable valDataSource, Dictionary<string, string> valParameters) {

            bool ImprimirTotales = LibConvert.SNToBool(valParameters["ImprimirTotales"]);
            bool ImprimirRenglones = LibConvert.SNToBool(valParameters["ImprimirRenglones"]);

            LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
            LibReport.ConfigLabel(this, "lblTituloInforme", ReportTiTleSolesMonedaDelDia());
            LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
            LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
            LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
            LibReport.ConfigFieldStr(this, "txtEstatus", string.Empty, "Estatus");
            LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
            LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
            LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
            LibReport.ConfigFieldStr(this, "txtSerial", string.Empty, "Serial");
            LibReport.ConfigFieldStr(this, "txtNombreProveedor", string.Empty, "NombreProveedor");
            LibReport.ConfigFieldStr(this, "txtRollo", string.Empty, "Rollo");
            LibReport.ConfigFieldStr(this, "txtCantidad", string.Empty, "Cantidad");
            LibReport.ConfigFieldDec(this, "txtTotalCompra", string.Empty, "TotalCompra");
            LibReport.ConfigLabel(this, "lblTotalCompra", "Compra Bolívares");
            LibReport.ConfigFieldDec(this, "txtCostoUnitario", string.Empty, "CostoUnitario");
            LibReport.ConfigFieldStr(this, "txtCambio", string.Empty, "cambio");
            LibReport.ConfigFieldDec(this, "txtCompraAlCambio", string.Empty, "CompraTotalAlcambio");
            LibReport.ConfigFieldDec(this, "txtCompraTotalAlCambio", string.Empty, "CompraTotalAlcambio");
            LibReport.ConfigFieldStr(this, "txtStatusCompra", string.Empty, "StatusCompra");
            LibReport.ConfigFieldStr(this, "txtCodigoDelArticulo", string.Empty, "CodigoDelArticulo");
            LibReport.ConfigFieldDec(this, "txtCantidadRecibida", string.Empty, "cantidadrecibida");
            LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraAlCambio", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.SubTotal);

            LibReport.ConfigGroupHeader(this, "GHSecEstatusCompra", "StatusCompra", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
            LibReport.ConfigGroupHeader(this, "GHSecMoneda", "Moneda", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);
            LibReport.ConfigGroupHeader(this, "GHSecCompra", "Numero", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);

            LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
            if (ImprimirTotales) {
                LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraTotalAlcambio", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMoneda", "CompraTotalAlcambio", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.All, SummaryType.SubTotal);
            } else {
                LibReport.ChangeControlVisibility(this, "txtCompraTotalAlCambio", true);
                LibReport.ConfigLabel(this, "lblTotalMoneda", "");
            }
            LibReport.ChangeControlVisibility(this, "txtTotalMoneda", ImprimirTotales);

            if (!ImprimirRenglones) {
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecCompra", false, 0);
                LibReport.ChangeControlVisibility(this, "txtTotalCompra", false);
                LibReport.ChangeControlVisibility(this, "txtCompraTotalAlCambio", true);
                LibReport.ChangeControlVisibility(this, "txtCompraAlCambio", false);
                LibReport.ChangeControlVisibility(this, "txtCostoUnitario", false);
                LibReport.ChangeControlVisibility(this, "txtCantidadRecibida", false);
                LibReport.ChangeControlVisibility(this, "txtRollo", false);
                LibReport.ChangeControlVisibility(this, "txtSerial", false);
                LibReport.ChangeControlVisibility(this, "txtCodigoDelArticulo", false);
                LibReport.ChangeControlLocation(this, "lblTotalCompra", 5.59f, 0.16f);

                LibReport.ConfigLabel(this, "lblCostoUnitarioTitulo", "");
                LibReport.ConfigLabel(this, "lblCodigoDelArticulo", "");
                LibReport.ConfigLabel(this, "lblSerial", "");
                LibReport.ConfigLabel(this, "lblRollo", "");
                LibReport.ConfigLabel(this, "lblCantidad", "");
            } else {
                LibReport.ConfigSummaryField(this, "txtT_CostoAlCambio_SecCompra", "CompraTotalAlcambio", SummaryFunc.Sum, "GHSecCompra", SummaryRunning.All, SummaryType.SubTotal);
                if (!ImprimirTotales) {
                    LibReport.ConfigSummaryField(this, "txtTotalMoneda", "TotalCompra", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.All, SummaryType.SubTotal);
                }
                LibReport.ChangeControlVisibility(this, "txtTotalCompra", false);
                LibReport.ChangeControlVisibility(this, "txtCompraTotalAlCambio", false);
                //Reposicionar
                LibReport.ChangeControlLocation(this, "lblTotalCompra", 5.59f, 0.16f);
            }
            LibReport.ChangeControlVisibility(this, "txtCambio", true);
            LibReport.ChangeControlVisibility(this, "txtCambioMaster", true);
            LibReport.ConfigLabel(this, "lblCompraAlCambio", "");

        }
        #endregion //Metodos Generados


    } //End of class dsrImprimirCompra
}