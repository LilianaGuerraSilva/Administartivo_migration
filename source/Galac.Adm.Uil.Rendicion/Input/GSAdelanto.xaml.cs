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

namespace Galac.Adm.Uil.CajaChica.Input {
    /// <summary>
    /// Lógica de interacción para GSDetalleDeRendicion.xaml
    /// </summary>
    internal partial class GSAdelantos : UserControl, INotifyPropertyChanged {
        #region Variables
        

        #region Constantes
        #endregion //Constantes

      
    
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
        #endregion //Variables
        #region Propiedades

        internal bool CancelValidations {
            get { return _CancelValidations; }
            set { _CancelValidations = value; }
        }
        internal eAccionSR Action {
            get { return _Action; }
            set { _Action = value; }
        }
        internal string ExtendedAction {
            get { return _ExtendedAction; }
            set { _ExtendedAction = value; }
        }
        internal string Title {
            get { return _Title; }
            private set { _Title = value; }
        }

        public bool ShowNew {
            get { return _ShowNew; }
            set {
                _ShowNew = value;
                OnPropertyChanged("ShowNew");
            }
        }

        public bool ShowLast {
            get { return _ShowLast; }
            set {
                _ShowLast = value;
                OnPropertyChanged("ShowLast");
            }
        }

        public bool EnabledDelete {
            get { return _EnabledDelete; }
            set {
                _EnabledDelete = value;
                OnPropertyChanged("EnabledDelete");
            }
        }

        public int ConsecutivoCompania {
            get { return _ConsecutivoCompania; }
            set { _ConsecutivoCompania = value; }
        }
        public LibXmlMemInfo AppMemoryInfo {
            get { return _AppMemoryInfo; }
            set { _AppMemoryInfo = value; }
        }
        public LibXmlMFC Mfc {
            get { return _Mfc; }
            set { _Mfc = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSAdelantos() {
            InitializeComponent();
            InitializeEvents();
        }

        static GSAdelantos() {
            FrameworkPropertyMetadata md = new FrameworkPropertyMetadata(new PropertyChangedCallback(TotalFacturaPropertyChanged));
            _TotalFacturas = DependencyProperty.Register("TotalFacturas",
             typeof(decimal), typeof(GSAdelantos), md);


        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Número del Documento, Número de Control, Fecha, Código del Proveedor, Monto Exento, Monto Gravable, Monto IVA";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        private static void TotalFacturaPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {

        }


        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _Action = initAction;
            _ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            //  grdDetalleDeRendicion.IsEnabled = false;

            if (grdDetalleDeRendicion.Items.Count > 0)
                LibListViewBound.GoToItem(grdDetalleDeRendicion, 0);

            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            SetLookAndFeelForCurrentRecord();
            RealizaLosCalculos();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
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

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
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

        private void InitializeEvents() {
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

        private void cc(object o, System.Collections.Specialized.NotifyCollectionChangedEventArgs arg) {
            RealizaLosCalculos();
        }

        void txtNumeroDocumento_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(((clsDetalleDeRendicionIpl)CurrentModel).AppMemoryInfo, ((clsDetalleDeRendicionIpl)CurrentModel).Mfc);
            //    if (!insDetalleDeRendicionIpl.IsValidNumeroDocumento(Action, txtNumeroDocumento.Text, true)) {
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

        void txtNumeroControl_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(((clsDetalleDeRendicionIpl)CurrentModel).AppMemoryInfo, ((clsDetalleDeRendicionIpl)CurrentModel).Mfc);
            //    if (!insDetalleDeRendicionIpl.IsValidNumeroControl(Action, txtNumeroControl.Text, true)) {
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

        void dtpFecha_Validating(object sender, LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(((clsDetalleDeRendicionIpl)CurrentModel).AppMemoryInfo, ((clsDetalleDeRendicionIpl)CurrentModel).Mfc);
            //    if (!insDetalleDeRendicionIpl.IsValidFecha(Action, dtpFecha.Date, true)) {
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

        void txtCodigoProveedor_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    if (LibString.Len(txtCodigoProveedor.Text)==0) {
            //        txtCodigoProveedor.Text = "*";
            //    }
            //    string vParamsInitializationList;
            //    string vParamsFixedList = "";
            //    LibSearch insLibSearch = new LibSearch();
            //    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            //    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            //    vParamsInitializationList = "Gv_Proveedor_B1.codigoProveedor=" + txtCodigoProveedor.Text + LibText.ColumnSeparator();
            //    vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
            //    vParamsFixedList = "Gv_Proveedor_B1.ConsecutivoCompania=" + _CurrentInstance.ConsecutivoCompania;
            //    vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
            //    XmlDocument XmlProperties = new XmlDocument();
            //    if (Galac.Adm.Uil.CajaChica.clsDetalleDeRendicionList.ChooseProveedor(null , ref XmlProperties, vSearchValues, vFixedValues)) {
            //        LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
            //        txtCodigoProveedor.Text = insParse.GetString(0, "codigoProveedor", "");
            //    }else{
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

        void txtMontoExento_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(((clsDetalleDeRendicionIpl)CurrentModel).AppMemoryInfo, ((clsDetalleDeRendicionIpl)CurrentModel).Mfc);
            //    if (!insDetalleDeRendicionIpl.IsValidMontoExento(Action, LibConvert.ToDec(txtMontoExento.Value, 2), true)) {
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
            RealizaLosCalculos();
            PropertyChanged(this, new PropertyChangedEventArgs("MontoExento"));
        }

        void txtMontoGravable_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(((clsDetalleDeRendicionIpl)CurrentModel).AppMemoryInfo, ((clsDetalleDeRendicionIpl)CurrentModel).Mfc);
            //    if (!insDetalleDeRendicionIpl.IsValidMontoGravable(Action, LibConvert.ToDec(txtMontoGravable.Value, 2), true)) {
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
            RealizaLosCalculos();
            PropertyChanged(this, new PropertyChangedEventArgs("MontoGravable"));

        }

        void txtMontoIVA_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsDetalleDeRendicionIpl insDetalleDeRendicionIpl = new clsDetalleDeRendicionIpl(((clsDetalleDeRendicionIpl)CurrentModel).AppMemoryInfo, ((clsDetalleDeRendicionIpl)CurrentModel).Mfc);
            //    if (!insDetalleDeRendicionIpl.IsValidMontoIVA(Action, LibConvert.ToDec(txtMontoIVA.Value, 2), true)) {
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
            RealizaLosCalculos();
            PropertyChanged(this, new PropertyChangedEventArgs("MontoIva"));

        }

        void chkAplicaParaLibroDeCompras_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
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

        private void OnPropertyChanged(string info) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        void Items_CurrentChanged(object sender, EventArgs e) {
            try {
                ShowLast = !(grdDetalleDeRendicion.SelectedIndex == ((ObservableCollection<DetalleDeRendicion>)DataContext).Count - 1);
                ShowNew = ((_Action == eAccionSR.Insertar || _Action == eAccionSR.Modificar));
                RealizaLosCalculos();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void DeleteRow(object sender, RoutedEventArgs e) {
            try {
                DetalleDeRendicion vDetailTmp = ((sender as Button).Tag as DetalleDeRendicion);
                ObservableCollection<DetalleDeRendicion> vCollection = DataContext as ObservableCollection<DetalleDeRendicion>;

                if (vCollection.Count == 1)
                    borrarItem(grdDetalleDeRendicion);
                else
                    LibListViewBound.DeleteRow<DetalleDeRendicion>(vDetailTmp, vCollection, grdDetalleDeRendicion);
                txtNumeroDocumento.Focus();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void btnAddItem_Click(object sender, RoutedEventArgs e) {
            try {
                ObservableCollection<DetalleDeRendicion> vCollection = DataContext as ObservableCollection<DetalleDeRendicion>;

                if (vCollection.Count == 0)
                    addItem(new DetalleDeRendicion(), grdDetalleDeRendicion);
                else
                    LibListViewBound.Add<DetalleDeRendicion>(vCollection, grdDetalleDeRendicion, "ConsecutivoRenglon");

                txtNumeroDocumento.Focus();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void btnNewItem_Click(object sender, RoutedEventArgs e) {
            try {
                LibListViewBound.GoToItem(grdDetalleDeRendicion, grdDetalleDeRendicion.Items.Count - 1);
                txtNumeroDocumento.Focus();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void grdDetalleDeRendicion_PreviewKeyDown(object sender, KeyEventArgs e) {
            try {

                if (e.Key == Key.Enter) {
                    txtNumeroDocumento.Focus();
                } else if (e.Key == Key.Delete) {
                    ListView aux = grdDetalleDeRendicion;
                    if (aux.SelectedItems.Count > 1)
                        borrarItem(aux);
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
            }

        }

        private void borrarItem(ListView aux) {
            if (aux.SelectedItems.Count > 0) {
                //  var result = LibNotifier.AcceptCancelMessage(null, "Desear borrar los Registros seleccionados?", "Adverencia", "Borrar Registros", TextAlignment.Center);
                var result = true;
                if (result) {
                    var arr = ((System.Collections.IList)aux.SelectedItems).Cast<DetalleDeRendicion>().ToList();
                    foreach (DetalleDeRendicion ll in arr) {
                        ((ICollection<DetalleDeRendicion>)aux.ItemsSource).Remove(ll);
                    }
                }
            }
        }



        private void RealizaLosCalculos() {
            txtTotalFacturas.Text = CalculaTotalFacturas().ToString();
            TotalFacturas = Decimal.Parse(txtTotalFacturas.Text);
            PropertyChanged(this, new PropertyChangedEventArgs("TotalFacturas"));

        }


        private decimal CalculaTotalFacturas() {
            return ((ICollection<DetalleDeRendicion>)DataContext).Cast<DetalleDeRendicion>().Sum(d => d.MontoGravable + d.MontoExento + d.MontoIVA);
        }
        #endregion //Metodos Generados

        private void AddItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DetalleDeRendicion detalle = new DetalleDeRendicion();
            addItem(detalle, grdDetalleDeRendicion);
            grdDetalleDeRendicion.SelectedItem = detalle;
            txtNumeroDocumento.Focus();
            RealizaLosCalculos();
        }

        private void addItem(DetalleDeRendicion detalle, ListView aux) {
            detalle.AplicaParaLibroDeCompras = (chkAplicaParaLibroDeCompras.IsChecked.Value ? "Si" : "No");
            detalle.AplicaParaLibroDeComprasAsBool = chkAplicaParaLibroDeCompras.IsChecked.Value;
            detalle.CodigoProveedor = txtCodigoProveedor.Text;
            detalle.ConsecutivoCompania = 1;
            detalle.ConsecutivoRendicion = 1;
            detalle.ConsecutivoRenglon = 1;
            detalle.Fecha = dtpFecha.Date;
            detalle.MontoExento = Decimal.Parse(txtMontoExento.Text);
            detalle.MontoGravable = Decimal.Parse(txtMontoGravable.Text);
            detalle.MontoIVA = Decimal.Parse(txtMontoIVA.Text);
            detalle.NumeroControl = txtNumeroControl.Text;
            detalle.NumeroDocumento = txtNumeroDocumento.Text;
            detalle.ObservacionesCxP = txtObservacionesCxP.Text;
            ((ICollection<DetalleDeRendicion>)aux.ItemsSource).Add(detalle);
        }

        private void newItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            ListView aux = grdDetalleDeRendicion;
            ((ICollection<DetalleDeRendicion>)aux.ItemsSource).Add(new DetalleDeRendicion());
            txtNumeroDocumento.Focus();
            RealizaLosCalculos();
        }

        private void txtTotalFacturas_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e) {

        }






    } //End of class GSDetalleDeRendicion.xaml

} //End of namespace Galac.Adm.Uil.CajaChica

