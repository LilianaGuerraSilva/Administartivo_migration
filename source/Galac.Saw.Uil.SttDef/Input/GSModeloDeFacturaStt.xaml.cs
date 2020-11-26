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
    /// Lógica de interacción para GSModeloDeFacturaStt.xaml
    /// </summary>
    internal partial class GSModeloDeFacturaStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ModeloDeFacturaStt _CurrentInstance;
        
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
        internal ModeloDeFacturaStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSModeloDeFacturaStt() {
            InitializeComponent();
            InitializeEvents();
            cmbModeloDeFactura.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eModeloDeFactura)));
            cmbTipoDePrefijo.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDePrefijo)));
            cmbModeloFacturaModoTexto.Fill(ListModeloPlanillas());
            cmbModeloDeFactura2.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eModeloDeFactura)));
            cmbTipoDePrefijo2.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDePrefijo)));
            cmbModeloFacturaModoTexto2.Fill(ListModeloPlanillas()); 
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Modelo De Factura, Tipo De Prefijo, Modelo De Factura 2, Tipo De Prefijo 2";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (ModeloDeFacturaStt)initInstance;

            Title = "Modelo de Factura";
            Action = initAction;
            ExtendedAction = initExtendedAction;
            //LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            Action  = ((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).Action;
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            SetLookAndFeelForCurrentRecord();
            Tool.AllDisabled(gwMain.Children, Action);
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
                HabilitaCampos();
                HabilitaCampos2();
                HabilitaControles();
                HabilitaControles2();
                BLoqueaTalonario2((bool)chkUsarDosTalonarios.IsChecked);
            }
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.ModeloDeFacturaAsEnum = (eModeloDeFactura) cmbModeloDeFactura.SelectedItemToInt();
            _CurrentInstance.NombrePlantillaFactura = txtNombrePlantillaFactura.Text;
            _CurrentInstance.PrimeraFactura = txtPrimeraFactura.Text;
            _CurrentInstance.TipoDePrefijoAsEnum = (eTipoDePrefijo) cmbTipoDePrefijo.SelectedItemToInt();
            _CurrentInstance.Prefijo = txtPrefijo.Text;
            _CurrentInstance.ModeloFacturaModoTexto = cmbModeloFacturaModoTexto.SelectedItemToString();
            _CurrentInstance.FacturaPreNumeradaAsBool = chkFacturaPreNumerada.IsChecked.Value;
            _CurrentInstance.ModeloDeFactura2AsEnum = (eModeloDeFactura)cmbModeloDeFactura2.SelectedItemToInt();
            _CurrentInstance.NombrePlantillaFactura2 = txtNombrePlantillaFactura2.Text;
            _CurrentInstance.FacturaPreNumerada2AsBool = chkFacturaPreNumerada2.IsChecked.Value;
            _CurrentInstance.PrimeraFactura2 = txtPrimeraFactura2.Text;
            _CurrentInstance.TipoDePrefijo2AsEnum = (eTipoDePrefijo)cmbTipoDePrefijo2.SelectedItemToInt();
            _CurrentInstance.Prefijo2 = txtPrefijo2.Text;
            _CurrentInstance.ModeloFacturaModoTexto2 = cmbModeloFacturaModoTexto2.SelectedItemToString();
            _CurrentInstance.UsarDosTalonariosAsBool = chkUsarDosTalonarios.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            cmbModeloDeFactura.SelectItem(_CurrentInstance.ModeloDeFacturaAsEnum);
            txtNombrePlantillaFactura.Text = _CurrentInstance.NombrePlantillaFactura;
            txtPrimeraFactura.Text = _CurrentInstance.PrimeraFactura;
            cmbTipoDePrefijo.SelectItem(_CurrentInstance.TipoDePrefijoAsEnum);
            txtPrefijo.Text = _CurrentInstance.Prefijo;
            cmbModeloFacturaModoTexto.SelectItem(_CurrentInstance.ModeloFacturaModoTexto);
            chkFacturaPreNumerada.IsChecked = _CurrentInstance.FacturaPreNumeradaAsBool;
            cmbModeloDeFactura2.SelectItem(_CurrentInstance.ModeloDeFactura2AsEnum);
            txtNombrePlantillaFactura2.Text = _CurrentInstance.NombrePlantillaFactura2;
            chkFacturaPreNumerada2.IsChecked = _CurrentInstance.FacturaPreNumerada2AsBool;
            txtPrimeraFactura2.Text = _CurrentInstance.PrimeraFactura2;
            cmbTipoDePrefijo2.SelectItem(_CurrentInstance.TipoDePrefijo2AsEnum);
            txtPrefijo2.Text = _CurrentInstance.Prefijo2;
            cmbModeloFacturaModoTexto2.SelectItem(_CurrentInstance.ModeloFacturaModoTexto2);
            chkUsarDosTalonarios.IsChecked = _CurrentInstance.UsarDosTalonariosAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbModeloDeFactura.Validating += new System.ComponentModel.CancelEventHandler(cmbModeloDeFactura_Validating);
            this.cmbTipoDePrefijo.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDePrefijo_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.btnNombrePlantilla.Click += new RoutedEventHandler(btnNombrePlantilla_Click);
            this.txtNombrePlantillaFactura.Validating += new CancelEventHandler(txtNombrePlantillaFactura_Validating);
            this.chkFacturaPreNumerada.Click += new RoutedEventHandler(chkFacturaPreNumerada_Click);
            this.cmbModeloDeFactura2.Validating += new System.ComponentModel.CancelEventHandler(cmbModeloDeFactura2_Validating);
            this.cmbTipoDePrefijo2.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDePrefijo2_Validating);
            this.btnNombrePlantilla2.Click += new RoutedEventHandler(btnNombrePlantilla2_Click);
            this.txtNombrePlantillaFactura2.Validating += new CancelEventHandler(txtNombrePlantillaFactura2_Validating);
            this.chkFacturaPreNumerada2.Click += new RoutedEventHandler(chkFacturaPreNumerada2_Click);
            this.chkUsarDosTalonarios.Click += new RoutedEventHandler(chkUsarDosTalonarios_Click);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
        }

        void txtNombrePlantillaFactura2_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaFactura2.Text) == 0) {
                txtNombrePlantillaFactura2.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaFactura2.Text)) {
                BuscarRpxFactura2();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaFactura2.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaFactura2.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
        }

        void txtNombrePlantillaFactura_Validating(object sender, CancelEventArgs e) {
            if (LibString.Len(txtNombrePlantillaFactura.Text) == 0) {
                txtNombrePlantillaFactura.Text = "*";
            }
            if (LibString.S1IsInS2("*", txtNombrePlantillaFactura.Text)) {
                BuscarRpxFactura();
            } else {
                if (!new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).EsValidoNombrePlantilla(txtNombrePlantillaFactura.Text)) {
                    MessageBox.Show("El RPX " + txtNombrePlantillaFactura.Text + ", en " + this.Title + ", no EXISTE.");
                    e.Cancel = true;
                }
            }
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

        void chkUsarDosTalonarios_Click(object sender, RoutedEventArgs e) {
            BLoqueaTalonario2((bool)chkUsarDosTalonarios.IsChecked);
            HabilitaCampos2();
            HabilitaControles2();
        }

        void HabilitaControles() {
            if (((Galac.Saw.Ccl.SttDef.eModeloDeFactura)cmbModeloDeFactura.SelectedItem) == Galac.Saw.Ccl.SttDef.eModeloDeFactura.eMD_OTRO) {
                txtNombrePlantillaFactura.IsEnabled = true;
                if (LibText.IsNullOrEmpty(txtNombrePlantillaFactura.Text)) {
                    txtNombrePlantillaFactura.Text = "rpxFacturaFormatoLibre";
                }
                btnNombrePlantilla.IsEnabled = true;
                cmbModeloFacturaModoTexto.IsEnabled = false;
                lblModeloFacturaModoTexto.Visibility = System.Windows.Visibility.Hidden;
                cmbModeloFacturaModoTexto.Visibility = System.Windows.Visibility.Hidden;
            } else if (((Galac.Saw.Ccl.SttDef.eModeloDeFactura)cmbModeloDeFactura.SelectedItem) == Galac.Saw.Ccl.SttDef.eModeloDeFactura.eMD_IMPRESION_MODO_TEXTO) {
                txtNombrePlantillaFactura.IsEnabled = false;
                txtNombrePlantillaFactura.Text = "";
                btnNombrePlantilla.IsEnabled = false;
                cmbModeloFacturaModoTexto.IsEnabled = true;
                lblModeloFacturaModoTexto.Visibility = System.Windows.Visibility.Visible;
                cmbModeloFacturaModoTexto.Visibility = System.Windows.Visibility.Visible;
            } else {
                txtNombrePlantillaFactura.IsEnabled = false;
                txtNombrePlantillaFactura.Text = "";
                btnNombrePlantilla.IsEnabled = false;
                cmbModeloFacturaModoTexto.IsEnabled = false;
                lblModeloFacturaModoTexto.Visibility = System.Windows.Visibility.Hidden;
                cmbModeloFacturaModoTexto.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        void HabilitaControles2() {
            if (((Galac.Saw.Ccl.SttDef.eModeloDeFactura)cmbModeloDeFactura2.SelectedItem) == Galac.Saw.Ccl.SttDef.eModeloDeFactura.eMD_OTRO) {
                txtNombrePlantillaFactura2.IsEnabled = true;
                if (LibText.IsNullOrEmpty(txtNombrePlantillaFactura2.Text)) {
                    txtNombrePlantillaFactura2.Text = "rpxFacturaFormatoLibre";
                }
                btnNombrePlantilla2.IsEnabled = true;
                cmbModeloFacturaModoTexto2.IsEnabled = false;
                lblModeloFacturaModoTexto2.Visibility = System.Windows.Visibility.Hidden;
                cmbModeloFacturaModoTexto2.Visibility = System.Windows.Visibility.Hidden;
            } else if (((Galac.Saw.Ccl.SttDef.eModeloDeFactura)cmbModeloDeFactura2.SelectedItem) == Galac.Saw.Ccl.SttDef.eModeloDeFactura.eMD_IMPRESION_MODO_TEXTO) {
                txtNombrePlantillaFactura2.IsEnabled = false;
                txtNombrePlantillaFactura2.Text = "";
                btnNombrePlantilla2.IsEnabled = false;
                cmbModeloFacturaModoTexto2.IsEnabled = true;
                lblModeloFacturaModoTexto2.Visibility = System.Windows.Visibility.Visible;
                cmbModeloFacturaModoTexto2.Visibility = System.Windows.Visibility.Visible;
            } else {
                txtNombrePlantillaFactura2.IsEnabled = false;
                txtNombrePlantillaFactura2.Text = "";
                btnNombrePlantilla2.IsEnabled = false;
                cmbModeloFacturaModoTexto2.IsEnabled = false;
                lblModeloFacturaModoTexto2.Visibility = System.Windows.Visibility.Hidden;
                cmbModeloFacturaModoTexto2.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        void BLoqueaTalonario2(bool valor) {
            cmbModeloDeFactura2.IsEnabled = valor;
            txtNombrePlantillaFactura2.IsEnabled = valor;
            btnNombrePlantilla2.IsEnabled = valor;
            chkFacturaPreNumerada2.IsEnabled = valor;
            txtPrimeraFactura2.IsEnabled = valor;
            cmbTipoDePrefijo2.IsEnabled = valor;
            txtPrefijo2.IsEnabled = valor;
        }

        private void HabilitaCampos2() {
            bool vUsarFacturaPreNumerada2 = (bool)chkFacturaPreNumerada2.IsChecked.Value;
            LibApiAwp.EnableControl(cmbTipoDePrefijo2, !vUsarFacturaPreNumerada2);
            LibApiAwp.EnableControl(txtPrimeraFactura2, !vUsarFacturaPreNumerada2);
            LibApiAwp.EnableControl(txtPrefijo2, ((eTipoDePrefijo)cmbTipoDePrefijo2.SelectedItemToInt() == eTipoDePrefijo.Indicar && !vUsarFacturaPreNumerada2));
            if (vUsarFacturaPreNumerada2) {
                cmbTipoDePrefijo2.SelectItem(eTipoDePrefijo.SinPrefijo);
            }
            LibApiAwp.EnableControl(cmbModeloFacturaModoTexto2, !vUsarFacturaPreNumerada2);
        }

        void cmbModeloDeFactura_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                HabilitaControles();
                cmbModeloDeFactura.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbTipoDePrefijo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDePrefijo.ValidateTextInCombo();
                HabilitaCampos();
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
        List<string> ListModeloPlanillas() {
            List<string> vResult = new List<string>();
            clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
            vResult = insSettValueByCompanyIpl.GetModelosPlanillasList();
            return vResult;
        }


      private void btnNombrePlantilla_Click(object sender, RoutedEventArgs e) {
          try {
              BuscarRpxFactura();
          } catch (GalacException gEx) {
              LibExceptionDisplay.Show(gEx, this.Title);
          } catch (Exception vEx) {
              if (vEx is System.AccessViolationException) {
                  throw;
              }
              LibExceptionDisplay.Show(vEx);
          }
        }
        
        private void BuscarRpxFactura() {
          string paramBusqueda = "rpx de Plantilla Factura (*.rpx)|*Factura*.rpx";
          txtNombrePlantillaFactura.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }

        private void BuscarRpxFactura2() {
            string paramBusqueda = "rpx de Plantilla Factura (*.rpx)|*Factura*.rpx";
            txtNombrePlantillaFactura2.Text = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc).BuscarNombrePlantilla(paramBusqueda);
        }   

        void chkFacturaPreNumerada_Click(object sender, RoutedEventArgs e) {
          try {
              if (CancelValidations) {
                  return;
              }
              HabilitaCampos();
          } catch (GalacException gEx) {
              LibExceptionDisplay.Show(gEx, this.Title);
          } catch (Exception vEx) {
              if (vEx is System.AccessViolationException) {
                  throw;
              }
              LibExceptionDisplay.Show(vEx);
          }
      }

      void cmbModeloDeFactura2_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
          try {
              if (CancelValidations) {
                  return;
              }
              HabilitaControles2();
              cmbModeloDeFactura2.ValidateTextInCombo();
          } catch (GalacException gEx) {
              LibExceptionDisplay.Show(gEx, this.Title);
          } catch (Exception vEx) {
              if (vEx is System.AccessViolationException) {
                  throw;
              }
              LibExceptionDisplay.Show(vEx);
          }
      }

      void cmbTipoDePrefijo2_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
          try {
              if (CancelValidations) {
                  return;
              }
              cmbTipoDePrefijo2.ValidateTextInCombo();
              HabilitaCampos2();
          } catch (GalacException gEx) {
              LibExceptionDisplay.Show(gEx, this.Title);
          } catch (Exception vEx) {
              if (vEx is System.AccessViolationException) {
                  throw;
              }
              LibExceptionDisplay.Show(vEx);
          }
      }

      void chkFacturaPreNumerada2_Click(object sender, RoutedEventArgs e) {
          try {
              if (CancelValidations) {
                  return;
              }
              HabilitaCampos2();
          } catch (GalacException gEx) {
              LibExceptionDisplay.Show(gEx, this.Title);
          } catch (Exception vEx) {
              if (vEx is System.AccessViolationException) {
                  throw;
              }
              LibExceptionDisplay.Show(vEx);
          }
      }

      private void btnNombrePlantilla2_Click(object sender, RoutedEventArgs e) {
          try {
              BuscarRpxFactura2();
          } catch (GalacException gEx) {
              LibExceptionDisplay.Show(gEx, this.Title);
          } catch (Exception vEx) {
              if (vEx is System.AccessViolationException) {
                  throw;
              }
              LibExceptionDisplay.Show(vEx);
          }
      }

      private void HabilitaCampos() {
         bool vUsarFacturaPreNumerada = chkFacturaPreNumerada.IsChecked.Value;
         LibApiAwp.EnableControl(cmbTipoDePrefijo,!vUsarFacturaPreNumerada);
         LibApiAwp.EnableControl(txtPrimeraFactura, !vUsarFacturaPreNumerada);
         LibApiAwp.EnableControl(txtPrefijo, ((eTipoDePrefijo)cmbTipoDePrefijo.SelectedItemToInt() == eTipoDePrefijo.Indicar && !vUsarFacturaPreNumerada));
         if (vUsarFacturaPreNumerada) {
             cmbTipoDePrefijo.SelectItem(eTipoDePrefijo.SinPrefijo);
         }
      }

      
    } //End of class GSModeloDeFacturaStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

