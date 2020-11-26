using System;
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
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef.Input {
    /// <summary>
    /// Lógica de interacción para GSNotaEntregaStt.xaml
    /// </summary>
    internal partial class GSNotaEntregaStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        NotaEntregaStt _CurrentInstance;
        
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
        internal NotaEntregaStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSNotaEntregaStt() {
            InitializeComponent();
            InitializeEvents();
            cmbModeloNotaEntrega.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eModeloDeFactura)));
            cmbTipoPrefijoNotaEntrega.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDePrefijoFactura)));
            cmbModeloNotaEntregaModoTexto.Fill(ListModeloPlanillas());
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Modelo Nota Entrega";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (NotaEntregaStt)initInstance;

            Title = "Nota Entrega";
            Action = initAction;
            ExtendedAction = initExtendedAction;
            //LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            Action = ((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).Action;
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            this.txtNombrePlantillaNotaEntrega.IsEnabled = false;
            this.btnBuscarRpxNotaEntrega.IsEnabled = false;
            SetLookAndFeelForCurrentRecord();
            Tool.AllDisabled(gwMain.Children, Action);
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
                MuestraModeloModoTexto();
            }
            
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.ModeloNotaEntregaAsEnum = (eModeloDeFactura) cmbModeloNotaEntrega.SelectedItemToInt();
            _CurrentInstance.NotaEntregaPreNumeradaAsBool = chkNotaEntregaPreNumerada.IsChecked.Value;
            _CurrentInstance.PrimeraNotaEntrega = txtPrimeraNotaEntrega.Text;
            _CurrentInstance.TipoPrefijoNotaEntregaAsEnum = (eTipoDePrefijoFactura) cmbTipoPrefijoNotaEntrega.SelectedItemToInt();
            _CurrentInstance.PrefijoNotaEntrega = txtPrefijoNotaEntrega.Text;
            _CurrentInstance.NombrePlantillaNotaEntrega = txtNombrePlantillaNotaEntrega.Text;
            _CurrentInstance.NombrePlantillaOrdenDeDespacho = txtNombrePlantillaOrdenDeDespacho.Text;
            _CurrentInstance.NumCopiasOrdenDeDespacho = LibConvert.ToInt(txtNumCopiasOrdenDeDespacho.Value);
            _CurrentInstance.ModeloNotaEntregaModoTexto = cmbModeloNotaEntregaModoTexto.SelectedItemToString();
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            cmbModeloNotaEntrega.SelectItem(_CurrentInstance.ModeloNotaEntregaAsEnum);
            chkNotaEntregaPreNumerada.IsChecked = _CurrentInstance.NotaEntregaPreNumeradaAsBool;
            txtPrimeraNotaEntrega.Text = _CurrentInstance.PrimeraNotaEntrega;
            cmbTipoPrefijoNotaEntrega.SelectItem(_CurrentInstance.TipoPrefijoNotaEntregaAsEnum);
            txtPrefijoNotaEntrega.Text = _CurrentInstance.PrefijoNotaEntrega;
            txtNombrePlantillaNotaEntrega.Text = _CurrentInstance.NombrePlantillaNotaEntrega;
            txtNombrePlantillaOrdenDeDespacho.Text = _CurrentInstance.NombrePlantillaOrdenDeDespacho;
            txtNumCopiasOrdenDeDespacho.Value = _CurrentInstance.NumCopiasOrdenDeDespacho;
            cmbModeloNotaEntregaModoTexto.SelectItem(_CurrentInstance.ModeloNotaEntregaModoTexto);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbModeloNotaEntrega.Validating += new System.ComponentModel.CancelEventHandler(cmbModeloNotaEntrega_Validating);
            this.cmbTipoPrefijoNotaEntrega.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoPrefijoNotaEntrega_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            this.cmbModeloNotaEntrega.SelectionChanged += new SelectionChangedEventHandler(cmbModeloNotaEntrega_SelectionChanged);
            this.chkNotaEntregaPreNumerada.Checked += new RoutedEventHandler(chkNotaEntregaPreNumerada_Checked);
            this.txtNombrePlantillaNotaEntrega.Validating +=new CancelEventHandler(txtNombrePlantillaNotaEntrega_Validating);
            this.btnBuscarRpxNotaEntrega.Click +=new RoutedEventHandler(btnBuscarRpxNotaEntrega_Click);
            this.txtPrimeraNotaEntrega.Validating +=new CancelEventHandler(txtPrimeraNotaEntrega_Validating);
            this.btnBuscarRpxOrdenDeDespacho.Click += new RoutedEventHandler(btnBuscarRpxOrdenDeDespacho_Click);
            this.txtNombrePlantillaOrdenDeDespacho.Validating += new CancelEventHandler(txtNombrePlantillaOrdenDeDespacho_Validating);
            this.cmbTipoPrefijoNotaEntrega.SelectionChanged += new SelectionChangedEventHandler(cmbTipoPrefijoNotaEntrega_SelectionChanged);

        }

        List<string> ListModeloPlanillas() {
            List<string> vResult = new List<string>();
            clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
            vResult = insSettValueByCompanyIpl.GetModelosPlanillasList();
            return vResult;
        }

        void cmbTipoPrefijoNotaEntrega_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            LibApiAwp.EnableControl(txtPrefijoNotaEntrega, (eTipoDePrefijoFactura)cmbTipoPrefijoNotaEntrega.SelectedItem == eTipoDePrefijoFactura.Indicar);
        }        

        private void OnUnloaded(object sender, RoutedEventArgs e) {
            SetNavigatorValuesFromForm();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            try {
                if (DataContext == null) {
                    return;
                }

                InitializeControl(DataContext, eAccionSR.Modificar, null);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }


        void cmbModeloNotaEntrega_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (((Galac.Saw.Ccl.SttDef.eModeloDeFactura)cmbModeloNotaEntrega.SelectedItem) == Galac.Saw.Ccl.SttDef.eModeloDeFactura.eMD_OTRO) {
                    txtNombrePlantillaNotaEntrega.IsEnabled = true;
                    if (LibText.IsNullOrEmpty(txtNombrePlantillaNotaEntrega.Text)) {
                        txtNombrePlantillaNotaEntrega.Text = "rpxNotaDeEntregaFormatoLibre";
                    }
                    btnBuscarRpxNotaEntrega.IsEnabled = true;
                } else {
                    txtNombrePlantillaNotaEntrega.IsEnabled = false;
                    btnBuscarRpxNotaEntrega.IsEnabled = false;
                }
                cmbModeloNotaEntrega.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoPrefijoNotaEntrega_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoPrefijoNotaEntrega.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void RealizaLosCalculos() {
            if (Action != eAccionSR.Consultar && Action != eAccionSR.Eliminar) {
                //throw new NotImplementedException("Debe sobreescribir el metodo RealizaLosCalculos para su caso especifico. Si no lo requiere no lo invoque.");
            }
        }
        #endregion //Metodos Generados


        void cmbModeloNotaEntrega_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                DeterminaSiSePuedeEditarModeloNotadeEntrega();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }

        }

        private void DeterminaSiSePuedeEditarModeloNotadeEntrega() {
            //if(cmbModeloNotaEntrega.SelectedItemToEnum<eModeloDeFactura>() == eModeloDeFactura.eMD_OTRO ) {
            //    LibApiAwp.EnableControl(txtNombrePlantillaNotaEntrega, false);
            //    LibApiAwp.EnableControl(lblNombrePlantillaNotaEntrega, false);
            //    LibApiAwp.EnableControl(btnBuscarRpxNotaEntrega, false);
            //} else {
            //    LibApiAwp.EnableControl(txtNombrePlantillaNotaEntrega, true);
            //    LibApiAwp.EnableControl(lblNombrePlantillaNotaEntrega, true);
            //    LibApiAwp.EnableControl(btnBuscarRpxNotaEntrega, true);
            //}
            LibApiAwp.EnableControl(txtNombrePlantillaNotaEntrega, cmbModeloNotaEntrega.SelectedItemToEnum<eModeloDeFactura>() == eModeloDeFactura.eMD_OTRO);
            LibApiAwp.EnableControl(lblNombrePlantillaNotaEntrega, cmbModeloNotaEntrega.SelectedItemToEnum<eModeloDeFactura>() == eModeloDeFactura.eMD_OTRO);
            LibApiAwp.EnableControl(btnBuscarRpxNotaEntrega, cmbModeloNotaEntrega.SelectedItemToEnum<eModeloDeFactura>() == eModeloDeFactura.eMD_OTRO);
            MuestraModeloModoTexto();
        }

        private void MuestraModeloModoTexto() {
            if (cmbModeloNotaEntrega.SelectedItemToEnum<eModeloDeFactura>() == eModeloDeFactura.eMD_IMPRESION_MODO_TEXTO) {
                lblModeloNotaEntregaModoTexto.Visibility = System.Windows.Visibility.Visible;
                cmbModeloNotaEntregaModoTexto.Visibility = System.Windows.Visibility.Visible;
            } else {
                lblModeloNotaEntregaModoTexto.Visibility = System.Windows.Visibility.Hidden;
                cmbModeloNotaEntregaModoTexto.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        void chkNotaEntregaPreNumerada_Checked(object sender, RoutedEventArgs e) {
            try {
                if((bool)chkNotaEntregaPreNumerada.IsChecked && cmbTipoPrefijoNotaEntrega.SelectedItemToEnum<eTipoDePrefijoFactura>() == eTipoDePrefijoFactura.Indicar) {
                    LibApiAwp.EnableControl(lblPrimeraNotaEntrega,false);
                    LibApiAwp.EnableControl(txtPrimeraNotaEntrega ,false);
                    cmbTipoPrefijoNotaEntrega.SelectedItem = eTipoDePrefijoFactura.SinPrefijo;
                    LibApiAwp.EnableControl(cmbModeloNotaEntregaModoTexto, false);
                } else {
                    LibApiAwp.EnableControl(lblPrimeraNotaEntrega, true );
                    LibApiAwp.EnableControl(txtPrimeraNotaEntrega, true);
                }
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtNombrePlantillaNotaEntrega_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaNotaEntrega.Text) == 0) {
                txtNombrePlantillaNotaEntrega.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaNotaEntrega.Text)) {
                BuscarRpxPlantillaNotaEntrega();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaNotaEntrega.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaNotaEntrega.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void btnBuscarRpxNotaEntrega_Click(object sender, RoutedEventArgs e) {
            try {
                 
                BuscarRpxPlantillaNotaEntrega();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxPlantillaNotaEntrega() {
            string paramBusqueda = "rpx de Nota Entrega (*.rpx)|*Nota*Entrega*.rpx";
            txtNombrePlantillaNotaEntrega.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        void txtPrimeraNotaEntrega_Validating(object sender, CancelEventArgs e) {
            try {
                if(CancelValidations) {
                    return;
                }                
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtNombrePlantillaOrdenDeDespacho_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaOrdenDeDespacho.Text) == 0) {
                txtNombrePlantillaOrdenDeDespacho.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaOrdenDeDespacho.Text)) {
                BuscarRpxPlantillaOrdenDeDespacho();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaOrdenDeDespacho.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaOrdenDeDespacho.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void btnBuscarRpxOrdenDeDespacho_Click(object sender, RoutedEventArgs e) {
            try {
                BuscarRpxPlantillaOrdenDeDespacho();
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        private void BuscarRpxPlantillaOrdenDeDespacho() {
            string paramBusqueda = "rpx de Orden Despacho (*.rpx)|*Orden*Despacho*.rpx";
            txtNombrePlantillaOrdenDeDespacho.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }
    } //End of class GSNotaEntregaStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

