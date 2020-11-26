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
    /// Lógica de interacción para GSGeneralStt.xaml
    /// </summary>
    internal partial class GSGeneralStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        GeneralStt _CurrentInstance;
        
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
        internal GeneralStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSGeneralStt() {
            InitializeComponent();
            InitializeEvents();
            cmbOrdenamientoDeCodigoString.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eFormaDeOrdenarCodigos)));            
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (GeneralStt)initInstance;

            Title = "General";
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
                lblPermitirEditarIVAenCxC_CxP.Content = "Permitir Editar " + clsGlobalValues.AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA") + " en CxC y CxP";
                if (LibConvert.ToBool(clsGlobalValues.AppMemoryInfo.GlobalValuesGetBool("Parametros", "EsPilotoNotaEntrega"))) {
                    lblUsaNotaEntrega.Visibility = System.Windows.Visibility.Visible;
                    chkUsaNotaEntrega.Visibility = System.Windows.Visibility.Visible;
                } else {
                    lblUsaNotaEntrega.Visibility = System.Windows.Visibility.Hidden;
                    chkUsaNotaEntrega.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        public void SetNavigatorValuesFromForm() {            
            _CurrentInstance.PermitirEditarIVAenCxC_CxPAsBool = chkPermitirEditarIVAenCxC_CxP.IsChecked.Value;
            _CurrentInstance.UsaMultiplesAlicuotasAsBool = chkUsaMultiplesAlicuotas.IsChecked.Value;
            _CurrentInstance.OrdenamientoDeCodigoStringAsEnum = (eFormaDeOrdenarCodigos) cmbOrdenamientoDeCodigoString.SelectedItemToInt();
            _CurrentInstance.ImprimirComprobanteDeCxCAsBool = chkImprimirComprobanteDeCxC.IsChecked.Value;
            _CurrentInstance.ImprimirComprobanteDeCxPAsBool = chkImprimirComprobanteDeCxP.IsChecked.Value;
            _CurrentInstance.ValidarRifEnLaWebAsBool = chkValidarRifEnLaWeb.IsChecked.Value;
            _CurrentInstance.UsaNotaEntregaAsBool = chkUsaNotaEntrega.IsChecked.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkPermitirEditarIVAenCxC_CxP.IsChecked = _CurrentInstance.PermitirEditarIVAenCxC_CxPAsBool;
            chkUsaMultiplesAlicuotas.IsChecked = _CurrentInstance.UsaMultiplesAlicuotasAsBool;
            cmbOrdenamientoDeCodigoString.SelectItem(_CurrentInstance.OrdenamientoDeCodigoStringAsEnum);
            chkImprimirComprobanteDeCxC.IsChecked = _CurrentInstance.ImprimirComprobanteDeCxCAsBool;
            chkImprimirComprobanteDeCxP.IsChecked = _CurrentInstance.ImprimirComprobanteDeCxPAsBool;
            chkValidarRifEnLaWeb.IsChecked = _CurrentInstance.ValidarRifEnLaWebAsBool;
            chkUsaNotaEntrega.IsChecked = _CurrentInstance.UsaNotaEntregaAsBool;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbOrdenamientoDeCodigoString.Validating += new System.ComponentModel.CancelEventHandler(cmbOrdenamientoDeCodigoString_Validating);
            this.chkUsaMultiplesAlicuotas.Click += new RoutedEventHandler(chkUsaMultiplesAlicuotas_Click);
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
        }

        void chkUsaMultiplesAlicuotas_Click(object sender, RoutedEventArgs e) {
            if ((bool)chkUsaMultiplesAlicuotas.IsChecked) {
                MessageBox.Show("Recuerde seleccionar en factura y cotización los modelos adecuados al manejo de 3 alícuotas para la impresión de sus documentos.");
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


        void cmbOrdenamientoDeCodigoString_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbOrdenamientoDeCodigoString.ValidateTextInCombo();
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


    } //End of class GSGeneralStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

