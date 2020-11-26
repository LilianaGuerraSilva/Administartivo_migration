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
    /// Lógica de interacción para GSMonedaStt.xaml
    /// </summary>
    internal partial class GSMonedaStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        MonedaStt _CurrentInstance;
        ILibView _CurrentModel;
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
        internal MonedaStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSMonedaStt() {
            InitializeComponent();
            InitializeEvents();
            cmbSolicitarIngresoDeTasaDeCambioAlEmitir.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeSolicitudDeIngresoDeTasaDeCambio)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Nombre Moneda Local, Nombre Moneda Extranjera";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (MonedaStt)initInstance;
          
            Title = "Moneda";
            Action = initAction;
            ExtendedAction = initExtendedAction;
            //LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            Action = ((GSSettValueByCompany)((Grid)((HeaderedContentControl)((StackPanel)((Grid)((ContentPresenter)this.TemplatedParent).Parent).Parent).TemplatedParent).Parent).Parent).Action;
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

                bool vSesionEspecialProgramador = clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "SesionEspecialProgramador");
                if (vSesionEspecialProgramador) {
                    lblCodigoMonedaLocal.Visibility = System.Windows.Visibility.Visible;
                    txtCodigoMonedaLocal.Visibility = System.Windows.Visibility.Visible;
                    txtNombreMonedaLocal.Visibility = System.Windows.Visibility.Visible;
                } else {
                    lblCodigoMonedaLocal.Visibility = System.Windows.Visibility.Hidden;
                    txtCodigoMonedaLocal.Visibility = System.Windows.Visibility.Hidden;
                    txtNombreMonedaLocal.Visibility = System.Windows.Visibility.Hidden;
                }
                clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
                if (insSettValueByCompanyIpl.EsSistemaParaIG()) {
                    lblCodigoMonedaExtranjera.Visibility = System.Windows.Visibility.Hidden;
                    txtCodigoMonedaExtranjera.Visibility = System.Windows.Visibility.Hidden;
                    txtNombreMonedaExtranjera.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            //chkUsaMonedaExtranjera.IsEnabled = _CurrentInstance.UsaMonedaExtranjeraAsBool;
        }

        public void SetNavigatorValuesFromForm() {
            if (_CurrentInstance == null) {
                return;
            }
            _CurrentInstance.CodigoMonedaLocal = txtCodigoMonedaLocal.Text;
            _CurrentInstance.NombreMonedaLocal = txtNombreMonedaLocal.Text;
            _CurrentInstance.UsaMonedaExtranjeraAsBool = chkUsaMonedaExtranjera.IsChecked.Value;
            _CurrentInstance.SolicitarIngresoDeTasaDeCambioAlEmitirAsEnum = (eTipoDeSolicitudDeIngresoDeTasaDeCambio) cmbSolicitarIngresoDeTasaDeCambioAlEmitir.SelectedItemToInt();
            _CurrentInstance.CodigoMonedaExtranjera = txtCodigoMonedaExtranjera.Text;
            _CurrentInstance.NombreMonedaExtranjera = txtNombreMonedaExtranjera.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtCodigoMonedaLocal.Text = _CurrentInstance.CodigoMonedaLocal;
            txtNombreMonedaLocal.Text = _CurrentInstance.NombreMonedaLocal;
            chkUsaMonedaExtranjera.IsChecked = _CurrentInstance.UsaMonedaExtranjeraAsBool;
            cmbSolicitarIngresoDeTasaDeCambioAlEmitir.SelectItem(_CurrentInstance.SolicitarIngresoDeTasaDeCambioAlEmitirAsEnum);
            txtCodigoMonedaExtranjera.Text = _CurrentInstance.CodigoMonedaExtranjera;
            txtNombreMonedaExtranjera.Text = _CurrentInstance.NombreMonedaExtranjera;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigoMonedaLocal.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoMonedaLocal_Validating);
            this.txtNombreMonedaLocal.Validating += new System.ComponentModel.CancelEventHandler(txtNombreMonedaLocal_Validating);
            this.cmbSolicitarIngresoDeTasaDeCambioAlEmitir.Validating += new System.ComponentModel.CancelEventHandler(cmbSolicitarIngresoDeTasaDeCambioAlEmitir_Validating);
            this.txtNombreMonedaExtranjera.Validating += new System.ComponentModel.CancelEventHandler(txtNombreMonedaExtranjera_Validating);
            this.txtCodigoMonedaExtranjera.Validating += new CancelEventHandler(txtCodigoMonedaExtranjera_Validating);
            this.chkUsaMonedaExtranjera.Click += new RoutedEventHandler(chkUsaMonedaExtranjera_Click);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
        }


        void chkUsaMonedaExtranjera_Click(object sender, RoutedEventArgs e) {
            LibApiAwp.EnableControl(cmbSolicitarIngresoDeTasaDeCambioAlEmitir, (bool)chkUsaMonedaExtranjera.IsChecked);
            LibApiAwp.EnableControl(txtNombreMonedaExtranjera, (bool)chkUsaMonedaExtranjera.IsChecked);
            if ((bool)chkUsaMonedaExtranjera.IsChecked) {
                txtNombreMonedaExtranjera.Focus();
            }
            //LibApiAwp.EnableControl(txtCodigoMonedaExtranjera, (bool)chkUsaMonedaExtranjera.IsChecked);
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

        private void OnUnloaded(object sender, RoutedEventArgs e) {
            SetNavigatorValuesFromForm();
        }

        void txtCodigoMonedaLocal_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoMonedaLocal.Text) == 0) {
                    txtCodigoMonedaLocal.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Moneda_B1.Codigo=" + txtCodigoMonedaLocal.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseMoneda(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoMonedaLocal.Text = insParse.GetString(0, "Codigo", "");
                    txtNombreMonedaLocal.Text = insParse.GetString(0, "Nombre", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCodigoMonedaExtranjera_Validating(object sender, CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoMonedaExtranjera.Text) == 0) {
                    txtCodigoMonedaExtranjera.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Moneda_B1.Codigo=" + txtCodigoMonedaExtranjera.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseMoneda(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoMonedaExtranjera.Text = insParse.GetString(0, "Codigo", "");
                    txtNombreMonedaExtranjera.Text = insParse.GetString(0, "Nombre", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtNombreMonedaLocal_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsSettValueByCompanyIpl insMonedaSttIpl = new clsSettValueByCompanyIpl(null, null);
                if (!insMonedaSttIpl.IsValidNombreMonedaLocal(Action, txtNombreMonedaLocal.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insMonedaSttIpl.Information.ToString(), _CurrentModel.MessageName);
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void cmbSolicitarIngresoDeTasaDeCambioAlEmitir_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbSolicitarIngresoDeTasaDeCambioAlEmitir.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtNombreMonedaExtranjera_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            //try {
            //    if (CancelValidations) {
            //        return;
            //    }
            //    clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(null, null);
            //    if (!insSettValueByCompanyIpl.IsValidNombreMonedaExtranjera(Action, txtNombreMonedaExtranjera.Text, true)) {
            //        LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insSettValueByCompanyIpl.Information.ToString(), _CurrentModel.MessageName);
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
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtNombreMonedaExtranjera.Text) == 0) {
                    txtNombreMonedaExtranjera.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_Moneda_B1.Nombre=" + txtNombreMonedaExtranjera.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_Moneda_B1.Activa=" + LibConvert.BoolToSN(true);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseMoneda(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoMonedaExtranjera.Text = insParse.GetString(0, "Codigo", "");
                    txtNombreMonedaExtranjera.Text = insParse.GetString(0, "Nombre", "");
                } else {
                    e.Cancel = true;
                }
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


    } //End of class GSMonedaStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

