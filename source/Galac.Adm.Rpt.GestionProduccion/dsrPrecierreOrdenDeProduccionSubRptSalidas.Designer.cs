namespace Galac.Adm.Rpt.GestionProduccion
{
    /// <summary>
    /// Summary description for dsrOrdenDeProduccionSubRptSalidas.
    /// </summary>
    partial class dsrPrecierreOrdenDeProduccionSubRptSalidas
    {
        private DataDynamics.ActiveReports.Detail detail;

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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dsrPrecierreOrdenDeProduccionSubRptSalidas));
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.GHSecDetalle = new DataDynamics.ActiveReports.GroupHeader();
            this.GFSecDetalle = new DataDynamics.ActiveReports.GroupFooter();
            this.lblArticulo = new DataDynamics.ActiveReports.Label();
            this.lblCantidadEstimadaAProducir = new DataDynamics.ActiveReports.Label();
            this.lblCantidadProducidaReal = new DataDynamics.ActiveReports.Label();
            this.lblUnidad = new DataDynamics.ActiveReports.Label();
            this.lnInsumos = new DataDynamics.ActiveReports.Line();
            this.txtArticulo = new DataDynamics.ActiveReports.TextBox();
            this.txtCantidadReservadaDeInventario = new DataDynamics.ActiveReports.TextBox();
            this.txtCantidadConsumoReal = new DataDynamics.ActiveReports.TextBox();
            this.txtUnidad = new DataDynamics.ActiveReports.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.lblArticulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCantidadEstimadaAProducir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCantidadProducidaReal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUnidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArticulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadReservadaDeInventario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadConsumoReal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.txtArticulo,
            this.txtCantidadReservadaDeInventario,
            this.txtCantidadConsumoReal,
            this.txtUnidad});
            this.detail.Height = 0.29125F;
            this.detail.Name = "detail";
            // 
            // GHSecDetalle
            // 
            this.GHSecDetalle.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.lblArticulo,
            this.lblCantidadEstimadaAProducir,
            this.lblCantidadProducidaReal,
            this.lblUnidad,
            this.lnInsumos});
            this.GHSecDetalle.Height = 0.1639444F;
            this.GHSecDetalle.Name = "GHSecDetalle";
            // 
            // GFSecDetalle
            // 
            this.GFSecDetalle.Height = 0.25F;
            this.GFSecDetalle.Name = "GFSecDetalle";
            // 
            // lblArticulo
            // 
            this.lblArticulo.Height = 0.15625F;
            this.lblArticulo.HyperLink = null;
            this.lblArticulo.Left = 0F;
            this.lblArticulo.Name = "lblArticulo";
            this.lblArticulo.Style = "font-size: 8pt; font-weight: bold; vertical-align: bottom; ddo-char-set: 1";
            this.lblArticulo.Text = "Artículo";
            this.lblArticulo.Top = 0F;
            this.lblArticulo.Width = 3.437F;
            // 
            // lblCantidadEstimadaAProducir
            // 
            this.lblCantidadEstimadaAProducir.Height = 0.15625F;
            this.lblCantidadEstimadaAProducir.HyperLink = null;
            this.lblCantidadEstimadaAProducir.Left = 4.469F;
            this.lblCantidadEstimadaAProducir.Name = "lblCantidadEstimadaAProducir";
            this.lblCantidadEstimadaAProducir.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: bottom; ddo" +
    "-char-set: 1";
            this.lblCantidadEstimadaAProducir.Text = "Cantidad a Producir";
            this.lblCantidadEstimadaAProducir.Top = 0F;
            this.lblCantidadEstimadaAProducir.Width = 1.5F;
            // 
            // lblCantidadProducidaReal
            // 
            this.lblCantidadProducidaReal.Height = 0.15625F;
            this.lblCantidadProducidaReal.HyperLink = null;
            this.lblCantidadProducidaReal.Left = 6F;
            this.lblCantidadProducidaReal.Name = "lblCantidadProducidaReal";
            this.lblCantidadProducidaReal.Style = "font-size: 8pt; font-weight: bold; text-align: right; vertical-align: bottom; ddo" +
    "-char-set: 1";
            this.lblCantidadProducidaReal.Text = "Cantidad Producida Real";
            this.lblCantidadProducidaReal.Top = 0F;
            this.lblCantidadProducidaReal.Width = 1.5F;
            // 
            // lblUnidad
            // 
            this.lblUnidad.Height = 0.15625F;
            this.lblUnidad.HyperLink = null;
            this.lblUnidad.Left = 3.437F;
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Style = "font-size: 8pt; font-weight: bold; vertical-align: bottom; ddo-char-set: 1";
            this.lblUnidad.Text = "Unidad";
            this.lblUnidad.Top = 0F;
            this.lblUnidad.Width = 1F;
            // 
            // lnInsumos
            // 
            this.lnInsumos.Height = 0F;
            this.lnInsumos.Left = 3.72529E-09F;
            this.lnInsumos.LineWeight = 1F;
            this.lnInsumos.Name = "lnInsumos";
            this.lnInsumos.Top = 0.1569999F;
            this.lnInsumos.Width = 7.5F;
            this.lnInsumos.X1 = 3.72529E-09F;
            this.lnInsumos.X2 = 7.5F;
            this.lnInsumos.Y1 = 0.1569999F;
            this.lnInsumos.Y2 = 0.1569999F;
            // 
            // txtArticulo
            // 
            this.txtArticulo.Height = 0.15625F;
            this.txtArticulo.Left = 0F;
            this.txtArticulo.Name = "txtArticulo";
            this.txtArticulo.Style = "font-size: 8pt; vertical-align: middle; ddo-char-set: 1";
            this.txtArticulo.Text = "txtArticulo";
            this.txtArticulo.Top = 0F;
            this.txtArticulo.Width = 3.437F;
            // 
            // txtCantidadReservadaDeInventario
            // 
            this.txtCantidadReservadaDeInventario.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCantidadReservadaDeInventario.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCantidadReservadaDeInventario.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCantidadReservadaDeInventario.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCantidadReservadaDeInventario.Height = 0.25F;
            this.txtCantidadReservadaDeInventario.Left = 4.469F;
            this.txtCantidadReservadaDeInventario.Name = "txtCantidadReservadaDeInventario";
            this.txtCantidadReservadaDeInventario.Style = "font-size: 8pt; text-align: right; vertical-align: middle; ddo-char-set: 1";
            this.txtCantidadReservadaDeInventario.Text = "txtCantidadReservadaDeInventario";
            this.txtCantidadReservadaDeInventario.Top = 0.01F;
            this.txtCantidadReservadaDeInventario.Width = 1.5F;
            // 
            // txtCantidadConsumoReal
            // 
            this.txtCantidadConsumoReal.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCantidadConsumoReal.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCantidadConsumoReal.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCantidadConsumoReal.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid;
            this.txtCantidadConsumoReal.Height = 0.25F;
            this.txtCantidadConsumoReal.Left = 6F;
            this.txtCantidadConsumoReal.Name = "txtCantidadConsumoReal";
            this.txtCantidadConsumoReal.Style = "font-size: 8pt; text-align: right; vertical-align: middle; ddo-char-set: 1";
            this.txtCantidadConsumoReal.Text = null;
            this.txtCantidadConsumoReal.Top = 0.01F;
            this.txtCantidadConsumoReal.Width = 1.5F;
            // 
            // txtUnidad
            // 
            this.txtUnidad.CanGrow = false;
            this.txtUnidad.Height = 0.15625F;
            this.txtUnidad.Left = 3.437F;
            this.txtUnidad.Name = "txtUnidad";
            this.txtUnidad.Style = "font-size: 8pt; vertical-align: middle; ddo-char-set: 1";
            this.txtUnidad.Text = "txtUnidad";
            this.txtUnidad.Top = 0F;
            this.txtUnidad.Width = 1F;
            // 
            // dsrOrdenDeProduccionSubRptSalidas
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.5F;
            this.Sections.Add(this.GHSecDetalle);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.GFSecDetalle);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.lblArticulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCantidadEstimadaAProducir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCantidadProducidaReal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUnidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArticulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadReservadaDeInventario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadConsumoReal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.GroupHeader GHSecDetalle;
        private DataDynamics.ActiveReports.GroupFooter GFSecDetalle;
        private DataDynamics.ActiveReports.Label lblArticulo;
        private DataDynamics.ActiveReports.Label lblCantidadEstimadaAProducir;
        private DataDynamics.ActiveReports.Label lblCantidadProducidaReal;
        private DataDynamics.ActiveReports.Label lblUnidad;
        private DataDynamics.ActiveReports.Line lnInsumos;
        private DataDynamics.ActiveReports.TextBox txtArticulo;
        private DataDynamics.ActiveReports.TextBox txtCantidadReservadaDeInventario;
        private DataDynamics.ActiveReports.TextBox txtCantidadConsumoReal;
        private DataDynamics.ActiveReports.TextBox txtUnidad;
    }
}
