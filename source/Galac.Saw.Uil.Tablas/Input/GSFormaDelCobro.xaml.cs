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
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas.Input {
    /// <summary>
    /// Lógica de interacción para GSFormaDelCobro.xaml
    /// </summary>
    internal partial class GSFormaDelCobro: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        FormaDelCobro _CurrentInstance;
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
        internal FormaDelCobro CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSFormaDelCobro() {
            InitializeComponent();
            InitializeEvents();
            cmbTipoDePago.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeFormaDePago)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Código";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (FormaDelCobro)initInstance;
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
            LibApiAwp.EnableControl(txtCodigo, Action == eAccionSR.Insertar );//|| Action == eAccionSR.Modificar);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.Codigo = txtCodigo.Text;
            _CurrentInstance.Nombre = txtNombre.Text;
            _CurrentInstance.TipoDePagoAsEnum = (eTipoDeFormaDePago) cmbTipoDePago.SelectedItemToInt();
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtCodigo.Text = _CurrentInstance.Codigo;
            txtNombre.Text = _CurrentInstance.Nombre;
            cmbTipoDePago.SelectItem(_CurrentInstance.TipoDePagoAsEnum);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(txtCodigo_Validating);
            this.txtCodigo.GotFocus += new RoutedEventHandler(txtCodigo_GotFocus);
            this.cmbTipoDePago.Validating += new System.ComponentModel.CancelEventHandler(cmbTipoDePago_Validating);
        }

        void txtCodigo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsFormaDelCobroIpl insFormaDelCobroIpl = new clsFormaDelCobroIpl();
                if (!insFormaDelCobroIpl.IsValidCodigo(Action, txtCodigo.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insFormaDelCobroIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void cmbTipoDePago_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipoDePago.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtCodigo_GotFocus(object sender, RoutedEventArgs e) {
            try {
                if (Action == eAccionSR.Insertar) {
                    txtCodigo.Text = LibConvert.ToStr(CurrentModel.NextSequential("Codigo"));
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


    } //End of class GSFormaDelCobro.xaml

} //End of namespace Galac.Saw.Uil.Tablas

