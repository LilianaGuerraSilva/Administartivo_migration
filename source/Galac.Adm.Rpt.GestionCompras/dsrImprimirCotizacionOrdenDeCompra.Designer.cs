namespace Galac.Adm.Rpt.GestionCompras {
    /// <summary>
    /// Summary description for ImprimirCotizacionOrdenDeCompra.
    /// </summary>
    partial class dsrImprimirCotizacionOrdenDeCompra {

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dsrImprimirCotizacionOrdenDeCompra));
            this.Detail = new DataDynamics.ActiveReports.Detail();
            this.txtNumeroOC = new DataDynamics.ActiveReports.TextBox();
            this.txtPorcentaje = new DataDynamics.ActiveReports.TextBox();
            this.txtNombreArticulo = new DataDynamics.ActiveReports.TextBox();
            this.txtSoles = new DataDynamics.ActiveReports.TextBox();
            this.txtDolares = new DataDynamics.ActiveReports.TextBox();
            this.txtOrden = new DataDynamics.ActiveReports.TextBox();
            this.PageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.txtNombreCompania = new DataDynamics.ActiveReports.TextBox();
            this.lblFechaYHoraDeEmision = new DataDynamics.ActiveReports.Label();
            this.lblFechaInicialYFinal = new DataDynamics.ActiveReports.Label();
            this.lblTituloInforme = new DataDynamics.ActiveReports.Label();
            this.txtNroDePagina = new DataDynamics.ActiveReports.TextBox();
            this.PageFooter = new DataDynamics.ActiveReports.PageFooter();
            this.GHDetail = new DataDynamics.ActiveReports.GroupHeader();
            this.txtCambio = new DataDynamics.ActiveReports.TextBox();
            this.lblTasaDeCambio = new DataDynamics.ActiveReports.Label();
            this.lblCotizacion = new DataDynamics.ActiveReports.Label();
            this.txtNumeroCot = new DataDynamics.ActiveReports.TextBox();
            this.GFDetail = new DataDynamics.ActiveReports.GroupFooter();
            this.lblUtilidadBruta = new DataDynamics.ActiveReports.Label();
            this.txtUtilidadSoles = new DataDynamics.ActiveReports.TextBox();
            this.txtUtilidadDolares = new DataDynamics.ActiveReports.TextBox();
            this.txtPorcentajeUtilidad = new DataDynamics.ActiveReports.TextBox();
            this.lblTotales = new DataDynamics.ActiveReports.Label();
            this.txtTotalML = new DataDynamics.ActiveReports.TextBox();
            this.txtTotalME = new DataDynamics.ActiveReports.TextBox();
            this.GHOrdenDeCompra = new DataDynamics.ActiveReports.GroupHeader();
            this.lblOC = new DataDynamics.ActiveReports.Label();
            this.lblNombreArticulo = new DataDynamics.ActiveReports.Label();
            this.lblVenta = new DataDynamics.ActiveReports.Label();
            this.lblSoles = new DataDynamics.ActiveReports.Label();
            this.lblDolares = new DataDynamics.ActiveReports.Label();
            this.GFOrdenDeCompra = new DataDynamics.ActiveReports.GroupFooter();
            this.lblCliente = new DataDynamics.ActiveReports.Label();
            this.txtNombreCliente = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreArticulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDolares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreCompania)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFechaYHoraDeEmision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFechaInicialYFinal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTituloInforme)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroDePagina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCambio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTasaDeCambio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCotizacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroCot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUtilidadBruta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUtilidadSoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUtilidadDolares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentajeUtilidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalML)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalME)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNombreArticulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDolares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.CanShrink = true;
            this.Detail.ColumnSpacing = 0F;
            this.Detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtNumeroOC,
            this.txtPorcentaje,
            this.txtNombreArticulo,
            this.txtSoles,
            this.txtDolares,
            this.txtOrden});
            this.Detail.Height = 0.15F;
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Format += new System.EventHandler(this.Detail_Format);
            this.Detail.BeforePrint += new System.EventHandler(this.Detail_BeforePrint);
            this.Detail.AfterPrint += new System.EventHandler(this.Detail_AfterPrint);
            // 
            // txtNumeroOC
            // 
            this.txtNumeroOC.CanShrink = true;
            this.txtNumeroOC.Height = 0.15F;
            this.txtNumeroOC.Left = 0F;
            this.txtNumeroOC.Name = "txtNumeroOC";
            this.txtNumeroOC.Style = "font-size: 8pt";
            this.txtNumeroOC.Tag = "No Movable";
            this.txtNumeroOC.Text = "txtNumeroOC";
            this.txtNumeroOC.Top = 0F;
            this.txtNumeroOC.Width = 0.887F;
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.CanShrink = true;
            this.txtPorcentaje.Height = 0.15F;
            this.txtPorcentaje.Left = 0.88F;
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Style = "font-size: 8pt";
            this.txtPorcentaje.Tag = "No Movable";
            this.txtPorcentaje.Text = "txtPorcentaje";
            this.txtPorcentaje.Top = 0F;
            this.txtPorcentaje.Width = 0.812F;
            // 
            // txtNombreArticulo
            // 
            this.txtNombreArticulo.CanShrink = true;
            this.txtNombreArticulo.Height = 0.15F;
            this.txtNombreArticulo.Left = 1.7F;
            this.txtNombreArticulo.Name = "txtNombreArticulo";
            this.txtNombreArticulo.Style = "font-size: 8pt";
            this.txtNombreArticulo.Tag = "No Movable";
            this.txtNombreArticulo.Text = "txtNombreArticulo";
            this.txtNombreArticulo.Top = 0F;
            this.txtNombreArticulo.Width = 1.518F;
            // 
            // txtSoles
            // 
            this.txtSoles.CanShrink = true;
            this.txtSoles.Height = 0.15F;
            this.txtSoles.Left = 3.215F;
            this.txtSoles.Name = "txtSoles";
            this.txtSoles.Style = "font-size: 8pt; text-align: right";
            this.txtSoles.Tag = "No Movable";
            this.txtSoles.Text = "txtSoles";
            this.txtSoles.Top = 0F;
            this.txtSoles.Width = 0.81F;
            // 
            // txtDolares
            // 
            this.txtDolares.CanShrink = true;
            this.txtDolares.Height = 0.15F;
            this.txtDolares.Left = 4.025F;
            this.txtDolares.MultiLine = false;
            this.txtDolares.Name = "txtDolares";
            this.txtDolares.Style = "font-size: 8pt; text-align: right";
            this.txtDolares.Tag = "No Movable";
            this.txtDolares.Text = "txtDolares";
            this.txtDolares.Top = 0F;
            this.txtDolares.Width = 0.81F;
            // 
            // txtOrden
            // 
            this.txtOrden.CanShrink = true;
            this.txtOrden.Height = 0.15F;
            this.txtOrden.Left = 4.835F;
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.Style = "font-size: 8pt";
            this.txtOrden.Tag = "No Movable";
            this.txtOrden.Text = "txtOrden";
            this.txtOrden.Top = 0F;
            this.txtOrden.Visible = false;
            this.txtOrden.Width = 0.178F;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtNombreCompania,
            this.lblFechaYHoraDeEmision,
            this.lblFechaInicialYFinal,
            this.lblTituloInforme,
            this.txtNroDePagina});
            this.PageHeader.Height = 1.202875F;
            this.PageHeader.Name = "PageHeader";
            // 
            // txtNombreCompania
            // 
            this.txtNombreCompania.Height = 0.156F;
            this.txtNombreCompania.Left = 0F;
            this.txtNombreCompania.Name = "txtNombreCompania";
            this.txtNombreCompania.Style = "text-align: center";
            this.txtNombreCompania.Text = "txtNombreCompania";
            this.txtNombreCompania.Top = 0.015625F;
            this.txtNombreCompania.Width = 4.09375F;
            // 
            // lblFechaYHoraDeEmision
            // 
            this.lblFechaYHoraDeEmision.Height = 0.156F;
            this.lblFechaYHoraDeEmision.HyperLink = null;
            this.lblFechaYHoraDeEmision.Left = 0F;
            this.lblFechaYHoraDeEmision.Name = "lblFechaYHoraDeEmision";
            this.lblFechaYHoraDeEmision.Style = "font-size: 8.25pt; text-align: center";
            this.lblFechaYHoraDeEmision.Text = "lblFechaYHoraDeEmision";
            this.lblFechaYHoraDeEmision.Top = 0.546875F;
            this.lblFechaYHoraDeEmision.Width = 4.09375F;
            // 
            // lblFechaInicialYFinal
            // 
            this.lblFechaInicialYFinal.Height = 0.156F;
            this.lblFechaInicialYFinal.HyperLink = null;
            this.lblFechaInicialYFinal.Left = 0.03125F;
            this.lblFechaInicialYFinal.Name = "lblFechaInicialYFinal";
            this.lblFechaInicialYFinal.Style = "font-size: 8.25pt; text-align: center";
            this.lblFechaInicialYFinal.Text = "lblFechaInicialYFinal";
            this.lblFechaInicialYFinal.Top = 0.359375F;
            this.lblFechaInicialYFinal.Width = 4.09375F;
            // 
            // lblTituloInforme
            // 
            this.lblTituloInforme.Height = 0.156F;
            this.lblTituloInforme.HyperLink = null;
            this.lblTituloInforme.Left = 0F;
            this.lblTituloInforme.Name = "lblTituloInforme";
            this.lblTituloInforme.Style = "font-size: 8.25pt; text-align: center";
            this.lblTituloInforme.Text = "Compra";
            this.lblTituloInforme.Top = 0.21875F;
            this.lblTituloInforme.Width = 4.09375F;
            // 
            // txtNroDePagina
            // 
            this.txtNroDePagina.Height = 0.156F;
            this.txtNroDePagina.Left = 6.46875F;
            this.txtNroDePagina.Name = "txtNroDePagina";
            this.txtNroDePagina.Style = "font-size: 8.25pt";
            this.txtNroDePagina.Text = "txtNroDePagina";
            this.txtNroDePagina.Top = 0F;
            this.txtNroDePagina.Width = 0.90625F;
            // 
            // PageFooter
            // 
            this.PageFooter.CanGrow = false;
            this.PageFooter.Height = 0.2083336F;
            this.PageFooter.Name = "PageFooter";
            // 
            // GHDetail
            // 
            this.GHDetail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblTasaDeCambio,
            this.txtCambio,
            this.lblCotizacion,
            this.txtNumeroCot,
            this.lblCliente,
            this.txtNombreCliente});
            this.GHDetail.Height = 0.31225F;
            this.GHDetail.Name = "GHDetail";
            // 
            // txtCambio
            // 
            this.txtCambio.Height = 0.15F;
            this.txtCambio.Left = 0.887F;
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.Style = "font-size: 8pt; vertical-align: middle";
            this.txtCambio.Tag = "Movable";
            this.txtCambio.Text = "txtCambio";
            this.txtCambio.Top = 0F;
            this.txtCambio.Width = 0.792F;
            // 
            // lblTasaDeCambio
            // 
            this.lblTasaDeCambio.Height = 0.15625F;
            this.lblTasaDeCambio.HyperLink = null;
            this.lblTasaDeCambio.Left = 0F;
            this.lblTasaDeCambio.MultiLine = false;
            this.lblTasaDeCambio.Name = "lblTasaDeCambio";
            this.lblTasaDeCambio.Style = "font-size: 8pt; font-weight: bold";
            this.lblTasaDeCambio.Tag = "Movable";
            this.lblTasaDeCambio.Text = "Tasa de Cambio";
            this.lblTasaDeCambio.Top = 0F;
            this.lblTasaDeCambio.Width = 0.887F;
            // 
            // lblCotizacion
            // 
            this.lblCotizacion.Height = 0.15625F;
            this.lblCotizacion.HyperLink = null;
            this.lblCotizacion.Left = 0F;
            this.lblCotizacion.MultiLine = false;
            this.lblCotizacion.Name = "lblCotizacion";
            this.lblCotizacion.Style = "font-size: 8pt; font-weight: bold";
            this.lblCotizacion.Tag = "Movable";
            this.lblCotizacion.Text = "Nro. Cotización";
            this.lblCotizacion.Top = 0.156F;
            this.lblCotizacion.Width = 0.887F;
            // 
            // txtNumeroCot
            // 
            this.txtNumeroCot.Height = 0.15F;
            this.txtNumeroCot.Left = 0.887F;
            this.txtNumeroCot.Name = "txtNumeroCot";
            this.txtNumeroCot.Style = "font-size: 8pt; vertical-align: middle";
            this.txtNumeroCot.Tag = "Movable";
            this.txtNumeroCot.Text = "txtNumeroCot";
            this.txtNumeroCot.Top = 0.15625F;
            this.txtNumeroCot.Width = 0.792F;
            // 
            // GFDetail
            // 
            this.GFDetail.CanShrink = true;
            this.GFDetail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblUtilidadBruta,
            this.txtUtilidadSoles,
            this.txtUtilidadDolares,
            this.txtPorcentajeUtilidad});
            this.GFDetail.Height = 0.15F;
            this.GFDetail.KeepTogether = true;
            this.GFDetail.Name = "GFDetail";
            this.GFDetail.Format += new System.EventHandler(this.GFDetail_Format);
            // 
            // lblUtilidadBruta
            // 
            this.lblUtilidadBruta.Height = 0.15F;
            this.lblUtilidadBruta.HyperLink = null;
            this.lblUtilidadBruta.Left = 1.7F;
            this.lblUtilidadBruta.MultiLine = false;
            this.lblUtilidadBruta.Name = "lblUtilidadBruta";
            this.lblUtilidadBruta.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: middle";
            this.lblUtilidadBruta.Tag = "Movable";
            this.lblUtilidadBruta.Text = "UTILIDAD BRUTA";
            this.lblUtilidadBruta.Top = 0F;
            this.lblUtilidadBruta.Width = 1.518F;
            // 
            // txtUtilidadSoles
            // 
            this.txtUtilidadSoles.CanShrink = true;
            this.txtUtilidadSoles.Height = 0.15F;
            this.txtUtilidadSoles.Left = 3.215F;
            this.txtUtilidadSoles.Name = "txtUtilidadSoles";
            this.txtUtilidadSoles.Style = "font-size: 8pt; text-align: right; vertical-align: middle";
            this.txtUtilidadSoles.Tag = "Movable";
            this.txtUtilidadSoles.Text = "txtUtilidadSoles";
            this.txtUtilidadSoles.Top = 0F;
            this.txtUtilidadSoles.Width = 0.81F;
            // 
            // txtUtilidadDolares
            // 
            this.txtUtilidadDolares.CanShrink = true;
            this.txtUtilidadDolares.Height = 0.15F;
            this.txtUtilidadDolares.Left = 4.025F;
            this.txtUtilidadDolares.Name = "txtUtilidadDolares";
            this.txtUtilidadDolares.Style = "font-size: 8pt; text-align: right; vertical-align: middle";
            this.txtUtilidadDolares.Tag = "Movable";
            this.txtUtilidadDolares.Text = "txtUtilidadDolares";
            this.txtUtilidadDolares.Top = 0F;
            this.txtUtilidadDolares.Width = 0.81F;
            // 
            // txtPorcentajeUtilidad
            // 
            this.txtPorcentajeUtilidad.CanShrink = true;
            this.txtPorcentajeUtilidad.Height = 0.15F;
            this.txtPorcentajeUtilidad.Left = 4.835F;
            this.txtPorcentajeUtilidad.Name = "txtPorcentajeUtilidad";
            this.txtPorcentajeUtilidad.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: middle";
            this.txtPorcentajeUtilidad.Tag = "Movable";
            this.txtPorcentajeUtilidad.Text = "txtPorcentajeUtilidad";
            this.txtPorcentajeUtilidad.Top = 0F;
            this.txtPorcentajeUtilidad.Width = 0.81F;
            // 
            // lblTotales
            // 
            this.lblTotales.Height = 0.15F;
            this.lblTotales.HyperLink = null;
            this.lblTotales.Left = 1.7F;
            this.lblTotales.MultiLine = false;
            this.lblTotales.Name = "lblTotales";
            this.lblTotales.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: middle";
            this.lblTotales.Tag = "Movable";
            this.lblTotales.Text = "Totales";
            this.lblTotales.Top = 0F;
            this.lblTotales.Width = 1.518F;
            // 
            // txtTotalML
            // 
            this.txtTotalML.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtTotalML.CanShrink = true;
            this.txtTotalML.Height = 0.15F;
            this.txtTotalML.Left = 3.215F;
            this.txtTotalML.Name = "txtTotalML";
            this.txtTotalML.Style = "font-size: 8pt; text-align: right; vertical-align: middle";
            this.txtTotalML.Tag = "Movable";
            this.txtTotalML.Text = "txtCostoTotalSoles";
            this.txtTotalML.Top = 0F;
            this.txtTotalML.Width = 0.81F;
            // 
            // txtTotalME
            // 
            this.txtTotalME.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtTotalME.CanShrink = true;
            this.txtTotalME.Height = 0.15F;
            this.txtTotalME.Left = 4.025F;
            this.txtTotalME.Name = "txtTotalME";
            this.txtTotalME.Style = "font-size: 8pt; text-align: right; vertical-align: middle";
            this.txtTotalME.Tag = "Movable";
            this.txtTotalME.Text = "txtCostoTotalDolares";
            this.txtTotalME.Top = 0F;
            this.txtTotalME.Width = 0.81F;
            // 
            // GHOrdenDeCompra
            // 
            this.GHOrdenDeCompra.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblOC,
            this.lblNombreArticulo,
            this.lblVenta,
            this.lblSoles,
            this.lblDolares});
            this.GHOrdenDeCompra.Height = 0.15F;
            this.GHOrdenDeCompra.Name = "GHOrdenDeCompra";
            this.GHOrdenDeCompra.Format += new System.EventHandler(this.GHOrdenDeCompra_Format);
            this.GHOrdenDeCompra.BeforePrint += new System.EventHandler(this.GHOrdenDeCompra_BeforePrint);
            // 
            // lblOC
            // 
            this.lblOC.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblOC.Height = 0.15F;
            this.lblOC.HyperLink = null;
            this.lblOC.Left = 0F;
            this.lblOC.MultiLine = false;
            this.lblOC.Name = "lblOC";
            this.lblOC.Style = "font-size: 8pt; font-weight: bold";
            this.lblOC.Tag = "Movable";
            this.lblOC.Text = "Nro. O/C";
            this.lblOC.Top = 0F;
            this.lblOC.Width = 0.887F;
            // 
            // lblNombreArticulo
            // 
            this.lblNombreArticulo.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblNombreArticulo.Height = 0.15F;
            this.lblNombreArticulo.HyperLink = null;
            this.lblNombreArticulo.Left = 1.7F;
            this.lblNombreArticulo.MultiLine = false;
            this.lblNombreArticulo.Name = "lblNombreArticulo";
            this.lblNombreArticulo.Style = "font-size: 8pt; font-weight: bold";
            this.lblNombreArticulo.Tag = "Movable";
            this.lblNombreArticulo.Text = "Nombre Articulo";
            this.lblNombreArticulo.Top = 0F;
            this.lblNombreArticulo.Width = 1.518F;
            // 
            // lblVenta
            // 
            this.lblVenta.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblVenta.Height = 0.15F;
            this.lblVenta.HyperLink = null;
            this.lblVenta.Left = 0.887F;
            this.lblVenta.MultiLine = false;
            this.lblVenta.Name = "lblVenta";
            this.lblVenta.Style = "font-size: 8pt; font-weight: bold";
            this.lblVenta.Tag = "Movable";
            this.lblVenta.Text = "% de Venta";
            this.lblVenta.Top = 0F;
            this.lblVenta.Width = 0.7920002F;
            // 
            // lblSoles
            // 
            this.lblSoles.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblSoles.Height = 0.15F;
            this.lblSoles.HyperLink = null;
            this.lblSoles.Left = 3.215F;
            this.lblSoles.MultiLine = false;
            this.lblSoles.Name = "lblSoles";
            this.lblSoles.Style = "font-size: 8pt; font-weight: bold; text-align: right";
            this.lblSoles.Tag = "Movable";
            this.lblSoles.Text = "Soles";
            this.lblSoles.Top = 0F;
            this.lblSoles.Width = 0.8100002F;
            // 
            // lblDolares
            // 
            this.lblDolares.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.lblDolares.Height = 0.15F;
            this.lblDolares.HyperLink = null;
            this.lblDolares.Left = 4.025F;
            this.lblDolares.MultiLine = false;
            this.lblDolares.Name = "lblDolares";
            this.lblDolares.Style = "font-size: 8pt; font-weight: bold; text-align: right";
            this.lblDolares.Tag = "Movable";
            this.lblDolares.Text = "Dolares";
            this.lblDolares.Top = 0F;
            this.lblDolares.Width = 0.81F;
            // 
            // GFOrdenDeCompra
            // 
            this.GFOrdenDeCompra.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtTotalML,
            this.lblTotales,
            this.txtTotalME});
            this.GFOrdenDeCompra.Height = 0.2F;
            this.GFOrdenDeCompra.Name = "GFOrdenDeCompra";
            this.GFOrdenDeCompra.Format += new System.EventHandler(this.GFOrdenDeCompra_Format);
            this.GFOrdenDeCompra.BeforePrint += new System.EventHandler(this.GFOrdenDeCompra_BeforePrint);
            // 
            // lblCliente
            // 
            this.lblCliente.Height = 0.15625F;
            this.lblCliente.HyperLink = null;
            this.lblCliente.Left = 1.679F;
            this.lblCliente.MultiLine = false;
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Style = "font-size: 8pt; font-weight: bold";
            this.lblCliente.Tag = "Movable";
            this.lblCliente.Text = "Cliente";
            this.lblCliente.Top = 0.15F;
            this.lblCliente.Width = 1.518F;
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Height = 0.15F;
            this.txtNombreCliente.Left = 3.21F;
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Style = "font-size: 8pt; vertical-align: middle";
            this.txtNombreCliente.Tag = "Movable";
            this.txtNombreCliente.Text = "txtNombreCliente";
            this.txtNombreCliente.Top = 0.15F;
            this.txtNombreCliente.Width = 2.43F;
            // 
            // dsrImprimirCotizacionOrdenDeCompra
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.5F;
            this.Sections.Add(this.PageHeader);
            this.Sections.Add(this.GHDetail);
            this.Sections.Add(this.GHOrdenDeCompra);
            this.Sections.Add(this.Detail);
            this.Sections.Add(this.GFOrdenDeCompra);
            this.Sections.Add(this.GFDetail);
            this.Sections.Add(this.PageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule(resources.GetString("$this.StyleSheet"), "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: \'inherit\'; font-style: inherit; font-variant: inherit; font-weight: " +
            "bold; font-size: 16pt; font-size-adjust: inherit; font-stretch: inherit", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: \'Times New Roman\'; font-style: italic; font-variant: inherit; font-w" +
            "eight: bold; font-size: 14pt; font-size-adjust: inherit; font-stretch: inherit", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: \'inherit\'; font-style: inherit; font-variant: inherit; font-weight: " +
            "bold; font-size: 13pt; font-size-adjust: inherit; font-stretch: inherit", "Heading3", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("", "Heading4", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreArticulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDolares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrden)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreCompania)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFechaYHoraDeEmision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFechaInicialYFinal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTituloInforme)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroDePagina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCambio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTasaDeCambio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCotizacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroCot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUtilidadBruta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUtilidadSoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUtilidadDolares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPorcentajeUtilidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTotales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalML)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalME)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNombreArticulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDolares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Detail Detail;
        private DataDynamics.ActiveReports.TextBox txtNumeroOC;
        private DataDynamics.ActiveReports.TextBox txtPorcentaje;
        private DataDynamics.ActiveReports.TextBox txtSoles;
        private DataDynamics.ActiveReports.TextBox txtDolares;
        private DataDynamics.ActiveReports.PageHeader PageHeader;
        private DataDynamics.ActiveReports.PageFooter PageFooter;
        private DataDynamics.ActiveReports.GroupHeader GHDetail;
        private DataDynamics.ActiveReports.GroupFooter GFDetail;
        private DataDynamics.ActiveReports.Label lblTotales;
        private DataDynamics.ActiveReports.TextBox txtTotalML;
        private DataDynamics.ActiveReports.TextBox txtTotalME;
        private DataDynamics.ActiveReports.Label lblUtilidadBruta;
        private DataDynamics.ActiveReports.TextBox txtUtilidadSoles;
        private DataDynamics.ActiveReports.TextBox txtUtilidadDolares;
        private DataDynamics.ActiveReports.TextBox txtPorcentajeUtilidad;
        private DataDynamics.ActiveReports.TextBox txtCambio;
        private DataDynamics.ActiveReports.Label lblTasaDeCambio;
        private DataDynamics.ActiveReports.GroupHeader GHOrdenDeCompra;
        private DataDynamics.ActiveReports.GroupFooter GFOrdenDeCompra;
        private DataDynamics.ActiveReports.TextBox txtOrden;
        private DataDynamics.ActiveReports.TextBox txtNombreArticulo;
        private DataDynamics.ActiveReports.TextBox txtNombreCompania;
        private DataDynamics.ActiveReports.Label lblFechaYHoraDeEmision;
        private DataDynamics.ActiveReports.Label lblFechaInicialYFinal;
        private DataDynamics.ActiveReports.Label lblTituloInforme;
        private DataDynamics.ActiveReports.TextBox txtNroDePagina;
        private DataDynamics.ActiveReports.Label lblOC;
        private DataDynamics.ActiveReports.Label lblNombreArticulo;
        private DataDynamics.ActiveReports.Label lblVenta;
        private DataDynamics.ActiveReports.Label lblSoles;
        private DataDynamics.ActiveReports.Label lblDolares;
        private DataDynamics.ActiveReports.Label lblCotizacion;
        private DataDynamics.ActiveReports.TextBox txtNumeroCot;
        private DataDynamics.ActiveReports.Label lblCliente;
        private DataDynamics.ActiveReports.TextBox txtNombreCliente;
    }
}
