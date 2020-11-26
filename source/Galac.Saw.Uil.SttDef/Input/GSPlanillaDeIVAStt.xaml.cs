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
    /// Lógica de interacción para GSPlanillaDeIVAStt.xaml
    /// </summary>
    internal partial class GSPlanillaDeIVAStt : UserControl, IInputView {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        PlanillaDeIVAStt _CurrentInstance;
        
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
        internal PlanillaDeIVAStt CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        
        #endregion //Propiedades
        #region Constructores

        public GSPlanillaDeIVAStt() {
            InitializeComponent();
            InitializeEvents();
            cmbModeloPlanillaForma00030.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eModeloPlanillaForma00030)));
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
            _CurrentInstance = (PlanillaDeIVAStt)initInstance;

            Title = "Planilla De IVA";
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
        }

        public void SetNavigatorValuesFromForm() {
            _CurrentInstance.ImprimirCentimosEnPlanillaAsBool = chkImprimirCentimosEnPlanilla.IsChecked.Value;
            _CurrentInstance.ModeloPlanillaForma00030AsEnum = (eModeloPlanillaForma00030) cmbModeloPlanillaForma00030.SelectedItemToInt();
            _CurrentInstance.NombreContador = txtNombreContador.Text;
            _CurrentInstance.CedulaContador = txtCedulaContador.Text;
            _CurrentInstance.NumeroCPC = txtNumeroCPC.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            ClearControl();
            chkImprimirCentimosEnPlanilla.IsChecked = _CurrentInstance.ImprimirCentimosEnPlanillaAsBool;
            cmbModeloPlanillaForma00030.SelectItem(_CurrentInstance.ModeloPlanillaForma00030AsEnum);
            txtNombreContador.Text = _CurrentInstance.NombreContador;
            txtCedulaContador.Text = _CurrentInstance.CedulaContador;
            txtNumeroCPC.Text = _CurrentInstance.NumeroCPC;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbModeloPlanillaForma00030.Validating += new System.ComponentModel.CancelEventHandler(cmbModeloPlanillaForma00030_Validating);
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


        void cmbModeloPlanillaForma00030_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbModeloPlanillaForma00030.ValidateTextInCombo();
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


    } //End of class GSPlanillaDeIVAStt.xaml

} //End of namespace Galac.Saw.Uil.SttDef

