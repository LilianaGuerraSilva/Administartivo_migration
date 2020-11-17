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
    /// Lógica de interacción para GSMaquinaFiscal.xaml
    /// </summary>
    internal partial class GSMaquinaFiscal: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        MaquinaFiscal _CurrentInstance;
        ILibView _CurrentModel;
        int _LongitudNumeroFiscal;
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
        internal MaquinaFiscal CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSMaquinaFiscal() {
            InitializeComponent();
            InitializeEvents();
            cmbStatus.Fill(LibEnumHelper.GetValuesInEnumeration(typeof(eStatusMaquinaFiscal)));
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Consecutivo";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (MaquinaFiscal)initInstance;
            _CurrentModel = initModel;
            Title = initModel.MessageName;
            Action = initAction;
            ExtendedAction = initExtendedAction;
            LibApiAwp.DisableAllFieldsIfActionIn(gwMain.Children, (int)_Action, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar, (int)eAccionSR.Activar, (int)eAccionSR.Desactivar });
            if (Action == eAccionSR.Insertar) {
                SetFormValuesFromNavigator(true);
            } else {
                SetFormValuesFromNavigator(false);
            }
            LibApiAwp.EnableControl(txtConsecutivoMaquinaFiscal, Action == eAccionSR.Insertar);//|| Action == eAccionSR.Modificar);
            LibApiAwp.EnableControl(cmbStatus, false);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.ConsecutivoMaquinaFiscal = txtConsecutivoMaquinaFiscal.Text;
            _CurrentInstance.Descripcion = txtDescripcion.Text;
            _CurrentInstance.NumeroRegistro = txtNumeroRegistro.Text;
            if (Action == eAccionSR.Insertar) {
                _CurrentInstance.StatusAsEnum = eStatusMaquinaFiscal.Activa;
            }
            if (Action == eAccionSR.Activar) {
                _CurrentInstance.StatusAsEnum = eStatusMaquinaFiscal.Activa;
            }
            if (Action == eAccionSR.Desactivar) {
                _CurrentInstance.StatusAsEnum = eStatusMaquinaFiscal.Inactiva;
            }
            _CurrentInstance.LongitudNumeroFiscal = _LongitudNumeroFiscal;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtConsecutivoMaquinaFiscal.Text = _CurrentInstance.ConsecutivoMaquinaFiscal;
            txtDescripcion.Text = _CurrentInstance.Descripcion;
            txtNumeroRegistro.Text = _CurrentInstance.NumeroRegistro;
            cmbStatus.SelectItem(_CurrentInstance.StatusAsEnum);
            _LongitudNumeroFiscal = _CurrentInstance.LongitudNumeroFiscal;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtConsecutivoMaquinaFiscal.Validating += new System.ComponentModel.CancelEventHandler(txtConsecutivoMaquinaFiscal_Validating);
            this.txtConsecutivoMaquinaFiscal.GotFocus += new RoutedEventHandler(txtConsecutivoMaquinaFiscal_GotFocus);
            this.cmbStatus.Validating += new System.ComponentModel.CancelEventHandler(cmbStatus_Validating);
        }

        void txtConsecutivoMaquinaFiscal_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsMaquinaFiscalIpl insMaquinaFiscalIpl = new clsMaquinaFiscalIpl(((clsMaquinaFiscalIpl)CurrentModel).AppMemoryInfo, ((clsMaquinaFiscalIpl)CurrentModel).Mfc);
                if (!insMaquinaFiscalIpl.IsValidConsecutivoMaquinaFiscal(Action, txtConsecutivoMaquinaFiscal.Text, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insMaquinaFiscalIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void cmbStatus_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                cmbStatus.ValidateTextInCombo();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, this.Title);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void txtConsecutivoMaquinaFiscal_GotFocus(object sender, RoutedEventArgs e) {
            try {
                if (Action == eAccionSR.Insertar) {
                    txtConsecutivoMaquinaFiscal.Text = LibConvert.ToStr(CurrentModel.NextSequential("ConsecutivoMaquinaFiscal"));
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


    } //End of class GSMaquinaFiscal.xaml

} //End of namespace Galac.Saw.Uil.Tablas

