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
    /// Lógica de interacción para GSPropAnalisisVenc.xaml
    /// </summary>
    internal partial class GSPropAnalisisVenc: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        PropAnalisisVenc _CurrentInstance;
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
        internal PropAnalisisVenc CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSPropAnalisisVenc() {
            InitializeComponent();
            InitializeEvents();
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Primer Vencimiento, Segundo Vencimiento, Tercer Vencimiento";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (PropAnalisisVenc)initInstance;
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
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.PrimerVencimiento = LibConvert.ToInt(txtPrimerVencimiento.Text);
            _CurrentInstance.SegundoVencimiento = LibConvert.ToInt(txtSegundoVencimiento.Text);
            _CurrentInstance.TercerVencimiento = LibConvert.ToInt(txtTercerVencimiento.Text);
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            txtPrimerVencimiento.Text = LibConvert.ToStr(_CurrentInstance.PrimerVencimiento);
            txtSegundoVencimiento.Text = LibConvert.ToStr(_CurrentInstance.SegundoVencimiento);
            txtTercerVencimiento.Text = LibConvert.ToStr(_CurrentInstance.TercerVencimiento);
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.txtPrimerVencimiento.Validating += new System.ComponentModel.CancelEventHandler(txtPrimerVencimiento_Validating);
            this.txtSegundoVencimiento.Validating += new System.ComponentModel.CancelEventHandler(txtSegundoVencimiento_Validating);
            this.txtTercerVencimiento.Validating += new System.ComponentModel.CancelEventHandler(txtTercerVencimiento_Validating);
        }

        void txtPrimerVencimiento_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsPropAnalisisVencIpl insPropAnalisisVencIpl = new clsPropAnalisisVencIpl();
                if (!insPropAnalisisVencIpl.IsValidPrimerVencimiento(Action, LibConvert.ToInt(txtPrimerVencimiento.Text), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insPropAnalisisVencIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtSegundoVencimiento_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsPropAnalisisVencIpl insPropAnalisisVencIpl = new clsPropAnalisisVencIpl();
                if (!insPropAnalisisVencIpl.IsValidSegundoVencimiento(Action, LibConvert.ToInt(txtSegundoVencimiento.Text), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insPropAnalisisVencIpl.Information.ToString(), _CurrentModel.MessageName);
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

        void txtTercerVencimiento_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsPropAnalisisVencIpl insPropAnalisisVencIpl = new clsPropAnalisisVencIpl();
                if (!insPropAnalisisVencIpl.IsValidTercerVencimiento(Action, LibConvert.ToInt(txtTercerVencimiento.Text), true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insPropAnalisisVencIpl.Information.ToString(), _CurrentModel.MessageName);
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


    } //End of class GSPropAnalisisVenc.xaml

} //End of namespace Galac.Saw.Uil.Tablas

