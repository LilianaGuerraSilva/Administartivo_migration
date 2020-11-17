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
using Galac.dbo.Ccl.CajaChica;

namespace Galac.Dbo.Uil.CajaChica.Input {
    /// <summary>
    /// Lógica de interacción para GSAlicuotaIVA.xaml
    /// </summary>
    internal partial class GSAlicuotaIVA: UserControl {
        #region Variables
        bool _CancelValidations;
        internal eAccionSR _Action;
        internal string _ExtendedAction;
        internal string _Title;
        AlicuotaIVA _CurrentInstance;
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
        internal AlicuotaIVA CurrentInstance {
            get { return _CurrentInstance; }
            set { _CurrentInstance = value; }
        }
        internal ILibView CurrentModel {
            get { return _CurrentModel; }
            set { _CurrentModel = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public GSAlicuotaIVA() {
            InitializeComponent();
            InitializeEvents();
        }
        #endregion //Constructores
        #region Metodos Generados

        private string RequiredFields() {
            return "Fecha De Inicio De Vigencia";
        }

        internal void ClearControl() {
            LibApiAwp.ClearInputControls(gwMain.Children);
        }

        internal void DisableControl() {
            LibApiAwp.DisableInputControls(gwMain.Children);
        }

        internal bool InitializeControl(object initInstance, ILibView initModel, eAccionSR initAction, string initExtendedAction) {
            bool vResult = true;
            _CurrentInstance = (AlicuotaIVA)initInstance;
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
            LibApiAwp.EnableControl(dtpFechaDeInicioDeVigencia, Action == eAccionSR.Insertar);
            SetLookAndFeelForCurrentRecord();
            return vResult;
        }

        private void SetLookAndFeelForCurrentRecord() {
            if (Action != eAccionSR.Insertar) {
            }
        }

        internal void SetNavigatorValuesFromForm() {
            _CurrentInstance.FechaDeInicioDeVigencia = dtpFechaDeInicioDeVigencia.Date;
            _CurrentInstance.MontoAlicuotaGeneral = txtMontoAlicuotaGeneral.Value;
            _CurrentInstance.MontoAlicuota2 = txtMontoAlicuota2.Value;
            _CurrentInstance.MontoAlicuota3 = txtMontoAlicuota3.Value;
        }

        internal void SetFormValuesFromNavigator(bool valClearRecord) {
            if (valClearRecord) {
                _CurrentModel.Clear(_CurrentInstance);
            }
            ClearControl();
            dtpFechaDeInicioDeVigencia.Date = _CurrentInstance.FechaDeInicioDeVigencia;
            txtMontoAlicuotaGeneral.Value = _CurrentInstance.MontoAlicuotaGeneral;
            txtMontoAlicuota2.Value = _CurrentInstance.MontoAlicuota2;
            txtMontoAlicuota3.Value = _CurrentInstance.MontoAlicuota3;
            RealizaLosCalculos();
        }

        private void InitializeEvents() {
            this.dtpFechaDeInicioDeVigencia.Validating += new EventHandler<LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs>(dtpFechaDeInicioDeVigencia_Validating);
        }

        void dtpFechaDeInicioDeVigencia_Validating(object sender, LibGalac.Aos.UI.WpfControls.DatePickerCancelEventArgs e) {
            try {
                if (CancelValidations) {
                    return;
                }
                clsAlicuotaIVAIpl insAlicuotaIVAIpl = new clsAlicuotaIVAIpl();
                if (!insAlicuotaIVAIpl.IsValidFechaDeInicioDeVigencia(Action, dtpFechaDeInicioDeVigencia.Date, true)) {
                    LibNotifier.ValidationError(LibApiAwp.GetWindow(sender), insAlicuotaIVAIpl.Information.ToString(), _CurrentModel.MessageName);
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


    } //End of class GSAlicuotaIVA.xaml

} //End of namespace Galac.Dbo.Uil.CajaChica

