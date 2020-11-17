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
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Uil.Integracion.Input {
    /// <summary>
    /// Lógica de interacción para GSIntegracionSaw.xaml
    /// </summary>
    internal partial class GSIntegracionSaw: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        IntegracionSaw _CurrentInstance;
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
        internal IntegracionSaw CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSIntegracionSaw() {
            InitializeComponent();
            InitializeEvents();
            cmbTipoIntegracion.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoIntegracion)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Tipo de Integracion, version";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (IntegracionSaw)initInstance;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            LibApiAwp.EnableControl(cmbTipoIntegracion, Action == eAccionSR.Insertar);
            LibApiAwp.EnableControl(txtversion, Action == eAccionSR.Insertar);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.TipoIntegracionAsEnum = (eTipoIntegracion) cmbTipoIntegracion.SelectedItemToInt();
            _CurrentInstance.version = txtversion.Text;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            cmbTipoIntegracion.SelectItem(_CurrentInstance.TipoIntegracionAsEnum);
            txtversion.Text = _CurrentInstance.version;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.cmbTipoIntegracion.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoIntegracion_Validating);
            this.txtversion.Validating += new System.ComponentModel.CancelEventHandler(txtversion_Validating);
        }

        void cmbTipoIntegracion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoIntegracion.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtversion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsIntegracionSawIpl insIntegracionSawIpl = new clsIntegracionSawIpl();
                if (!insIntegracionSawIpl.IsValidversion(Action, txtversion.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insIntegracionSawIpl.Information.ToString(), _CurrentModel.MessageName);
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


    } //End of class GSIntegracionSaw.xaml

} //End of namespace Galac.Saw.Uil.Integracion

