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
    /// Lógica de interacción para GSBancosStt.xaml
    /// </summary>
    internal partial class GSBancosStt : UserControl, IInputView {    
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        BancosStt _CurrentInstance;

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
        internal BancosStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }

        #endregion //Propiedades
        #region Constructores

        public GSBancosStt() {
            InitializeComponent();
            InitializeEvents();
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Concepto Débito Bancario, Concepto Crédito Bancario";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (BancosStt)initInstance;

            Title = "Bancos";
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
            }
            DeterminaSiSePuedeModificarParametrosDeConciliacion();
            gbDebito.Header = clsGlobalValues.AppMemoryInfo.GlobalValuesGetString("Parametros", "TituloImpuestoPorTransaccionesFinacieras");
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.UsaCodigoBancoEnPantallaAsBool = chkUsaCodigoBancoEnPantalla.IsChecked.Value;
            _CurrentInstance.CodigoGenericoCuentaBancaria = txtCodigoGenericoCuentaBancaria.Text;
            _CurrentInstance.ManejaDebitoBancarioAsBool = chkManejaDebitoBancario.IsChecked.Value;
            _CurrentInstance.RedondeaMontoDebitoBancarioAsBool = chkRedondeaMontoDebitoBancario.IsChecked.Value;
            _CurrentInstance.ConceptoDebitoBancario = txtConceptoDebitoBancario.Text;
            _CurrentInstance.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool = chkConsideraConciliadosLosMovIngresadosAntesDeFecha.IsChecked.Value;
            _CurrentInstance.FechaDeInicioConciliacion = dtpFechaDeInicioConciliacion.Date;
            _CurrentInstance.ManejaCreditoBancarioAsBool = chkManejaCreditoBancario.IsChecked.Value;
            _CurrentInstance.RedondeaMontoCreditoBancarioAsBool = chkRedondeaMontoCreditoBancario.IsChecked.Value;
            _CurrentInstance.ConceptoCreditoBancario = txtConceptoCreditoBancario.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkUsaCodigoBancoEnPantalla.IsChecked = _CurrentInstance.UsaCodigoBancoEnPantallaAsBool;
            txtCodigoGenericoCuentaBancaria.Text = _CurrentInstance.CodigoGenericoCuentaBancaria;
            chkManejaDebitoBancario.IsChecked = _CurrentInstance.ManejaDebitoBancarioAsBool;
            chkRedondeaMontoDebitoBancario.IsChecked = _CurrentInstance.RedondeaMontoDebitoBancarioAsBool;
            txtConceptoDebitoBancario.Text = _CurrentInstance.ConceptoDebitoBancario;
            chkConsideraConciliadosLosMovIngresadosAntesDeFecha.IsChecked = _CurrentInstance.ConsideraConciliadosLosMovIngresadosAntesDeFechaAsBool;
            dtpFechaDeInicioConciliacion.Date = _CurrentInstance.FechaDeInicioConciliacion;
            chkManejaCreditoBancario.IsChecked = _CurrentInstance.ManejaCreditoBancarioAsBool;
            chkRedondeaMontoCreditoBancario.IsChecked = _CurrentInstance.RedondeaMontoCreditoBancarioAsBool;
            txtConceptoCreditoBancario.Text = _CurrentInstance.ConceptoCreditoBancario;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigoGenericoCuentaBancaria.Validating += new System.ComponentModel.CancelEventHandler(txtCodigoGenericoCuentaBancaria_Validating);
            this.txtConceptoDebitoBancario.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoDebitoBancario_Validating);
            this.txtConceptoCreditoBancario.Validating += new System.ComponentModel.CancelEventHandler(txtConceptoCreditoBancario_Validating);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
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


        void txtCodigoGenericoCuentaBancaria_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtCodigoGenericoCuentaBancaria.Text) == 0) {
                    txtCodigoGenericoCuentaBancaria.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Gv_CuentaBancaria_B1.Codigo=" + txtCodigoGenericoCuentaBancaria.Text + LibText.ColumnSeparator();
                vParamsInitializationList += "Gv_CuentaBancaria_B1.EsCajaChica=" + LibConvert.BoolToSN(false);
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vParamsFixedList = "Gv_CuentaBancaria_B1.ConsecutivoCompania=" + clsGlobalValues.Mfc.GetInt("Compania");
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseCuentaBancaria(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtCodigoGenericoCuentaBancaria.Text = insParse.GetString(0, "Codigo", "");
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

        void txtConceptoDebitoBancario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtConceptoDebitoBancario.Text) == 0) {
                    txtConceptoDebitoBancario.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.codigo=" + txtConceptoDebitoBancario.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoDebitoBancario.Text = insParse.GetString(0, "Codigo", "");
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

        void txtConceptoCreditoBancario_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                if (LibString.Len(txtConceptoCreditoBancario.Text) == 0) {
                    txtConceptoCreditoBancario.Text = "*";
                }
                string vParamsInitializationList;
                string vParamsFixedList = "";
                LibSearch insLibSearch = new LibSearch();
                List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vParamsInitializationList = "Adm.Gv_ConceptoBancario_B1.codigo=" + txtConceptoCreditoBancario.Text + LibText.ColumnSeparator();
                vSearchValues = insLibSearch.CreateListOfParameter(vParamsInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vParamsFixedList);
                XmlDocument XmlProperties = new XmlDocument();
                if (clsSettValueByCompanyList.ChooseConceptoBancario(null, ref XmlProperties, vSearchValues, vFixedValues)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(XmlProperties);
                    txtConceptoCreditoBancario.Text = insParse.GetString(0, "Codigo", "");
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

        private void DeterminaSiSePuedeModificarParametrosDeConciliacion() {
            clsSettValueByCompanyIpl insSettValueByCompanyIpl = new clsSettValueByCompanyIpl(clsGlobalValues.AppMemoryInfo, clsGlobalValues.Mfc);
            bool vPuedoModificar = insSettValueByCompanyIpl.SePuedeModificarParametrosDeConciliacion();
            dtpFechaDeInicioConciliacion.IsEnabled = vPuedoModificar;
            chkConsideraConciliadosLosMovIngresadosAntesDeFecha.IsEnabled = vPuedoModificar;
        }     

    } //End of class GSBancosStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

