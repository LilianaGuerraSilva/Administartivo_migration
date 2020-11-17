using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using LibGalac.Aos.UI.Cib;
using Galac.Adm.Ccl.CajaChica;


namespace Galac.Adm.Uil.CajaChica.Input
{
    /// <summary>
    /// Lógica de interacción para GSDetalleDeRendicion.xaml
    /// </summary>
    internal partial class GSDetalleDeRendicion : UserControl, INotifyPropertyChanged
    {
        #region Variables
        #region Constantes
        #endregion //Constantes
        static DependencyProperty _TotalFacturas;//= DependencyProperty.Register("TotalFactura", typeof(string), typeof(GSDetalleDeRendicion),new PropertyMetadata("",OnPropertyChanged);
        static DependencyProperty _TotalGravable;
        static DependencyProperty _TotalExento;
        static DependencyProperty _TotalIVA;

        public decimal TotalFacturas
        {
            get { return (decimal)GetValue(_TotalFacturas); }
            set
            {
                SetValue(_TotalFacturas, value);
                // TotalFacturaPropertyChanged(this, new DependencyPropertyChangedEventArgs());
            }
        }

        public decimal TotalGravable
        {
            get { return (decimal)GetValue(_TotalGravable); }
            set { SetValue(_TotalGravable, value); }
        }

        public decimal TotalExento
        {
            get { return (decimal)GetValue(_TotalExento); }
            set
            {
                SetValue(_TotalExento, value);
            }
        }

        public decimal TotalIVA
        {
            get { return (decimal)GetValue(_TotalIVA); }
            set
            {
                SetValue(_TotalIVA, value);
            }
        }




        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion //Eventos
        int _ConsecutivoCompania;
        internal LibXmlMemInfo _AppMemoryInfo;
        internal LibXmlMFC _Mfc;
        bool _ShowNew;
        bool _ShowLast;
        bool _EnabledDelete;
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        decimal Iva;
        Rendicion _CurrentInstance;
        ILibView _CurrentModel;

        #endregion //Variables
        #region Propiedades
        internal bool CancelValidations
        {
            get { return _CancelValidations; }
            set { _CancelValidations = value; }
        }
        internal eAccionSR Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        internal string ExtendedAction
        {
            get { return _ExtendedAction; }
            set { _ExtendedAction = value; }
        }
        internal string Title
        {
            get { return _Title; }
            private set { _Title = value; }
        }

        public bool ShowNew
        {
            get { return _ShowNew; }
            set
            {
                _ShowNew = value;
                OnPropertyChanged("ShowNew");
            }
        }

        public bool ShowLast
        {
            get { return _ShowLast; }
            set
            {
                _ShowLast = value;
                OnPropertyChanged("ShowLast");
            }
        }

        public bool EnabledDelete
        {
            get { return _EnabledDelete; }
            set
            {
                _EnabledDelete = value;
                OnPropertyChanged("EnabledDelete");
            }
        }

        public int ConsecutivoCompania
        {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }
        public LibXmlMemInfo AppMemoryInfo
        {
            get { return _AppMemoryInfo; }
            set { _AppMemoryInfo = value; }
        }
        public LibXmlMFC Mfc
        {
            get { return _Mfc; }
            set { _Mfc = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSDetalleDeRendicion()
        {
            InitializeComponent();
            InitializeEvents();
        }

        static GSDetalleDeRendicion()
        {
            FrameworkPropertyMetadata md = new FrameworkPropertyMetadata(new PropertyChangedCallback(TotalesPropertyChanged));
            _TotalFacturas = DependencyProperty.Register("TotalFacturas",
             typeof(decimal), typeof(GSDetalleDeRendicion), md);

            md = new FrameworkPropertyMetadata(new PropertyChangedCallback(TotalesPropertyChanged));
            _TotalExento = DependencyProperty.Register("TotalExento",
             typeof(decimal), typeof(GSDetalleDeRendicion), md);

            md = new FrameworkPropertyMetadata(new PropertyChangedCallback(TotalesPropertyChanged));
            _TotalGravable = DependencyProperty.Register("TotalGravable",
             typeof(decimal), typeof(GSDetalleDeRendicion), md);

            md = new FrameworkPropertyMetadata(new PropertyChangedCallback(TotalesPropertyChanged));
            _TotalIVA = DependencyProperty.Register("TotalIVA",
             typeof(decimal), typeof(GSDetalleDeRendicion), md);
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields()
        {
            return "Número del Documento, Número de Control, Fecha, Código del Proveedor, Monto Exento, Monto Gravable, Monto IVA";
        }

        internal void ClearControl()
        {
            LibApiAwp.ClearInputControls(gwMain.Children);
            dtpFecha.Date = DateTime.Today;
            dtpFecha.Text = DateTime.Today.ToShortDateString();
        }

        internal void DisableControl()
        {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        private static void TotalesPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }


        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction)
        {
            bool vResult = true;
            _Action = initAction;
            _CurrentInstance = (Rendicion)initInstance;
            _CurrentModel = initModel;
            _ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.ReImprimir, (int)eAccionSR.Anular, (int)eAccionSR.Cerrar, (int)eAccionSR.Contabilizar, (int)eAccionSR.Eliminar });
            //  grdDetalleDeRendicion.IsEnabled = false;

            if (grdDetalleDeRendicion.Items.Count > 0)
                LibListViewBound.GoToItem(grdDetalleDeRendicion, 0);

            if (Action == eAccionSR.Insertar)
            {
                SetFormValuesFromNavigator(true);
            }
            else
            {
                SetFormValuesFromNavigator(false);
            }
            dtpFecha.Date = DateTime.Today;
            SetLookAndFeelForCurrentRecord();
            ConsecutivoCompania = ((Rendicion)initInstance).ConsecutivoCompania;
            RealizaLosCalculos();
            actualizarIVA();


            System.Drawing.Icon icon = System.Drawing.SystemIcons.Exclamation;
            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            img1.Source = bs;

            icon = System.Drawing.SystemIcons.Asterisk;
            bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            img2.Source = bs;

            return vResult;
        }

        private void actualizarIVA()
        {
            DateTime dt = new DateTime();
            if (dtpFecha.Date.Equals(dt))
                return;
            else
                if (!DateTime.TryParse(dtpFecha.Text, out dt))
                {
                    return;
                }
            ILibBusinessComponentWithSearch<IList<AlicuotaIVA>, IList<AlicuotaIVA>> IvaNav = new Brl.CajaChica.clsAlicuotaIVANav();
            LibGpParams vParams = new LibGpParams();
            dtpFecha.UpdateLayout();
            vParams.AddInString("SQLWhere", "FechaDeInicioDeVigencia <= '" + dt.ToShortDateString() + "'", 50);
            vParams.AddInString("SQLOrderBy", "FechaDeInicioDeVigencia DESC", 50);
            vParams.AddInString("DateFormat", "DMY", 50);
            IList<AlicuotaIVA> lstIva = IvaNav.GetData(eProcessMessageType.SpName, "AlicuotaIVASCH", vParams.Get());
            if (lstIva.Count > 0)
                Iva = lstIva[0].MontoAlicuotaGeneral;
            else
                Iva = 12;
            Iva = Iva / 100;

            decimal montoIva = Decimal.Parse(calcularIva(txtMontoGravable.Value, Iva));

            if (txtMontoIVA.Value != 0 && txtMontoIVA.Value != montoIva)
            {
                lblMontoIVA.Content = "IVA(" + "Manual" +  ")";
                //txtMontoIVA.Value = Decimal.Parse(calcularIva(txtMontoGravable.Value, Iva));
            }
            else {
                lblMontoIVA.Content = "IVA(" + Iva.ToString("P") + ")";
                txtMontoIVA.Value = montoIva;
            }
        }

        private void SetLookAndFeelForCurrentRecord()
        {
            if (Action != eAccionSR.Insertar)
            {
            }
        }

        internal void SetNavigatorValuesFromForm()
        {
            //_CurrentInstance.NumeroDocumento = txtNumeroDocumento.Text;
            //_CurrentInstance.NumeroControl = txtNumeroControl.Text;
            //_CurrentInstance.Fecha = dtpFecha.Date;
            //_CurrentInstance.CodigoProveedor = txtCodigoProveedor.Text;
            //_CurrentInstance.MontoExento = txtMontoExento.Value;
            //_CurrentInstance.MontoGravable = txtMontoGravable.Value;
            //_CurrentInstance.MontoIVA = txtMontoIVA.Value;
            //_CurrentInstance.AplicaParaLibroDeComprasAsBool = chkAplicaParaLibroDeCompras.IsChecked.Value;
            //_CurrentInstance.ObservacionesCxP = txtObservacionesCxP.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord)
        {
            if (valClearRecord)
            {
                //_CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();

            //txtNumeroDocumento.Text = _CurrentInstance.NumeroDocumento;
            //txtNumeroControl.Text = _CurrentInstance.NumeroControl;
            //dtpFecha.Date = _CurrentInstance.Fecha;
            //txtCodigoProveedor.Text = _CurrentInstance.CodigoProveedor;
            //txtMontoExento.Value = _CurrentInstance.MontoExento;
            //txtMontoGravable.Value = _CurrentInstance.MontoGravable;
            //txtMontoIVA.Value = _CurrentInstance.MontoIVA;
            //chkAplicaParaLibroDeCompras.IsChecked = _CurrentInstance.AplicaParaLibroDeComprasAsBool;
            //txtObservacionesCxP.Text = _CurrentInstance.ObservacionesCxP;
            RealizaLosCalculos();
        }

        private void InitializeEvents()
        {
            this.txtNumeroDocumento.Validating += new System.ComponentModel.CancelEventHandler(txtNumeroDocumento_Validating);
            this.txtNumeroControl.Validating += new System.ComponentModel.CancelEventHandler(txtNumeroControl_Validating);
            this.dtpFecha.Validating += new EventHandler<LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs>(dtpFecha_Validating);
            this.txtCodigoProveedor.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoProveedor_Validating);
            this.txtMontoExento.Validating += new System.ComponentModel.CancelEventHandler(txtMontoExento_Validating);
            this.txtMontoGravable.Validating += new System.ComponentModel.CancelEventHandler(txtMontoGravable_Validating);
            this.txtMontoIVA.Validating += new System.ComponentModel.CancelEventHandler(txtMontoIVA_Validating);
            //this.chkAplicaParaLibroDeCompras.Validating += new System.ComponentModel.CancelEventHandler(chkAplicaParaLibroDeCompras_Validating);
            this.btnAddItem.Click += new RoutedEventHandler(btnAddItem_Click);
            this.btnNewItem.Click += new RoutedEventHandler(btnNewItem_Click);
            this.grdDetalleDeRendicion.PreviewKeyDown += new KeyEventHandler(grdDetalleDeRendicion_PreviewKeyDown);
            this.grdDetalleDeRendicion.Items.CurrentChanged += new EventHandler(Items_CurrentChanged);

        }

        private void cc(object o, System.Collections.Specialized.NotifyCollectionChangedEventArgs arg)
        {
            RealizaLosCalculos();
        }

        void txtNumeroDocumento_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string msj;
            try
            {
                if (CancelValidations)
                {
                    if (!Action.Equals(eAccionSR.Cerrar))
                        return;
                    else
                        if (!((ObservableCollection<DetalleDeRendicion>)DataContext).Any<DetalleDeRendicion>(d => d.ValidoAsBool == false))
                            return;
                }

               
                clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(AppMemoryInfo, Mfc);
                if (!insDetalleDeRendicionIpl.IsValidNumeroDocumento(Action, txtNumeroDocumento.Text, true))
                {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (txtCodigoProveedor.Text != string.Empty)
                    {
                        if (!validaDetalles(sender, insDetalleDeRendicionIpl, out msj))
                        {
                            //if(!Action.Equals(eAccionSR.Cerrar))
                            //   e.Cancel = true;
                        }
                    }
                }
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private bool validaDetalles(object sender, clsDetalleDeRendicionIpl insDetalleDeRendicionIpl, out string msj)
        {
            msj = "";
            bool result = true;
            //if (txtCodigoProveedor.Text != string.Empty)
            //{

                 foreach (DetalleDeRendicion detalle in ((ObservableCollection<DetalleDeRendicion>)DataContext))
                 {
                    detalle.ValidoAsBool = true;
                  }

                if (ExistenDetallesRepetidosEnGrid())
                {
                    //LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), "No es posible Insertar Documentos Repetidos.", _CurrentModel.MessageName);
                    result &= false;
                }
                if (_Action.Equals(eAccionSR.Cerrar))
                {

                    DetalleDeRendicion registro = ((ObservableCollection<DetalleDeRendicion>)DataContext).Where<DetalleDeRendicion>(d => d.NumeroDocumento.Equals(txtNumeroDocumento.Text)
                                                                                                        && d.CodigoProveedor.Equals(txtCodigoProveedor.Text)).ToList<DetalleDeRendicion>()[0];
                    registro.ConsecutivoCompania = _CurrentInstance.ConsecutivoCompania;
                    if (!insDetalleDeRendicionIpl.IsValidNroDocumentoCodigoProveedorKey(registro))
                    {
                        LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
                        registro.ValidoAsBool = false;
                        result &= false;
                    }
                    //if(!
                    if (!((clsReposicionIpl)_CurrentModel).ValidateAll(_CurrentInstance, Action, out msj)) {
                        result &= false;
                    }

                    //){
                    //LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), msj, _CurrentModel.MessageName);
                    //}

                //}
            }

            return result;
            
        }

        void txtNumeroControl_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (CancelValidations)
                {
                    return;
                }
                clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(AppMemoryInfo, Mfc);
                if (!insDetalleDeRendicionIpl.IsValidNumeroControl(Action, txtNumeroControl.Text, true))
                {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
                    e.Cancel = true;
                }
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void dtpFecha_Validating(object sender, LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs e)
        {
            string msj;
            try
            {

                if (CancelValidations)
                    if (!Action.Equals(eAccionSR.Cerrar))
                        return;
                    else
                        if (!((ObservableCollection<DetalleDeRendicion>)DataContext).Any<DetalleDeRendicion>(d => d.ValidoAsBool == false))
                            return;

                
                clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(AppMemoryInfo, Mfc);
                if (!insDetalleDeRendicionIpl.IsValidFecha(Action, dtpFecha.Date, true))
                {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
                    e.Cancel = true;
                }else {

                   if (txtCodigoProveedor.Text != string.Empty && txtNumeroDocumento.Text != string.Empty)
                        if (!validaDetalles(sender, insDetalleDeRendicionIpl, out msj))
                        {
                            //e.Cancel = true;
                        }

                }

            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCodigoProveedor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string msj;
            try
            {
                if (CancelValidations)
                {
                    if (!Action.Equals(eAccionSR.Cerrar))
                        return;
                    else
                        if (!((ObservableCollection<DetalleDeRendicion>)DataContext).Any<DetalleDeRendicion>(d => d.ValidoAsBool == false))
                            return;
                }
                if (LibString.Len(txtCodigoProveedor.Text) == 0)
                {
                    txtCodigoProveedor.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                DateTime vFechaHoy = LibDate.Today();
                DateTime vFechaInicioVigencia;
                Galac.Comun.Ccl.TablasLey.ITablaRetencionPdn insTablaRetencion = new Galac.Comun.Brl.TablasLey.clsTablaRetencionNav();
                vFechaInicioVigencia = insTablaRetencion.BuscaFechaDeInicioDeVigencia(vFechaHoy);
                
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_Proveedor_B1.CodigoProveedor=" + txtCodigoProveedor.Text + LibText.ColumnSeparator();
                vParamsInitializationList = vParamsInitializationList + "Adm.Gv_Proveedor_B1.FechaDeInicioDeVigencia=" + vFechaInicioVigencia.ToShortDateString() + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Adm.Gv_Proveedor_B1.ConsecutivoCompania=" + _ConsecutivoCompania;
                
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();

                if (Galac.Adm.Uil.CajaChica.clsRendicionList.ChooseProveedor(null, ref XmlProperties, vSearchValues, vFixedValues))
                {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoProveedor.Text = insParse.GetString(0, "CodigoProveedor", "");
                    txtNombreProveedor.Text = insParse.GetString(0, "NombreProveedor", "");
                }
                else
                {
                    e.Cancel = true;
                }

                clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(AppMemoryInfo, Mfc);
                if (txtNumeroDocumento.Text != string.Empty)
                {


                     if (!validaDetalles(sender, insDetalleDeRendicionIpl, out msj))
                        {
                            //e.Cancel = true;
                        }

                    //if (ExistenDetallesRepetidosEnGrid())
                    //{
                    //    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), "No es posible Insertar Documentos Repetidos.", _CurrentModel.MessageName);
                    //    e.Cancel = true;
                    //    return;
                    //}

                    //if (_Action.Equals(eAccionSR.Cerrar))
                    //{
                    //    DetalleDeRendicion registro = ((ObservableCollection<DetalleDeRendicion>)DataContext).Where<DetalleDeRendicion>(d => d.NumeroDocumento.Equals(txtNumeroDocumento.Text)
                    //                                                                                        && d.CodigoProveedor.Equals(txtCodigoProveedor.Text)).ToList<DetalleDeRendicion>()[0];
                    //    registro.ConsecutivoCompania = _CurrentInstance.ConsecutivoCompania;
                    //    if (!insDetalleDeRendicionIpl.IsValidNroDocumentoCodigoProveedorKey(registro))
                    //    {
                    //        LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
                    //        e.Cancel = true;
                    //    }
                    //    else
                    //    {
                    //        registro.ValidoAsBool = true;
                    //    }

                    }
                }

            
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtMontoExento_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (CancelValidations)
                {
                    return;
                }
                clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(AppMemoryInfo, Mfc);
                if (!insDetalleDeRendicionIpl.IsValidMontoExento(Action, LibConvert.ToDec(txtMontoExento.Value, 2), true))
                {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
                    e.Cancel = true;
                }
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            RealizaLosCalculos();
            PropertyChanged(this, new PropertyChangedEventArgs("MontoExento"));
        }

        void txtMontoGravable_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (CancelValidations)
                {
                    return;
                }


                clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(AppMemoryInfo, Mfc);
                if (!insDetalleDeRendicionIpl.IsValidMontoGravable(Action, LibConvert.ToDec(txtMontoGravable.Value, 2), true))
                {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
                    e.Cancel = true;
                }
                else
                {

                    if (txtMontoExento.Value == 0)
                    {
                        if (txtMontoGravable.Value == 0)
                        {
                            LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), "No es posible registra un Factura con monto 0,00.", _CurrentModel.MessageName);
                            e.Cancel = true;
                        }
                    }
                }



            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            RealizaLosCalculos();
            txtMontoIVA.Value = Decimal.Parse(calcularIva(txtMontoGravable.Value, Iva));
            lblMontoIVA.Content = "IVA(" + Iva.ToString("P") + ")";
            PropertyChanged(this, new PropertyChangedEventArgs("MontoGravable"));
        }

        void txtMontoIVA_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (CancelValidations)
                {
                    return;
                }
                clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(AppMemoryInfo, Mfc);
                if (!insDetalleDeRendicionIpl.IsValidMontoIVA(Action, LibConvert.ToDec(txtMontoIVA.Value, 2), true))
                {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
                    e.Cancel = true;
                }
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            RealizaLosCalculos();
            PropertyChanged(this, new PropertyChangedEventArgs("MontoIva"));

        }

        void chkAplicaParaLibroDeCompras_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(((clsDetalleDeRendicionIpl)CurrentModel).AppMemoryInfo, ((clsDetalleDeRendicionIpl)CurrentModel).Mfc);
            //    if (!insDetalleDeRendicionIpl.IsValidAplicaParaLibroDeCompras(Action, chkAplicaParaLibroDeCompras.IsChecked, true)) {
            //        LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insDetalleDeRendicionIpl.Information.ToString(), _CurrentModel.MessageName);
            //        e.Cancel = true;
            //    }
            //} catch (GalacException gEx) {
            //    LibExceptionDisplay.Show(gEx, this.Title);
            //} catch (Exception vEx) {
            //    if (vEx is System.AccessViolationException) {
            //        throw;
            //    }
            //    LibExceptionDisplay.Show(vEx);
            //}
        }

        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        void Items_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                ShowLast = !(grdDetalleDeRendicion.SelectedIndex == ((ObservableCollection<DetalleDeRendicion>)DataContext).Count - 1);
                ShowNew = ((_Action == eAccionSR.Insertar || _Action == eAccionSR.Modificar));
                RealizaLosCalculos();
                actualizarIVA();
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void DeleteRow(object sender, RoutedEventArgs e)
        {
            try
            {
                DetalleDeRendicion vDetailTmp = ((sender as Control).Tag as DetalleDeRendicion);
                ObservableCollection<DetalleDeRendicion> vCollection = DataContext as ObservableCollection<DetalleDeRendicion>;
                LibListViewBound.DeleteRow<DetalleDeRendicion>(vDetailTmp, vCollection, grdDetalleDeRendicion);
                // txtNumeroDocumento.Focus();
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<DetalleDeRendicion> vCollection = DataContext as ObservableCollection<DetalleDeRendicion>;

                if (vCollection.Count == 0)
                    addItem(new DetalleDeRendicion(), grdDetalleDeRendicion);
                else
                    LibListViewBound.Add<DetalleDeRendicion>(vCollection, grdDetalleDeRendicion, "ConsecutivoRenglon");

                txtNumeroDocumento.Focus();
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void btnNewItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LibListViewBound.GoToItem(grdDetalleDeRendicion, grdDetalleDeRendicion.Items.Count - 1);
                txtNumeroDocumento.Focus();
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void grdDetalleDeRendicion_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Key == Key.Enter)
                {
                    txtNumeroDocumento.Focus();
                }
                else if (e.Key == Key.Delete)
                {
                    ListView aux = grdDetalleDeRendicion;
                    if (aux.SelectedItems.Count > 0)
                        borrarItem(aux);
                    //else
                    //    DeleteRow(sender, e);
                }
            }
            catch (GalacException gEx)
            {
                LibExceptionDisplay.Show(gEx, this.Title);
            }
            catch (Exception vEx)
            {
                if (vEx is System.AccessViolationException)
                {
                    throw;
                }
            }

        }

        private void borrarItem(ListView aux)
        {
            //if (aux.SelectedItems.Count > 0) {
            //  var result = LibNotifier.AcceptCancelMessage(null, "Desear borrar los Registros seleccionados?", "Adverencia", "Borrar Registros", TextAlignment.Center);
            var result = true;
            if (result)
            {
                var arr = ((System.Collections.IList)aux.SelectedItems).Cast<DetalleDeRendicion>().ToList();
                foreach (DetalleDeRendicion ll in arr)
                {
                 //   ((ICollection<DetalleDeRendicion>)aux.ItemsSource).Remove(ll);

                    DeleteRow(new Button() { Tag = ll }, new RoutedEventArgs());
                }
                if (((ICollection<DetalleDeRendicion>)aux.ItemsSource).Count == 0)
                {
                    ((ICollection<DetalleDeRendicion>)aux.ItemsSource).Add(new DetalleDeRendicion());
                    aux.SelectedIndex = 0;
                }

            }
            //}
        }



        private void RealizaLosCalculos()
        {
            txtTotalFacturas.Value = calculaTotalFacturas();
            TotalFacturas = calculaTotalFacturas();
            TotalExento = calculaTotalExento();
            TotalGravable = calculaTotalGravable();
            TotalIVA = calculaTotalIVA();
            PropertyChanged(this, new PropertyChangedEventArgs("TotalFacturas"));
        }


        private decimal calculaTotalFacturas()
        {
            return ((ICollection<DetalleDeRendicion>)DataContext).Cast<DetalleDeRendicion>().Sum(d => d.MontoGravable + d.MontoExento + d.MontoIVA);
        }
        private decimal calculaTotalGravable()
        {
            return ((ICollection<DetalleDeRendicion>)DataContext).Cast<DetalleDeRendicion>().Sum(d => d.MontoGravable);
        }
        private decimal calculaTotalExento()
        {
            return ((ICollection<DetalleDeRendicion>)DataContext).Cast<DetalleDeRendicion>().Sum(d => d.MontoExento);
        }
        private decimal calculaTotalIVA()
        {
            return ((ICollection<DetalleDeRendicion>)DataContext).Cast<DetalleDeRendicion>().Sum(d => d.MontoIVA);
        }
        #endregion //Metodos Generados

        private void AddItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DetalleDeRendicion detalle = new DetalleDeRendicion();
            addItem(detalle, grdDetalleDeRendicion);
            grdDetalleDeRendicion.SelectedItem = detalle;
            txtNumeroDocumento.Focus();
            RealizaLosCalculos();
        }

        private void addItem(DetalleDeRendicion detalle, ListView aux)
        {
            detalle.AplicaParaLibroDeCompras = (chkAplicaParaLibroDeCompras.IsChecked.Value ? "Si" : "No");
            detalle.AplicaParaLibroDeComprasAsBool = chkAplicaParaLibroDeCompras.IsChecked.Value;
            detalle.CodigoProveedor = txtCodigoProveedor.Text;
            detalle.ConsecutivoCompania = 1;
            detalle.ConsecutivoRendicion = 1;
            detalle.ConsecutivoRenglon = 1;
            detalle.Fecha = dtpFecha.Date;
            detalle.MontoExento = txtMontoExento.Value;
            detalle.MontoGravable = txtMontoGravable.Value;
            detalle.MontoIVA = txtMontoIVA.Value;
            detalle.NumeroControl = txtNumeroControl.Text;
            detalle.NumeroDocumento = txtNumeroDocumento.Text;
            detalle.ObservacionesCxP = txtObservacionesCxP.Text;
            ((ICollection<DetalleDeRendicion>)aux.ItemsSource).Add(detalle);
        }

        private void newItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListView aux = grdDetalleDeRendicion;
            ((ICollection<DetalleDeRendicion>)aux.ItemsSource).Add(new DetalleDeRendicion());
            txtNumeroDocumento.Focus();
            RealizaLosCalculos();
        }

        private void txtTotalFacturas_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void dtpFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            actualizarIVA();
        }

        private string calcularIva(decimal montoGravable, decimal iva)
        {
            return ((iva) * montoGravable).ToString();
        }

        private void txtMontoGravable_TextChanged(object sender, TextChangedEventArgs e)
        {
            calcularIva(txtMontoGravable.Value, Iva);
        }


        private void dtpFecha_Validating_1(object sender, LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs e)
        {
            actualizarIVA();
        }



        private bool ExistenDetallesRepetidosEnGrid()
        {
                         
            foreach (DetalleDeRendicion detalle in ((ObservableCollection<DetalleDeRendicion>)DataContext))
            {
                int total = ((ObservableCollection<DetalleDeRendicion>)DataContext).Where<DetalleDeRendicion>(de => de.NumeroDocumento == detalle.NumeroDocumento && de.CodigoProveedor == detalle.CodigoProveedor).Count();
                if (total > 1)
                {
                    detalle.ValidoAsBool = false;
                    detalle.ErrorMsj = "Este Registro se encuentra Repetido.";
                }
                else
                {
                    //detalle.ValidoAsBool = true;
                }
            }

            int invalidos = ((ObservableCollection<DetalleDeRendicion>)DataContext).Where<DetalleDeRendicion>(de => de.ValidoAsBool == false).Count();
            if (invalidos > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //  actualizarIVA();
        }

        private void btnAddItem_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void dtpFecha_TextInput(object sender, TextCompositionEventArgs e)
        {
            DateTime result;
            if (DateTime.TryParse(dtpFecha.Text, out result))
            {
                dtpFecha.Date = result;
            }
        }

    } //End of class GSDetalleDeRendicion.xaml

    public class ColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Brush result;
            if (((bool)value))
            {
                result = new SolidColorBrush(Colors.Black);
            }
            else
            {
                result = new SolidColorBrush(Colors.Red);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiSumConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal d;
            d = values.Sum<object>(e => Decimal.TryParse(e.ToString(), out d) ? Decimal.Parse(e.ToString()) : Decimal.Zero);
            return d.ToString("#,#0.00");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FontWeightsConverter : IValueConverter { 

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (((bool)value))
            {
                return FontWeights.Normal;
            }
            else
            {
                return FontWeights.Black;
            }
    
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class DateConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((DateTime)value).ToShortDateString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result))
                return result;
            return null;

        }
    }

    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (((bool)value)) { 
            return Visibility.Hidden;
            }else{
                return Visibility.Visible;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    
    


} //End of namespace Galac.Adm.Uil.CajaChica

