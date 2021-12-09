namespace Galac.Adm.Rpt.Venta
{
    /// <summary>
    /// Summary description for dsrFacturacionPorArticuloResumido.
    /// </summary>
    partial class dsrFacturacionPorArticuloResumido
    {

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dsrFacturacionPorArticuloResumido));
			this.Detail = new DataDynamics.ActiveReports.Detail();
			this.txtFecha = new DataDynamics.ActiveReports.TextBox();
			this.txtCodigoCliente = new DataDynamics.ActiveReports.TextBox();
			this.txtCantidad = new DataDynamics.ActiveReports.TextBox();
			this.txtMonto = new DataDynamics.ActiveReports.TextBox();
			this.PageHeader = new DataDynamics.ActiveReports.PageHeader();
			this.lblTituloDelReporte = new DataDynamics.ActiveReports.Label();
			this.txtCompania = new DataDynamics.ActiveReports.TextBox();
			this.lblFechaYHoraDeEmision = new DataDynamics.ActiveReports.Label();
			this.txtNumeroDePagina = new DataDynamics.ActiveReports.TextBox();
			this.lblFechaInicialYFinal = new DataDynamics.ActiveReports.Label();
			this.PageFooter = new DataDynamics.ActiveReports.PageFooter();
			this.lblNota = new DataDynamics.ActiveReports.Label();
			this.GHArticulo = new DataDynamics.ActiveReports.GroupHeader();
			this.lblArticulo = new DataDynamics.ActiveReports.Label();
			this.txtCodigo = new DataDynamics.ActiveReports.TextBox();
			this.lblUnidadDeVenta = new DataDynamics.ActiveReports.Label();
			this.txtUnidadDeVenta = new DataDynamics.ActiveReports.TextBox();
			this.txtDescripcion = new DataDynamics.ActiveReports.TextBox();
			this.txtMoneda = new DataDynamics.ActiveReports.TextBox();
			this.lblMoneda = new DataDynamics.ActiveReports.Label();
			this.GFArticulo = new DataDynamics.ActiveReports.GroupFooter();
			this.GHMoneda = new DataDynamics.ActiveReports.GroupHeader();
			this.lblFecha = new DataDynamics.ActiveReports.Label();
			this.lblCantidad = new DataDynamics.ActiveReports.Label();
			this.lblMontoRenglon = new DataDynamics.ActiveReports.Label();
			this.GFMoneda = new DataDynamics.ActiveReports.GroupFooter();
			this.txtTotalCantidad = new DataDynamics.ActiveReports.TextBox();
			this.txtTotalMonto = new DataDynamics.ActiveReports.TextBox();
			this.lblTotales = new DataDynamics.ActiveReports.Label();
			((System.ComponentModel.ISupportInitialize)(this.txtFecha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCodigoCliente)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMonto)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblTituloDelReporte)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCompania)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFechaYHoraDeEmision)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNumeroDePagina)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFechaInicialYFinal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblNota)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblArticulo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblUnidadDeVenta)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUnidadDeVenta)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDescripcion)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMoneda)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblMoneda)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFecha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblCantidad)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblMontoRenglon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalCantidad)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalMonto)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblTotales)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// Detail
			// 
			this.Detail.ColumnSpacing = 0F;
			this.Detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtFecha,
            this.txtCodigoCliente,
            this.txtCantidad,
            this.txtMonto});
			this.Detail.Height = 0.15625F;
			this.Detail.KeepTogether = true;
			this.Detail.Name = "Detail";
			// 
			// txtFecha
			// 
			this.txtFecha.Height = 0.15625F;
			this.txtFecha.Left = 0F;
			this.txtFecha.Name = "txtFecha";
			this.txtFecha.Style = "font-size: 8pt";
			this.txtFecha.Text = "txtFecha";
			this.txtFecha.Top = 0F;
			this.txtFecha.Width = 3.75F;
			// 
			// txtCodigoCliente
			// 
			this.txtCodigoCliente.Height = 0.15625F;
			this.txtCodigoCliente.Left = 0F;
			this.txtCodigoCliente.Name = "txtCodigoCliente";
			this.txtCodigoCliente.Style = "color: rgb(255,255,255); font-size: 8pt";
			this.txtCodigoCliente.Text = "txtCodigoCliente";
			this.txtCodigoCliente.Top = 0.78125F;
			this.txtCodigoCliente.Visible = false;
			this.txtCodigoCliente.Width = 0.625F;
			// 
			// txtCantidad
			// 
			this.txtCantidad.Height = 0.15625F;
			this.txtCantidad.Left = 3.75F;
			this.txtCantidad.Name = "txtCantidad";
			this.txtCantidad.Style = "font-size: 8pt; text-align: right";
			this.txtCantidad.Text = "txtCantidad";
			this.txtCantidad.Top = 0F;
			this.txtCantidad.Width = 1.375F;
			// 
			// txtMonto
			// 
			this.txtMonto.Height = 0.15625F;
			this.txtMonto.Left = 5.125F;
			this.txtMonto.Name = "txtMonto";
			this.txtMonto.Style = "font-size: 8pt; text-align: right";
			this.txtMonto.Text = "txtMonto";
			this.txtMonto.Top = 0F;
			this.txtMonto.Width = 2.375F;
			// 
			// PageHeader
			// 
			this.PageHeader.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblTituloDelReporte,
            this.txtCompania,
            this.lblFechaYHoraDeEmision,
            this.txtNumeroDePagina,
            this.lblFechaInicialYFinal});
			this.PageHeader.Height = 1F;
			this.PageHeader.Name = "PageHeader";
			// 
			// lblTituloDelReporte
			// 
			this.lblTituloDelReporte.Height = 0.25F;
			this.lblTituloDelReporte.HyperLink = null;
			this.lblTituloDelReporte.Left = 0F;
			this.lblTituloDelReporte.Name = "lblTituloDelReporte";
			this.lblTituloDelReporte.Style = "font-size: 12pt; font-weight: bold; text-align: center; vertical-align: top";
			this.lblTituloDelReporte.Text = "Facturación por Artículo - Resumido";
			this.lblTituloDelReporte.Top = 0.28125F;
			this.lblTituloDelReporte.Width = 3.53125F;
			// 
			// txtCompania
			// 
			this.txtCompania.Height = 0.28125F;
			this.txtCompania.HyperLink = null;
			this.txtCompania.Left = 0F;
			this.txtCompania.Name = "txtCompania";
			this.txtCompania.Style = "font-size: 14pt; font-weight: bold; text-align: center; vertical-align: top";
			this.txtCompania.Text = "txtCompania";
			this.txtCompania.Top = 0F;
			this.txtCompania.Width = 3.53125F;
			// 
			// lblFechaYHoraDeEmision
			// 
			this.lblFechaYHoraDeEmision.Height = 0.125F;
			this.lblFechaYHoraDeEmision.HyperLink = null;
			this.lblFechaYHoraDeEmision.Left = 0F;
			this.lblFechaYHoraDeEmision.Name = "lblFechaYHoraDeEmision";
			this.lblFechaYHoraDeEmision.Style = "font-size: 7pt; text-align: center; vertical-align: top";
			this.lblFechaYHoraDeEmision.Text = "lblFechaYHoraDeEmision";
			this.lblFechaYHoraDeEmision.Top = 0.6875F;
			this.lblFechaYHoraDeEmision.Width = 3.53125F;
			// 
			// txtNumeroDePagina
			// 
			this.txtNumeroDePagina.Height = 0.15625F;
			this.txtNumeroDePagina.HyperLink = null;
			this.txtNumeroDePagina.Left = 6.65625F;
			this.txtNumeroDePagina.Name = "txtNumeroDePagina";
			this.txtNumeroDePagina.Style = "font-size: 10pt; vertical-align: top";
			this.txtNumeroDePagina.Text = "txtNumeroDePagina";
			this.txtNumeroDePagina.Top = 0F;
			this.txtNumeroDePagina.Width = 0.84375F;
			// 
			// lblFechaInicialYFinal
			// 
			this.lblFechaInicialYFinal.Height = 0.15625F;
			this.lblFechaInicialYFinal.HyperLink = null;
			this.lblFechaInicialYFinal.Left = 0F;
			this.lblFechaInicialYFinal.Name = "lblFechaInicialYFinal";
			this.lblFechaInicialYFinal.Style = "font-weight: bold; text-align: center";
			this.lblFechaInicialYFinal.Text = "lblFechaInicialYFinal";
			this.lblFechaInicialYFinal.Top = 0.5F;
			this.lblFechaInicialYFinal.Width = 3.53125F;
			// 
			// PageFooter
			// 
			this.PageFooter.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblNota});
			this.PageFooter.Height = 0.3125F;
			this.PageFooter.Name = "PageFooter";
			// 
			// lblNota
			// 
			this.lblNota.Height = 0.312F;
			this.lblNota.HyperLink = null;
			this.lblNota.Left = 0F;
			this.lblNota.Name = "lblNota";
			this.lblNota.Style = "font-size: 8pt";
			this.lblNota.Text = "lblNota";
			this.lblNota.Top = 0F;
			this.lblNota.Width = 7.5F;
			// 
			// GHArticulo
			// 
			this.GHArticulo.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblArticulo,
            this.txtCodigo,
            this.lblUnidadDeVenta,
            this.txtUnidadDeVenta,
            this.txtDescripcion});
			this.GHArticulo.Height = 0.3125F;
			this.GHArticulo.KeepTogether = true;
			this.GHArticulo.Name = "GHArticulo";
			this.GHArticulo.RepeatStyle = DataDynamics.ActiveReports.RepeatStyle.OnPage;
			// 
			// lblArticulo
			// 
			this.lblArticulo.Height = 0.15625F;
			this.lblArticulo.HyperLink = null;
			this.lblArticulo.Left = 0F;
			this.lblArticulo.Name = "lblArticulo";
			this.lblArticulo.Style = "font-size: 8pt; font-weight: bold";
			this.lblArticulo.Text = "Artículo";
			this.lblArticulo.Top = 0F;
			this.lblArticulo.Width = 0.5F;
			// 
			// txtCodigo
			// 
			this.txtCodigo.Height = 0.15625F;
			this.txtCodigo.Left = 0.5F;
			this.txtCodigo.Name = "txtCodigo";
			this.txtCodigo.Style = "font-size: 8pt";
			this.txtCodigo.Text = "txtCodigo";
			this.txtCodigo.Top = 0F;
			this.txtCodigo.Width = 3.25F;
			// 
			// lblUnidadDeVenta
			// 
			this.lblUnidadDeVenta.Height = 0.15625F;
			this.lblUnidadDeVenta.HyperLink = null;
			this.lblUnidadDeVenta.Left = 3.75F;
			this.lblUnidadDeVenta.Name = "lblUnidadDeVenta";
			this.lblUnidadDeVenta.Style = "font-size: 8pt; font-weight: bold; text-align: left";
			this.lblUnidadDeVenta.Text = "Unidad de Venta";
			this.lblUnidadDeVenta.Top = 0F;
			this.lblUnidadDeVenta.Width = 1.375F;
			// 
			// txtUnidadDeVenta
			// 
			this.txtUnidadDeVenta.Height = 0.15625F;
			this.txtUnidadDeVenta.Left = 5.124F;
			this.txtUnidadDeVenta.Name = "txtUnidadDeVenta";
			this.txtUnidadDeVenta.Style = "font-size: 8pt; text-align: left";
			this.txtUnidadDeVenta.Text = "txtUnidadDeVenta";
			this.txtUnidadDeVenta.Top = 0F;
			this.txtUnidadDeVenta.Width = 2.376001F;
			// 
			// txtDescripcion
			// 
			this.txtDescripcion.Height = 0.15625F;
			this.txtDescripcion.Left = 0.5F;
			this.txtDescripcion.Name = "txtDescripcion";
			this.txtDescripcion.Style = "font-size: 8pt";
			this.txtDescripcion.Text = "txtDescripcion";
			this.txtDescripcion.Top = 0.156F;
			this.txtDescripcion.Width = 7F;
			// 
			// txtMoneda
			// 
			this.txtMoneda.Height = 0.15625F;
			this.txtMoneda.Left = 0.5F;
			this.txtMoneda.Name = "txtMoneda";
			this.txtMoneda.Style = "font-size: 8pt";
			this.txtMoneda.Text = "txtMoneda";
			this.txtMoneda.Top = 0F;
			this.txtMoneda.Width = 7F;
			// 
			// lblMoneda
			// 
			this.lblMoneda.Height = 0.15625F;
			this.lblMoneda.HyperLink = null;
			this.lblMoneda.Left = 5.960464E-08F;
			this.lblMoneda.Name = "lblMoneda";
			this.lblMoneda.Style = "font-size: 8pt; font-weight: bold";
			this.lblMoneda.Text = "Moneda";
			this.lblMoneda.Top = 0F;
			this.lblMoneda.Width = 0.4999999F;
			// 
			// GFArticulo
			// 
			this.GFArticulo.Height = 0F;
			this.GFArticulo.KeepTogether = true;
			this.GFArticulo.Name = "GFArticulo";
			// 
			// GHMoneda
			// 
			this.GHMoneda.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblFecha,
            this.lblCantidad,
            this.lblMontoRenglon,
            this.txtMoneda,
            this.lblMoneda});
			this.GHMoneda.Height = 0.3125F;
			this.GHMoneda.KeepTogether = true;
			this.GHMoneda.Name = "GHMoneda";
			this.GHMoneda.RepeatStyle = DataDynamics.ActiveReports.RepeatStyle.OnPage;
			// 
			// lblFecha
			// 
			this.lblFecha.Border.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblFecha.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.lblFecha.Height = 0.15625F;
			this.lblFecha.HyperLink = null;
			this.lblFecha.Left = 0F;
			this.lblFecha.Name = "lblFecha";
			this.lblFecha.Style = "font-size: 8pt; font-weight: bold";
			this.lblFecha.Text = "Fecha";
			this.lblFecha.Top = 0.15625F;
			this.lblFecha.Width = 3.75F;
			// 
			// lblCantidad
			// 
			this.lblCantidad.Border.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblCantidad.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.lblCantidad.Height = 0.15625F;
			this.lblCantidad.HyperLink = null;
			this.lblCantidad.Left = 3.75F;
			this.lblCantidad.Name = "lblCantidad";
			this.lblCantidad.Style = "font-size: 8pt; font-weight: bold; text-align: right";
			this.lblCantidad.Text = "Cantidad";
			this.lblCantidad.Top = 0.156F;
			this.lblCantidad.Width = 1.375F;
			// 
			// lblMontoRenglon
			// 
			this.lblMontoRenglon.Border.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblMontoRenglon.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.lblMontoRenglon.Height = 0.15625F;
			this.lblMontoRenglon.HyperLink = null;
			this.lblMontoRenglon.Left = 5.125F;
			this.lblMontoRenglon.Name = "lblMontoRenglon";
			this.lblMontoRenglon.Style = "font-size: 8pt; font-weight: bold; text-align: right";
			this.lblMontoRenglon.Text = "Monto Facturado";
			this.lblMontoRenglon.Top = 0.156F;
			this.lblMontoRenglon.Width = 2.375F;
			// 
			// GFMoneda
			// 
			this.GFMoneda.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtTotalCantidad,
            this.txtTotalMonto,
            this.lblTotales});
			this.GFMoneda.Height = 0.15625F;
			this.GFMoneda.KeepTogether = true;
			this.GFMoneda.Name = "GFMoneda";
			// 
			// txtTotalCantidad
			// 
			this.txtTotalCantidad.Border.TopColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.txtTotalCantidad.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.txtTotalCantidad.Height = 0.15625F;
			this.txtTotalCantidad.Left = 3.75F;
			this.txtTotalCantidad.Name = "txtTotalCantidad";
			this.txtTotalCantidad.Style = "font-size: 8pt; text-align: right";
			this.txtTotalCantidad.SummaryGroup = "GHArticulo";
			this.txtTotalCantidad.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
			this.txtTotalCantidad.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
			this.txtTotalCantidad.Text = "txtTotalCantidad";
			this.txtTotalCantidad.Top = 0F;
			this.txtTotalCantidad.Width = 1.374F;
			// 
			// txtTotalMonto
			// 
			this.txtTotalMonto.Border.TopColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.txtTotalMonto.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.txtTotalMonto.Height = 0.15625F;
			this.txtTotalMonto.Left = 5.124F;
			this.txtTotalMonto.Name = "txtTotalMonto";
			this.txtTotalMonto.Style = "font-size: 8pt; text-align: right";
			this.txtTotalMonto.SummaryGroup = "GHArticulo";
			this.txtTotalMonto.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
			this.txtTotalMonto.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
			this.txtTotalMonto.Text = "txtTotalMonto";
			this.txtTotalMonto.Top = 0F;
			this.txtTotalMonto.Width = 2.376F;
			// 
			// lblTotales
			// 
			this.lblTotales.Height = 0.15625F;
			this.lblTotales.HyperLink = null;
			this.lblTotales.Left = 0F;
			this.lblTotales.Name = "lblTotales";
			this.lblTotales.Style = "font-size: 8pt; font-weight: bold; text-align: right";
			this.lblTotales.Text = "Totales   ";
			this.lblTotales.Top = 0F;
			this.lblTotales.Width = 3.75F;
			// 
			// dsrFacturacionPorArticuloResumido
			// 
			this.MasterReport = false;
			this.PageSettings.PaperHeight = 11F;
			this.PageSettings.PaperWidth = 8.5F;
			this.PrintWidth = 8.375F;
			this.Sections.Add(this.PageHeader);
			this.Sections.Add(this.GHArticulo);
			this.Sections.Add(this.GHMoneda);
			this.Sections.Add(this.Detail);
			this.Sections.Add(this.GFMoneda);
			this.Sections.Add(this.GFArticulo);
			this.Sections.Add(this.PageFooter);
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule(resources.GetString("$this.StyleSheet"), "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: \'inherit\'; font-style: inherit; font-variant: inherit; font-weight: " +
            "bold; font-size: 16pt; font-size-adjust: inherit; font-stretch: inherit", "Heading1", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: \'Times New Roman\'; font-style: italic; font-variant: inherit; font-w" +
            "eight: bold; font-size: 14pt; font-size-adjust: inherit; font-stretch: inherit", "Heading2", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: \'inherit\'; font-style: inherit; font-variant: inherit; font-weight: " +
            "bold; font-size: 13pt; font-size-adjust: inherit; font-stretch: inherit", "Heading3", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("", "Heading4", "Normal"));
			((System.ComponentModel.ISupportInitialize)(this.txtFecha)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCodigoCliente)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMonto)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblTituloDelReporte)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCompania)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFechaYHoraDeEmision)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNumeroDePagina)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFechaInicialYFinal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblNota)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblArticulo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblUnidadDeVenta)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUnidadDeVenta)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDescripcion)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtMoneda)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblMoneda)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFecha)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblCantidad)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblMontoRenglon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalCantidad)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalMonto)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblTotales)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Detail Detail;
        private DataDynamics.ActiveReports.TextBox txtFecha;
        private DataDynamics.ActiveReports.TextBox txtCodigoCliente;
        private DataDynamics.ActiveReports.TextBox txtCantidad;
        private DataDynamics.ActiveReports.TextBox txtMonto;
        private DataDynamics.ActiveReports.PageHeader PageHeader;
        private DataDynamics.ActiveReports.Label lblTituloDelReporte;
        private DataDynamics.ActiveReports.TextBox txtCompania;
        private DataDynamics.ActiveReports.Label lblFechaYHoraDeEmision;
        private DataDynamics.ActiveReports.TextBox txtNumeroDePagina;
        private DataDynamics.ActiveReports.Label lblFechaInicialYFinal;
        private DataDynamics.ActiveReports.PageFooter PageFooter;
        private DataDynamics.ActiveReports.Label lblNota;
        private DataDynamics.ActiveReports.GroupHeader GHArticulo;
        private DataDynamics.ActiveReports.TextBox txtMoneda;
        private DataDynamics.ActiveReports.Label lblMoneda;
        private DataDynamics.ActiveReports.GroupFooter GFArticulo;
        private DataDynamics.ActiveReports.GroupHeader GHMoneda;
        private DataDynamics.ActiveReports.Label lblFecha;
        private DataDynamics.ActiveReports.Label lblCantidad;
        private DataDynamics.ActiveReports.Label lblMontoRenglon;
        private DataDynamics.ActiveReports.GroupFooter GFMoneda;
        private DataDynamics.ActiveReports.TextBox txtTotalCantidad;
        private DataDynamics.ActiveReports.TextBox txtTotalMonto;
        private DataDynamics.ActiveReports.Label lblTotales;
		private DataDynamics.ActiveReports.Label lblArticulo;
		private DataDynamics.ActiveReports.TextBox txtCodigo;
		private DataDynamics.ActiveReports.Label lblUnidadDeVenta;
		private DataDynamics.ActiveReports.TextBox txtUnidadDeVenta;
		private DataDynamics.ActiveReports.TextBox txtDescripcion;
	}
}
