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
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Banco.Input {
    /// <summary>
    /// Lógica de interacción para GSConceptoBancario.xaml
    /// </summary>
    internal partial class GSConceptoBancario: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        ConceptoBancario _CurrentInstance;
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
        internal ConceptoBancario CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSConceptoBancario() {
            InitializeComponent();
            InitializeEvents();
            cmbTipo.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eIngresoEgreso)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Codigo, Descripcion";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (ConceptoBancario)initInstance;
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
            if (Action == eAccionSR.Modificar) {
                cmbTipo.IsEnabled = false;
            }
            LibApiAwp.EnableControl(txtCodigo, Action == eAccionSR.Insertar);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.Codigo = txtCodigo.Text;
            _CurrentInstance.Descripcion = txtDescripcion.Text;
            _CurrentInstance.TipoAsEnum = (eIngresoEgreso) cmbTipo.SelectedItemToInt();
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtCodigo.Text = _CurrentInstance.Codigo;
            txtDescripcion.Text = _CurrentInstance.Descripcion;
            cmbTipo.SelectItem(_CurrentInstance.TipoAsEnum);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtCodigo.Validating += new System.ComponentModel.CancelEventHandler(txtCodigo_Validating);
            this.txtDescripcion.Validating += new System.ComponentModel.CancelEventHandler(txtDescripcion_Validating);
            this.cmbTipo.Validating += new System.ComponentModel.CancelEventHandler(cmbTipo_Validating);
        }

        void txtCodigo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsConceptoBancarioIpl insConceptoBancarioIpl = new clsConceptoBancarioIpl();
                if (!insConceptoBancarioIpl.IsValidCodigo(Action, txtCodigo.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insConceptoBancarioIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtDescripcion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsConceptoBancarioIpl insConceptoBancarioIpl = new clsConceptoBancarioIpl();
                if (!insConceptoBancarioIpl.IsValidDescripcion(Action, txtDescripcion.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insConceptoBancarioIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void cmbTipo_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbTipo.ValidateTextInCombo();
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


    } //End of class GSConceptoBancario.xaml

} //End of namespace Galac.Adm.Uil.Banco

