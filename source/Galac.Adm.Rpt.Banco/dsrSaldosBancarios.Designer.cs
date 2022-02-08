namespace Galac.Adm.Rpt.Banco {
	/// <summary>
	/// Summary description for dsrSaldosBancarios.
	/// </summary>
	partial class dsrSaldosBancarios {

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dsrSaldosBancarios));
			this.Detail = new DataDynamics.ActiveReports.Detail();
			this.txtCodigo = new DataDynamics.ActiveReports.TextBox();
			this.txtNombreCuenta = new DataDynamics.ActiveReports.TextBox();
			this.txtEgresos = new DataDynamics.ActiveReports.TextBox();
			this.txtSaldoFinal = new DataDynamics.ActiveReports.TextBox();
			this.txtSaldoInicial = new DataDynamics.ActiveReports.TextBox();
			this.txtIngresos = new DataDynamics.ActiveReports.TextBox();
			this.txtNumeroCuenta = new DataDynamics.ActiveReports.TextBox();
			this.PageHeader = new DataDynamics.ActiveReports.PageHeader();
			this.lblTituloDelReporte = new DataDynamics.ActiveReports.Label();
			this.txtCompania = new DataDynamics.ActiveReports.TextBox();
			this.lblFechaYHoraDeEmision = new DataDynamics.ActiveReports.Label();
			this.txtNumeroDePagina = new DataDynamics.ActiveReports.TextBox();
			this.lblFechaInicialYFinal = new DataDynamics.ActiveReports.Label();
			this.PageFooter = new DataDynamics.ActiveReports.PageFooter();
			this.GHMoneda = new DataDynamics.ActiveReports.GroupHeader();
			this.lblIngresos = new DataDynamics.ActiveReports.Label();
			this.lblEgresos = new DataDynamics.ActiveReports.Label();
			this.lblSaldoInicial = new DataDynamics.ActiveReports.Label();
			this.lblSaldoFinal = new DataDynamics.ActiveReports.Label();
			this.txtNombreDeLaMoneda = new DataDynamics.ActiveReports.TextBox();
			this.lblMoneda = new DataDynamics.ActiveReports.Label();
			this.txtCuenta = new DataDynamics.ActiveReports.Label();
			this.GFMoneda = new DataDynamics.ActiveReports.GroupFooter();
			this.lblTotales = new DataDynamics.ActiveReports.Label();
			this.txtTotalEgresos = new DataDynamics.ActiveReports.TextBox();
			this.txtTotalSaldoFinal = new DataDynamics.ActiveReports.TextBox();
			this.txtTotalSaldoInicial = new DataDynamics.ActiveReports.TextBox();
			this.txtTotalIngresos = new DataDynamics.ActiveReports.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNombreCuenta)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEgresos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSaldoFinal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSaldoInicial)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIngresos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNumeroCuenta)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblTituloDelReporte)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCompania)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFechaYHoraDeEmision)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNumeroDePagina)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFechaInicialYFinal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblIngresos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblEgresos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSaldoInicial)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSaldoFinal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNombreDeLaMoneda)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblMoneda)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCuenta)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblTotales)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalEgresos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalSaldoFinal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalSaldoInicial)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalIngresos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// Detail
			// 
			this.Detail.ColumnSpacing = 0F;
			this.Detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtCodigo,
            this.txtNombreCuenta,
            this.txtEgresos,
            this.txtSaldoFinal,
            this.txtSaldoInicial,
            this.txtIngresos,
            this.txtNumeroCuenta});
			this.Detail.Height = 0.188F;
			this.Detail.KeepTogether = true;
			this.Detail.Name = "Detail";
			this.Detail.Format += new System.EventHandler(this.Detail_Format);
			// 
			// txtCodigo
			// 
			this.txtCodigo.Height = 0.188F;
			this.txtCodigo.Left = 0F;
			this.txtCodigo.Name = "txtCodigo";
			this.txtCodigo.Style = "font-size: 8pt; text-align: left";
			this.txtCodigo.Text = "txtCodigo";
			this.txtCodigo.Top = 0F;
			this.txtCodigo.Width = 0.375F;
			// 
			// txtNombreCuenta
			// 
			this.txtNombreCuenta.Height = 0.188F;
			this.txtNombreCuenta.Left = 0.375F;
			this.txtNombreCuenta.Name = "txtNombreCuenta";
			this.txtNombreCuenta.Style = "font-size: 8pt; text-align: left";
			this.txtNombreCuenta.Text = "txtNombreCuenta";
			this.txtNombreCuenta.Top = 0F;
			this.txtNombreCuenta.Width = 2.5F;
			// 
			// txtEgresos
			// 
			this.txtEgresos.DataField = "Montototal";
			this.txtEgresos.Height = 0.188F;
			this.txtEgresos.Left = 5.1875F;
			this.txtEgresos.Name = "txtEgresos";
			this.txtEgresos.Style = "font-size: 8pt; text-align: right";
			this.txtEgresos.Text = "txtEgresos";
			this.txtEgresos.Top = 0F;
			this.txtEgresos.Width = 1.15625F;
			// 
			// txtSaldoFinal
			// 
			this.txtSaldoFinal.DataField = "Montototal";
			this.txtSaldoFinal.Height = 0.188F;
			this.txtSaldoFinal.Left = 6.34375F;
			this.txtSaldoFinal.Name = "txtSaldoFinal";
			this.txtSaldoFinal.Style = "font-size: 8pt; text-align: right";
			this.txtSaldoFinal.Text = "txtSaldoFinal";
			this.txtSaldoFinal.Top = 0F;
			this.txtSaldoFinal.Width = 1.15625F;
			// 
			// txtSaldoInicial
			// 
			this.txtSaldoInicial.DataField = "Montototal";
			this.txtSaldoInicial.Height = 0.188F;
			this.txtSaldoInicial.Left = 2.875F;
			this.txtSaldoInicial.Name = "txtSaldoInicial";
			this.txtSaldoInicial.Style = "font-size: 8pt; text-align: right";
			this.txtSaldoInicial.Text = "txtSaldoInicial";
			this.txtSaldoInicial.Top = 0F;
			this.txtSaldoInicial.Width = 1.15625F;
			// 
			// txtIngresos
			// 
			this.txtIngresos.DataField = "Montototal";
			this.txtIngresos.Height = 0.188F;
			this.txtIngresos.Left = 4.03125F;
			this.txtIngresos.Name = "txtIngresos";
			this.txtIngresos.Style = "font-size: 8pt; text-align: right";
			this.txtIngresos.Text = "txtIngresos";
			this.txtIngresos.Top = 0F;
			this.txtIngresos.Width = 1.15625F;
			// 
			// txtNumeroCuenta
			// 
			this.txtNumeroCuenta.Height = 0.188F;
			this.txtNumeroCuenta.Left = 0.375F;
			this.txtNumeroCuenta.Name = "txtNumeroCuenta";
			this.txtNumeroCuenta.Style = "font-size: 8pt; text-align: left";
			this.txtNumeroCuenta.Text = "txtNumeroCuenta";
			this.txtNumeroCuenta.Top = 0.479F;
			this.txtNumeroCuenta.Width = 2.5F;
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
			this.lblTituloDelReporte.Text = "Saldos Bancarios";
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
			this.lblFechaInicialYFinal.Top = 0.531F;
			this.lblFechaInicialYFinal.Width = 3.53125F;
			// 
			// PageFooter
			// 
			this.PageFooter.Height = 0F;
			this.PageFooter.Name = "PageFooter";
			// 
			// GHMoneda
			// 
			this.GHMoneda.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblIngresos,
            this.lblEgresos,
            this.lblSaldoInicial,
            this.lblSaldoFinal,
            this.txtNombreDeLaMoneda,
            this.lblMoneda,
            this.txtCuenta});
			this.GHMoneda.Height = 0.376F;
			this.GHMoneda.KeepTogether = true;
			this.GHMoneda.Name = "GHMoneda";
			this.GHMoneda.RepeatStyle = DataDynamics.ActiveReports.RepeatStyle.OnPage;
			this.GHMoneda.Format += new System.EventHandler(this.GHMoneda_Format);
			// 
			// lblIngresos
			// 
			this.lblIngresos.Border.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblIngresos.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.lblIngresos.Height = 0.188F;
			this.lblIngresos.HyperLink = null;
			this.lblIngresos.Left = 4.03125F;
			this.lblIngresos.Name = "lblIngresos";
			this.lblIngresos.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: middle";
			this.lblIngresos.Text = "Ingresos";
			this.lblIngresos.Top = 0.1875F;
			this.lblIngresos.Width = 1.15625F;
			// 
			// lblEgresos
			// 
			this.lblEgresos.Border.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblEgresos.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.lblEgresos.Height = 0.188F;
			this.lblEgresos.HyperLink = null;
			this.lblEgresos.Left = 5.1875F;
			this.lblEgresos.Name = "lblEgresos";
			this.lblEgresos.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: middle";
			this.lblEgresos.Text = "Egresos";
			this.lblEgresos.Top = 0.1875F;
			this.lblEgresos.Width = 1.15625F;
			// 
			// lblSaldoInicial
			// 
			this.lblSaldoInicial.Border.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblSaldoInicial.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.lblSaldoInicial.Height = 0.188F;
			this.lblSaldoInicial.HyperLink = null;
			this.lblSaldoInicial.Left = 2.875F;
			this.lblSaldoInicial.Name = "lblSaldoInicial";
			this.lblSaldoInicial.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: middle";
			this.lblSaldoInicial.Text = "Saldo Inicial";
			this.lblSaldoInicial.Top = 0.1875F;
			this.lblSaldoInicial.Width = 1.15625F;
			// 
			// lblSaldoFinal
			// 
			this.lblSaldoFinal.Border.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lblSaldoFinal.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.lblSaldoFinal.Height = 0.188F;
			this.lblSaldoFinal.HyperLink = null;
			this.lblSaldoFinal.Left = 6.34375F;
			this.lblSaldoFinal.Name = "lblSaldoFinal";
			this.lblSaldoFinal.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: middle";
			this.lblSaldoFinal.Text = "Saldo Final";
			this.lblSaldoFinal.Top = 0.1875F;
			this.lblSaldoFinal.Width = 1.15625F;
			// 
			// txtNombreDeLaMoneda
			// 
			this.txtNombreDeLaMoneda.Height = 0.188F;
			this.txtNombreDeLaMoneda.Left = 0.5F;
			this.txtNombreDeLaMoneda.Name = "txtNombreDeLaMoneda";
			this.txtNombreDeLaMoneda.Style = "font-size: 8pt; text-align: left; vertical-align: middle";
			this.txtNombreDeLaMoneda.Text = "txtNombreDeLaMoneda";
			this.txtNombreDeLaMoneda.Top = 0F;
			this.txtNombreDeLaMoneda.Width = 7F;
			// 
			// lblMoneda
			// 
			this.lblMoneda.Height = 0.188F;
			this.lblMoneda.HyperLink = null;
			this.lblMoneda.Left = 0F;
			this.lblMoneda.Name = "lblMoneda";
			this.lblMoneda.Style = "font-size: 8pt; font-weight: bold; text-align: left; vertical-align: middle";
			this.lblMoneda.Text = "Moneda";
			this.lblMoneda.Top = 0F;
			this.lblMoneda.Width = 0.5F;
			// 
			// txtCuenta
			// 
			this.txtCuenta.Border.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.txtCuenta.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.txtCuenta.Height = 0.188F;
			this.txtCuenta.HyperLink = null;
			this.txtCuenta.Left = 0F;
			this.txtCuenta.Name = "txtCuenta";
			this.txtCuenta.Style = "font-size: 8pt; font-weight: bold; text-align: left; vertical-align: middle";
			this.txtCuenta.Text = "Cuenta";
			this.txtCuenta.Top = 0.188F;
			this.txtCuenta.Width = 2.875F;
			// 
			// GFMoneda
			// 
			this.GFMoneda.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblTotales,
            this.txtTotalEgresos,
            this.txtTotalSaldoFinal,
            this.txtTotalSaldoInicial,
            this.txtTotalIngresos});
			this.GFMoneda.Height = 0.188F;
			this.GFMoneda.KeepTogether = true;
			this.GFMoneda.Name = "GFMoneda";
			this.GFMoneda.Format += new System.EventHandler(this.GFMoneda_Format);
			// 
			// lblTotales
			// 
			this.lblTotales.Height = 0.188F;
			this.lblTotales.HyperLink = null;
			this.lblTotales.Left = 0F;
			this.lblTotales.Name = "lblTotales";
			this.lblTotales.Style = "font-size: 8pt; font-weight: bold; text-align: right";
			this.lblTotales.Text = "Totales   ";
			this.lblTotales.Top = 0F;
			this.lblTotales.Width = 2.875F;
			// 
			// txtTotalEgresos
			// 
			this.txtTotalEgresos.Border.TopColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.txtTotalEgresos.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.txtTotalEgresos.DataField = "Montototal";
			this.txtTotalEgresos.Height = 0.188F;
			this.txtTotalEgresos.Left = 5.1875F;
			this.txtTotalEgresos.Name = "txtTotalEgresos";
			this.txtTotalEgresos.Style = "font-size: 8pt; font-weight: bold; text-align: right";
			this.txtTotalEgresos.SummaryGroup = "GHMoneda";
			this.txtTotalEgresos.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
			this.txtTotalEgresos.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
			this.txtTotalEgresos.Text = "txtTotalEgresos";
			this.txtTotalEgresos.Top = 0F;
			this.txtTotalEgresos.Width = 1.15625F;
			// 
			// txtTotalSaldoFinal
			// 
			this.txtTotalSaldoFinal.Border.TopColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.txtTotalSaldoFinal.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.txtTotalSaldoFinal.DataField = "Montototal";
			this.txtTotalSaldoFinal.Height = 0.188F;
			this.txtTotalSaldoFinal.Left = 6.34375F;
			this.txtTotalSaldoFinal.Name = "txtTotalSaldoFinal";
			this.txtTotalSaldoFinal.Style = "font-size: 8pt; font-weight: bold; text-align: right";
			this.txtTotalSaldoFinal.Text = "txtTotalSaldoFinal";
			this.txtTotalSaldoFinal.Top = 0F;
			this.txtTotalSaldoFinal.Width = 1.15625F;
			// 
			// txtTotalSaldoInicial
			// 
			this.txtTotalSaldoInicial.Border.TopColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.txtTotalSaldoInicial.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.txtTotalSaldoInicial.DataField = "Montototal";
			this.txtTotalSaldoInicial.Height = 0.188F;
			this.txtTotalSaldoInicial.Left = 2.875F;
			this.txtTotalSaldoInicial.Name = "txtTotalSaldoInicial";
			this.txtTotalSaldoInicial.Style = "font-size: 8pt; font-weight: bold; text-align: right";
			this.txtTotalSaldoInicial.SummaryGroup = "GHMoneda";
			this.txtTotalSaldoInicial.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
			this.txtTotalSaldoInicial.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
			this.txtTotalSaldoInicial.Text = "txtTotalSaldoInicial";
			this.txtTotalSaldoInicial.Top = 0F;
			this.txtTotalSaldoInicial.Width = 1.15625F;
			// 
			// txtTotalIngresos
			// 
			this.txtTotalIngresos.Border.TopColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.txtTotalIngresos.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
			this.txtTotalIngresos.DataField = "Montototal";
			this.txtTotalIngresos.Height = 0.188F;
			this.txtTotalIngresos.Left = 4.03125F;
			this.txtTotalIngresos.Name = "txtTotalIngresos";
			this.txtTotalIngresos.Style = "font-size: 8pt; font-weight: bold; text-align: right";
			this.txtTotalIngresos.SummaryGroup = "GHMoneda";
			this.txtTotalIngresos.SummaryRunning = DataDynamics.ActiveReports.SummaryRunning.Group;
			this.txtTotalIngresos.SummaryType = DataDynamics.ActiveReports.SummaryType.SubTotal;
			this.txtTotalIngresos.Text = "txtTotalIngresos";
			this.txtTotalIngresos.Top = 0F;
			this.txtTotalIngresos.Width = 1.15625F;
			// 
			// dsrSaldosBancarios
			// 
			this.MasterReport = false;
			this.PageSettings.PaperHeight = 11F;
			this.PageSettings.PaperWidth = 8.5F;
			this.PrintWidth = 7.53125F;
			this.Sections.Add(this.PageHeader);
			this.Sections.Add(this.GHMoneda);
			this.Sections.Add(this.Detail);
			this.Sections.Add(this.GFMoneda);
			this.Sections.Add(this.PageFooter);
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule(resources.GetString("$this.StyleSheet"), "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: inherit; font-style: inherit; font-variant: inherit; font-weight: bo" +
            "ld; font-size: 16pt; font-size-adjust: inherit; font-stretch: inherit", "Heading1", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-style: italic; font-variant: inherit; font-wei" +
            "ght: bold; font-size: 14pt; font-size-adjust: inherit; font-stretch: inherit", "Heading2", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: inherit; font-style: inherit; font-variant: inherit; font-weight: bo" +
            "ld; font-size: 13pt; font-size-adjust: inherit; font-stretch: inherit", "Heading3", "Normal"));
			this.StyleSheet.Add(new DDCssLib.StyleSheetRule("", "Heading4", "Normal"));
			((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNombreCuenta)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEgresos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSaldoFinal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSaldoInicial)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIngresos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNumeroCuenta)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblTituloDelReporte)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCompania)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFechaYHoraDeEmision)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNumeroDePagina)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblFechaInicialYFinal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblIngresos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblEgresos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSaldoInicial)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSaldoFinal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNombreDeLaMoneda)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblMoneda)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCuenta)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblTotales)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalEgresos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalSaldoFinal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalSaldoInicial)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtTotalIngresos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		private DataDynamics.ActiveReports.Detail Detail;
		private DataDynamics.ActiveReports.TextBox txtCodigo;
		private DataDynamics.ActiveReports.TextBox txtNombreCuenta;
		private DataDynamics.ActiveReports.TextBox txtEgresos;
		private DataDynamics.ActiveReports.TextBox txtSaldoFinal;
		private DataDynamics.ActiveReports.TextBox txtSaldoInicial;
		private DataDynamics.ActiveReports.TextBox txtIngresos;
		private DataDynamics.ActiveReports.TextBox txtNumeroCuenta;
		private DataDynamics.ActiveReports.PageHeader PageHeader;
		private DataDynamics.ActiveReports.Label lblTituloDelReporte;
		private DataDynamics.ActiveReports.TextBox txtCompania;
		private DataDynamics.ActiveReports.Label lblFechaYHoraDeEmision;
		private DataDynamics.ActiveReports.TextBox txtNumeroDePagina;
		private DataDynamics.ActiveReports.Label lblFechaInicialYFinal;
		private DataDynamics.ActiveReports.PageFooter PageFooter;
		private DataDynamics.ActiveReports.GroupHeader GHMoneda;
		private DataDynamics.ActiveReports.Label lblIngresos;
		private DataDynamics.ActiveReports.Label lblEgresos;
		private DataDynamics.ActiveReports.Label lblSaldoInicial;
		private DataDynamics.ActiveReports.Label lblSaldoFinal;
		private DataDynamics.ActiveReports.TextBox txtNombreDeLaMoneda;
		private DataDynamics.ActiveReports.Label lblMoneda;
		private DataDynamics.ActiveReports.Label txtCuenta;
		private DataDynamics.ActiveReports.GroupFooter GFMoneda;
		private DataDynamics.ActiveReports.Label lblTotales;
		private DataDynamics.ActiveReports.TextBox txtTotalEgresos;
		private DataDynamics.ActiveReports.TextBox txtTotalSaldoFinal;
		private DataDynamics.ActiveReports.TextBox txtTotalSaldoInicial;
		private DataDynamics.ActiveReports.TextBox txtTotalIngresos;
	}
}
